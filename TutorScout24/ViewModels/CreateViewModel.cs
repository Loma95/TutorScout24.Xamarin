using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using MvvmNano;
using TutorScout24.Controls;
using TutorScout24.CustomData;
using TutorScout24.Models.Tutorings;
using TutorScout24.Services;
using TutorScout24.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using MasterDetailPage = TutorScout24.Pages.MasterDetailPage;

namespace TutorScout24.ViewModels
{
    public class CreateViewModel : MvvmNanoViewModel<CreateTutoring>, IToolBarItem
    {
        public enum PosSelection
        {
            Adresse,
            Karte
        }

        private string _adress;

        private ToolbarItem _CreateSwitch;
        private CreateTutoring _ct;
        private DateTime _expDate = DateTime.Today.AddDays(10);

        private List<string> _geoCodeSuggestions;

        private string _pageTitle;

        private string _selection;

        private bool _selectionMode;

        private bool _showAdressField;

        private bool _showMap;

        private string _subject;

        private string _text;

        /// <summary>
        /// Set Title according to mode, subscribe to mode changes to change title later and add toolbaritems
        /// </summary>
        public CreateViewModel()
        {
            _ct = new CreateTutoring();
            _pageTitle = MasterDetailViewModel.CurrentMode == MasterDetailViewModel.Mode.STUDENT
                ? "Neue Anfrage erstellen"
                : "Neues Angebot erstellen";

            MvvmNanoIoC.Resolve<IMessenger>().Subscribe(this, (object arg1, ChangeModeMessage arg2) =>
            {
                _pageTitle = arg2.NewMode == MasterDetailViewModel.Mode.STUDENT
                    ? "Neue Anfrage erstellen"
                    : "Neues Angebot erstellen";
                NotifyPropertyChanged("PageTitle");
            });

            AddToolBarItem();
        }

        public List<string> GeoCodeSuggestions
        {
            get => _geoCodeSuggestions;
            set
            {
                _geoCodeSuggestions = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand PosSelCommand => new Command(PosSel);

        public string SelectedText { get; private set; }

        public string Subject
        {
            get => _subject;
            set
            {
                _subject = value;
                _ct.subject = value;
            }
        }

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                _ct.text = value;
            }
        }

        public string Adress
        {
            get => _adress;
            set
            {
                _adress = value;
                if (value != "")
                    GetSuggestionsAsync(value);
            }
        }

        public bool ShowAdressField
        {
            get => _showAdressField;
            set
            {
                _showAdressField = value;
                NotifyPropertyChanged("ShowAdressField");
            }
        }

        public string PageTitle
        {
            get => _pageTitle;
            set
            {
                _pageTitle = value;
                NotifyPropertyChanged("PageTitle");
            }
        }

        public bool ShowMap
        {
            get => _showMap;
            set
            {
                _showMap = value;
                NotifyPropertyChanged("ShowMap");
            }
        }

        public bool SelectionMode
        {
            get => _selectionMode;

            set
            {
                _selectionMode = value;
                if (value) Selection = "Karte";
                else Selection = "Adresse";
                NotifyPropertyChanged("SelectionMode");
            }
        }

        public string Selection
        {
            get => _selection;
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
            get => _expDate;
            set
            {
                _expDate = value;
                _ct.duration = _expDate.Day - DateTime.Today.Day;
            }
        }

        public DateTime MinDate { get; set; } = DateTime.Today;

        public DateTime MaxDate { get; set; } = DateTime.Today.AddDays(100);

        /// <summary>
        /// Add Switch for creation to toolbar
        /// </summary>
        public void AddToolBarItem()
        {
            var master = (MasterDetailPage) Application.Current.MainPage;
            master.ToolbarItems.Clear();
            _CreateSwitch = new ToolbarItem
            {
                Text = "\uf1d8"
            };

            _CreateSwitch.Clicked += async (sender, e) =>
            {
                _CreateSwitch.Text = "\uf1d8";

                if (_ct == null)
                    _ct = new CreateTutoring
                    {
                        duration = _expDate.Day - DateTime.Today.Day,
                        text = Text,
                        subject = Subject
                    };

                var success = await MvvmNanoIoC.Resolve<TutorScoutRestService>().CreateTutoring(_ct);
                if (success)
                {
                    master.SendBackButtonPressed();
                }
                else
                {
                    if (MasterDetailViewModel.CurrentMode == MasterDetailViewModel.Mode.STUDENT)
                        MvvmNanoIoC.Resolve<IMessenger>()
                            .Send(new DialogMessage("Fehler", "Anfrage konnte nicht erstellt werden."));
                    else
                        MvvmNanoIoC.Resolve<IMessenger>()
                            .Send(new DialogMessage("Fehler", "Angebot konnte nicht erstellt werden."));
                }
            };

            master.ToolbarItems.Add(_CreateSwitch);
        }

        /// <summary>
        /// When Position Select is pressed, open dialog for selection
        /// </summary>
        public void PosSel()
        {
            DialogView.IsVisible = true;
            DialogView.DialogClosed += (sender, e) => { DialogView.IsVisible = false; };
            DialogView.ShowDialog();
            SelectionMode = true;
            SetPos();
        }

        public void PositionSelected(double lat, double lon)
        {
            _ct.latitude = lat;
            _ct.longitude = lon;

            DialogView.IsVisible = false;
            SelectedText = lat + " " + lon;
            NotifyPropertyChanged("SelectedText");
        }

        private async void GetSuggestionsAsync(string value)
        {
            GeoCodeSuggestions = await MvvmNanoIoC.Resolve<GeocodeAutocompleteService>().GetSuggestions(value);
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
            var master = (MasterDetailPage) Application.Current.MainPage;
            master.ToolbarItems.Clear();
        }


        public override void Dispose()
        {
            base.Dispose();
            RemoveToolbarItem();
        }


        #region "SelectPosition"

        public async void SetLocationWithAdress()
        {
            if (Adress != null)
            {
                var response = await MvvmNanoIoC.Resolve<GeocodeService>().GetResponseForString(Adress);
                var pos = new Plugin.Geolocator.Abstractions.Position(response.results[0].geometry.location.lat,
                    response.results[0].geometry.location.lng);
                if (pos.Latitude == 0 && pos.Longitude == 0)
                {
                    MvvmNanoIoC.Resolve<IMessenger>().Send(new DialogMessage("Fehler", "Kein Ort gefunden."));
                    return;
                }
                _ct.latitude = pos.Latitude;
                _ct.longitude = pos.Longitude;

                SelectedText = Adress;
                NotifyPropertyChanged("SelectedText");

                DialogView.IsVisible = false;
            }
            else
            {
                MvvmNanoIoC.Resolve<IMessenger>().Send(new DialogMessage("Fehler", "Bitte eine Adresse eingeben"));
            }
        }

        public Position Position { get; set; }


        public PopUpDialogView DialogView { get; set; }

        private Plugin.Geolocator.Abstractions.Position _posSelect;

        public Plugin.Geolocator.Abstractions.Position PosSelect
        {
            get => _posSelect;
            set
            {
                _posSelect = value;
                Debug.WriteLine(value.Latitude);
            }
        }


        private async void SetPos()
        {
            var tempPos = await LocationService.GetInstance().GetPosition();
            Position = new Position(tempPos.Latitude, tempPos.Longitude);
            NotifyPropertyChanged("Position");
        }


        public SelectionMap map;

        #endregion
    }
}