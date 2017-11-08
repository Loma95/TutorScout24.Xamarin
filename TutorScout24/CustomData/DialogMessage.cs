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

       
        public DialogMessage(string text)
        {
            this.Text = text;

        }
    }
}
