using System;
using Messanger.Models;

namespace Messanger
{
    public class Message
    {
        public int id;
        public string text;
        public DateTime time;
        public int userSend;
        public int userTake;

        public Message(int id,string text,DateTime time,int userSend,int userTake)
        {
            this.id = id;
            this.text = text;
            this.time = time;
            this.userSend = userSend;
            this.userTake = userTake;
        }
    }
}