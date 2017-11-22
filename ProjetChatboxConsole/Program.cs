using System;
using System.Text;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Web;

namespace ProjetChatboxConsole
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            int choix;
            Authentification auth = new Authentification("users.txt");

            do
            {
                choix = auth.menu();

                switch (choix)
                {
                    case 1:
                        if (!auth._connected)
                        {
                            auth.authentify();
                        }
                        else
                        {
                            auth.removeUser(auth._login);
                        } 
                    break;

                    case 2:
                        if (auth._connected)
                        {
                            Console.Write("Chat disponible...");
                        }
                        else
                        {
                            auth.addUser();
                        }
                    break;

                    case 0:
                        Console.Write("Au revoir...");
                    break;
                }

                Console.ReadLine();

            } while (choix != 0);
        }
    }
}
