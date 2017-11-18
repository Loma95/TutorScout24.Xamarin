using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmNano;
using TutorScout24.Services;
using Xamarin.Auth;
using Xamarin.Forms;

namespace TutorScout24.ViewModels
{
    public class LoginViewModel:MvvmNano.MvvmNanoViewModel
    {
        ICredentialService cService;
        public LoginViewModel()
        {
          
        }

        public override void Initialize()
        {
            base.Initialize();

            InitView<MasterDetailViewModel>();
            cService = new CredentialService();
            if (cService.DoCredentialsExist())
            {

                //Application.Current.MainPage = (Xamarin.Forms.Page)_view;
            }

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

        public ICommand LoginCommand => new Command(Login);

        private  void Login()
        {
            cService.SaveCredentials(UserName,Password);
            Application.Current.MainPage = (Xamarin.Forms.Page)_view;
        }

        private void InitView<TViewModel>()where TViewModel : MvvmNanoViewModel
        {
            var viewModel = MvvmNanoIoC.Resolve<TViewModel>();
            _view = MvvmNanoIoC.Resolve<IPresenter>().CreateViewFor<TViewModel>();
            _view.SetViewModel(viewModel);

        }


    }
}
