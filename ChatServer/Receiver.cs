using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net.Sockets;
using System.Net;
using Chat;
using Communication;
using Authentication;

namespace ChatServer
{
    class Receiver
    {
        //Locks are used to access server resources
        private TcpClient comm;
        private Server server;
        private User user;

        public Receiver(TcpClient s, Server serv)
        {
            comm = s;
            //Beware this server resources are common to all receivers
            server = serv;
        }

        public void handleChatter()
        {
            authenticate();

            joinChatRoom();


        }

        private void joinChatRoom()
        {
            sendServMsg("Please enter a name to join a chatroom among the list :");
            foreach(String chatroom in server.tm.listTopics())
            {
                sendServMsg(chatroom);
            }

            //rcvMsg
        }

        private void sendServMsg(string text)
        {
            Net.sendMsg(comm.GetStream(), new ServerMessage(text));
        }
        private void sendMsg(Message msg)
        {
            Net.sendMsg(comm.GetStream(), msg);
        }

        private Message rcvMsg()
        {
           return Net.rcvMsg(comm.GetStream());
        }

        public bool authenticate()
        {

            while (true)
            {
                //Procédure d'identification
                sendServMsg("Please enter alias");
                TextMessage alias = (TextMessage)Net.rcvMsg(comm.GetStream());
                sendServMsg("Please enter login");
                TextMessage login = (TextMessage)Net.rcvMsg(comm.GetStream());
                sendServMsg("Please enter password");
                TextMessage password = (TextMessage)Net.rcvMsg(comm.GetStream());
                this.user = new User(alias.GetText(), login.GetText(), password.GetText());

                lock (server.accessAuth)
                {
                    try
                    {
                        server.am.authentify(user.Login, user.Password);
                        sendServMsg("Authentication successful.\n");
                        return true;
                    }
                    catch (UserWrongPasswordException e)
                    {
                        Console.WriteLine(e);
                    }
                    catch (UserUnknownException e)
                    {
                        Console.WriteLine(e);
                    }
                }

                sendServMsg("Login or password incorrect, please try again.");
            }
        }

    }
}

