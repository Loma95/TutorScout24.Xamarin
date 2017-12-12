using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Ninject.Infrastructure.Language;
using TutorScout24.Models.Geocoding;

namespace TutorScout24.Services
{
    class GeocodeAutocompleteService
    {
        private static string API_KEY = "AIzaSyA4Wf45IAxCu1lhljKowfztBRZR9A9r09U";
        private static string API_URL = "https://maps.googleapis.com/maps/api/place/autocomplete/json?input=";
        private readonly HttpClient _client;

        public GeocodeAutocompleteService()
        {
            _client = new HttpClient {MaxResponseContentBufferSize = 256000};
        }

        public async Task<List<String>> GetSuggestions(string query)
        {
            var result = await GetResponseForString(query);
            return new List<string>(result.predictions.Select(r => r.description));
        }

        public async Task<AutocompleteResponse> GetResponseForString(string query)
        {
            string uriAdressString = "";
            foreach(string s in query.Split(' '))
            {
                uriAdressString = string.Concat(uriAdressString, s, "+");
            }
            if (uriAdressString.EndsWith("+"))
            {
                uriAdressString.Remove(uriAdressString.Length - 1);
            }
            string additionalParams = "&types=geocode&language=ger";
            if (LocationService.pos != null)
            {
                var pos = LocationService.pos;
                additionalParams += "&location=" + pos.Longitude + "," + pos.Latitude + "&radius=10000";
            }
            additionalParams += "&key=";

            string requestString = string.Concat(API_URL, uriAdressString, additionalParams, API_KEY);
            Debug.WriteLine("Autocomplete Request:" + requestString);
            var uri = new Uri(string.Format(requestString, string.Empty));
            Debug.WriteLine(requestString);
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<AutocompleteResponse>(await response.Content.ReadAsStringAsync());

            }
            return null;
        }
    }
}
