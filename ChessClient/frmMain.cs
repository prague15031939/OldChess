using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chess;

namespace OldChess
{
    public partial class frmMain : Form
    {
        private readonly int CellSize = 50;
        private Panel[,] GameMap;
        private Chess.Chess chess;
        private bool wait;
        private int xFrom, yFrom;

        public frmMain()
        {
            InitializeComponent();
            InitGameMap();
            wait = true;
            chess = new Chess.Chess();// "rnb1kb1r/p3p1p1/B4n2/p1qP2pp/1P1Ppp2/4Q1PN/PP3PRP/RNB1K3 w Qkq - 0 1");
            ShowPosition();
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
                chess = chess.Move(move);
            }
            ShowPosition();
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
            if (wait) 
                MarkSquaresFrom(); 
            else 
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

    }
}
