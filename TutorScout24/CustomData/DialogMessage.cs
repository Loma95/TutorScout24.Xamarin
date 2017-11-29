using System;
namespace TutorScout24
{
    public class DialogMessage:MvvmNano.IMessage
    {
        public string Text
        {
            get;
            set;
        }
        public string Header
        {
            get;
            set;
        }
       
        public DialogMessage(string header,string text)
        {
            this.Text = text;
            this.Header = header;
        }
    }
}
