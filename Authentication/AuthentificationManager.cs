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
        void addUser(String alias, String login, String password);
        void removeUser(String login);
        void authentify(String login, String password);
        void save(String path);
        String getAlias(String login);
    }
}
