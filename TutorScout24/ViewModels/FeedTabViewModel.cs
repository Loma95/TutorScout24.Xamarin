using System;
using System.Collections.Generic;
using System.Diagnostics;
using MvvmNano.Forms;
using TutorScout24.Utils;
using Xamarin.Forms;

namespace TutorScout24.ViewModels
{
    public class FeedTabViewModel : MvvmNano.MvvmNanoViewModel,IThemeable
    {
        public FeedTabViewModel()
        {
            
            _themeColor = (Xamarin.Forms.Color)Application.Current.Resources["MainColor"];
        }


      
        private Color _themeColor;
        public Color ThemeColor { get { return _themeColor; } set {var master = (Pages.MasterDetailPage)Application.Current.MainPage;
                var navigation = (MvvmNanoNavigationPage)master.Detail;
                var p = (TabbedPage)navigation.RootPage;
                IThemeable themable = (IThemeable)p.CurrentPage.BindingContext;
                themable.ThemeColor = value;
                _themeColor = value; _themeColor = value; NotifyPropertyChanged("ThemeColor"); } }

     

    }
}
