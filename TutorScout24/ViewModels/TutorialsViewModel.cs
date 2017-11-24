using MvvmNano;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorScout24.Utils;
using Xamarin.Forms;

namespace TutorScout24.ViewModels
{
    public class TutorialsViewModel : MvvmNanoViewModel,IThemeable
    {

        public TutorialsViewModel(){
            _themeColor = (Xamarin.Forms.Color)Application.Current.Resources["MainColor"];
        }


        private Color _themeColor;
        public Color ThemeColor { get { return _themeColor; } set { _themeColor = value; NotifyPropertyChanged("ThemeColor"); } }
    }
}
