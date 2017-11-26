using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using MvvmNano;
using TutorScout24.Models;
using TutorScout24.Pages;
using TutorScout24.Services;
using TutorScout24.Utils;
using Xamarin.Forms;

namespace TutorScout24.ViewModels
{
    public enum Genders { Männlich, Weiblich };
    
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

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                NotifyPropertyChanged("FirstName");
            }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                NotifyPropertyChanged("LastName");
            }
        }

        private string _age;
        public string Age
        {
            get { return _age; }
            set
            {
                _age = value;
                NotifyPropertyChanged("Age");
            }
        }

        private Genders _selectedGender ;
        public Genders SelectedGender
        {
            get { return _selectedGender; }
            set
            {
                _selectedGender = value;
                NotifyPropertyChanged("SelectedGender");
            }
        }


        private string _placeOfResidence;
        public string PlaceOfResidence
        {
            get { return _placeOfResidence; }
            set
            {
                _placeOfResidence = value;
                NotifyPropertyChanged("PlaceOfResidence");
            }
        }

        private string _graduation;
        public string Graduation
        {
            get { return _graduation; }
            set
            {
                _graduation = value;
                NotifyPropertyChanged("Graduation");
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                NotifyPropertyChanged("Description");
            }
        }

        public List<string> Gender
        {
            get
            {
                return Enum.GetNames(typeof(Genders)).Select(b => b).ToList();
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
            usr.gender = SelectedGender.ToString();
            usr.firstName = FirstName;
            usr.lastName = LastName;
            usr.age = int.Parse(Age);
            usr.maxGraduation = Graduation;
            usr.note = Description;
            usr.placeOfResidence = PlaceOfResidence;
            usr.email = Email;
            usr.password = Password;
            usr.userName = UserName;
            bool Succeeded = await MvvmNanoIoC.Resolve<TutorScoutRestService>().CreateUser(usr);

            if(Succeeded){
                NavigateTo<LoginViewModel>();
            }else{
                MvvmNanoIoC.Resolve<IMessenger>().Send(new DialogMessage("Das hat leider nicht geklappt Lol"));

            }
     
        }


        private Color _themeColor;
        public Color ThemeColor { get { return _themeColor; } set { _themeColor = value; NotifyPropertyChanged("ThemeColor"); } }

    }
}
