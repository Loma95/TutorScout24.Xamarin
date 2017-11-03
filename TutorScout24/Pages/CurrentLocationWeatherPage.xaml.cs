using System;
using System.Collections.Generic;
using System.Diagnostics;
using MvvmNano;
using MvvmNano.Forms;
using Plugin.Geolocator;
using TutorScout24.Services;
using TutorScout24.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TutorScout24
{
    public partial class CurrentLocationWeatherPage
    {
        
        public CurrentLocationWeatherPage()
        {
            InitializeComponent();
           
           
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MvvmNanoIoC.Resolve<IMessenger>().Unsubscribe<GPSMessage>(this);
        }


    }
}
