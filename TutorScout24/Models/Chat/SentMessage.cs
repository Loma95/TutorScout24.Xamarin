using System;
namespace TutorScout24.Models.Chat
{
    public class SentMessage:Message
    {
        public SentMessage()
        {
        }

        public override string MyTypeName { get => this.GetType().Name; }
    }
}
