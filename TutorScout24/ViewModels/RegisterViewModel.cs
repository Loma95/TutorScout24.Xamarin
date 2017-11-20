using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace TutorScout24.ViewModels
{
    public class RegisterViewModel: MvvmNano.MvvmNanoViewModel
    {
        public RegisterViewModel()
        {
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

        private string _lastName;
        public string LastName
        {
            get { return _userName; }
            set
            {
                _lastName = value;
                NotifyPropertyChanged("LastName");
            }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _password; }
            set
            {
                _firstName = value;
                NotifyPropertyChanged("FirstName");
            }
        }
        private string _email;
        public string Email
        {
            get { return _password; }
            set
            {
                _email = value;
                NotifyPropertyChanged("Email");
            }
        }

        public ICommand StartCommand => new Command(Start);

        private void Start()
        {
            
        }


    }
}
