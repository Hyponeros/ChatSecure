using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Communication
{
    public class Net
    {
        // Envoi du message avec sérialisation au format binaire
        public static void SendUser(Stream s, User user)
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(s, user);
        }

        // Réception du message avec déserialisation
        public static User ReceiveUser(Stream s)
        {
            BinaryFormatter bf = new BinaryFormatter();
            return (User)bf.Deserialize(s);
        }

        public static void SendUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}

