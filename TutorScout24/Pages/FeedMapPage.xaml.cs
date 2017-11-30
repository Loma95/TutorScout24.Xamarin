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

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            FeedMapViewModel vM = (FeedMapViewModel) BindingContext;
            vM.Map = RequestMap;
        }
    }
}