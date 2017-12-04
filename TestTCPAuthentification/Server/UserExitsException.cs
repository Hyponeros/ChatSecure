using System;

namespace Server
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
