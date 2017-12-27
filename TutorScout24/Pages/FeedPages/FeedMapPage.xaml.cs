using System.Collections.Generic;
using TutorScout24.Controls;
using TutorScout24.ViewModels;

namespace TutorScout24.Pages
{
    public partial class FeedMapPage
    {
        public FeedMapPage()
        {
            InitializeComponent();

            RequestMap.CustomPins = new List<CustomPin>();
        }

        /// <summary>
        ///     When Page appears, make Map known to ViewModel so it can change the pins.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var vM = (FeedMapViewModel) BindingContext;
            vM.Map = RequestMap;
            vM.SetPinsAsync();
        }
    }
}