using System;

using Xamarin.Forms;

namespace TutorScout24.Controls
{
    public class MySwitch: ToolbarItem
    {
        public MySwitch()
        {
            OnImage = new FileImageSource();
            OffImage = new FileImageSource();
    

            this.Icon = OffImage;
            this.Clicked += (o, i) =>
            {
                Toggle();
            };
        }

        public void Toggle()
        {
            IsOn = !IsOn;
        }

        public FileImageSource OnImage
        {
            get;
            set;
        }

        public FileImageSource OffImage
        {
            get;
            set;
        }
        public Boolean IsOn
        {
            get { return IsOn; }
            set
            {
                IsOn = value;
                if (IsOn)
                {
                    this.Icon = OnImage;
                }
                else
                {
                    this.Icon = OffImage;
                }
            }
        }
    }
}

