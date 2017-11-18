using System;
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
            cService = MvvmNanoIoC.Resolve<ICredentialService>();
            if (cService.DoCredentialsExist())
            {
                SetUpMainPage<MasterDetailViewModel>();
            }
        }
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
            SetUpMainPage<MasterDetailViewModel>();
        }

        private void SetUpMainPage<TViewModel>()where TViewModel : MvvmNanoViewModel
        {
            var viewModel = MvvmNanoIoC.Resolve<TViewModel>();
            var view = MvvmNanoIoC.Resolve<IPresenter>().CreateViewFor<TViewModel>();
            view.SetViewModel(viewModel);
            Application.Current.MainPage = (Xamarin.Forms.Page)view;
        }


    }
}
