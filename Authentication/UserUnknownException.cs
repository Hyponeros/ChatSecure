using System;
using System.Text;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication
{
    public class UserUnknownException : AuthentificationException
    {
        public UserUnknownException(string login) : base(login, "L'utilisateur " + login + " n'est pas connu.")
        {
        }
    }
}
