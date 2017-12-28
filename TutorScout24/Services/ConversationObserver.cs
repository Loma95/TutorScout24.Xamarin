using System;
using TutorScout24.Models.Chat;

namespace TutorScout24.Services
{
    /// <summary>
    /// Interface  to make a single Conversation observable
    /// </summary>
    public interface ConversationObserver:IObserver<Conversation>
    {
        string ConversationId { get;  }
    }
}
