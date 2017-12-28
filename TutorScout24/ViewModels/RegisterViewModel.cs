using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using MvvmNano;
using TutorScout24.CustomData;
using TutorScout24.Models.UserData;
using TutorScout24.Services;
using TutorScout24.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace TutorScout24.ViewModels
{
    public enum Genders
    {
        Maennlich,
        Weiblich
    }

    public class RegisterViewModel : MvvmNanoViewModel, IThemeable
    {
        private DateTime _birthdate;

        private string _description;


        private string _email;

        private string _errorText;

        private string _firstName;

        private string _graduation;

        private bool _isNotValid;

        private string _lastName;

        private string _password;


        private string _placeOfResidence;

        private Genders _selectedGender;


        private Color _themeColor;

        private string _userName;
        private User _usr = new User();

        public RegisterViewModel()
        {
            _themeColor = (Color) Application.Current.Resources["MainColor"];
        }

        public bool IsNotValid
        {
            get => _isNotValid;
            set
            {
                _isNotValid = value;
                NotifyPropertyChanged("IsNotValid");
            }
        }

        public string ErrorText
        {
            get => _errorText;
            set
            {
                _errorText = value;
                if (value != "")
                    IsNotValid = true;
                else IsNotValid = false;
                NotifyPropertyChanged("ErrorText");
            }
        }

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                NotifyPropertyChanged("UserName");
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                NotifyPropertyChanged("Password");
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                NotifyPropertyChanged("Email");
            }
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                NotifyPropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                NotifyPropertyChanged("LastName");
            }
        }

        public DateTime BirthDate
        {
            get => _birthdate;
            set
            {
                _birthdate = value;
                NotifyPropertyChanged("BirthDate");
            }
        }

        public Genders SelectedGender
        {
            get => _selectedGender;
            set
            {
                _selectedGender = value;
                NotifyPropertyChanged("SelectedGender");
            }
        }

        public string PlaceOfResidence
        {
            get => _placeOfResidence;
            set
            {
                _placeOfResidence = value;
                NotifyPropertyChanged("PlaceOfResidence");
            }
        }

        public string Graduation
        {
            get => _graduation;
            set
            {
                _graduation = value;
                NotifyPropertyChanged("Graduation");
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                NotifyPropertyChanged("Description");
            }
        }

        public List<string> Gender
        {
            get { return Enum.GetNames(typeof(Genders)).Select(b => b).ToList(); }
        }


        public ICommand StartCommand => new Command(Start);

        public Color ThemeColor
        {
            get => _themeColor;
            set
            {
                _themeColor = value;
                NotifyPropertyChanged("ThemeColor");
            }
        }


        private void Start()
        {
            var type = GetType();
            var properties = type.GetProperties();

            var NotNullProperties = new List<string>
            {
                "FirstName",
                "LastName",
                "Age",
                "Graduation",
                "PlaceOfResidence",
                "Description",
                "UserName"
            };

            foreach (var property in properties)
                if (NotNullProperties.Contains(property.Name))
                    if (!InputValidator.IsNotEmpty((string) property.GetValue(this, null)))
                    {
                        ErrorText = property.Name + " darf nicht leer sein!";
                        return;
                    }

            if (!InputValidator.IsNotEmpty(SelectedGender.ToString()))
            {
                ErrorText = "Bitte wählen Sie ein Geschlecht aus";
                return;
            }
            if (!InputValidator.IsValidEmail(Email))
            {
                ErrorText = "Email nicht valide";
                return;
            }
            if (!InputValidator.IsValidPassword(Password))
            {
                ErrorText = "Passwort nicht valide";
                return;
            }

            ErrorText = "";

            CreateUser();
        }


        private async void CreateUser()
        {
            var usr = new User();
            usr.gender = SelectedGender.ToString();
            usr.firstName = FirstName;
            usr.lastName = LastName;

            usr.birthdate = BirthDate.Year + BirthDate.Month.ToString("D2") + BirthDate.Day.ToString("D2");
            usr.maxGraduation = Graduation;
            usr.note = Description;
            usr.placeOfResidence = PlaceOfResidence;
            usr.email = Email;
            usr.password = Password;
            usr.userName = UserName;
            var _serverMessage = await MvvmNanoIoC.Resolve<TutorScoutRestService>().CreateUser(usr);


            if (_serverMessage == "true") NavigateTo<LoginViewModel>();
            else MvvmNanoIoC.Resolve<IMessenger>().Send(new DialogMessage("Problem", _serverMessage));
        }
    }
}