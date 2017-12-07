using System;
using System.Collections.Generic;
using System.Text;

namespace Chat
{
    interface ChatRoom
    {
        void post(string msg, Chatter chatter);
        void quit(Chatter chatter);
        void join(Chatter chatter);
        string getTopic();
    }
}
