using System;
using TutorScout24.Models.Chat;

namespace TutorScout24.Services
{
    public interface ConversationObserver:IObserver<Conversation>
    {
        string ConversationId { get;  }
    }
}
