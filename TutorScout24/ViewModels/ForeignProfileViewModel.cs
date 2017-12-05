using System;
using TutorScout24.Models;
using TutorScout24.Services;
using MvvmNano;
using TutorScout24.Utils;
using Xamarin.Forms;

namespace TutorScout24.ViewModels
{
    public class ForeignProfileViewModel:MvvmNano.MvvmNanoViewModel<string>,IThemeable
    {

        public ForeignProfileViewModel(){
            _themeColor = (Xamarin.Forms.Color)Application.Current.Resources["MainColor"];
        }
        private async void GetUserInfo(string userName)
        {

            UserInfo = await MvvmNanoIoC.Resolve<TutorScoutRestService>().GetUserInfo(userName);

        }


        private UserInfo _userInfo;
        public UserInfo UserInfo
        {
            get { return _userInfo; }
            set
            {
                _userInfo = value;

               
                NotifyPropertyChanged("UserInfo");
            }
        }



        public override void Initialize(string parameter)
        {
            base.Initialize(parameter);

         
            if (parameter != null)
            {

                GetUserInfo(parameter);

            }

        }

        private Color _themeColor;
        public Color ThemeColor
        {
            get { return _themeColor; }
            set
            {
                _themeColor = value;

                NotifyPropertyChanged("ThemeColor");

            }
        }
    }
}
