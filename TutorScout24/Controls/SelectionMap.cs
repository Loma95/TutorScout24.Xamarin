using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TutorScout24.Controls
{
    /// <summary>
    /// A map that lets you select a position.
    /// A marker is always placed in the middle and the selected position is stored in a property.
    /// </summary>
    public class SelectionMap : Map
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

        /// <summary>
        /// Called when position is changed.
        /// Moves Map to new center.
        /// </summary>
        /// <param name="bindable">The Map which has a Position Change</param>
        /// <param name="oldValue">Old Position</param>
        /// <param name="newValue">New Position</param>
        public static void OnPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var map = (SelectionMap)bindable;
            var newPos = (Position)newValue;
            map.MoveToRegion(MapSpan.FromCenterAndRadius(
                new Xamarin.Forms.Maps.Position(newPos.Latitude, newPos.Longitude), Distance.FromMiles(0.5)));
        }

        /// <summary>
        /// Called when any property changes.
        /// When VisibleRegion changes, move pin to new center.
        /// </summary>
        /// <param name="propertyName"></param>
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