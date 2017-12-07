using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication
{
    public class IOException : AuthentificationException
    {
        public IOException(string path) : base(path, "Erreur lors de la lecture ou l'écriture de " + path)
        {
        }
    }
}
