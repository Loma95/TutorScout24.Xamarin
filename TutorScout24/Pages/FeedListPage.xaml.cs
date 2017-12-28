using System;
using System.Collections.Generic;
using System.Diagnostics;
using MvvmNano;
using MvvmNano.Forms;
using Plugin.Geolocator;
using TutorScout24.Services;
using TutorScout24.Utils;
using TutorScout24.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TutorScout24.Pages
{
    public partial class FeedListPage
    {

        public FeedListPage()
        {
            InitializeComponent();


        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            try
            {
                MyListView.ItemTapped += new SingleClick(ViewModel.GoToDetailPage).Click;
            }
            catch (Exception e)
            { }

        }



    }
}
