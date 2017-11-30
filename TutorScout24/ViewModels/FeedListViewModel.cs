using MvvmNano;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorScout24.CustomData;
using TutorScout24.Models;
using TutorScout24.Services;
using TutorScout24.Utils;
using Xamarin.Forms;

namespace TutorScout24.ViewModels
{
    public class FeedListViewModel : MvvmNanoViewModel,IThemeable
    {
        private Tutoring myVar;

        public Tutoring CurrentItem
        {
            get { return myVar; }
            set { myVar = value; }
        } 

        public FeedListViewModel()
        {
            GetTutoringsAsync(MasterDetailViewModel.CurrentMode);
            _themeColor = (Xamarin.Forms.Color)Application.Current.Resources["MainColor"];
            MvvmNanoIoC.Resolve<IMessenger>().Subscribe(this, (object arg1, ChangeModeMessage arg2) =>
            {
                _tut = new ObservableCollection<Tutoring>();
                NotifyPropertyChanged("Tutorings");
                GetTutoringsAsync(arg2.newMode);
            });
        }

        private ObservableCollection<Tutoring> _tut = new ObservableCollection<Tutoring>();
        public ObservableCollection<Tutoring> Tutorings
        {
            get { return _tut; }
            set
            {
                _tut = value;
                NotifyPropertyChanged("Tutorings");

            }
        }

        private Color _themeColor;
        public Color ThemeColor { get{ return _themeColor; } set { _themeColor = value; NotifyPropertyChanged("ThemeColor");} }

        private async void GetTutoringsAsync(MasterDetailViewModel.Mode mode)
        {
            var tutServ = MvvmNanoIoC.Resolve<TutorScoutRestService>();
            List<Tutoring> offers;
            offers = await tutServ.GetTutorings();
            
            _tut = new ObservableCollection<Tutoring>(offers);
            NotifyPropertyChanged("Tutorings");
            Debug.WriteLine(offers + "Size::::" + offers.Count);
            foreach (var VARIABLE in offers)
            {
                Debug.WriteLine(VARIABLE.text);
            }
        }
    }
}
