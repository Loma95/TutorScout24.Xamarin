using MvvmNano;
using TutorScout24.Utils;
using Xamarin.Forms;

namespace TutorScout24.ViewModels
{
    public class ProfileViewModel : MvvmNanoViewModel,IThemeable
    {


        public ProfileViewModel(){
            _themeColor = (Xamarin.Forms.Color)Application.Current.Resources["MainColor"];
        }

        private Color _themeColor;
        public Color ThemeColor { get { return _themeColor; } set { _themeColor = value; NotifyPropertyChanged("ThemeColor"); } }
    }
}
