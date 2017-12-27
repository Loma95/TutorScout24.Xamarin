namespace TutorScout24.Models.Chat
{
    public class DeleteMessage : RestCommandWithAuthentication
    {
        public int messageId { get; set; }
    }
}