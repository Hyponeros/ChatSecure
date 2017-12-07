using System;
using System.Text;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication
{
    public class UserExistException : AuthentificationException
    {

        public UserExistException(string login) : base(login, "L'utilisateur " + login + "est déjà enregistré.")
        {
        }
    }
}
