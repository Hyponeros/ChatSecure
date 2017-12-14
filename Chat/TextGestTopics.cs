using System;
using System.Collections.Generic;
using System.Text;


namespace Chat
{
    public class TextGestTopics : TopicsManager
    {
        private Dictionary<string, ChatRoom> chatroomByTopic;

        public TextGestTopics()
        {
            chatroomByTopic = new Dictionary<string, ChatRoom>();
        }

        public void createTopic(string topic)
        {
            if (!listTopics().Contains(topic))
            {
                chatroomByTopic.Add(topic, new TextChatRoom());
            }
        }


        public ChatRoom joinTopic(string topic)
        {
            //Creation de l'entrée dans le dictionnaire et de la chatroom si inexistante
            if (!listTopics().Contains(topic))
            {
                createTopic(topic);
            }

            return chatroomByTopic[topic];
        }

        public List<string> listTopics() { 
            return new List<String>(chatroomByTopic.Keys);
        }
    }
}
