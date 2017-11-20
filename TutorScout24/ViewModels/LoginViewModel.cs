using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmNano;
using TutorScout24.Models;
using TutorScout24.Services;
using Xamarin.Auth;
using Xamarin.Forms;

namespace TutorScout24.ViewModels
{
    public class LoginViewModel:MvvmNano.MvvmNanoViewModel
    {
        CredentialService CService;
        public LoginViewModel()
        {
          
        }

        public override void Initialize()
        {
            base.Initialize();

            InitView<MasterDetailViewModel>();

            CService = MvvmNanoIoC.Resolve<CredentialService>();
          

        }

        private IView _view;

        private string _userName;
        public string UserName
        {
            get{ return _userName; }
            set { _userName = value;
                NotifyPropertyChanged("UserName");
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value;
                NotifyPropertyChanged("Password");
            }
        }

        public ICommand LoginCommand => new Command(async () => Login());

        private async void Login()
        {
            CheckAuthentication auth = new CheckAuthentication();
            auth.authentication = new Authentication();
            auth.authentication.password = Password;
            auth.authentication.userName = UserName;

            bool result = await IsValidAuthentication(auth);
            if(result){
                CService.SaveCredentials(UserName, Password);
                Application.Current.MainPage = (Xamarin.Forms.Page)_view;
            }else{
                Debug.WriteLine("Not authenticated");
            }
           
        }

        public ICommand SignUpCommand => new Command(SignUp);

        private void SignUp()
        {
            InitView<RegisterViewModel>();
            Application.Current.MainPage = (Xamarin.Forms.Page)_view;
        }

        private void InitView<TViewModel>()where TViewModel : MvvmNanoViewModel
        {
            var viewModel = MvvmNanoIoC.Resolve<TViewModel>();
            _view = MvvmNanoIoC.Resolve<IPresenter>().CreateViewFor<TViewModel>();
            _view.SetViewModel(viewModel);

        }

        private async Task<bool> IsValidAuthentication(CheckAuthentication auth)
        {
            return await MvvmNanoIoC.Resolve<TutorScoutRestService>().CanAuthenticate(auth);

        }



    }
}
