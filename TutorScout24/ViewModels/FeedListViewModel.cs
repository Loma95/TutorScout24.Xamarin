using MvvmNano;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var tutServ = MvvmNanoIoC.Resolve<TutorScoutRestService>();

        
            _tut = new ObservableCollection<Tutoring>(tutServ.GetTutorings());
            _themeColor = (Xamarin.Forms.Color)Application.Current.Resources["MainColor"];
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
    }
}
