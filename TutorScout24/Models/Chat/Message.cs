using System;
namespace TutorScout24.Models.Chat
{
    public abstract class Message
    {
        public string ToUser
        {
            get;
            set;
        }

        public string FromUser
        {
            get;
            set;
        }


        public string Text
        {
            get;
            set;
   
        }

        public DateTime Time
        {
            get;
            set;
        }

        public int ID
        {
            get;
            set;
        }

        public abstract string MyTypeName
        {
            get;
        }


    }
}
