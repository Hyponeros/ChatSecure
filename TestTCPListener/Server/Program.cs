using System;
using System.Text;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace Server
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Serveur ChatBox");

            DateTime localDate = DateTime.Now;
            Console.WriteLine(localDate.ToString());

            Server serv = new Server(8976);
            serv.startServer();
        }
    }
}
