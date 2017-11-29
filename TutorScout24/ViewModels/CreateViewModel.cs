using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorScout24.ViewModels
{
    public class CreateViewModel : MvvmNano.MvvmNanoViewModel
    {
        private double _duration = 10;
        private DateTime _expDate = DateTime.Today.AddDays(10);
        private DateTime _minDate = DateTime.Today;
        private DateTime _maxDate = DateTime.Today.AddDays(100);
        private PosSelection _selection = PosSelection.MAP;

        public enum PosSelection{ADRESS, MAP }

        public String Subject { get; set; }
        public String Text { get; set; }

        public List<string> Selections
        {
            get
            {
                return Enum.GetNames(typeof(PosSelection)).Select(b => b).ToList();
            }
        }

        public PosSelection Selection
        {
            get { return _selection; }
            set { _selection = value; }
        }

        public DateTime ExpDate
        {
            get { return _expDate; }
            set { _expDate = value; }
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
    }
}