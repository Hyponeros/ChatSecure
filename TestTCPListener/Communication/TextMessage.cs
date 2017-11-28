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
    [Serializable]
    public class TextMessage
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
            this.error = false;
        }

        public string _username
        {
            get
            {
                return username;
            }

            set
            {
                username = value;
            }
        }

        public string _message
        {
            get
            {
                return message;
            }

            set
            {
                message = value;
            }
        }

        public string _datetime
        {
            get
            {
                return datetime;
            }

            set
            {
                datetime = value;
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
                error = value;
            }
        }

        public override string ToString()
        {
            return datetime + " : " + username + " : " + message;
        }

    }
}
