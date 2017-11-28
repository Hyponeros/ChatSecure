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

namespace Communication
{
    public class Net
    {
        // Envoi du message avec sérialisation au format binaire
        public static void SendMessage(Stream s, TextMessage msg)
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(s, msg);
        }

        // Réception du message avec déserialisation
        public static TextMessage ReceiveMessage(Stream s)
        {
            BinaryFormatter bf = new BinaryFormatter();
            return (TextMessage)bf.Deserialize(s);
        }

        public static void SendMessageNotSerialized(TcpClient client, TextMessage msg)
        {
            Console.WriteLine(">>> Envoi...");
            StreamWriter sw = new StreamWriter(client.GetStream());
            sw.WriteLine("date:" + msg._datetime);
            sw.WriteLine("user:" + msg._username);
            sw.WriteLine("msg:" + msg._message);
            sw.AutoFlush = true;
            // sw.Close();
        }

        public static TextMessage ReceiveMessageNotSerialized(TcpClient client)
        {
            StreamReader sr = new StreamReader(client.GetStream());
            Console.WriteLine(" -> Extraction du message...");
            string line = "";
            TextMessage msg = new TextMessage("", "", "", false);
            int found = 0;

            string s1 = string.Empty;
            string s2 = string.Empty;
            string s3 = string.Empty;

            try
            {
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return msg;
        }
    }
}
