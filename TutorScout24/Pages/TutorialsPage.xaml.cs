using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using TutorScout24.Controls;
using TutorScout24.ViewModels;
using Xamarin.Forms;

namespace TutorScout24
{
    public partial class TutorialsPage
    {
        public TutorialsPage()
        {

            InitializeComponent();


       BindingContext = new TutorialsViewModel();

    }
      
        public override void OnViewModelSet()
        {
            base.OnViewModelSet();
            ViewModel.AddToolBarItem();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.AddToolBarItem();
        }

    }
}
