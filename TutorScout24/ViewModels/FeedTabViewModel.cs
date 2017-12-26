using System;
using System.Collections.Generic;
using System.Diagnostics;
using MvvmNano.Forms;
using TutorScout24.Utils;
using Xamarin.Forms;

namespace TutorScout24.ViewModels
{
    public class FeedTabViewModel : MvvmNano.MvvmNanoViewModel,IThemeable,IToolBarItem
    {
        public FeedTabViewModel()
        {
            
            _themeColor = (Xamarin.Forms.Color)Application.Current.Resources["MainColor"];

       
        }

        private ToolbarItem switchI = new ToolbarItem
        {
            Text = "\uf0ec"
        };

     
  

        public void AddToolBarItem()
        {
            var master = (Pages.MasterDetailPage)Application.Current.MainPage;
            if (master != null)
            {
                master.ToolbarItems.Clear();
                master.ToolbarItems.Add(switchI);
                switchI.SetBinding(ToolbarItem.CommandProperty, nameof(MasterDetailViewModel.ChangeCommand));
            }
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
