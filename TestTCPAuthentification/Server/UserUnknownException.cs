using System;

namespace Server
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
