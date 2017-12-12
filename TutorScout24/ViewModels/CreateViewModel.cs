using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmNano;
using Plugin.Geolocator.Abstractions;
using TutorScout24.CustomData;
using TutorScout24.Models;
using TutorScout24.Services;
using Xamarin.Forms;

namespace TutorScout24.ViewModels
{
    public class CreateViewModel : MvvmNano.MvvmNanoViewModel<CreateTutoring>
    {
        private CreateTutoring _ct;

        public CreateViewModel()
        {
            _ct = new CreateTutoring();
            _pageTitle = MasterDetailViewModel.CurrentMode == MasterDetailViewModel.Mode.STUDENT
                ? "Neue Anfrage erstellen"
                : "Neues Angebot erstellen";

            MvvmNanoIoC.Resolve<IMessenger>().Subscribe(this, (object arg1, ChangeModeMessage arg2) =>
            {
                _pageTitle = arg2.newMode == MasterDetailViewModel.Mode.STUDENT
                    ? "Neue Anfrage erstellen"
                    : "Neues Angebot erstellen";
                NotifyPropertyChanged("PageTitle");
            });
            AddToolbarItem();
        }

        private ToolbarItem _CreateSwitch;
        private DateTime _expDate = DateTime.Today.AddDays(10);
        private DateTime _minDate = DateTime.Today;
        private DateTime _maxDate = DateTime.Today.AddDays(100);
        private string _selection;

        public enum PosSelection
        {
            Adresse,
            Karte
        }

        public ICommand PosSelCommand
        {
            get { return new Command(PosSel); }
        }

        public void PosSel()
        {
            NavigateTo<PositionSelectViewModel, CreateTutoring>(_ct);
        }


        private string _subject;

        public string Subject
        {
            get { return _subject; }
            set
            {
                _subject = value;
                _ct.subject = value;
            }
        }

        private string _text;

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                _ct.text = value;
            }
        }

        private string _adress;

        public string Adress
        {
            get { return _adress; }
            set { _adress = value; }
        }

        private bool _showAdressField = false;

        public bool ShowAdressField
        {
            get { return _showAdressField; }
            set
            {
                _showAdressField = value;
                NotifyPropertyChanged("ShowAdressField");
            }
        }

        private string _pageTitle;

        public string PageTitle
        {
            get { return _pageTitle; }
            set
            {
                _pageTitle = value;
                NotifyPropertyChanged("PageTitle");
            }
        }

        private bool _showMapButton = false;

        public bool ShowMapButton
        {
            get { return _showMapButton; }
            set
            {
                _showMapButton = value;
                NotifyPropertyChanged("ShowMapButton");
            }
        }


        public List<string> Selections
        {
            get { return Enum.GetNames(typeof(PosSelection)).Select(b => b).ToList(); }
        }

        public string Selection
        {
            get { return _selection; }
            set
            {
                _selection = value;
                if (value == "Karte")
                {
                    _showMapButton = true;
                    _showAdressField = false;
                    NotifyPropertyChanged("ShowMapButton");
                    NotifyPropertyChanged("ShowAdressField");
                }
                else
                {
                    _showMapButton = false;
                    _showAdressField = true;
                    NotifyPropertyChanged("ShowMapButton");
                    NotifyPropertyChanged("ShowAdressField");
                }
                NotifyPropertyChanged("PosSelection");
                Debug.WriteLine(value);
            }

        }

        public DateTime ExpDate
        {
            get { return _expDate; }
            set
            {
                _expDate = value;
                _ct.duration = _expDate.Day - DateTime.Today.Day;
            }
        }

        public DateTime MinDate
        {
            get { return _minDate; }
            set { _minDate = value; }
        }

        public DateTime MaxDate
        {
            get { return _maxDate; }
            set { _maxDate = value; }
        }

        public override void Initialize(CreateTutoring parameter)
        {
            base.Initialize(parameter);
            if (parameter.subject == null && parameter.text == null && parameter.duration == 0) return;
            _ct = parameter;
            Subject = _ct.subject;
            Text = _ct.text;
            ExpDate = DateTime.Today.AddDays(_ct.duration);
        }


        public void RemoveToolbarItem(){
            var master = (Pages.MasterDetailPage)Application.Current.MainPage;
            master.ToolbarItems.Remove(_CreateSwitch);
        }

        public void AddToolbarItem()
        {
            var master = (Pages.MasterDetailPage)Application.Current.MainPage;

            _CreateSwitch = new ToolbarItem
            {
                Text = "\uf1d8"
            };

            _CreateSwitch.Clicked += async (sender, e) =>
            {
                _CreateSwitch.Text = "\uf1d8";


                _ct = new CreateTutoring
                {
                    duration = _expDate.Day - DateTime.Today.Day,
                    text = Text,
                    subject = Subject
                };
                if (_selection == "Adresse")
                {
                    Position pos = await LocationService.getInstance().AdressToPos(_adress);
                    _ct.latitude = pos.Latitude;
                    _ct.longitude = pos.Longitude;
                }
                await MvvmNanoIoC.Resolve<TutorScoutRestService>().CreateTutoring(_ct);
            };

            master.ToolbarItems.Add(_CreateSwitch);
        }
      
    }
}