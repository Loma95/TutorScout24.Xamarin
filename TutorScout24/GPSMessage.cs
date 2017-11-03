using System;
namespace TutorScout24
{
    public class GPSMessage:MvvmNano.IMessage
    {
        public string Text
        {
            get;
            set;
        }

       
        public GPSMessage(string text)
        {
            this.Text = text;

        }
    }
}
