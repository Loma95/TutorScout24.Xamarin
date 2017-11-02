using System;
using System.Diagnostics;
using System.ComponentModel;

using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms.Maps;
using TutorScout24.Services;

namespace TutorScout24.ViewModels
{

    public class CurrentLocationWeatherViewModel : MvvmNano.MvvmNanoViewModel, IObserver<Plugin.Geolocator.Abstractions.Position>
    {

        public CurrentLocationWeatherViewModel()
        {


            getWeatherJSON();
            getFirstPosition();


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

        private Xamarin.Forms.Maps.Position _pos;
        public Xamarin.Forms.Maps.Position Position
        {
            get { return _pos; }
            set
            {
                _pos = value;
                NotifyPropertyChanged("Position");

            }
        }

        private async void getWeatherJSON()
        {
            RestService service = new RestService();
            Debug.WriteLine("requestWeather");
            Weather = await service.GetWeatherForCurrentLocation();
            Debug.WriteLine("gotWeather");


        }
        private async void getFirstPosition()
        {
            LocationService service = LocationService.getInstance();
            Plugin.Geolocator.Abstractions.Position p = await service.GetPosition();
            Position = new Xamarin.Forms.Maps.Position(p.Latitude, p.Longitude);
            service.Subscribe(this);
        }

        public void OnCompleted()
        {
            
        }

        public void OnError(Exception error)
        {
            
        }

        public void OnNext(Plugin.Geolocator.Abstractions.Position value)
        {
            Position = new Xamarin.Forms.Maps.Position(value.Latitude, value.Longitude);
        }
    }
}
