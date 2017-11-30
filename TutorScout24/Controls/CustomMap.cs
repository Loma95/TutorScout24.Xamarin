using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TutorScout24.Controls
{
    public class CustomMap : Map
    {
        public static readonly BindableProperty PositionProperty = BindableProperty.Create("Position", typeof(Position), typeof(CustomMap),
            new Position(0,0), propertyChanged: OnPropertyChanged);

/*        public static readonly BindableProperty PinsProperty = BindableProperty.Create(nameof(FeedPins), typeof(List<Pin>), typeof(Pin), defaultValue:new List<Pin>());*/
   

        private Position _position;
        public Position Position
        {
            get { return _position; }
            set { _position = value; }
        }

/*        private List<Pin> _feedPins;

       public List<Pin> FeedPins
        {
            get => _feedPins;
            set => _feedPins = value;
        }*/

 /*       public static void OnPinChange(BindableObject bindable, object oldValue, object newValue)
        {
            var map = (CustomMap) bindable;
            var newPins = (List<Pin>) newValue;
            map.Pins.Clear();
            foreach (Pin pin in newPins)
            {
                map.Pins.Add(pin);
            }

        }*/

        public static void OnPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var map = (CustomMap)bindable;
            var newPos = (Position)newValue;
            map.MoveToRegion(MapSpan.FromCenterAndRadius(
                    new Xamarin.Forms.Maps.Position(newPos.Latitude, newPos.Longitude), Distance.FromMiles(0.5)));
            //AddLocationAsPin(map, newPos);
        }

        private static void AddLocationAsPin(CustomMap map, Position newPos)
        {
            var pin = new Pin()
            {
                Position = new Position(newPos.Latitude, newPos.Longitude),
                Label = newPos.Latitude + " " + newPos.Longitude
            };
            map.Pins.Clear();
            map.Pins.Add(pin);
        }
    }
}