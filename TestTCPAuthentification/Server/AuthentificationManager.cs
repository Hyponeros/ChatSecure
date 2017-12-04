using System;

namespace Server
{
    public interface AuthentificationManager
    {
        void addUser(string login, string password);
        void removeUser(string login);
        void authentify(string login, string password);
        int menu();
    }
}
