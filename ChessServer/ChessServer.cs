using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace ChessServer
{
    class ChessServer
    {
        private TcpListener listener;
        private List<User> UserList = new List<User>();
        private List<GameSession> SessionList = new List<GameSession>();
        public string ipAddress { get; set; }
        public int port { get; set; }

        public ChessServer(string ip, int port)
        {
            this.ipAddress = ip;
            this.port = port;
        }

        public void StartWork()
        {
            IPAddress localAddr = IPAddress.Parse(ipAddress);
            listener = new TcpListener(localAddr, port);
            listener.Start();
        }

        public void MainLoop()
        {
            try
            {
                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();

                    Thread clientThread = new Thread(new ThreadStart(() => ProcessClient(client)));
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (listener != null)
                    listener.Stop();
            }
        }
        private string GetClientMessage(TcpClient client)
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

        private void SendMessage(TcpClient client, string msg)
        {
            NetworkStream stream = client.GetStream();
            byte[] data = Encoding.Unicode.GetBytes(msg);
            stream.Write(data, 0, data.Length);
        }

        private void ProcessClient(TcpClient client)
        {
            while (true)
            {
                string msg = GetClientMessage(client);
                try
                {
                    string request = msg.Split(' ')[0];
                    string UserName = msg.Split(' ')[1];

                    if (request == "CONNECT")
                    {
                        UserList.Add(new User(client, UserName));
                        Console.WriteLine($"{UserName} connected");
                    }
                    else if (request == "NEWGAME")
                    {
                        string side = msg.Split(' ')[2];

                        User CreatorUser = GetUser(UserName);
                        CreatorUser.side = side;
                        SessionList.Add(new GameSession(CreatorUser, side));
                        Console.WriteLine($"{UserName} creates new game session as {side}");
                    }
                    else if (request == "GETAVAIL")
                    {
                        SendMessage(client, GetAvailableGames(UserName));
                        Console.WriteLine($"{UserName} requests a list of waiting games");
                    }
                    else if (request == "JOIN")
                    {
                        int SessionID = Convert.ToInt32(msg.Split(' ')[2]);
                        string side = SessionList[SessionID].GetInactiveSide();

                        User JoinerUser = GetUser(UserName);
                        JoinerUser.side = side;

                        SessionList[SessionID].SetSecondPlayer(JoinerUser);
                        Console.WriteLine($"{UserName} joins game session #{SessionID} as {side}");

                        SendAcceptions(SessionID);
                    }
                    else if (request == "ACCEPT")
                    {
                        User user = GetUser(UserName);
                        Console.WriteLine($"{UserName} accepted game session #{user.SessionID}");
                        if (SessionList[user.SessionID].status == GameStatus.wait)
                            SessionList[user.SessionID].status = GameStatus.AcceptOne;
                        else if (SessionList[user.SessionID].status == GameStatus.AcceptOne)
                        {
                            SessionList[user.SessionID].status = GameStatus.inProgress;
                            StartGame(user.SessionID);
                        }
                    }
                    else if (request == "REJECT")
                    {
                        User user = GetUser(UserName);
                        Console.WriteLine($"{UserName} rejected game session #{user.SessionID}");
                        DestroySession(user);
                    }
                    else if (msg.Split(':')[0] == "MOVE")
                    {
                        UserName = msg.Split(':')[1];
                        User user = GetUser(UserName);
                        User userOp = SessionList[user.SessionID].GetOpponent(user);
                        SendMessage(userOp.client, msg);
                    }

                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
        }

        private void StartGame(int SessionID)
        {
            User player1 = SessionList[SessionID].PlayerBlack;
            User player2 = SessionList[SessionID].PlayerWhite;
            SendMessage(player1.client, $"GAMESTART {player1.side}");
            SendMessage(player2.client, $"GAMESTART {player2.side}");
        }

        private void DestroySession(User user)
        {
            if (SessionList[user.SessionID].status != GameStatus.destroyed)
            {
                User player1 = SessionList[user.SessionID].PlayerBlack;
                User player2 = SessionList[user.SessionID].PlayerWhite;
                SessionList[user.SessionID].status = GameStatus.destroyed;
                Console.WriteLine($"game session #{user.SessionID} has been destroyed");
                player1.SessionID = -1;
                player2.SessionID = -1;
                player1.side = "";
                player2.side = "";
                SendMessage(player1.client, $"DESTROY");
                SendMessage(player2.client, $"DESTROY");
            }
        }

        private void SendAcceptions(int SessionID)
        {
            User UserBlack = SessionList[SessionID].PlayerBlack;
            User UserWhite = SessionList[SessionID].PlayerWhite;
            UserBlack.SessionID = UserWhite.SessionID = SessionID;
            SendMessage(UserWhite.client, $"GAMEREADY {UserBlack.name} {UserBlack.side}");
            SendMessage(UserBlack.client, $"GAMEREADY {UserWhite.name} {UserWhite.side}");
        }

        private User GetUser(string UserName)
        {
            foreach (User user in UserList)
            {
                if (user.name == UserName)
                    return user;
            }
            return null;
        }

        private string GetAvailableGames(string finder)
        {
            string response = "";
            int i = 0;
            foreach (GameSession game in SessionList)
            {
                if (game != null && game.status == GameStatus.wait)
                {
                    User ActivePlayer = game.GetActivePlayer();
                    if (ActivePlayer.name != finder)
                        response += $"{i} {ActivePlayer.name} {ActivePlayer.side}\n";
                }
                i++;
            }
            if (response == "") response = "none";
            return response;
        }

    }
}

