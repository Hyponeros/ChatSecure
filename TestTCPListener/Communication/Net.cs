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
        // Sérialisation au format binaire
        public static void sendMsg(NetworkStream s, TextMessage msg)
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(s, msg);
            s.Flush();
        }

        // Déserialisation
        public static TextMessage receiveMsg(NetworkStream s)
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

            while ((line = sr.ReadLine()) != null)
            {
                found = line.IndexOf(":"); // Extract point
                Console.WriteLine(line);

                if (line.Substring(0, found) == "date")
                {
                    msg._datetime = line.Substring(found + 1); 
                }
                else if (line.Substring(0, found) == "user")
                {
                    msg._username = line.Substring(found + 1); 
                }
                else if (line.Substring(0, found) == "msg")
                {
                    msg._message = line.Substring(found + 1); 
                }
                else
                {
                    Console.WriteLine("Erreur...");
                }
            }

            return msg;
        }
    }
}
