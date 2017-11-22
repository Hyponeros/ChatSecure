using System;
using System.Text;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetChatboxConsole
{
    public class UserUnknownException : Exception
    {
        public UserUnknownException() : base(">>> Erreur: Utilisateur inconnu")
        {
            
        }

        public UserUnknownException(string userName) : base(">>> Erreur: L'utilisateur " + userName + " est inconnu")
        {

        }
    }
}
