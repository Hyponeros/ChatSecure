using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Communication;

namespace Server
{
    public class Authentification: AuthentificationManager
    {
        private static Dictionary<int, User> userList;
        private static Dictionary<string, string> userIdentifiersList;

        private User user;
        private bool connected;
        private string path;

        public Dictionary<int, User> _userList
        {
            get
            {
                return userList;
            }

            set
            {
                userList = value;
            }
        }

        public bool _connected
        {
            get
            {
                return connected;
            }

            set
            {
                connected = value;
            }
        }

        public Authentification(string path)
        {
            this.path = path;
            userIdentifiersList = new Dictionary<string, string>();
            userList = new Dictionary<int, User>();
            user = new User();

            try
            {
                // File 
                if (File.Exists(path))
                {
                    Console.WriteLine(">>> Chargement du fichier de configuration " + path);
                }
                else
                {
                    // Create the file.
                    using (FileStream fs = File.Create(path))
                    {
                        Console.WriteLine(">>> Première utilisation de l'application: fichier de configuration " + path + " créé.");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Le fichier n'a pas pu être vérifié: ");
                Console.WriteLine(e.Message);
            }
        }

        private void initList()
        {
            // Initialization of the userList (Dictionary)
            try
            {
                string line = "";

                // Checking if user information was saved successfully
                using (StreamReader sr = new StreamReader(path))
                {
                    int found = 0;
                    int users = 0;

                    while ((line = sr.ReadLine()) != null)
                    {
                        found = line.IndexOf(":"); // Extract point (login:password)
                        users++;
                        user._login = line.Substring(0, found);
                        user._password = line.Substring(found + 1);
                        userList.Add(users, user);
                        userIdentifiersList.Add(user._login, user._password);
                    }

                    Console.WriteLine(users + " utilisateurs ajoutés.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Le fichier n'a pas pu être ouvert: ");
                Console.WriteLine(e.Message);
            }
        }

        public void addUser(User user)
        {
            throw new NotImplementedException();
        }

        public void addUser(string login, string password)
        {
            throw new NotImplementedException();
        }

        public void authentify(User user)
        {
            if (checkIdentifiers() == true)
            {
                connected = true;
            }
            else
            {
                connected = false;
            }
        }

        private bool checkIdentifiers()
        {
            try
            {
                // Check user
                if (userIdentifiersList.ContainsKey(user._login))
                {
                    // Check password
                    if (userIdentifiersList.ContainsValue(user._password))
                    {
                        Console.WriteLine(">>> Connexion validée pour l'utilisateur " + user._login);
                        user._error = 0; // 0 = identifiants corrects

                        return true;
                    }
                    else
                    {
                        throw new UserWrongPasswordException();
                    }
                }
                else
                {
                    throw new UserUnknownException(user._login);
                }
            }
            catch (UserWrongPasswordException ex)
            {
                Console.WriteLine(ex.Message);
                user._error = 1; // 1 = mot de passe incorrect

                return false;
            }
            catch (UserUnknownException ex)
            {
                Console.WriteLine(ex.Message);
                user._error = 2; // 2 = nom d'utilisateur inconnu

                return false;
            }
        }

        public void authentify(string login, string password)
        {
            throw new NotImplementedException();
        }

        public int menu()
        {
            throw new NotImplementedException();
        }

        public void removeUser(string login)
        {
            throw new NotImplementedException();
        }
    }
}
