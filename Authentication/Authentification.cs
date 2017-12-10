using System;
using System.Text;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Web;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Authentication
{
    [Serializable]
    public class Authentification : AuthentificationManager
    {

        private Dictionary<string, User> Users { get; set; }

        public Authentification()
        {
            Users = new Dictionary<string, User>();
            new Dictionary<string, string>();
        }

        public void addUser(string login, string password) 
        {
            if (Users.Keys.Contains(login))
            {
                throw new UserExistException(login);
            } else
            {
                Users.Add(login, new User(login, password));
            }

        }

        public void authentify(string login, string password)
        {
            if (!Users.Keys.Contains(login))
            {
                throw new UserUnknownException(login);
            } else if (!Users[login].Password.Equals(password))
            {
                throw new UserWrongPasswordException(login);
            }
        }

        public void removeUser(string login)
        {
            
            if (!Users.Keys.Contains(login))
            {
                throw new UserUnknownException(login);
            }
            else
            {
                Users.Remove(login);
            }
        }

        //Return an initialised Authentification Object, or an empty one if no file can be read
        public static AuthentificationManager load(String path)
        {
            try { 
                AuthentificationManager am = new Authentification();

                BinaryFormatter bf = new BinaryFormatter();

                Stream stream = new FileStream(path , FileMode.Open, FileAccess.Read, FileShare.None);

                am = (AuthentificationManager) bf.Deserialize(stream);

                stream.Close();

                return am;

            } catch(System.IO.IOException e) {
                Console.WriteLine(e);
                return new Authentification();
            }
        }

        public void save(string path)
        {
            BinaryFormatter bf = new BinaryFormatter();

            Stream stream = new FileStream(path , FileMode.Create, FileAccess.Write, FileShare.None);


            bf.Serialize(stream, this);

            stream.Close();
        }
    }
}