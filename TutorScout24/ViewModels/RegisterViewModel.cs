using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using MvvmNano;
using TutorScout24.Models;
using TutorScout24.Pages;
using TutorScout24.Services;
using TutorScout24.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace TutorScout24.ViewModels
{
    public enum Genders { Maennlich, Weiblich };
    
    public class RegisterViewModel: MvvmNano.MvvmNanoViewModel,IThemeable
    {
        User _usr = new User();
        public RegisterViewModel()
        {
           
            _themeColor = (Xamarin.Forms.Color)Application.Current.Resources["MainColor"];
           
        }

        private bool _isNotValid;
        public bool IsNotValid
        {
            get { return _isNotValid; }
            set
            {
                _isNotValid = value;
                NotifyPropertyChanged("IsNotValid");
            }
        }

        private string _errorText;
        public string ErrorText
        {
            get { return _errorText; }
            set
            {
                _errorText = value;
                if (value != "")
                {
                    IsNotValid = true;
                }else{
                    IsNotValid = false;
                }
                NotifyPropertyChanged("ErrorText");
            }
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

        private DateTime _birthdate ;
        public DateTime BirthDate
        {
            get { return _birthdate; }
            set
            {
                _birthdate = value;
                NotifyPropertyChanged("BirthDate");
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

      
            

            Type type = this.GetType();
            var properties = type.GetProperties();

            List<string> NotNullProperties = new List<string>() { "FirstName", "LastName", "Age", "Graduation", "PlaceOfResidence", "Description", "UserName" };

            foreach (PropertyInfo property in properties)
            {
                if (NotNullProperties.Contains(property.Name))
                {
                    if(!InputValidator.IsNotEmpty((string)property.GetValue(this, null))){
                        ErrorText = property.Name + " darf nicht leer sein!";
                        return;
                    }
                }
               
            }

            if(!InputValidator.IsNotEmpty(SelectedGender.ToString())){
                ErrorText = "Bitte wählen Sie ein Geschlecht aus";
                return;
            }
            if(!InputValidator.IsValidEmail(Email)){
                ErrorText = "Email nicht valide";
                return;
            }
            if (!InputValidator.IsValidPassword(Password)){
                ErrorText = "Passwort nicht valide";
                return;
            }

            ErrorText = "";

            CreateUser();
            
        }


        private async void CreateUser()
        {
              User usr = new User();
              usr.gender = SelectedGender.ToString();
              usr.firstName = FirstName;
              usr.lastName = LastName;

              usr.birthdate = BirthDate.Year.ToString() + BirthDate.Month.ToString("D2") + BirthDate.Day.ToString("D2");
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
                Debug.WriteLine("send");
                MvvmNanoIoC.Resolve<IMessenger>().Send(new DialogMessage("Fehler","Benutzer oder Email existiert schon"));

            }
     
        }


        private Color _themeColor;
        public Color ThemeColor { get { return _themeColor; } set { _themeColor = value; NotifyPropertyChanged("ThemeColor"); } }

    }
}
