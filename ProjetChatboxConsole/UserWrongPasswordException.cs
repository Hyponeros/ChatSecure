using System;
using System.Text;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetChatboxConsole
{
    public class UserWrongPasswordException : Exception
    {
        public UserWrongPasswordException() : base(">>> Erreur: Mot de passe incorrect !")
        {
            
        }
    }
}
