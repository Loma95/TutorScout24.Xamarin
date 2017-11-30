using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmNano;
using TutorScout24.CustomData;
using TutorScout24.Models;
using TutorScout24.Services;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TutorScout24.ViewModels
{
    public class PositionSelectViewModel: MvvmNanoViewModel<CreateTutoring>
    {
        private CreateTutoring _ct;
        private Xamarin.Forms.Maps.Position _position;

        public Xamarin.Forms.Maps.Position Position
        {
            get { return _position; }
            set { _position = value; }
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


        public PositionSelectViewModel()
        {
            SetPos();
        }

        private async void SetPos()
        {
            var tempPos= await LocationService.getInstance().GetPosition();
            _position = new Position(tempPos.Latitude, tempPos.Longitude);
            NotifyPropertyChanged("Position");
        }

        public override void Initialize(CreateTutoring parameter)
        {
            base.Initialize(parameter);
            _ct = parameter;
        }
    }
}
