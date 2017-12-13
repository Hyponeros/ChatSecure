using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Communication;
using System.IO;
using System.Threading;

namespace ChatClient
{
    class Client
    {
        private string hostname;
        private int port;
        private TcpClient comm;

        public Client(string h, int p)
        {
            hostname = h;
            port = p;
        }

        public void start()
        {
            comm = new TcpClient(hostname, port);
            Console.WriteLine("Connection established");

            Thread readerThread = new Thread(this.ReadStream);
            readerThread.Start();

            while (true)
            {
                Message toSendMessage = new TextMessage(Console.ReadLine());

                Net.sendMsg(comm.GetStream(), toSendMessage);
            }
        }


        public void ReadStream()
        {
            try
            {
                while (true)
                {
                    Message message = Net.rcvMsg(comm.GetStream());

                    Console.WriteLine(message.ToString());

                }
            } catch(IOException e)
            {
                Console.WriteLine("Error while receiving message!");
                Console.WriteLine(e);
            }
        }

    }
}
