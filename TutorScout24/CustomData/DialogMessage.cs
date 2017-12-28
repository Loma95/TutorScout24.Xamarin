using MvvmNano;

namespace TutorScout24.CustomData
{
    /// <summary>
    /// Data Object for displaying DialogMessages within MasterDetailPage.
    /// </summary>
    public class DialogMessage : IMessage
    {
        public DialogMessage(string header, string text)
        {
            Text = text;
            Header = header;
        }

        public string Text { get; set; }

        public string Header { get; set; }
    }
}