using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace OldChess
{
    public enum ClientState
    {
        initial, connect, create, join, accept, play
    }

    public partial class frmMain : Form
    {
        public string UserName { get; set; }
        public string ServerInfo { get; set; }
        public int JoinedGameID { get; set; }

        private TcpClient client; 

        private ClientState state;
        private bool active;

        private readonly int CellSize = 50;
        private Panel[,] GameMap;
        private Chess.Chess chess;
        private bool wait;
        private int xFrom, yFrom;

        public frmMain()
        {
            InitializeComponent();
            state = ClientState.initial;
            JoinedGameID = -1;
        }

        private void StartGame(string side)
        {
            InitGameMap();
            wait = true;
            state = ClientState.play;
            active = side == "white" ? true : false;
            chess = new Chess.Chess();
            ShowPosition();

            Thread ServerThread = new Thread(new ThreadStart(ProcessServer));
            ServerThread.Start();
        }

        private void InitGameMap()
        {
            GameMap = new Panel[8, 8];
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    GameMap[i, j] = AddCell(i, j);
        }

        Panel AddCell(int x, int y)
        {
            var panel = new Panel();
            panel.BackColor = GetColor(x, y);
            panel.Location = GetLocation(x, y);
            panel.Name = $"p{x}{y}";
            panel.Size = new Size(CellSize, CellSize);
            panel.BackgroundImageLayout = ImageLayout.Stretch;
            panel.MouseClick += new MouseEventHandler(panel_MouseClick);
            panelBoard.Controls.Add(panel);
            return panel;
        }
        Color GetColor(int x, int y)
        {
            return (x + y) % 2 == 0 ? Color.DarkGray : Color.White;
        }

        Color GetMarkedColor(int x, int y)
        {
            return (x + y) % 2 == 0 ? Color.Green : Color.LightGreen;
        }

        Point GetLocation(int x, int y)
        {
            return new Point(CellSize / 50 + x * CellSize, CellSize / 50 + (7 - y) * CellSize);
        }

        private void panel_MouseClick(object sender, MouseEventArgs e)
        {
            if (active)
            {
                string xy = ((Panel)sender).Name.Substring(1);
                int x = xy[0] - '0';
                int y = xy[1] - '0';

                if (wait)
                {
                    wait = false;
                    xFrom = x;
                    yFrom = y;
                }
                else
                {
                    wait = true;
                    string figure = chess.GetFigureAt(xFrom, yFrom).ToString();
                    string move = figure +
                        ((char)('a' + xFrom)).ToString() + ((char)('1' + yFrom)).ToString() +
                        ((char)('a' + x)).ToString() + ((char)('1' + y)).ToString();
                    if (!chess.Equals(chess.Move(move)))
                    {
                        chess = chess.Move(move);
                        SendMessage($"MOVE:{UserName}:{chess.fen}");
                        active = false;
                    }
                }
                ShowPosition();
            }
        }

        private void MarkSquaresFrom()
        {
            foreach (string move in chess.GetAllMoves())
            {
                int x = move[1] - 'a';
                int y = move[2] - '1';
                GameMap[x, y].BackColor = GetMarkedColor(x, y);
            }
        }

        private void MarkSquaresTo()
        {
            string rest = ((char)('a' + xFrom)).ToString() + ((char)('1' + yFrom)).ToString();
            string suffix = chess.GetFigureAt(xFrom, yFrom) + rest;
            foreach (string move in chess.GetAllMoves())
            {
                if (move.StartsWith(suffix))
                {
                    int x = move[3] - 'a';
                    int y = move[4] - '1';
                    GameMap[x, y].BackColor = GetMarkedColor(x, y);
                }
            }
        }

        private void MarkSquares()
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    GameMap[i, j].BackColor = GetColor(i, j);
            if (wait && active) 
                MarkSquaresFrom(); 
            else if (active) 
                MarkSquaresTo();
        }

        public void ShowPosition()
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    ShowFigure(i, j, chess.GetFigureAt(i, j));
            MarkSquares();
        }

        public void ShowFigure(int x, int y, char figure)
        {
            Image FigureImage = null;
            switch (figure)
            {
                case 'R': FigureImage = Properties.Resources.WhiteRook; break;
                case 'N': FigureImage = Properties.Resources.WhiteKnight; break;
                case 'B': FigureImage = Properties.Resources.WhiteBishop; break;
                case 'Q': FigureImage = Properties.Resources.WhiteQueen; break;
                case 'K': FigureImage = Properties.Resources.WhiteKing; break;
                case 'P': FigureImage = Properties.Resources.WhitePawn; break;

                case 'r': FigureImage = Properties.Resources.BlackRook; break;
                case 'n': FigureImage = Properties.Resources.BlackKnight; break;
                case 'b': FigureImage = Properties.Resources.BlackBishop; break;
                case 'q': FigureImage = Properties.Resources.BlackQueen; break;
                case 'k': FigureImage = Properties.Resources.BlackKing; break;
                case 'p': FigureImage = Properties.Resources.BlackPawn; break;

                default: FigureImage = null; break;
            }
            GameMap[x, y].BackgroundImage = FigureImage;
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmConnect();
            frm.Owner = this;
            frm.ShowDialog();

            try
            {
                ConnectToServer();
                state = ClientState.connect;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetServerResponse()
        {
            NetworkStream stream = client.GetStream();
            var data = new byte[256];
            string dataStr = "";
            int bytes = 0;
            do
            {
                bytes = stream.Read(data, 0, data.Length);
                dataStr += Encoding.Unicode.GetString(data, 0, bytes);
            }
            while (stream.DataAvailable);
            return dataStr;
        }

        private void SendMessage(string msg)
        {
            NetworkStream stream = client.GetStream();
            byte[] data = Encoding.Unicode.GetBytes(msg);
            stream.Write(data, 0, data.Length);
        }

        private void asWhiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendMessage($"NEWGAME {UserName} white");
            WaitForGame();
        }

        private void asBlackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendMessage($"NEWGAME {UserName} black");
            WaitForGame();
        }

        private void WaitForGame()
        {
            while (true)
            {
                string msg = GetServerResponse();
                if (msg.Split(' ')[0] == "GAMEREADY")
                {
                    string enemy = msg.Split(' ')[1];
                    string side = msg.Split(' ')[2];
                    if (MessageBox.Show($"playing vs: {enemy} as {side}", "game is ready", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                        SendMessage($"ACCEPT {UserName}");
                    else
                        SendMessage($"REJECT {UserName}");
                    msg = GetServerResponse();
                    if (msg == "DESTROY")
                    {
                        state = ClientState.connect;
                        MessageBox.Show($"game was rejected", "error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (msg.StartsWith("GAMESTART"))
                        StartGame(msg.Split(' ')[1]);

                    return;
                }
            }
        }

        private void joinAGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendMessage($"GETAVAIL {UserName}");
            string msg = GetServerResponse();
            if (msg == "none")
            {
                MessageBox.Show("no games available", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var frm = new frmJoin(msg);
            frm.Owner = this;
            frm.ShowDialog();
            if (JoinedGameID != -1)
            {
                SendMessage($"JOIN {UserName} {JoinedGameID}");
                WaitForGame();
            }
        }

        private void ConnectToServer()
        {
            if (state == ClientState.initial)
            {
                client = new TcpClient();
                IPAddress ip = IPAddress.Parse(ServerInfo.Split(':')[0]);
                IPEndPoint server = new IPEndPoint(ip, Convert.ToInt32(ServerInfo.Split(':')[1]));
                client.Connect(server);

                SendMessage($"CONNECT {UserName}");
            }
        }

        private void ProcessServer()
        {
            while (true)
            {
                string msg = GetServerResponse();
                string request = msg.Split(':')[0];
                if (request == "MOVE")
                {
                    string fen = msg.Split(':')[2];
                    chess = new Chess.Chess(fen);
                    active = true;
                    ShowPosition();
                }
            }
        }

    }
}
