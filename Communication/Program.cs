﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication
{
    class Program
    {
        static void Main(string[] args)
        {
            Message m = new TextMessage("arthur", "salut");
            Console.WriteLine(m);
            Console.WriteLine(new Net().GetType());
            Console.Read();
        }
    }
}