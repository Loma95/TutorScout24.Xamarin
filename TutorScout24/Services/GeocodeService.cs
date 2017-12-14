using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TutorScout24.Models.Geocoding;

namespace TutorScout24.Services
{
    class GeocodeService
    {
        private static string API_KEY = "AIzaSyA4Wf45IAxCu1lhljKowfztBRZR9A9r09U";
        private static string API_URL = "https://maps.googleapis.com/maps/api/geocode/json?address=";
        private readonly HttpClient _client;

        public GeocodeService()
        {
            _client = new HttpClient { MaxResponseContentBufferSize = 256000 };
        }

        public async Task<GeocodingResponse> GetResponseForString(string query)
        {
            string uriAdressString = "";
            foreach (string s in query.Split(' '))
            {
                uriAdressString = string.Concat(uriAdressString, s, "+");
            }
            if (uriAdressString.EndsWith("+"))
            {
                uriAdressString.Remove(uriAdressString.Length - 1);
            }
            string additionalParams = "&key=";

            string requestString = string.Concat(API_URL, uriAdressString, additionalParams, API_KEY);
            Debug.WriteLine("Geocode Request:" + requestString);
            var uri = new Uri(string.Format(requestString, string.Empty));
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<GeocodingResponse>(await response.Content.ReadAsStringAsync());

            }
            return null;
        }
    }
}
