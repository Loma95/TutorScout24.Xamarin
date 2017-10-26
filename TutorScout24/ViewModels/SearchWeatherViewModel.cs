using System;
using MvvmNano;
using TutorScout24.Services;

namespace TutorScout24.ViewModels
{
    public class SearchWeatherViewModel:MvvmNanoViewModel
    {
        public SearchWeatherViewModel()
        {
            getWeatherJSON();
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

        private async void getWeatherJSON()
        {
            RestService service = new RestService();


            Weather = await service.GetWeatherForCity(Input);

        }
    }
}
