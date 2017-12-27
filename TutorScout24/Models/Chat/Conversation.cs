using System.Collections.Generic;

namespace TutorScout24.Models.Chat
{
    public class Conversation
    {
        public string id { get; set; }

        public List<Message> Messages { get; set; } = new List<Message>();
    }
}