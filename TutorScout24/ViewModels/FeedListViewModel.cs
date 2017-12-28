using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using MvvmNano;
using TutorScout24.CustomData;
using TutorScout24.Models.Tutorings;
using TutorScout24.Services;
using TutorScout24.Utils;
using Xamarin.Forms;

namespace TutorScout24.ViewModels
{
    public class FeedListViewModel : MvvmNanoViewModel, IThemeable
    {
        private bool _isRefreshing;

        private Color _themeColor;


        private ObservableCollection<Tutoring> _tut = new ObservableCollection<Tutoring>();

        /// <summary>
        /// Get Tutorings and subscribe to mode changes
        /// </summary>
        public FeedListViewModel()
        {
            GetTutoringsAsync(MasterDetailViewModel.CurrentMode);
            ThemeColor = (Color) Application.Current.Resources["MainColor"];
            MvvmNanoIoC.Resolve<IMessenger>().Subscribe(this, (object arg1, ChangeModeMessage arg2) =>
            {
                _tut = new ObservableCollection<Tutoring>();
                NotifyPropertyChanged("Tutorings");
                Debug.WriteLine("updateColor");
                ThemeColor = (Color) Application.Current.Resources["MainColor"];
                GetTutoringsAsync(arg2.NewMode);
            });
        }

        public Tutoring CurrentItem { get; set; }

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                _isRefreshing = value;
                NotifyPropertyChanged("IsRefreshing");
            }
        }


        public ICommand RefreshCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsRefreshing = true;

                    GetTutoringsAsync(MasterDetailViewModel.CurrentMode);
                });
            }
        }


        public ObservableCollection<Tutoring> Tutorings
        {
            get => _tut;
            set
            {
                _tut = value;
                NotifyPropertyChanged("Tutorings");
            }
        }

        public Color ThemeColor
        {
            get => _themeColor;
            set
            {
                _themeColor = value;
                NotifyPropertyChanged("ThemeColor");
            }
        }


        public void GoToDetailPage(object sender, EventArgs e)
        {
            NavigateToAsync<TutoringDetailViewModel, Tutoring>(CurrentItem);
        }

        /// <summary>
        /// Get Tutorings for given mode via service
        /// </summary>
        /// <param name="mode">mode for which to get items</param>
        private async void GetTutoringsAsync(MasterDetailViewModel.Mode mode)
        {
            var tutServ = MvvmNanoIoC.Resolve<TutorScoutRestService>();
            List<Tutoring> offers;
            offers = await tutServ.GetTutorings();
            if (offers != null)
            {
                IsRefreshing = false;
                _tut = new ObservableCollection<Tutoring>(offers);
                NotifyPropertyChanged("Tutorings");
                Debug.WriteLine(offers + "Size::::" + offers.Count);
                foreach (var VARIABLE in offers)
                    VARIABLE.daysLeft = (int) (VARIABLE.expirationDate - DateTime.Today).TotalDays;
            }
        }
    }
}