using System;
using System.Text;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication
{
    public class UserWrongPasswordException : AuthentificationException
    {
        public UserWrongPasswordException(string login) : base(login, "Mot de passe pour " + login + " incorrect.")
        {   
        }
    }
}
