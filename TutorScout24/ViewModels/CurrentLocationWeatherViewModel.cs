using System;
using System.Diagnostics;
using System.ComponentModel;

using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms.Maps;
using TutorScout24.Services;

namespace TutorScout24.ViewModels
{

    public class CurrentLocationWeatherViewModel : MvvmNano.MvvmNanoViewModel
    {

        public CurrentLocationWeatherViewModel()
        {


            getWeatherJSON();


        }

        private RootWeather _weather;
        public RootWeather Weather
        {
            get { return _weather; }
            set
            {
                _weather = value;
                NotifyPropertyChanged("Weather");

            }
        }

       
        /// <summary>
        /// Gets the weather json.
        /// </summary>
        private async void getWeatherJSON()
        {
            RestService service = new RestService();
            Debug.WriteLine("requestWeather");
            Weather = await service.GetWeatherForCurrentLocation();
            Debug.WriteLine("gotWeather");


        }
    }
}
