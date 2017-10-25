using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MvvmNano;
using MvvmNano.Forms.MasterDetail;

namespace TutorScout24.ViewModels
{
    public class MasterDetailViewModel : MvvmNanoViewModel
    {

        private string _username;

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                NotifyPropertyChanged(nameof(Username));
            }
        }

        public MvvmNanoCommand LogoutCommand
        {
            get { return new MvvmNanoCommand(Logout); }
        }

        private void Logout()
        {
            NavigateTo<CurrentLocationWeatherViewModel>();
        }

        public MasterDetailViewModel()
        {
            
        }
       
    }

}
