using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server serv = new Server(8976);

            //Initatilize Authentification Manager
            serv.am.addUser("admin", "admin");
            serv.am.addUser("Bob", "123");
            serv.am.addUser("", "");

            //Create ChatRooms
            serv.tm.createTopic("Games");
            serv.tm.createTopic("C#");
            serv.tm.createTopic("Movies");

            serv.start();

        }
    }
}
