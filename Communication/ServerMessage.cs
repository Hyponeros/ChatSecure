using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication
{
    [Serializable]
    public class ServerMessage : Message
    {
        private String text;

        public ServerMessage(String text)
        {
            this.text = text;
        }

        public string GetText()
        {
            return text;
        }

        public override string ToString()
        {
            return text;
        }

    }
}
