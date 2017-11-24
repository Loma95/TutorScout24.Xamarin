using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmNano;
using TutorScout24.Models;
using TutorScout24.Services;
using TutorScout24.Utils;
using Xamarin.Forms;

namespace TutorScout24.ViewModels
{
    public class SearchWeatherViewModel:MvvmNanoViewModel,IThemeable
    {

        public SearchWeatherViewModel()
        {
            var tutServ =  MvvmNanoIoC.Resolve<TutorScoutRestService>();
            _tut = tutServ.GetTutorings().ToArray();
            _themeColor = (Xamarin.Forms.Color)Application.Current.Resources["MainColor"];

        }


        private Xamarin.Forms.Maps.Position _pos;
        public Xamarin.Forms.Maps.Position Position
        {
            get { return _pos; }
            set
            {
                _pos = value;
                NotifyPropertyChanged("Position");

            }
        }

        private Tutoring[] _tut;
        public Tutoring[] tutorings
        {
            get { return _tut; }
            set
            {
                _tut = value;
                NotifyPropertyChanged("Tutorings");

            }
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

        private Color _themeColor;
        public Color ThemeColor { get { return _themeColor; } set { _themeColor = value; NotifyPropertyChanged("ThemeColor"); } }


    }
}
