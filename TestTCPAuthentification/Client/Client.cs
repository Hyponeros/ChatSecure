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
    public class Client
    {
        private string hostname;
        private int port;
        private User user;

        public Client(string h, int p)
        {
            hostname = h;
            port = p;
            user = new User();
        }

        public void StartClient()
        {
            User u = new User();
            u._connected = false;
            TcpClient comm = new TcpClient(hostname, port);
            Console.WriteLine(">>> Connexion établie.");
            int choix;
            // Démarrer la réception du broadcast du serveur
            Thread threadReceiver = new Thread(new ParameterizedThreadStart(receiveResponse));
            threadReceiver.Start(comm);

            do
            {

            } while ();
        }

        public void receiveResponse(object o)
        {
            
        }
    }
}
