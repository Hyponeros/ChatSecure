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
        private string author;
        private string text;

        public void SetAlias(String alias)
        {
            author = alias;
        }
        public TextMessage(string author, string text)
        {
            this.text = text;
            this.author = author;
        }
        public TextMessage(string text) : this(null, text) { }

        public override string ToString()
        {
            return DateTime.Now.ToString("HH:mm:ss") + " (" + author + ") : " + text;
        }

        public string GetText()
        {
            return text;
        }
    }
}