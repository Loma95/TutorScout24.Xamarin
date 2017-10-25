using System;
using MvvmNano.Forms.MasterDetail;
using TutorScout24.ViewModels;
using Xamarin.Forms;

namespace TutorScout24.Pages
{
    public class MasterDetailPage : MvvmNanoDefaultMasterDetailPage<MasterDetailViewModel>
    {
        private Label _usernameLabel = new Label
        {
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            Margin = 10,
            FontSize = 40
        };

        private Button _logoutButton = new Button
        {
            Text = "Logout"
        };

        public MasterDetailPage()
        {
            AddDetailData<CurrentLocationWeatherViewModel>(new MvvmNanoMasterDetailData("Mein Wetter"));
            AddDetailData<SearchWeatherViewModel>(new MvvmNanoMasterDetailData("Suche"));
        }

        protected override Page CreateMasterPage()
        {
            DetailListView.Header = _usernameLabel;
            DetailListView.Footer = _logoutButton;
            return base.CreateMasterPage();
        }

    }
}
