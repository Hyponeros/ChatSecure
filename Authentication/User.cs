using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat;

namespace Authentication
{
    [Serializable]
    public class User : TextChatter
    {
        private string _login;
        private string _password;
        public string Password { get { return _password; } set { _password = value; } }
        public string Login { get { return _login; } set { _login = value; } }
        public User(string login, string password) : base("Guest")
        {
            _password = password;
            _login = login;
        }
        public User(string alias, string login, string password) : base(alias)
        {
            _password = password;
            _login = login;
        }
    }
}
