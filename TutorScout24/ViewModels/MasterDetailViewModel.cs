using MvvmNano;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using TutorScout24.Models;
using TutorScout24.Services;
using Xamarin.Forms;
using MvvmNano.Forms;
using MvvmNano.Forms.MasterDetail;
using System.Reflection;
using System.Diagnostics;
using System.Linq;
using System.Diagnostics.Contracts;
using TutorScout24.Utils;

namespace TutorScout24.ViewModels
{
    public class MasterDetailViewModel : MvvmNano.MvvmNanoViewModel
    {
        public enum Mode  { TUTOR,STUDENT };
        public static Mode CurrentMode;
        public ICommand ChangeCommand => new Command(Change);

        private  void Change()
        {


            if(CurrentMode == Mode.STUDENT){
                CurrentMode = Mode.TUTOR;
                Application.Current.Resources["MainColor"] = Color.FromHex("#EF5350");
            }else{
                CurrentMode = Mode.STUDENT;
                Application.Current.Resources["MainColor"] = Color.FromHex("#78909c");
            }


            var master = (Pages.MasterDetailPage)Application.Current.MainPage;
            var navigation = (MvvmNanoNavigationPage)master.Detail;
            IThemeable vc =  (IThemeable)navigation.RootPage.BindingContext;
            vc.ThemeColor = (Xamarin.Forms.Color)Application.Current.Resources["MainColor"];


            SetBarColor();
        }
        /// <summary>
        /// Sets the color of the bar.
        /// </summary>
        private void SetBarColor(){
            var master = (Pages.MasterDetailPage)Application.Current.MainPage;
            var navigation = (MvvmNanoNavigationPage)master.Detail;
            navigation.BarBackgroundColor = (Xamarin.Forms.Color)Application.Current.Resources["MainColor"];
        }


    }
}
