using System;
using System.Collections.Generic;
using MvvmNano.Forms;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TutorScout24
{
    public partial class CurrentLocationWeatherPage
    {
        public CurrentLocationWeatherPage()
        {
            InitializeComponent();

            MoveMapsToMyLocation();
        }

        private async void MoveMapsToMyLocation(){
            Plugin.Geolocator.Abstractions.Position p = await CrossGeolocator.Current.GetPositionAsync();
            MyMap.MoveToRegion(
            MapSpan.FromCenterAndRadius(
                    new Position(p.Latitude, p.Longitude), Distance.FromMiles(0.5)));
        }
    }
}
