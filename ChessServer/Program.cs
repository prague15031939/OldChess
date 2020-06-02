using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ChessServer
{
    class Program
    {
        public static ChessServer server;

        public static string GetLocalIPAddress()
        {
            return "127.0.0.1";
            /*var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");*/
        }

        static void Main(string[] args)
        {
            Console.Write("OldChess server, v. 1.01\ninitial config..\nport: ");
            int port = Convert.ToInt32(Console.ReadLine());
            string ip = GetLocalIPAddress();
            server = new ChessServer(ip, port);
            server.StartWork();
            Console.Write("server started\n");
            server.MainLoop();
            Console.Write("server shutdown, press any key..\n");
            Console.ReadKey();
        }

    }
}
