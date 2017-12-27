using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmNano;
using MvvmNano.Forms;
using Plugin.Geolocator.Abstractions;
using TutorScout24.Controls;
using TutorScout24.CustomData;
using TutorScout24.Models;
using TutorScout24.Pages;
using TutorScout24.Services;
using TutorScout24.Utils;
using Xamarin.Forms;

namespace TutorScout24.ViewModels
{
    public class CreateViewModel : MvvmNano.MvvmNanoViewModel<CreateTutoring>, IToolBarItem
    {
        private CreateTutoring _ct;

        private List<string> _geoCodeSuggestions;

        public List<string> GeoCodeSuggestions
        {
            get => _geoCodeSuggestions;
            set
            {
                _geoCodeSuggestions = value;
                NotifyPropertyChanged();
            }
        }


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

            AddToolBarItem();

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
            DialogView.IsVisible = true;
            DialogView.DialogClosed += (sender, e) => {
                DialogView.IsVisible = false;
            };
            DialogView.ShowDialog();
            SelectionMode = true;
            SetPos();
        }

        public void PositionSelected(double lat,double lon)
        {

            _ct.latitude = lat;
            _ct.longitude = lon;

            DialogView.IsVisible = false;
            _selectedText = lat + " " + lon;
            NotifyPropertyChanged("SelectedText");
        }

        private string _selectedText;
        public string SelectedText
        {
            get { return _selectedText; }
      

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
            set
            {
                _adress = value;
                if (value != "")
                {
                    GetSuggestionsAsync(value);
                }
            }
        }

        private async void GetSuggestionsAsync(string value)
        {
            GeoCodeSuggestions = await MvvmNanoIoC.Resolve<GeocodeAutocompleteService>().GetSuggestions(value);
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

        private bool _showMap = false;

        public bool ShowMap
        {
            get { return _showMap; }
            set
            {
                _showMap = value;
                NotifyPropertyChanged("ShowMap");
            }
        }

        private bool _selectionMode;
        public bool SelectionMode
        {
            get
            {
                return _selectionMode;
            }  

            set{
                _selectionMode = value;
                if(value){
                    Selection = "Karte";

                }else{
                    Selection = "Adresse";
                }
                NotifyPropertyChanged("SelectionMode");
            }
        }

        public string Selection
        {
            get { return _selection; }
            set
            {
                _selection = value;
                if (value == "Karte")
                {
                    ShowMap = true;
                    ShowAdressField = false;
                }
                else
                {
                    ShowMap = false;
                    ShowAdressField = true;
                }
                NotifyPropertyChanged("PosSelection");
                Debug.WriteLine(value);
                NotifyPropertyChanged("Selection");
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
            Debug.WriteLine("init");
            base.Initialize(parameter);




            if (parameter == null || parameter.longitude == 0 && parameter.latitude == 0) return;
            _ct = parameter;
            Subject = _ct.subject;
            Text = _ct.text;
            ExpDate = DateTime.Today.AddDays(_ct.duration);
            Debug.WriteLine("long:" + _ct.longitude + "lat:" + _ct.latitude);


        }


        public void RemoveToolbarItem()
        {
            var master = (Pages.MasterDetailPage)Application.Current.MainPage;
            master.ToolbarItems.Clear();
        }


        public override void Dispose()
        {
            base.Dispose();
            RemoveToolbarItem();
        }

        public void AddToolBarItem()
        {
            var master = (Pages.MasterDetailPage)Application.Current.MainPage;
            master.ToolbarItems.Clear();
            _CreateSwitch = new ToolbarItem
            {
                Text = "\uf1d8"
            };

            _CreateSwitch.Clicked += async (sender, e) =>
            {
                _CreateSwitch.Text = "\uf1d8";

                if (_ct == null)
                {
                    _ct = new CreateTutoring
                    {
                        duration = _expDate.Day - DateTime.Today.Day,
                        text = Text,
                        subject = Subject
                    };
                }
             
                bool success = await MvvmNanoIoC.Resolve<TutorScoutRestService>().CreateTutoring(_ct);
                if (success)
                {
                    master.SendBackButtonPressed();
                }
                else
                {
                    if (MasterDetailViewModel.CurrentMode == MasterDetailViewModel.Mode.STUDENT)
                    {
                        MvvmNanoIoC.Resolve<IMessenger>().Send(new DialogMessage("Fehler", "Anfrage konnte nicht erstellt werden."));
                    }
                    else
                    {
                        MvvmNanoIoC.Resolve<IMessenger>().Send(new DialogMessage("Fehler", "Angebot konnte nicht erstellt werden."));
                    }
                }
            };

            master.ToolbarItems.Add(_CreateSwitch);

        }


        #region "SelectPosition"

        public  async void SetLocationWithAdress(){

            if (Adress != null)
            {
                var response = await MvvmNanoIoC.Resolve<GeocodeService>().GetResponseForString(Adress);
                var pos = new Position(response.results[0].geometry.location.lat, response.results[0].geometry.location.lng);
                if (pos.Latitude == 0 && pos.Longitude == 0)
                {
                    MvvmNanoIoC.Resolve<IMessenger>().Send(new DialogMessage("Fehler", "Kein Ort gefunden."));
                    return;
                }
                _ct.latitude = pos.Latitude;
                _ct.longitude = pos.Longitude;

                _selectedText = Adress;
                NotifyPropertyChanged("SelectedText");

                DialogView.IsVisible = false;
            }else{
                MvvmNanoIoC.Resolve<IMessenger>().Send(new DialogMessage("Fehler", "Bitte eine Adresse eingeben"));
            }
        }

        private Xamarin.Forms.Maps.Position _position;

        public Xamarin.Forms.Maps.Position Position
        {
            get { return _position; }
            set { _position = value; }
        }


        private PopUpDialogView _dialogView;
        public PopUpDialogView DialogView
        {
            get { return _dialogView; }
            set { _dialogView = value; }
        }

        private Position _posSelect;

        public Position PosSelect
        {
            get { return _posSelect; }
            set
            {
                _posSelect = value;
                Debug.WriteLine(value.Latitude);
            }
        }


        private async void SetPos()
        {
            var tempPos = await LocationService.getInstance().GetPosition();
            _position = new Xamarin.Forms.Maps.Position(tempPos.Latitude, tempPos.Longitude);
            NotifyPropertyChanged("Position");
        }


        public SelectionMap map;
        #endregion
    }
}