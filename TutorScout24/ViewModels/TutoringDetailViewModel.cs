using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmNano;
using TutorScout24.Models;

namespace TutorScout24.ViewModels
{
    public class TutoringDetailViewModel : MvvmNanoViewModel<Tutoring>
    {
        private Tutoring _tutoring;

        public Tutoring Tutoring
        {
            get { return _tutoring; }
            set
            {
                _tutoring = value;
                NotifyPropertyChanged();
            }
        }

        public override void Initialize(Tutoring parameter)
        {
            base.Initialize(parameter);
            Tutoring = parameter;
        }
    }
}
