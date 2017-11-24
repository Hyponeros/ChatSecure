using System;
using System.Text;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Collections.Generic;

namespace Server
{
    public class Server
    {
        private int port;
        private int connected_users;
        private List<TcpClient> comm_list = new List<TcpClient>();

        // Constructeur initialisant le port défini
        public Server(int port)
        {
            this.port = port;
        }

        public void startServer()
        {
            // 127.0.0.1: IP du serveur local localhost avec un port quelconque donné.
            TcpListener l = new TcpListener(new IPAddress(new byte[] { 127, 0, 0, 1 }), port);
            l.Start();

            while (true)
            {
                TcpClient comm = l.AcceptTcpClient();
                Console.WriteLine(">>> Connexion établie @" + comm);
                comm_list.Add(comm);
                new Thread(new Receiver(comm, comm_list).doOperation).Start();
            }
        }
    }
}
