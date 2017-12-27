namespace TutorScout24.Models.Chat
{
    public class SendMessage : RestCommandWithAuthentication
    {
        public string toUserId { get; set; }
        public string text { get; set; }
    }
}