using System;
using System.Collections.Generic;

namespace TutorScout24.Models.Chat
{
    public class Conversation
    {

        public string id
        {
            get;
            set;
        }

        private List<Message> _messages = new List<Message>();
        public List<Message> Messages 
        {
            get { return _messages; }
            set { _messages = value; }
        }


       
    }
}
