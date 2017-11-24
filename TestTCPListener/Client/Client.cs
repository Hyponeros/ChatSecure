using System;
using System.Text;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using Communication;

namespace Client
{
    class Client
    {
        private string hostname;
        private int port;

        public Client(string h, int p)
        {
            hostname = h;
            port = p;
        }

        public void startClient()
        {
            TcpClient comm = new TcpClient(hostname, port);
            Console.WriteLine(">>> Connexion établie.");

            Console.Write("> Votre nom d'utilisateur: ");
            string username = Console.ReadLine();

            Console.WriteLine("\n");

            while (true)
            {
                Console.Write("> Votre message: ");

                string message = Console.ReadLine();
                DateTime localDate = DateTime.Now;

                Console.WriteLine("\nEnvoi du message: " + message + " au serveur");

                Net.sendMsg(comm.GetStream(), new TextMessage(username, message, localDate.ToString(), false));
                new Thread(new Sender(comm, comm_list).doOperation).Start();
                // Console.WriteLine("Resultat = " + (TextMessage)Net.receiveMsg(comm.GetStream()));
                // Thread t = new Thread(new ThreadStart((TextMessage)Net.receiveMsg));
            }
        }

    }
}
