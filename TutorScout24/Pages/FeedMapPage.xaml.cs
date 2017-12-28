using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorScout24.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutorScout24.Pages
{
    public partial class FeedMapPage
    {
        public FeedMapPage()
        {
            InitializeComponent();

            RequestMap.CustomPins = new List<Controls.CustomPin>();
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.Map = RequestMap;
            ViewModel.SetPinsAsync();
           
        }
    }
}