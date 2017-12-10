using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Communication;
using Chat;
using Authentication;

namespace ChatServer
{
    public class Server
    {
        private int port;
        //Control access to am
        public Object accessAuth { get; }
        //Control access to tm
        public Object accessLog { get; }
        public TopicsManager tm { get; }

        public AuthentificationManager am { get; }

        public Server(int port)
        {
            this.port = port;
            tm = new TextGestTopics();
            am = new Authentification();
            accessAuth = new Object();
            accessLog = new Object();
        }
        

        public void start()
        {
            TcpListener l = new TcpListener(new IPAddress(new byte[] { 127, 0, 0, 1 }), port);
            l.Start();

            while (true)
            {
                TcpClient comm = l.AcceptTcpClient();
                //the new client is served in a new thread
                new Thread(new Receiver(comm, this).handleChatter).Start();

            }   
        }   
    }
}
