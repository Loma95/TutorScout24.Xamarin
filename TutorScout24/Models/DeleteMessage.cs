using System;
namespace TutorScout24.Models
{
    public class DeleteMessage:RestCommandWithAuthentication
    {
        public DeleteMessage()
        {
            
        }

        public int messageId { get; set; }
    }
}
