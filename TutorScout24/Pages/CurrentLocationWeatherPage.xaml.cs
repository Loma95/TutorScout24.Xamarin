using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            //MoveMapToMyLocation();
        }
        private async void MoveMapToMyLocation()
        {
            Debug.WriteLine("MoveMap");
            Plugin.Geolocator.Abstractions.Position p = await LocationService.getInstance().GetPosition();
            MyMap.MoveToRegion(
            MapSpan.FromCenterAndRadius(
                    new Xamarin.Forms.Maps.Position(p.Latitude, p.Longitude), Distance.FromMiles(0.5)));
            Debug.WriteLine("MoveMap");

        }


       
    }
}
