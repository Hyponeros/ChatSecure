using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication
{
    public class AuthentificationException : Exception
    {
        private string _login;
        public string login { get { return _login; } set { _login = value; } }

        public AuthentificationException(string login, string message) : base(message)
        {
            this.login = login;
        }
    }
}
