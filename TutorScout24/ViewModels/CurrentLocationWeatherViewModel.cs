using System;
using System.Diagnostics;
using System.ComponentModel;

using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms.Maps;
using TutorScout24.Services;
using Xamarin.Forms;
using MvvmNano;
using System.Windows.Input;
using TutorScout24.Models;

namespace TutorScout24.ViewModels
{

    public class CurrentLocationWeatherViewModel : MvvmNano.MvvmNanoViewModel, IObserver<Plugin.Geolocator.Abstractions.Position>
    {
        private readonly IMessenger _messenger;

        public CurrentLocationWeatherViewModel()
        {
            _messenger = MvvmNanoIoC.Resolve<IMessenger>();

            getWeatherJSON();
            getFirstPosition();


        }

        private bool _hasData;
        public bool HasData
        {
            get { return _hasData; }
            set
            {
                _hasData = value;
             
                NotifyPropertyChanged("HasData");

            }
        }
       

        private bool _searchingGPS;
        public bool Loads
        {
            get { return _searchingGPS; }
            set
            {
                _searchingGPS = value;
             
                NotifyPropertyChanged("Loads");

            }
        }
       

        private RootWeather _weather;
        public RootWeather Weather
        {
            get { return _weather; }
            set
            {
                _weather = value;
                Debug.WriteLine(value.name);
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
            Weather = await service.GetWeatherForCurrentLocation();
            HasData = true;


        }
        private async void getFirstPosition()
        {
            Loads = true;
            LocationService service = LocationService.getInstance();
            Plugin.Geolocator.Abstractions.Position p = await service.GetPosition();
            Position = new Xamarin.Forms.Maps.Position(p.Latitude, p.Longitude);
            service.Subscribe(this);
            Loads = false;
        }

        public void OnCompleted()
        {
            
        }

        public void OnError(Exception error)
        {
            
        }


        public ICommand FabCommand
        {
            get { return new Command(CreateUser); }

        }


        private async void CreateUser()
        {
            User usr = new User();
            usr.age = 11;
            usr.email = "test@dhbw.de";
            usr.firstName = "Swagger";
            usr.lastName = "IO";
            usr.gender = "female";
            usr.password = "passwort";
            usr.maxGraduation = "Bachelor";
            usr.note = "hi";
            usr.placeOfResidence = "Stuggi";
            usr.userName = "YoloSwagger";
            string response = await MvvmNanoIoC.Resolve<TutorScoutRestService>().CreateUser(usr);

            MvvmNanoIoC.Resolve<IMessenger>().Send(new DialogMessage(response.ToString()));
        }



        public void OnNext(Plugin.Geolocator.Abstractions.Position value)
        {
            Position = new Xamarin.Forms.Maps.Position(value.Latitude, value.Longitude);
            getWeatherJSON();

        }
    }
}
