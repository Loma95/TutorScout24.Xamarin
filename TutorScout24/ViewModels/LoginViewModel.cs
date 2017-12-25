using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmNano;
using TutorScout24.Models;
using TutorScout24.Services;
using TutorScout24.Utils;
using Xamarin.Auth;
using Xamarin.Forms;
using System.Net.Http;

namespace TutorScout24.ViewModels
{
    public class LoginViewModel : MvvmNano.MvvmNanoViewModel
    {
        CredentialService CService;
        public LoginViewModel()
        {

        }

        public override void Initialize()
        {
            base.Initialize();

            CService = MvvmNanoIoC.Resolve<CredentialService>();

            Info = IsNotConnected();
            InfoText = "Keine Verbindung zum Internet";
        }

        private bool IsNotConnected()
        {

            return !System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();

        }

        private bool _info;
        public bool Info
        {
            get { return _info; }
            set { _info = value; }
        }

        private string _infoText;
        public string InfoText
        {
            get { return _infoText; }
            set { _infoText = value; }
        }

        private bool _passwordShouldBeSaved;
        public bool PasswordShouldBeSaved
        {
            get { return _passwordShouldBeSaved; }
            set { _passwordShouldBeSaved = value; }
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

        public ICommand LoginCommand => new Command(async () => LoginAsync());

        private async Task LoginAsync()
        {



            CheckAuthentication auth = new CheckAuthentication();
            auth.authentication = new Authentication();
            auth.authentication.password = Password;
            auth.authentication.userName = UserName;

            bool result = await IsValidAuthentication(auth);
            if (result)
            {
                if (PasswordShouldBeSaved)
                {
                    CService.SaveCredentials(UserName, Password);
                }
                MvvmNanoIoC.RegisterAsSingleton<Authentication>(auth.authentication);
                NavigateTo<MasterDetailViewModel>();
            }
            else
            {
                Info = true;
                InfoText = "Passwort ist nicht korrekt";
                Debug.WriteLine("Not authenticated");
                NotifyPropertyChanged("Info");
                NotifyPropertyChanged("InfoText");
            }

        }

        public ICommand SignUpCommand => new Command(SignUp);

        private void SignUp()
        {
            NavigateTo<RegisterViewModel>();
        }


        private async Task<bool> IsValidAuthentication(CheckAuthentication auth)
        {
            return await MvvmNanoIoC.Resolve<TutorScoutRestService>().CanAuthenticate(auth);

        }



    }
}
