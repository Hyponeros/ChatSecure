using System;
using System.Text;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Web; 

namespace ProjetChatboxConsole
{
    public interface AuthentificationManager
    {
        void addUser();
        void removeUser(string login);
        void authentify();
        int menu();

        string _login
        {
            get;
            set;
        }

        string _password
        {
            get;
            set;
        }

        Dictionary<string, string> liste
        {
            get;
            set;
        }

        bool _connected
        {
            get;
            set;
        }
    }
}
