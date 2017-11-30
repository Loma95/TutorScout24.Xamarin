using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TutorScout24.Controls
{
    class SelectionMap : Map
    {
        public static readonly BindableProperty PositionProperty = BindableProperty.Create("Position", typeof(Position), typeof(SelectionMap),
            new Position(0, 0), propertyChanged: OnPropertyChanged);

        public static readonly BindableProperty SelectionProperty = BindableProperty.Create("PosSelection", typeof(Position), typeof(SelectionMap),
            new Position(0, 0), propertyChanged: OnPropertyChanged);


        private Position _position;
        public Position Position
        {
            get { return _position; }
            set { _position = value; }
        }

       private Position _posSelection;
        public Position PosSelection
        {
            get { return _posSelection; }
            set { _posSelection = value; }
        }

        public static void OnPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var map = (SelectionMap)bindable;
            var newPos = (Position)newValue;
            map.MoveToRegion(MapSpan.FromCenterAndRadius(
                new Xamarin.Forms.Maps.Position(newPos.Latitude, newPos.Longitude), Distance.FromMiles(0.5)));
            //AddLocationAsPin(map, newPos);
        }


        private static void AddLocationAsPin(SelectionMap map, Position newPos)
        {
            var pin = new Pin()
            {
                Position = new Position(newPos.Latitude, newPos.Longitude),
                Label = newPos.Latitude + " " + newPos.Longitude
            };
            map.Pins.Clear();
            map.Pins.Add(pin);
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == "VisibleRegion")
            {
                var pin = new Pin()
                {
                    Position = VisibleRegion.Center,
                    Label = "Auswahl"
                };
                Pins.Clear();
                Pins.Add(pin);
                PosSelection = VisibleRegion.Center;
            }
        }
    }
}