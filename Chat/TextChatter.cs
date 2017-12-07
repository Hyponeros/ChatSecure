﻿using System;
using System.Collections.Generic;
using System.Text;


namespace Chat
{
    [Serializable]
    public class TextChatter : Chatter
    {
        private string _alias;
        public string Alias { get { return _alias; } set { _alias = value; } }

        public TextChatter(string alias)
        {
            _alias = alias;
        }

        
        public bool Equals(Chatter chatter)
        {
            if(_alias == chatter.getAlias())
            {
                return true;
            }
            return false;
        }
        
        public string getAlias()
        {
            return _alias;
        }

        public void receiveAMessage(string msg, Chatter chatter)
        {
            //ToDo afficher le message dans la GUI
            Console.WriteLine("(At " + this._alias + ") : " + chatter.getAlias() + " $> " + msg);
        }

        public override string ToString()
        {
            return _alias;
        }
    }
}