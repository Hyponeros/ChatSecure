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
        private static int connected_users;

        // La liste des clients est commune à tous les threads que chaque client exécute
        private static TcpListener responseListener;
        private static List<TcpClient> comm_list = new List<TcpClient>();

        // Constructeur initialisant le port défini
        public Server(int port)
        {
            this.port = port;
        }

        public void startServer()
        {
            // 127.0.0.1: IP du serveur local localhost avec un port quelconque donné.
            responseListener  = new TcpListener(IPAddress.Any, port);
            responseListener.Start(); // Démarrage de l'écoute
            connected_users = 0;

            while (true)
            {
                TcpClient comm = responseListener.AcceptTcpClient();
                Console.WriteLine(">>> Connexion établie @" + comm.Client.RemoteEndPoint);
                connected_users++;
                Console.WriteLine("-> Utilisateurs en ligne: " + connected_users);
                comm_list.Add(comm);

                Thread receiverThread = new Thread(new Receiver(comm, comm_list).doOperation);
                receiverThread.Start();
            }
        }
    }
}
