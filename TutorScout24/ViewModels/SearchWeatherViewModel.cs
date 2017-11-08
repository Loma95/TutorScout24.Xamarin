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

        public string HeaderText { get { return "Control Template Demo App"; } }


        public ICommand FabCommand
        {
            get { return new Command(CreateUser); }

        }


        private  async void CreateUser()
        {
            User usr = new User();
            usr.age = 11;
            usr.email = "test@dhbw.de";
            usr.firstName = "Swagger";
            usr.lastName = "IO";
            usr.gender = "female";
            usr.password = "passwort";
            usr.maxGraduation = "Bachelor";
            usr.note = "hi";
            usr.placeOfResidence = "Stuggi";
            usr.userName = "YoloSwagger";
            string response = await MvvmNanoIoC.Resolve<TutorScoutRestService>().CreateUser(usr);

            MvvmNanoIoC.Resolve<IMessenger>().Send(new DialogMessage(response.ToString()));
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
