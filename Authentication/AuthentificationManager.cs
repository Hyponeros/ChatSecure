using System;
using System.Text;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Web; 

namespace Authentication
{
    public interface AuthentificationManager
    {
        void addUser(string login, string password);
        void removeUser(string login);
        void authentify(string login, string password);
        void save(String path);
    }
}
