using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using MvvmNano.Forms;
using TutorScout24.Controls;
using TutorScout24.ViewModels;
using Xamarin.Forms;

namespace TutorScout24.Pages
{
    public partial class PositionSelectPage
    {
        public PositionSelectPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            ViewModel.map = MyMap2;
        }
    }
}
