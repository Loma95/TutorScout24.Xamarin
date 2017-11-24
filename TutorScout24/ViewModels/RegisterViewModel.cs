using System;
using System.Windows.Input;
using MvvmNano;
using TutorScout24.Models;
using TutorScout24.Pages;
using TutorScout24.Services;
using TutorScout24.Utils;
using Xamarin.Forms;

namespace TutorScout24.ViewModels
{
    public class RegisterViewModel: MvvmNano.MvvmNanoViewModel,IThemeable
    {
        User _usr = new User();
        public RegisterViewModel()
        {
           
            _themeColor = (Xamarin.Forms.Color)Application.Current.Resources["MainColor"];
           
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                NotifyPropertyChanged("UserName");
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyPropertyChanged("Password");
            }
        }

  
        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                NotifyPropertyChanged("Email");
            }
        }

        public ICommand StartCommand => new Command(Start);

        private void Start()
        {
            /*Check Password and all inputs*/
            CreateUser();
            
        }


        private async void CreateUser()
        {
            User usr = new User();
            usr.gender = "Male";
            usr.firstName = "Max";
            usr.lastName = "Mustermann";
            usr.maxGraduation = "Test";
            usr.note = "hi";
            usr.placeOfResidence = "YoloTown";
            usr.email = _email;
            usr.password = _password;
            usr.userName = _userName;
            bool response = await MvvmNanoIoC.Resolve<TutorScoutRestService>().CreateUser(usr);

            MvvmNanoIoC.Resolve<IMessenger>().Send(new DialogMessage(response.ToString()));
        }


        private Color _themeColor;
        public Color ThemeColor { get { return _themeColor; } set { _themeColor = value; NotifyPropertyChanged("ThemeColor"); } }

    }
}
