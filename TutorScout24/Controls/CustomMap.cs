using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TutorScout24.Controls
{
    /// <summary>
    /// Custom Map control that has its ccurrent center as a bindable property and supports Custom Pins.
    /// </summary>
    public class CustomMap : Map
    {
        public static readonly BindableProperty PositionProperty = BindableProperty.Create("Position", typeof(Position), typeof(CustomMap),
            new Position(0, 0), propertyChanged: OnPropertyChanged);
        List<CustomPin> _customPins = new List<CustomPin>();

        public List<CustomPin> CustomPins
        {
            get { return _customPins; }
            set { _customPins = value; }
        }
        private Position _position;
        public Position Position
        {
            get { return _position; }
            set { _position = value; }
        }

        /// <summary>
        /// Called when position is changed.
        /// Moves Map to new center.
        /// </summary>
        /// <param name="bindable">The Map which has a Position Change</param>
        /// <param name="oldValue">Old Position</param>
        /// <param name="newValue">New Position</param>
        public static void OnPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var map = (CustomMap)bindable;
            var newPos = (Position)newValue;
            map.MoveToRegion(MapSpan.FromCenterAndRadius(
                    new Xamarin.Forms.Maps.Position(newPos.Latitude, newPos.Longitude), Distance.FromMiles(0.5)));
        }
    }
}