using MvvmNano;
using TutorScout24.Models.UserData;
using TutorScout24.Services;
using TutorScout24.Utils;
using Xamarin.Forms;
using MasterDetailPage = TutorScout24.Pages.MasterDetailPage;

namespace TutorScout24.ViewModels
{
    public class ForeignProfileViewModel : MvvmNanoViewModel<string>, IThemeable, IToolBarItem
    {
        private Color _themeColor;


        private UserInfo _userInfo;

        public ForeignProfileViewModel()
        {
            _themeColor = (Color) Application.Current.Resources["MainColor"];
        }

        public UserInfo UserInfo
        {
            get => _userInfo;
            set
            {
                _userInfo = value;


                NotifyPropertyChanged("UserInfo");
            }
        }

        public Color ThemeColor
        {
            get => _themeColor;
            set
            {
                _themeColor = value;

                NotifyPropertyChanged("ThemeColor");
            }
        }

        public void AddToolBarItem()
        {
            var master = (MasterDetailPage) Application.Current.MainPage;
            if (master != null)
                master.ToolbarItems.Clear();
        }

        private async void GetUserInfo(string userName)
        {
            UserInfo = await MvvmNanoIoC.Resolve<TutorScoutRestService>().GetUserInfo(userName);
        }


        public override void Initialize(string parameter)
        {
            base.Initialize(parameter);


            if (parameter != null)
                GetUserInfo(parameter);
        }
    }
}