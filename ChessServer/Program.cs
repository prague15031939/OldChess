using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;


namespace ChessServer
{
    class Program
    {
        public static ChessServer server;

        private static void ProcessControlCommands(ChessServer server)
        {
            while (true)
            {
                string command = Console.ReadLine();
                if (command == "!stop")
                {
                    try
                    {
                        server.StopWork();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        continue;
                    }
                    Console.WriteLine("server shutdowned, press any key..");
                    Console.ReadKey();
                    return;
                }
                else if (command == "!users")
                {
                    server.PrintUsers();
                }
                else if (command == "!sessions")
                {
                    server.PrintSessions();
                }
            }
        }

        static void Main(string[] args)
        {
            Console.Write("OldChess server, v. 1.01\ninitial config..\n");
            while (true)
            {
                string PatternServer = @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b:[0-9]{1,5}$";
                Console.Write("ip: ");
                string ip = Console.ReadLine();
                Console.Write("port: ");
                int port = Convert.ToInt32(Console.ReadLine());
                if (Regex.IsMatch($"{ip}:{port}", PatternServer))
                {
                    server = new ChessServer(ip, port);
                    break;
                }
                else
                    Console.WriteLine("invalid data, try again");
            }
            server.StartWork();
            Console.Write("server started\n");
            Thread ServerThread = new Thread(new ThreadStart(() => ProcessControlCommands(server)));
            ServerThread.Start();

            server.MainLoop();
        }

    }
}
