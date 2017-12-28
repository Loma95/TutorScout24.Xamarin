using System.Collections.Generic;
using System.Diagnostics;
using MvvmNano;
using TutorScout24.Controls;
using TutorScout24.CustomData;
using TutorScout24.Services;
using TutorScout24.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TutorScout24.ViewModels
{
    public class FeedMapViewModel : MvvmNanoViewModel, IThemeable
    {
        private Position _pos;
        private bool _searchingGPS;
        private Color _themeColor;

        /// <summary>
        ///     Set first position and theme color, refresh pins on mode change
        /// </summary>
        public FeedMapViewModel()
        {
            GetFirstPosition();
            _themeColor = (Color) Application.Current.Resources["MainColor"];
            MvvmNanoIoC.Resolve<IMessenger>()
                .Subscribe(this, (object arg1, ChangeModeMessage arg2) => { SetPinsAsync(); });
        }

        public Position Position
        {
            get => _pos;
            set
            {
                _pos = value;
                NotifyPropertyChanged("Position");
            }
        }

        public CustomMap Map { get; set; }

        public bool Loads
        {
            get => _searchingGPS;
            set
            {
                _searchingGPS = value;
                NotifyPropertyChanged("Loads");
            }
        }

        public Color ThemeColor
        {
            get => _themeColor;
            set
            {
                _themeColor = value;
                NotifyPropertyChanged("ThemeColor");
            }
        }

        /// <summary>
        ///     Set pins on map according to current mode
        /// </summary>
        public async void SetPinsAsync()
        {
            var list = await MvvmNanoIoC.Resolve<TutorScoutRestService>().GetTutorings();
            var pins = new List<Pin>();

            if (Map != null)
            {
                Map.Pins.Clear();
                Map.CustomPins.Clear();
                foreach (var tutoring in list)
                {
                    Debug.WriteLine("Set Pin:" + tutoring.latitude);
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        var pin = new CustomPin
                        {
                            Position = new Position(tutoring.latitude, tutoring.longitude),
                            Label = tutoring.userName,
                            Description = tutoring.subject,
                            UserName = tutoring.userName
                        };


                        Map.CustomPins.Add(pin);
                        Map.Pins.Add(pin);
                    });
                }
            }
        }

        /// <summary>
        /// Recieve first position for initial map state
        /// </summary>
        private async void GetFirstPosition()
        {
            Loads = true;
            var service = LocationService.GetInstance();
            var p = await service.GetPosition();
            Position = new Position(p.Latitude, p.Longitude);
            Loads = false;
        }
    }
}