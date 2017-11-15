using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmNano;
using TutorScout24.Models;
using TutorScout24.Services;
using Xamarin.Forms;

namespace TutorScout24.ViewModels
{
    public class SearchWeatherViewModel:MvvmNanoViewModel
    {

        public SearchWeatherViewModel()
        {





        }




      
        private RootWeather _weather;

        private string _input;
        public string Input
        {
            get { return _input; }
            set { _input = value;
                getWeatherJSON();
                NotifyPropertyChanged("Input");}
        }
         


        public RootWeather Weather
        {
            get { return _weather; }
            set
            {
                _weather = value;
                NotifyPropertyChanged("Weather");
            }
        }

        /// <summary>
        /// Gets the weather json.
        /// </summary>
        private async void getWeatherJSON()
        {
            RestService service = new RestService();
            Weather = await service.GetWeatherForCity(Input);

        }
    }
}
