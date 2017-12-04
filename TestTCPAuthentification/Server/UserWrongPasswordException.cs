using System;

namespace Server
{
    public class UserWrongPasswordException : Exception
    {
        public UserWrongPasswordException() : base(">>> Erreur: Mot de passe incorrect !")
        {

        }
    }
}