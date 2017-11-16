using MvvmNano;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorScout24.Models;
using TutorScout24.Services;

namespace TutorScout24.ViewModels
{
    public class FeedListViewModel : MvvmNanoViewModel
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
    }
}
