﻿using System;
using System.Collections.Generic;
using System.Windows.Input;
using TutorScout24.Controls;
using TutorScout24.Utils;
using TutorScout24.ViewModels;
using Xamarin.Forms;

namespace TutorScout24.Pages
{
    public partial class CreatePage
    {
        public CreatePage()
        {
            InitializeComponent();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.AddToolbarItem();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            
            SuggestionListView.ItemTapped += (sender, e) =>
            {
                AdressEntry.Text = e.Item.ToString();
            };
           
        }
    }
}
