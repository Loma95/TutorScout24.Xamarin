using MvvmNano;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TutorScout24.CustomData;
using TutorScout24.Models;
using TutorScout24.Services;
using TutorScout24.Utils;
using Xamarin.Forms;

namespace TutorScout24.ViewModels
{
    public class TutorialsViewModel : MvvmNanoViewModel,IThemeable
    {

        public TutorialsViewModel()
        {
            _themeColor = (Xamarin.Forms.Color)Application.Current.Resources["MainColor"];
            SetTutorings();
            MvvmNanoIoC.Resolve<IMessenger>().Subscribe(this, (object arg1, ChangeModeMessage arg2) =>
            {
                SetTutorings();
            });

        }

        private async void SetTutorings()
        {
            Tutorings = await MvvmNanoIoC.Resolve<TutorScoutRestService>().GetMyTutorings();
        }

        private List<MyTutoring> _tutorings;

        public List<MyTutoring> Tutorings
        {
            get { return _tutorings; }
            set
            {
                _tutorings = value;
                NotifyPropertyChanged();
            }
        }



        public ICommand FabCommand
        {
            get { return new Command(Fab); }

        }
        private void Fab()
        {
            //NavigateTo<ForeignProfileViewModel, string>("RobertAndroid");
            NavigateTo<CreateViewModel, CreateTutoring>(new CreateTutoring());
            Debug.WriteLine("Command");
        }


        private Color _themeColor;
        public Color ThemeColor { get { return _themeColor; } set { _themeColor = value; NotifyPropertyChanged("ThemeColor"); } }
    }
}
