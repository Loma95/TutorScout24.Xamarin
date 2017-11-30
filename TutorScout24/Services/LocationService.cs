﻿using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System.Collections.Generic;
using Xamarin.Forms.Maps;
using Position = Plugin.Geolocator.Abstractions.Position;

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
                Debug.WriteLine(pos.Latitude);
                if (pos == null)
                {
                    Debug.WriteLine("Use Last Known Location");
                    pos = await CrossGeolocator.Current.GetLastKnownLocationAsync();
                }
                await StartListening();
            }

            return pos;
        }

          async Task StartListening()
          {
              if (CrossGeolocator.Current.IsListening)
                  return;

              await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(5), 10, true);

              CrossGeolocator.Current.PositionChanged += PositionChanged;
              CrossGeolocator.Current.PositionError += PositionError;
          }

        public async Task<Position> AdressToPos(string adress)
        {
            Geocoder gc = new Geocoder();
            var positions = await gc.GetPositionsForAddressAsync(adress);
            Position pos = new Position(
                positions.GetEnumerator().Current.Latitude, positions.GetEnumerator().Current.Longitude);
            return pos;
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
              pos = position;
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
