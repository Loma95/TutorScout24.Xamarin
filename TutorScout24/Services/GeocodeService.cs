using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TutorScout24.Models.Geocoding;

namespace TutorScout24.Services
{
    /// <summary>
    /// Service for geocoding from string to lat/long
    /// </summary>
    internal class GeocodeService
    {
        private static readonly string API_KEY = "AIzaSyA4Wf45IAxCu1lhljKowfztBRZR9A9r09U";
        private static readonly string API_URL = "https://maps.googleapis.com/maps/api/geocode/json?address=";
        private readonly HttpClient _client;

        public GeocodeService()
        {
            _client = new HttpClient {MaxResponseContentBufferSize = 256000};
        }

        /// <summary>
        /// Make Geocoding Request to Google maps api
        /// </summary>
        /// <param name="query">Input Query</param>
        /// <returns>Geocoding Response</returns>
        public async Task<GeocodingResponse> GetResponseForString(string query)
        {
            var uriAdressString = "";
            foreach (var s in query.Split(' '))
                uriAdressString = string.Concat(uriAdressString, s, "+");
            if (uriAdressString.EndsWith("+"))
                uriAdressString.Remove(uriAdressString.Length - 1);
            var additionalParams = "&key=";

            var requestString = string.Concat(API_URL, uriAdressString, additionalParams, API_KEY);
            Debug.WriteLine("Geocode Request:" + requestString);
            var uri = new Uri(string.Format(requestString, string.Empty));
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<GeocodingResponse>(await response.Content.ReadAsStringAsync());
            return null;
        }
    }
}