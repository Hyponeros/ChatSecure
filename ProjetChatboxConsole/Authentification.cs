using System;
using System.Text;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Web;
using System.IO;

namespace ProjetChatboxConsole
{
    public class Authentification : AuthentificationManager
    {
        private Dictionary<string, string> ListeUtilisateurs;
        private string login;
        private string password;
        private string path;
        private bool connected;

        public string _login
        {
            get
            {
                return login;
            }

            set
            {
                login = _login;
            }
        }

        public string _password
        {
            get
            {
                return password;
            }

            set
            {
                password = _password;
            }
        }

        public Dictionary<string, string> liste
        {
            get
            {
                return ListeUtilisateurs;
            }

            set
            {
                ListeUtilisateurs.Add(login, password);
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
                connected = _connected;
            }
        }

        public Authentification(string path)
        {
            this.login = "";
            this.password = "";
            this.path = path;
            this.ListeUtilisateurs = new Dictionary<string, string>();

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

            initList();
        }

        public int menu()
        {
            bool check = false;
            int selection = 0;

            Console.Clear();

            Console.WriteLine("***********************************************");
            Console.WriteLine("***             Projet C# ChatBox           ***");
            Console.WriteLine("***   Koussaïla BEN MAMAR - Arthur SCHICKEL ***");
            Console.WriteLine("***********************************************\n");

            if (connected == false)
            {
                Console.WriteLine("1 - Connexion");
                Console.WriteLine("2 - Inscription");
                Console.WriteLine("0 - Quitter\n");

                do
                {
                    Console.Write("Votre choix: ");

                    var choix = Console.ReadLine();

                    if (int.TryParse(choix, out int number1))
                    {
                        Console.WriteLine($"{number1} est un entier");

                        if ((number1 < 0) || (number1 > 2))
                        {
                            Console.WriteLine("Erreur: Saisie invalide !");
                            check = false;
                        }
                        else
                        {
                            check = true;
                            selection = number1;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{number1} n'est pas un entier");
                        Console.WriteLine("Erreur: Saisie invalide !");

                        check = false;
                    }

                } while (check == false);
            }
            else
            {
                Console.WriteLine("1 - Déconnexion");
                Console.WriteLine("2 - Chat\n");

                do
                {
                    Console.Write("Votre choix: ");
                    var choix = Console.ReadLine();

                    if (int.TryParse(choix, out int number1))
                    {
                        Console.WriteLine($"{number1} est un entier");

                        if ((number1 < 1) || (number1 > 2))
                        {
                            Console.WriteLine("Erreur: Saisie invalide !");
                            check = false;
                        }
                        else
                        {
                            check = true;
                            selection = number1;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{number1} n'est pas un entier");
                        Console.WriteLine("Erreur: Saisie invalide !");

                        check = false;
                    }

                } while (check == false);
            }

            return selection;
        }

        public void addUser()
        {
            bool password_check = false;
            string password1, password2, loginID;

            Console.WriteLine("*****************************");
            Console.WriteLine("** INSCRIPTION UTILISATEUR **");
            Console.WriteLine("*****************************\n");

            Console.Write(">>> Définissez votre nom d'utilisateur: ");
            loginID = Console.ReadLine();

            do
            {
                Console.Write(">>> Définissez votre mot de passe: ");
                password1 = this.readPassword();

                Console.Write(">>> Confirmez votre mot de passe: ");
                password2 = this.readPassword();

                if (password1 != password2)
                {
                    Console.WriteLine("-> ERREUR: Les 2 mots de passe sont différents, recommencez !");
                    Console.WriteLine("");

                    password_check = false;
                }
                else
                {
                    password_check = true;
                }

            } while (password_check == false);

            Console.WriteLine(">>> Utilisateur crée -> Identifiant: " + loginID + ", mot de passe: " + password1);

            save(path, loginID, password1);
        }

        public void removeUser(string login)
        {
            Console.WriteLine(">>> Vous êtes déconnecté.");
            ListeUtilisateurs.Remove(login);
            connected = false;
        }

        public void authentify()
        {
            Console.WriteLine("******************************");
            Console.WriteLine("** AUTHENTIFICATION REQUISE **");
            Console.WriteLine("******************************\n");

            Console.Write(">>> Nom d'utilisateur: ");
            this.login = Console.ReadLine();


            Console.Write(">>> Mot de passe: ");
            this.password = this.readPassword();

            if (checkIdentifiers() == true)
            {
                connected = true;
            }
            else
            {
                connected = false;
            }
        }

        // Registration
        private void save(string path, string login, string password)
        {
            try
            {
                bool user_exists = false;
                string line = "";

                // Check if the user already exists
                using (StreamReader sr = new StreamReader(path))
                {
                    int found = 0;

                    while ((line = sr.ReadLine()) != null)
                    {
                        found = line.IndexOf(":"); // Extract point (login:password)

                        // Check if the user was already saved
                        if ((line.Substring(0, found) == login))
                        {
                            user_exists = true;
                        }
                    }
                }

                if (user_exists == false)
                {
                    // Writing the user information into the file
                    using (StreamWriter sw = new StreamWriter(path, append: true))
                    {
                        sw.WriteLine(login + ":" + password);
                        ListeUtilisateurs.Add(login, password);
                    }

                    // Checking if user information was saved successfully
                    using (StreamReader sr = new StreamReader(path))
                    {
                        int found = 0;

                        while ((line = sr.ReadLine()) != null)
                        {
                            found = line.IndexOf(":"); // Extract point (login:password)

                            // Check if the user was saved
                            if ((line.Substring(0, found) == login) && (line.Substring(found + 1) == password))
                            {
                                Console.WriteLine(">>> Votre pseudo " + login + " a été sauvegardé avec succès.");
                            }
                            else
                            {
                                Console.WriteLine(">>> ERREUR: Échec de la sauvegarde du pseudo.");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine(">>> ERREUR: L'utilisateur " + login + " existe déjà !");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Le fichier n'a pas pu être ouvert: ");
                Console.WriteLine(e.Message);
            }
        }

        private bool checkIdentifiers()
        {
            try
            {
                /*
                string correctLogin = "";
                string correctPassword = "";
                bool user_exists = false;
                string line = "";

                // Check if the user already exists
                using (StreamReader sr = new StreamReader(path))
                {
                    int found = 0;

                    while ((line = sr.ReadLine()) != null)
                    {
                        found = line.IndexOf(":"); // Extract point (login:password)

                        // Check if the user was already saved
                        if ((line.Substring(0, found) == login))
                        {
                            user_exists = true;
                            correctLogin = line.Substring(0, found);
                            correctPassword = line.Substring(found + 1);
                        }
                    }

                    if (user_exists == true)
                    {
                        // Check password
                        if (password == correctPassword)
                        {
                            Console.WriteLine(">>> Bienvenue " + correctLogin);
                        }
                        else
                        {
                            throw new UserWrongPasswordException();
                        }
                    }
                    else
                    {
                        throw new UserUnknownException(login);
                    }
                } */

                // Check user
                if (ListeUtilisateurs.ContainsKey(login))
                {
                    // Check password
                    if (ListeUtilisateurs.ContainsValue(password))
                    {
                        Console.WriteLine(">>> Bienvenue " + login);
                        Console.ReadLine();
                    }
                    else
                    {
                        throw new UserWrongPasswordException();
                    }
                }
                else
                {
                    throw new UserUnknownException(login);
                }
            }
            catch (UserWrongPasswordException ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
            catch (UserUnknownException ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }

            return true;
        }

        // Hidden chars while entering password: first security method of project
        private string readPassword()
        {
            string password_string = "";
            ConsoleKeyInfo info = Console.ReadKey(true);

            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*"); // Hidden char
                    password_string += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password_string))
                    {
                        // remove one character from the list of password characters
                        password_string = password_string.Substring(0, password_string.Length - 1);
                        // get the location of the cursor
                        int pos = Console.CursorLeft;
                        // move the cursor to the left by one character
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        // replace it with space
                        Console.Write(" ");
                        // move the cursor to the left by one character again
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }
            // add a new line because user pressed enter at the end of their password
            Console.WriteLine();

            return password_string;
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
                        ListeUtilisateurs.Add(line.Substring(0, found), line.Substring(found + 1));
                        users++;
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
    }
}