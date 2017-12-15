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
            //Establish connection
            comm = new TcpClient(hostname, port);

            Thread writerThread = new Thread(WriteStream);
            writerThread.Start();

            try
            {
                ReadStream();
            } catch(Exception e) {}

            writerThread.Abort();
            return;
        }

        public void WriteStream()
        {
            while (true)
            {
                Message toSendMessage = new TextMessage(Console.ReadLine());


                Net.sendMsg(comm.GetStream(), toSendMessage);
            }
        }

        public void ReadStream()
        {
            while (true)
            {
                Message message = Net.rcvMsg(comm.GetStream());
                if (message.ToString().Substring(0, 1).Equals("/"))
                {
                    specialInstruction(message);
                }
                else
                {
                    Console.WriteLine(message.ToString());
                }
            }
        }

        private void specialInstruction(Message message)
        {
            if (message.ToString().Equals("/quit")){
                throw new Exception();
            } else if (message.ToString().Equals("/clear"))
            {
                Console.Clear();
            }
        }


    }
}
