using System;
using System.Collections.Generic;
using System.Diagnostics;
using MvvmNano;
using TutorScout24.Controls;
using TutorScout24.CustomData;
using TutorScout24.Models;
using TutorScout24.Services;
using TutorScout24.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Position = Plugin.Geolocator.Abstractions.Position;

namespace TutorScout24.ViewModels
{
    public class FeedMapViewModel : MvvmNano.MvvmNanoViewModel, IObserver<Plugin.Geolocator.Abstractions.Position>,IThemeable
    {

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

/*        private List<Pin> _pins = new List<Pin>();

        public List<Pin> Pins
        {
            get { return _pins; }
            set { _pins = value; }
        }*/


        private CustomMap _map;

        public CustomMap Map
        {
            get { return _map; }
            set { _map = value; }
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

        public FeedMapViewModel()
        {
            GetFirstPosition();
            _themeColor = (Xamarin.Forms.Color)Application.Current.Resources["MainColor"];
            MvvmNanoIoC.Resolve<IMessenger>().Subscribe(this, (object arg1, ChangeModeMessage arg2) =>
            {
                SetPinsAsync();
            });
        }

        public async void SetPinsAsync()
        {
            List<Tutoring> list = await MvvmNanoIoC.Resolve<TutorScoutRestService>().GetTutorings();
            List<Pin> pins = new List<Pin>();
            Map.Pins.Clear();
            Map.CustomPins.Clear();
            if(Map!=null)
            foreach (Tutoring tutoring in list)
            {
                Debug.WriteLine("Set Pin:" + tutoring.latitude);
                Device.BeginInvokeOnMainThread(() =>
                {
                    
                    var pin = new CustomPin
                    {
                        Position = new Xamarin.Forms.Maps.Position(tutoring.latitude, tutoring.longitude),
                        Label = tutoring.userName ,
                        Description = tutoring.subject
                    };

               
                    Map.CustomPins.Add(pin);
                    Map.Pins.Add(pin);
                

                });

            }
        }

        private async void GetFirstPosition()
        {
            Loads = true;
            LocationService service = LocationService.getInstance();
            Position p = await service.GetPosition();
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

        public void OnNext(Position value)
        {
            //Position = new Xamarin.Forms.Maps.Position(value.Latitude, value.Longitude);
        }


        private Color _themeColor;
        public Color ThemeColor { get { return _themeColor; } set { _themeColor = value; NotifyPropertyChanged("ThemeColor"); } }
    }
}
