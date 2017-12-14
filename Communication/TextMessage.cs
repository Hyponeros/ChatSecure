using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication
{
    [Serializable]
    public class TextMessage : Message
    {
        private string text;
        private string author;

        public TextMessage(string text, string author)
        {
            this.text = text;
            this.author = author;    
        }

        public TextMessage(string text) : this(text, null) { }

        public override string ToString()
        {
            if (this.author == null)
            {
                return text;
            }
            else
            {
                return DateTime.Now.ToString("HH:mm:ss") + " (" + author + ") : " + text;
            }
        }

        public string GetText()
        {
            return text;
        }
    }
}