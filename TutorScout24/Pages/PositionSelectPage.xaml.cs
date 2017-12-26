using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using MvvmNano.Forms;
using TutorScout24.Controls;
using TutorScout24.Utils;
using TutorScout24.ViewModels;
using Xamarin.Forms;

namespace TutorScout24.Pages
{
    public partial class PositionSelectPage
    {
        public event EventHandler PositionSelected;

        public PositionSelectPage()
        {
            InitializeComponent();

            selectButton.Clicked += (sender, e) => {
                PositionSelected(this, new SelectedLocationArgs(MyMap2.VisibleRegion.Center.Latitude, MyMap2.VisibleRegion.Center.Longitude));
            };

          
        }

  
    
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            ViewModel.map = MyMap2;

        }

      
    }
}
