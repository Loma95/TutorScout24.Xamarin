using System;
using System.Threading.Tasks;

namespace TutorScout24.Utils
{
    /// <summary>
    /// get rid of the double click problem in xamarin credits to http://www.c-sharpcorner.com/blogs/workaround-to-prevent-rapid-clicks-on-a-button-in-xamarin-forms
    /// </summary>
    public class SingleClick
    {
        bool hasClicked;
        Action<object, EventArgs> _setClick;
        public SingleClick(Action<object, EventArgs> setClick)
        {
            _setClick = setClick;
        }

        public void Click(object s, EventArgs e)
        {
            if (!hasClicked)
            {
                _setClick(s, e);
                hasClicked = true;
            }
            Reset();
        }
        async void Reset()
        {
            await Task.Delay(100);
            await Task.Run(new Action(() => hasClicked = false));
        }
    }
}
