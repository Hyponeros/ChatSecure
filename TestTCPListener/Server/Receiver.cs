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
using Communication;
using System.Collections.Generic;

namespace Server
{
    public class Receiver
    {
        private TcpClient comm;
        private TextMessage msg;
        private static List<TcpClient> comm_list = new List<TcpClient>();

        public Receiver(TcpClient s, List<TcpClient> cl)
        {
            comm = s;
            comm_list = cl;
            msg = new TextMessage();
        }

        // Le serveur reçoit les messages avant de les diffuser
        public void doOperation()
        {
            // StreamReader sr = new StreamReader(comm.GetStream());
            // TextMessage msg = new TextMessage("", "", "", false);
                
            while (true)
            {
                // Lecture du message
                // NetworkStream stream1 = comm.GetStream();
                // TextMessage msg = (TextMessage)Net.receiveMsg(stream1);

                /*
                StreamReader sr = new StreamReader(comm.GetStream());
                string msg = sr.ReadLine();
                Console.WriteLine(" -> Message reçu (" + msg + ")");
                */

                Console.WriteLine(">>> En attente d'un message ...");
                /*
                string line = "";
                int found = 0;

                string s1 = string.Empty;
                string s2 = string.Empty;
                string s3 = string.Empty;
                */

                try
                {
                    /*
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
                            msg._message = line.Substring(found + 1);
                            // Console.WriteLine(" >> reçu: " + line.Substring(found + 1));
                        }
                        else
                        {
                            Console.WriteLine("Erreur...");
                        }
                    }
                    */

                    // msg = Net.ReceiveMessageNotSerialized(comm);
                    msg = Net.ReceiveMessage(comm.GetStream());
                    Console.WriteLine(" -> Message reçu (" + msg._datetime + "): " + msg._username + " > " + msg._message + ")");

                    messageBroadcast(msg, comm);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    break;
                }
            }
            
           // TextMessage msg = Net.ReceiveMessageNotSerialized(comm);
           // Console.WriteLine("[" + msg._datetime + "] " + msg._username + " > " + msg._message);
        }

        public void messageBroadcast(TextMessage msgBroad, TcpClient excludeClient)
        {
            // Diffusion du message
            foreach (TcpClient stream in comm_list)
            {
                Console.WriteLine("Test...");

                if (stream != excludeClient)
                {
                    /*
                    StreamWriter sw = new StreamWriter(stream.GetStream());
                    Console.WriteLine(">>> Diffusion du message de " + msgBroad._username + "(@" + comm.Client.RemoteEndPoint + ")");
                    sw.WriteLine("date:" + msgBroad._datetime);
                    sw.WriteLine("user:" + msgBroad._username);
                    sw.WriteLine("msg:" + msgBroad._message);
                    sw.Flush();
                    */
                    // Net.SendMessageNotSerialized(stream, msgBroad);
                    Net.SendMessage(stream.GetStream(), msgBroad);
                }

                // Net.SendMessageNotSerialized(stream, new TextMessage(msg._username, msg._message, msg._datetime, msg._error));
            }
        }
    }
}
