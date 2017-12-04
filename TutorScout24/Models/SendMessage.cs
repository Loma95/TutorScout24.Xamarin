using System;
namespace TutorScout24.Models
{
    public class SendMessage:RestCommandWithAuthentication
    {
        public string toUserId { get; set; }
        public string text { get; set; }
    }
}
