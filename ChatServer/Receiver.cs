﻿using System;
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
        private Chatter chatter;
        private ChatRoom chatroom;

        public Receiver(TcpClient s, Server serv)
        {
            comm = s;
            //Beware this server resources are common to all receivers
            server = serv;
        }

        public void handleChatter()
        {
            try
            {
                authenticate();

                joinChatRoom();

                while (true)
                {
                    Message msg = rcvMsg();
                    if (msg.GetText().Substring(0,1).Equals("/"))
                    {
                        specialInstruction(msg);
                    } else
                    {
                        msg.SetAlias(chatter.getAlias());
                        chatroom.post(msg);
                    }
                }

            } catch(Exception e)
            {
                Console.WriteLine(e);
            }

        }

        private void specialInstruction(Message msg)
        {
            throw new NotImplementedException();
        }

        private void joinChatRoom()
        {

            sendServMsg("Please enter a name to join a chatroom among the list to joing or another name to create a new chatroom :");
            String list = "";
            foreach (String chatRoom in server.tm.listTopics())
            {
                list += chatRoom + "\n";
            }
            sendServMsg(list);

            Message chatname = rcvMsg();
            
            //Get appropriate chatroom
            this.chatroom = server.tm.joinTopic(chatname.GetText());
            chatroom.join(chatter, new SenderDel(sendMsg));

            sendServMsg("Welcome to the ${chatroom.getTopic()} chat.");
            sendServMsg("Use '/quit' to quit the application and '/change' to change chatroom");
        }

        private void sendServMsg(string text)
        {
            sendMsg(new TextMessage(text));
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
                User user = new User(alias.GetText(), login.GetText(), password.GetText());

                lock (server.accessAuth)
                {
                    try
                    {
                        server.am.authentify(user.Login, user.Password);
                        sendServMsg("Authentication successful.\n");
                        chatter = (Chatter)user;
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

