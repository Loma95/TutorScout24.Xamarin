using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmNano;
using TutorScout24.Models;
using TutorScout24.Services;
using TutorScout24.Utils;
using Xamarin.Forms;

namespace TutorScout24.ViewModels
{
    public class ProfileViewModel : MvvmNanoViewModel,IThemeable
    {


        public ProfileViewModel(){
            _themeColor = (Xamarin.Forms.Color)Application.Current.Resources["MainColor"];

            GetMyUserInfo();

            CredentialService CService = MvvmNanoIoC.Resolve<CredentialService>();
           
            PasswordWasSaved = CService.DoCredentialsExist();
            Debug.WriteLine(_passwordWasSaved);
        }

        public ICommand RemovePassword => new Command(RemovePass);

        private void RemovePass()
        {
            CredentialService CService = MvvmNanoIoC.Resolve<CredentialService>();
            CService.DeleteCredentials();
            PasswordWasSaved = false;
        }

        private bool _passwordWasSaved;
        public bool PasswordWasSaved
        {
            get { return _passwordWasSaved; }
            set { _passwordWasSaved = value;
                NotifyPropertyChanged("PasswordWasSaved");}
        }

        private UserInfo _userInfo;
        public UserInfo UserInfo
        {
            get { return _userInfo; }
            set { _userInfo = value; 
                NotifyPropertyChanged("UserInfo");
            }
        }

        private async void GetMyUserInfo(){
                  
            UserInfo = await MvvmNanoIoC.Resolve<TutorScoutRestService>().GetMyUserInfo();
          
        }

        private Color _themeColor;
        public Color ThemeColor { get { return _themeColor; } set { _themeColor = value; NotifyPropertyChanged("ThemeColor"); } }
    }
}
