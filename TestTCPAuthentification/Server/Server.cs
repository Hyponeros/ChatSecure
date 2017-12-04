using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using Communication;

namespace Server
{
    public class Server
    {
        private int port;
        private static int connected_users;
        private static int temporary_connected;
        private string localDate;


        private static TcpListener responseListener;
        private static List<TcpClient> comm_list = new List<TcpClient>();

        public string serverDate
        {
            get
            {
                return localDate;
            }
            set
            {
                localDate = value;
            }
        }

        public Server(int port)
        {
            this.port = port;
            this.localDate = DateTime.Now.ToString();
        }

        public void StartServer()
        {
            // 127.0.0.1: IP du serveur local localhost avec un port quelconque donné.
            responseListener = new TcpListener(IPAddress.Any, port);
            responseListener.Start(); // Démarrage de l'écoute
            connected_users = 0;
            temporary_connected = 0;

            Console.WriteLine("> Serveur lancé (" + localDate + ")");
            Console.WriteLine(">>> Prêt à recevoir les flux des utilisateurs...");

            while (true)
            {
                TcpClient client = responseListener.AcceptTcpClient();
                temporary_connected++;

                // Vérification d'authentification
                if (AuthenticateUserToServer(client) == true)
                {
                    connected_users++;
                    temporary_connected--;
                    sendResponse(client);
                }
                else
                {
                    sendResponse(client);
                }
            }
        }

        public bool AuthenticateUserToServer(TcpClient user)
        {
            UserReceiver ur = new UserReceiver(user);
            Thread receiverThread = new Thread(ur.CheckUserIdentifiers);
            receiverThread.Start();

            if (ur._authentified)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void sendResponse(TcpClient user)
        {
            User u = Net.ReceiveUser(user.GetStream());

            Console.WriteLine(">>> Utilisateur " + u._login + ": erreur " + u._error);
        }
    }
}
