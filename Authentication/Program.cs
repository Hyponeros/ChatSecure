using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication
{
    class Program
    {
        static void Main(string[] args)
        {
                AuthentificationManager am = new Authentification();
                // users management 
                try
                {
                    am.addUser("bob", "123");
                    Console.WriteLine("Bob has been added !");
                    am.removeUser("bob");
                    Console.WriteLine("Bob has been removed !");
                    am.removeUser("bob");
                    Console.WriteLine("Bob has been removes twice !");
                }
                catch (UserUnknownException e)
                {
                    Console.WriteLine(e.login + " : user unknown (enable to remove)!");
                } catch (UserExistException e) {
                    Console.WriteLine(e.login + " has already been added !");
                }
                // authentification 
                try
                {
                    am.addUser("bob", "123");
                    Console.WriteLine("Bob has been added !");
                    am.authentify("bob", "123");
                    Console.WriteLine("Authentification OK !");
                    am.authentify("bob", "456");
                    Console.WriteLine("Invalid password !");
                }
                catch (UserWrongPasswordException e) {
                    Console.WriteLine(e.login + " has provided an invalid password !");
                }
                catch (UserExistException e) {
                    Console.WriteLine(e.login + " has already been aded !");
                }
                catch (UserUnknownException e) {
                    Console.WriteLine(e.login + " : user unknown (enable to remove)");
                }
                // persistance 
                try
                {
                    am.save("users.txt");
                    AuthentificationManager am1 = Authentification.load("users.txt");
                    am1.authentify("bob", "123");
                    Console.WriteLine("Loading complete !");
                } catch (UserUnknownException e) {
                    Console.WriteLine(e.login + " is unknown ! error during thesaving / loading.");
                } catch (UserWrongPasswordException e)
                {
                    Console.WriteLine(e.login + " has provided an invalid password!error during the saving / loading.");
                }
                catch (IOException e)
                {
                    Console.WriteLine(e);
                }
        }
    }
}
