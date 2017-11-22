using System;
using System.Text;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetChatboxConsole
{
    public class UserExitsException : Exception
    {
        public UserExitsException() : base("Déconnexion de l'utilisateur")
        {
            
        }

        public UserExitsException(string userName) : base("L'utilisateur " + userName + " s'est déconnecté")
        {
            
        }
    }
}
