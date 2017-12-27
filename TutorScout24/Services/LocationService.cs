using System.Diagnostics;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;

namespace TutorScout24.Services
{
    /// <summary>
    ///     Position Service singleton for getting current position
    /// </summary>
    public class LocationService
    {
        public static Position Pos;
        private static LocationService _instance;

        public static LocationService GetInstance()
        {
            return _instance ?? (_instance = new LocationService());
        }

        /// <summary>
        ///     Get Dvice Location
        /// </summary>
        /// <returns>Current Device Location</returns>
        public async Task<Position> GetPosition()
        {
            if (Pos != null) return Pos;
            Pos = await CrossGeolocator.Current.GetPositionAsync();
            if (Pos != null) return Pos;
            Pos = await CrossGeolocator.Current.GetLastKnownLocationAsync();
            return Pos;
        }
    }
}