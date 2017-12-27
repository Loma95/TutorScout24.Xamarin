using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmNano;
using TutorScout24.Models.UserData;
using TutorScout24.Services;
using Xamarin.Forms;

namespace TutorScout24.ViewModels
{
    public class LoginViewModel : MvvmNanoViewModel
    {
        private string _password;

        private string _userName;
        private CredentialService CService;

        public bool Info { get; set; }

        public string InfoText { get; set; }

        public bool PasswordShouldBeSaved { get; set; }

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

        public ICommand LoginCommand => new Command(async () => LoginAsync());

        public ICommand SignUpCommand => new Command(SignUp);

        public override void Initialize()
        {
            base.Initialize();

            CService = MvvmNanoIoC.Resolve<CredentialService>();

            Info = IsNotConnected();
            InfoText = "Keine Verbindung zum Internet";
        }

        private bool IsNotConnected()
        {
            return !NetworkInterface.GetIsNetworkAvailable();
        }

        private async Task LoginAsync()
        {
            var auth = new CheckAuthentication();
            auth.authentication = new Authentication();
            auth.authentication.password = Password;
            auth.authentication.userName = UserName;

            var result = await IsValidAuthentication(auth);
            if (result)
            {
                if (PasswordShouldBeSaved)
                    CService.SaveCredentials(UserName, Password);
                MvvmNanoIoC.RegisterAsSingleton(auth.authentication);
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