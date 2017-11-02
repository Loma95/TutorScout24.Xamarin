using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System.Collections.Generic;

namespace TutorScout24.Services
{
    public class LocationService : IObservable<Position>
    {
        private static Position pos;
        private static LocationService instance;
        private List<IObserver<Position>> observers = new List<IObserver<Position>>();

        private LocationService(){

        }

        public static LocationService getInstance()
        {
            if(instance==null)
            {
                instance = new LocationService();
            }
            return instance;
        }


        public async Task<Position> GetPosition()
        {
            if (pos == null)
            {
                pos = await CrossGeolocator.Current.GetPositionAsync();
                if (pos == null)
                {
                    pos = await CrossGeolocator.Current.GetLastKnownLocationAsync();
                }
                await StartListening();
                //new LocationService();
            }

            Debug.WriteLine(pos.Latitude);  
            return pos;
        }

        //TODO:update Position on change


          async Task StartListening()
          {
              if (CrossGeolocator.Current.IsListening)
                  return;

              await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(5), 10, true);

              CrossGeolocator.Current.PositionChanged += PositionChanged;
              CrossGeolocator.Current.PositionError += PositionError;
          }

          private void PositionChanged(object sender, PositionEventArgs e)
          {

              //If updating the UI, ensure you invoke on main thread
              var position = e.Position;
              var output = "Full: Lat: " + position.Latitude + " Long: " + position.Longitude;
              output += "\n" + $"Time: {position.Timestamp}";
              output += "\n" + $"Heading: {position.Heading}";
              output += "\n" + $"Speed: {position.Speed}";
              output += "\n" + $"Accuracy: {position.Accuracy}";
              output += "\n" + $"Altitude: {position.Altitude}";
              output += "\n" + $"Altitude Accuracy: {position.AltitudeAccuracy}";
              Debug.WriteLine(output);
            foreach(var obs in observers)
            {
                obs.OnNext(position);
            }
          }

          private void PositionError(object sender, PositionErrorEventArgs e)
          {
              Debug.WriteLine(e.Error);
              //Handle event here for errors
          }

          async Task StopListening()
          {
              if (!CrossGeolocator.Current.IsListening)
                  return;

              await CrossGeolocator.Current.StopListeningAsync();

              CrossGeolocator.Current.PositionChanged -= PositionChanged;
              CrossGeolocator.Current.PositionError -= PositionError;
          }

        public IDisposable Subscribe(IObserver<Position> observer)
        {
            observers.Add(observer);
            return null;
        }
    }
}
