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
using System.Threading;
using System.Collections.Generic;
using Communication;

namespace Client
{
    public class Sender
    {
        private TcpClient comm;
        private List<TcpClient> comm_list;

        public Sender(TcpClient s, List<TcpClient> cl)
        {
            comm = s;
            comm_list = cl;
        }


    }
}
