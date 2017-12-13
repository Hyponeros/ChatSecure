using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Communication;

namespace Chat
{
    public delegate void SenderDel(Message msg);
    public class TextChatRoom : ChatRoom
    {
        private List<Chatter> chatters;
        private string topic;
        //lock to use the delegate of chatroonm
        private Object accessCR;
        //Delegate déclaration, this delegate will trigger the message sending to all chatter.
        private SenderDel dispatchMessage;

        public TextChatRoom()
        {
            chatters = new List<Chatter>();
            accessCR = new Object();
            dispatchMessage = null;
        }
        
        

        public string getTopic()
        {
            return topic;
        }
        public void setTopic(string topic)
        {
            this.topic = topic;
        }

        public void join(Chatter chatter, SenderDel del)
        {
            chatters.Add(chatter);
            this.dispatchMessage += del;
        }

        public void quit(Chatter chatter)
        {
            chatters.Remove(chatter);
        }

        public void post(Message message)
        {
            dispatchMessage(message);
        }

        public override string ToString()
        {
            string cr = "";

            foreach(Chatter chatter in chatters)
            {
                cr += chatter.getAlias();
                cr += "\n";
            }
            return cr;
        }

        public void setDelegate(SenderDel del)
        {
            dispatchMessage += del;
        }
    }
}
