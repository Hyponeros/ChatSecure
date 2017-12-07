using System;
using System.Collections.Generic;
using System.Text;

namespace Chat
{
    public interface Chatter
    {
        void receiveAMessage(string msg, Chatter chatter);
        string getAlias();
    }
}
