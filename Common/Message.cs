using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface Message
    {
        string ToString();
    }

    [Serializable]
    public class MsgText : Message
    {
        private string msg;

        public MsgText(string author, string content)
        {
            string now = DateTime.Now.ToLongTimeString();
            this.msg = "[" + now + "] " + author + " : " + content; 
        }


        public override string ToString()
        {
            return msg;
        }

    }
}

