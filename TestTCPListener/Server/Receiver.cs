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
        private static List<TcpClient> comm_list;

        public Receiver(TcpClient s, List<TcpClient> cl)
        {
            comm = s;
            comm_list = cl;
        }

        public void doOperation()
        {
            Console.WriteLine(">>> Réception du message ...");

            while (true)
            {
                // Lecture du message
                TextMessage msg = (TextMessage)Net.receiveMsg(comm.GetStream());

                Console.WriteLine(" -> Message reçu (" + msg._datetime + "): " + msg._message + " > " + msg._username + ")");

                // Envoi du message
                foreach (TcpClient stream in comm_list)
                {
                    Console.WriteLine(stream);
                    /*
                    StreamWriter sw = new StreamWriter(stream.GetStream());
                    sw.WriteLine();
                    sw.AutoFlush = true;
                    */
                    Net.sendMsg(stream.GetStream(), new TextMessage(msg._username, msg._message, msg._datetime, msg._error));
                }
            }
        }
    }
}
