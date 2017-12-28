using System;

namespace TutorScout24.Models.Chat
{
    public class RestMessage
    {
        public int messageId { get; set; }
        public DateTime datetime { get; set; }
        public string fromUserId { get; set; }
        public string toUserId { get; set; }
        public string text { get; set; }
    }
}