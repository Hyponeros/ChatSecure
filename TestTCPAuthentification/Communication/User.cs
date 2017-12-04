using System;

namespace Communication
{
    [Serializable]
    public class User
    {
        private string login;
        private string password;
        private int error;
        private bool connected;

        public string _login
        {
            get
            {
                return login;
            }
            set
            {
                login = value;
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
                password = value;
            }
        }

        /* 0 = identifiants corrects
         * 1 = mot de passe incorrect
         * 2 = identifiant inconnu */

        public int _error
        {
            get
            {
                return error;
            }
            set
            {
                error = value;
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

        public User()
        {
            error = 0;
        }

        public User(string login, string password)
        {
            this.login = login;
            this.password = password;
            error = 0;
        }

        public override bool Equals(object obj)
        {
            var item = obj as User;

            if (item == null)
            {
                return false;
            }

            if ((this.login == item._login) || (this.password == item._password))
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("[User: _login={0}, _password={1}]", _login, _password);
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
                        // Console.WriteLine($"{number1} est un entier");

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
                        // Console.WriteLine($"{number1} n'est pas un entier");
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
                        // Console.WriteLine($"{number1} est un entier");

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
                        // Console.WriteLine($"{number1} n'est pas un entier");
                        Console.WriteLine("Erreur: Saisie invalide !");

                        check = false;
                    }

                } while (check == false);
            }

            return selection;
        }
    }
}
