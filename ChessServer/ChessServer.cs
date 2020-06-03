using System;
using System.Collections.Generic;
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
                        if (!UsernameIsUnique(UserName))
                        {
                            SendMessage(client, $"{UserName} already exists on the server");
                            return;
                        }
                        else
                            SendMessage(client, "OK");
                        UserList.Add(new User(client, UserName));
                        GetUser(UserName).SessionID = -1;
                        Console.WriteLine($"{UserName} connected");
                    }
                    else if (request == "DISCONNECT")
                    {
                        User user = GetUser(UserName);
                        if (user.SessionID != -1)
                            DestroySession(user.SessionID);
                        StopCreatedSessions(user);
                        Console.WriteLine($"{UserName} has been disconnected");
                        UserList.Remove(user);
                        return;
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

                        if (SessionList[SessionID].status != GameStatus.destroyed)
                        {
                            string side = SessionList[SessionID].GetInactiveSide();
                            User JoinerUser = GetUser(UserName);
                            JoinerUser.side = side;

                            SessionList[SessionID].SetSecondPlayer(JoinerUser);
                            Console.WriteLine($"{UserName} joins game session #{SessionID} as {side}");

                            SendAcceptions(SessionID);
                        }
                        else
                            SendMessage(client, "DESTROY");
                    }
                    else if (request == "ACCEPT")
                    {
                        User user = GetUser(UserName);
                        if (user.SessionID != -1)
                        {
                            Console.WriteLine($"{UserName} accepted game session #{user.SessionID}");
                            if (SessionList[user.SessionID].status == GameStatus.wait)
                                SessionList[user.SessionID].status = GameStatus.AcceptOne;
                            else if (SessionList[user.SessionID].status == GameStatus.AcceptOne)
                            {
                                SessionList[user.SessionID].status = GameStatus.inProgress;
                                StartGame(user.SessionID);
                            }
                        }
                    }
                    else if (request == "REJECT")
                    {
                        User user = GetUser(UserName);
                        Console.WriteLine($"{UserName} rejected game session #{user.SessionID}");
                        DestroySession(user.SessionID);
                    }
                    else if (msg.Split(':')[0] == "MOVE")
                    {
                        UserName = msg.Split(':')[1];
                        User user = GetUser(UserName);
                        User userOp = SessionList[user.SessionID].GetOpponent(user);
                        SendMessage(userOp.client, msg);
                        Console.WriteLine($"{user.name} moves with {msg.Split(':')[2]}");
                    }
                    else if (request == "QUITGAME")
                    {
                        User user = GetUser(UserName);
                        Console.WriteLine($"{UserName} quit game session #{user.SessionID}");
                        DestroySession(user.SessionID);
                    }
                    else if (request == "CANCELNEW")
                    {
                        User user = GetUser(UserName);
                        int SessionID = GetCreatedSessionID(user);
                        Console.WriteLine($"{UserName} canceled game session #{SessionID}");
                        DestroySession(SessionID);
                    }

                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
        }

        private void StartGame(int SessionID)
        {
            User player1 = SessionList[SessionID].PlayerBlack;
            User player2 = SessionList[SessionID].PlayerWhite;
            SendMessage(player1.client, $"GAMESTART {player1.side} {SessionID}");
            SendMessage(player2.client, $"GAMESTART {player2.side} {SessionID}");
            Console.WriteLine($"game session #{SessionID} has been started");
        }

        private void StopCreatedSessions(User user)
        {
            int i = 0;
            foreach (GameSession game in SessionList)
            {
                if ((game.PlayerBlack != null && game.PlayerBlack.name == user.name) || (game.PlayerWhite != null && game.PlayerWhite.name == user.name))
                    if (game.status == GameStatus.wait || game.status == GameStatus.AcceptOne)
                        DestroySession(i);
                i++;
            }
        }

        private int GetCreatedSessionID(User user)
        {
            int i = 0;
            foreach (GameSession game in SessionList)
            {
                if ((game.PlayerBlack != null && game.PlayerBlack.name == user.name) || (game.PlayerWhite != null && game.PlayerWhite.name == user.name))
                    if (game.status == GameStatus.wait)
                        return i;
                i++;
            }
            return -1;
        }

        private void DestroySession(int SessionID)
        {
            if (SessionList[SessionID].status != GameStatus.destroyed)
            {
                User player1 = SessionList[SessionID].PlayerBlack;
                User player2 = SessionList[SessionID].PlayerWhite;
                SessionList[SessionID].status = GameStatus.destroyed;
                Console.WriteLine($"game session #{SessionID} has been destroyed");
                if (player1 != null)
                {
                    player1.SessionID = -1;
                    player1.side = null;
                    SendMessage(player1.client, $"DESTROY");
                }
                if (player2 != null)
                {
                    player2.SessionID = -1;
                    player2.side = null;
                    SendMessage(player2.client, $"DESTROY");
                }
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
                if (user != null && user.name == UserName)
                    return user;
            }
            return null;
        }

        private bool UsernameIsUnique(string name)
        {
            foreach (User user in UserList)
            {
                if (user != null && user.name == name)
                    return false;
            }
            return true;
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

