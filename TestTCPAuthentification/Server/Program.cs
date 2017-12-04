using System;
using Communication;

namespace Server
{
    class Program
    {
        public static void Main(string[] args)
        {
            DateTime localDate = DateTime.Now;
            Console.WriteLine("> Initialisation du serveur (" + localDate.ToString() + ")");
            Console.WriteLine("Serveur d'authentification d'utilisateurs...");

            Server server = new Server(8976);
            server.StartServer();
        }
    }
}
