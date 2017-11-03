using System;
using System.Diagnostics;
using MvvmNano;
using TutorScout24.Models;
using TutorScout24.Services;

namespace TutorScout24.ViewModels
{
    public class SearchWeatherViewModel:MvvmNanoViewModel
    {
        public SearchWeatherViewModel()
        {

            GetUserInfo();

        }


        private async void GetUserInfo(){
            UserInfos userI = await MvvmNanoIoC.Resolve<TutorScoutRestService>().GetUserInfo();
            Debug.WriteLine("UserCount " + userI.UserCount);
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
