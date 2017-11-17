using System;
using Plugin.Geolocator.Abstractions;
using TutorScout24.Services;

namespace TutorScout24.ViewModels
{
    public class FeedMapViewModel : MvvmNano.MvvmNanoViewModel, IObserver<Plugin.Geolocator.Abstractions.Position>
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
    }
}
