using System;
using System.Collections.Generic;
using System.Text;
using Communication;

namespace Chat
{
    public interface ChatRoom
    {
        void post(String text, String author);
        void quit(Chatter chatter, SenderDel del);
        void join(Chatter chatter, SenderDel del);
        string getTopic();
        Object getAccess();
    }
}
