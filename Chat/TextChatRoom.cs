using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Chat
{
    class TextChatRoom : ChatRoom
    {
        private List<Chatter> chatters;
        private string topic;

        public TextChatRoom()
        {
            chatters = new List<Chatter>();

        }

        public string getTopic()
        {
            return topic;
        }
        public void setTopic(string topic)
        {
            this.topic = topic;
        }

        public void join(Chatter chatter)
        {
            chatters.Add(chatter);
        }

        public void quit(Chatter chatter)
        {
            chatters.Remove(chatter);
        }

        public void post(string msg, Chatter chatter)
        {

            IEnumerator Enum = this.chatters.GetEnumerator();

            while (Enum.MoveNext())
            {
                ((Chatter)Enum.Current).receiveAMessage(msg, chatter);
            }
           
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
    }
}
