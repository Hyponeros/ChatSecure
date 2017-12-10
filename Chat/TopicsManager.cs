using System;
using System.Collections.Generic;
using System.Text;

namespace Chat
{
    public interface TopicsManager
    {
        List<string> listTopics();
        ChatRoom joinTopic(string topic);
        void createTopic(string topic);
    }
}
