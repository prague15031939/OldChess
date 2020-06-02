using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ChessServer
{
    class User
    {
        public TcpClient client { get; set; }
        public string name { get; set; }
        public string side { get; set; }
        public int SessionID { get; set; }

        public User(TcpClient client, string name)
        {
            this.client = client;
            this.name = name;
        }
    }
}
