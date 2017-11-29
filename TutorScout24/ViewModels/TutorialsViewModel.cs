using MvvmNano;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TutorScout24.Utils;
using Xamarin.Forms;

namespace TutorScout24.ViewModels
{
    public class TutorialsViewModel : MvvmNanoViewModel,IThemeable
    {

        public TutorialsViewModel()
        {
            _themeColor = (Xamarin.Forms.Color)Application.Current.Resources["MainColor"];
    
        }


        public ICommand FabCommand
        {
            get { return new Command(Fab); }

        }
        private void Fab()
        {
            NavigateTo<CreateViewModel>();
            Debug.WriteLine("Command");
        }


        private Color _themeColor;
        public Color ThemeColor { get { return _themeColor; } set { _themeColor = value; NotifyPropertyChanged("ThemeColor"); } }
    }
}
