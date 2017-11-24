using System;
using System.Text;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace Client
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Client c1 = new Client("127.0.0.1", 8976);
            c1.startClient();
        }
    }
}
