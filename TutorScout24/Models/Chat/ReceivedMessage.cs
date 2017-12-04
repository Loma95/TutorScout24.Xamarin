using System;
namespace TutorScout24.Models.Chat
{
    public class ReceivedMessage:Message
    {
        public ReceivedMessage()
        {
        }

        public override string MyTypeName { get => this.GetType().Name; }
    }
}
