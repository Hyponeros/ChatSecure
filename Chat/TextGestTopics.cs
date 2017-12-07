using System;
using System.Collections.Generic;
using System.Text;


namespace Chat
{
    class TextGestTopics : TopicsManager
    {
        Dictionary<string, ChatRoom> chatroomByTopic;

        public TextGestTopics()
        {
            chatroomByTopic = new Dictionary<string, ChatRoom>();
        }

        public void createTopic(string topic)
        {
            if (!getTopics().Contains(topic))
            {
                chatroomByTopic.Add(topic, new TextChatRoom());
            }
        }

        public List<string> getTopics()
        {
            return new List<String>(chatroomByTopic.Keys);
        }

        public ChatRoom joinTopic(string topic)
        {
            //Creation de l'entrée dans le dictionnaire et de la chatroom si inexistante
            if (!getTopics().Contains(topic))
            {
                createTopic(topic);
            }

            return chatroomByTopic[topic];
        }

        public List<string> listTopics()
        {
            List<String> topics = getTopics();
            if (topics.Count.Equals(0))
            {
                Console.WriteLine("Sujets ouverts : aucun.");

            }
            else
            {
                Console.WriteLine("Sujets ouverts : ");

                foreach (string topic in topics)
                {
                    Console.WriteLine(topic);
                }
            }
            
            return topics;
        }
    }
}
