using System;
using System.Net;
using System.Net.Sockets;
using Communication;

namespace Server
{
    public class UserReceiver
    {
        private TcpClient client;
        private User user;
        private bool authentified;
        private int authentificationCode;

        public User _user
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
            }
        }

        public bool _authentified
        {
            get
            {
                return authentified;
            }
            set
            {
                authentified = value;
            }
        }

        public int _authCode
        {
            get
            {
                return authentificationCode;
            }
            set
            {
                authentificationCode = value;
            }
        }

        public UserReceiver(TcpClient client)
        {
            this.client = client;
            authentified = false;
            authentificationCode = 0;
        }
        
        public void CheckUserIdentifiers()
        {
            user = Net.ReceiveUser(client.GetStream());

            Authentification auth = new Authentification("users.txt");
            auth.authentify(user);

            if (auth._connected)
            {
                authentificationCode = 0; 
            }
            else
            {
                if (user._error == 1)
                {
                    authentificationCode = 1;
                }

                if (user._error == 2)
                {
                    authentificationCode = 2;
                }
            }

            Net.SendUser(client.GetStream(), user);
        }
    }
}
