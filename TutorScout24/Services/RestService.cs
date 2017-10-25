﻿using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;

namespace TutorScout24
{
    public class RestService
    {
        String RestUrl;
        HttpClient client;
        public RestService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;

        }

        /// <summary>
        /// builds the api path and returns the weather for the given cityName
        /// </summary>
        /// <returns>The weather for city.</returns>
        /// <param name="cityName">City name.</param>
        public async Task<RootWeather> GetWeatherForCity(string cityName )
        {
            RestUrl =  "http://api.openweathermap.org/data/2.5/weather?q="+ cityName + "&appid=d3d7a0ec7620eb0ba79308afb2e25b6a&units=metric";
            return await GetWeather();
        }

        /// <summary>
        /// Gets the weather for current location.
        /// </summary>
        /// <returns>The weather for current location.</returns>
        public async Task<RootWeather> GetWeatherForCurrentLocation(){
            Position p = await CrossGeolocator.Current.GetPositionAsync();
            RestUrl = "http://api.openweathermap.org/data/2.5/weather?lat="+ p.Latitude + "&lon="+ p.Longitude +"&appid=d3d7a0ec7620eb0ba79308afb2e25b6a&units=metric";
            Debug.WriteLine(RestUrl);
            return await GetWeather();
        }

        /// <summary>
        /// Gets the weather.
        /// </summary>
        /// <returns>The weather.</returns>
        public async Task<RootWeather> GetWeather()
        {
            var uri = new Uri(string.Format(RestUrl, string.Empty));
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<RootWeather>(content);
            }
            return new RootWeather();
        }


    }
}
