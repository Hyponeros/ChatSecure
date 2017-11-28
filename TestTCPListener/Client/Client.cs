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
        private TextMessage msg;

        public Client(string h, int p)
        {
            hostname = h;
            port = p;
            msg = new TextMessage();
        }

        public void startClient()
        {

            Console.Write("> Votre nom d'utilisateur: ");
            string username = Console.ReadLine();

            Console.WriteLine("\n");

            TcpClient comm = new TcpClient(hostname, port);
            Console.WriteLine(">>> Connexion établie.");

            // Démarrer la réception du broadcast du serveur
            Thread threadReceiver = new Thread(new ParameterizedThreadStart(receiveServerBroadcast));
            threadReceiver.Start(comm);

            // StreamWriter sw = new StreamWriter(comm.GetStream());

            Console.WriteLine("==========================");
            Console.WriteLine("====      ChatBox     ====");
            Console.WriteLine("==========================");

            while (true)
            {
                if (comm.Connected)
                {
                    Console.Write("> Votre message: ");
                    string message = Console.ReadLine();

                    DateTime localDate = DateTime.Now;

                    msg._username = username;
                    msg._message = message;
                    msg._datetime = localDate.ToString();
                    msg._error = false;

                    /*
                    sw.WriteLine("date:" + msg._datetime);
                    sw.WriteLine("user:" + msg._username);
                    sw.WriteLine("msg:" + msg._message);
                    sw.Flush(); */

                    Console.WriteLine("\n>>> Envoi du message: \"" + msg._message + "\" au serveur");
                    // Net.SendMessageNotSerialized(comm, msg);
                    Net.SendMessage(comm.GetStream(), msg);
                }

                // Net.SendMessageNotSerialized(comm, new TextMessage(username, message, localDate.ToString(), false));

                /*
                StreamWriter sw = new StreamWriter(comm.GetStream());
                string request = sw.ToString();
                sw.WriteLine(request);
                sw.AutoFlush = true;
                */

                // Console.WriteLine("Resultat = " + (TextMessage)Net.receiveMsg(comm.GetStream()));
                // Thread t = new Thread(new ThreadStart((TextMessage)Net.receiveMsg));
            }
        }

        public void receiveServerBroadcast(object o)
        {
            TcpClient client = (TcpClient)o;
            StreamReader sr = new StreamReader(client.GetStream());

            // NetworkStream stream = client.GetStream();

            // StreamReader sr = new StreamReader(client.GetStream());
            // TextMessage msg = (TextMessage)Net.receiveMsg(stream);

            // string msg = sr.ReadLine();
            // Console.WriteLine(" -> Message reçu (" + msg._datetime + "): " + msg._message + " > " + msg._username + ")");

            while (true)
            {
                // string line = "";
                // int found = 0;

                try
                {
                    /*
                    string s1 = string.Empty;
                    string s2 = string.Empty;
                    string s3 = string.Empty;

                    for (int i = 0; i < 3; i++)
                    {
                        line = sr.ReadLine();
                        found = line.IndexOf(":"); // Extract point

                        if (line.Substring(0, found) == "date")
                        {
                            s1 = line.Substring(found + 1);
                            msg._datetime = line.Substring(found + 1);
                            // Console.WriteLine(" >> reçu: " + line.Substring(found + 1));
                        }
                        else if (line.Substring(0, found) == "user")
                        {
                            s2 = line.Substring(found + 1);
                            msg._username = line.Substring(found + 1);
                            // Console.WriteLine(" >> reçu: " + line.Substring(found + 1));
                        }
                        else if (line.Substring(0, found) == "msg")
                        {
                            s3 = line.Substring(found + 1);
                            // Console.WriteLine(" >> reçu: " + line.Substring(found + 1));
                        }
                        else
                        {
                            Console.WriteLine("Erreur...");
                        }
                    } */

                    // msg = Net.ReceiveMessageNotSerialized(client);
                    msg = Net.ReceiveMessage(client.GetStream());
                    Console.WriteLine("[" + msg._datetime + "] " + msg._username + " > " + msg._message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    break;
                }
            }
        }
    }
}
