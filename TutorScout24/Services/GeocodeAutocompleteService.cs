using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TutorScout24.Models.Geocoding;

namespace TutorScout24.Services
{

    /// <summary>
    /// Class for autocompletion of user input strings via Google API.
    /// </summary>
    internal class GeocodeAutocompleteService
    {
        private static readonly string API_KEY = "AIzaSyA4Wf45IAxCu1lhljKowfztBRZR9A9r09U";
        private static readonly string API_URL = "https://maps.googleapis.com/maps/api/place/autocomplete/json?input=";
        private readonly HttpClient _client;

        public GeocodeAutocompleteService()
        {
            _client = new HttpClient {MaxResponseContentBufferSize = 256000};
        }

        /// <summary>
        /// Get List of strings out of the Google API response.
        /// </summary>
        /// <param name="query">User Query as input for Suggestion API</param>
        /// <returns>List of suggestions</returns>
        public async Task<List<string>> GetSuggestions(string query)
        {
            var result = await GetResponseForString(query);
            return new List<string>(result.predictions.Select(r => r.description));
        }

        /// <summary>
        /// Constructs Request URI and retrievs response object.
        /// </summary>
        /// <param name="query">User Query as input for Suggestion API</param>
        /// <returns>Response Object from Google API</returns>
        private async Task<AutocompleteResponse> GetResponseForString(string query)
        {
            var uriAdressString = "";
            foreach (var s in query.Split(' '))
                uriAdressString = string.Concat(uriAdressString, s, "+");
            if (uriAdressString.EndsWith("+"))
                uriAdressString.Remove(uriAdressString.Length - 1);
            var additionalParams = "&types=geocode&language=ger";
            if (LocationService.Pos != null)
            {
                var pos = LocationService.Pos;
                additionalParams += "&location=" + pos.Longitude + "," + pos.Latitude + "&radius=10000";
            }
            additionalParams += "&key=";

            var requestString = string.Concat(API_URL, uriAdressString, additionalParams, API_KEY);
            Debug.WriteLine("Autocomplete Request:" + requestString);
            var uri = new Uri(string.Format(requestString, string.Empty));
            Debug.WriteLine(requestString);
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<AutocompleteResponse>(await response.Content.ReadAsStringAsync());
            return null;
        }
    }
}