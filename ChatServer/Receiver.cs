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
                    if (msg.ToString().Substring(0,1).Equals("/"))
                    {
                        if (specialInstruction(msg))
                        {
                            //Quitting
                            return;
                        }
                    } else
                    {
                        lock (chatroom.getAccess())
                        {
                            chatroom.post(msg.ToString(), this.chatter.getAlias());
                        }
                    }
                }

            } catch(Exception e)
            {
                Console.WriteLine(e);
            }

        }

        //Return true if the user is leaving
        private bool specialInstruction(Message msg)
        {
            if (msg.ToString().Equals("/quit") || msg.ToString().Equals("/change"))
            {
                lock (chatroom.getAccess())
                {
                    chatroom.quit(chatter);
                }

                if (msg.ToString().Equals("/change"))
                {
                    joinChatRoom();
                    return false;
                }
                return true;
            }  else
            {
                sendSimpleMessage("Incorrect special instruction, use '/quit' to quit the application and '/change' to change chatroom.");
                return false;
            }
        }

        private void joinChatRoom()
        {
            //Joining a chat always succeed because a ne chatroom can be created
            sendSimpleMessage("Please enter a name to join a chatroom among the list to joing or another name to create a new chatroom :");
            String list = "";
            foreach (String chatRoom in server.tm.listTopics())
            {
                list += chatRoom + "\n";
            }
            sendSimpleMessage(list);

            Message chatname = rcvMsg();

            //Get appropriate chatroom
            lock (server.accessLog)
            {
                this.chatroom = server.tm.joinTopic(chatname.ToString());
            }
            lock (chatroom.getAccess())
            {
                chatroom.join(chatter, new SenderDel(sendMsg));
            }
            

            sendSimpleMessage("Welcome to the " + chatroom.getTopic() + " chat.\n" +
                 "Use '/quit' to quit the application and '/change' to change chatroom");
           
        }
        private void sendSimpleMessage(String text)
        {
            Net.sendMsg(comm.GetStream(), new TextMessage(text));
        }
        private void sendMsg(String text, String author)
        {
            Net.sendMsg(comm.GetStream(), new TextMessage(text, author));
        }

        private Message rcvMsg()
        {
            return Net.rcvMsg(comm.GetStream());
        }

        public bool authenticate()
        {
            
            User user;

            //Procédure d'identification
            while (true)
            {

                sendSimpleMessage("Please enter login");
                Message login = Net.rcvMsg(comm.GetStream());
                sendSimpleMessage("Please enter password");
                Message password = Net.rcvMsg(comm.GetStream());
                user = new User(login.ToString(), password.ToString());

                lock (server.accessAuth)
                {
                    try
                    {
                        server.am.authentify(user.Login, user.Password);
                        sendSimpleMessage("Authentication successful.\n");
                        user.Alias = server.am.getAlias(user.Login);
                        this.chatter = (Chatter) user;
                        return true;
                    }
                    catch (UserWrongPasswordException e)
                    {
                        Console.WriteLine(e);
                        sendSimpleMessage("Login or password incorrect, please try again.");
                    }
                    catch (UserUnknownException e)
                    {
                        Console.WriteLine(e);
                        sendSimpleMessage("Login or password incorrect, please try again.");
                    }
                }

            }
            
            
        }

    }
}

