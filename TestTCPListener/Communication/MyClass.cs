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
        public static void sendMsg(Stream s, Message msg)
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(s, msg);
        }

        // Déserialisation
        public static Message receiveMsg(Stream s)
        {
            BinaryFormatter bf = new BinaryFormatter();
            return (Message)bf.Deserialize(s);
        }
    }

    public interface Message
    {
        string ToString();
    }

    [Serializable]
    public class TextMessage : Message
    {
        private string username;
        private string message;
        private string datetime;
        private bool error;

        public TextMessage(string username, string message, string datetime, bool error)
        {
            this.username = username;
            this.message = message;
            this.datetime = datetime;
            this.error = error;
        }

        public TextMessage()
        {
            
        }

        public string _username
        {
            get
            {
                return username;
            }
        }

        public string _message
        {
            get
            {
                return message;
            }
        }

        public string _datetime
        {
            get
            {
                return datetime;
            }
        }

        public bool _error
        {
            get
            {
                return error;
            }

            set
            {
                error = _error;
            }
        }

        public override string ToString()
        {
            return ">>> [" + datetime + "] " + username + " > " + message;
        }

    }
}
