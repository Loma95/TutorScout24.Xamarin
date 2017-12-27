using System;
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

            selectButton.Clicked += (sender, e) => {
                if (ViewModel.ShowMap)
                {
                    ViewModel.PositionSelected(MyMap2.VisibleRegion.Center.Latitude, MyMap2.VisibleRegion.Center.Longitude);
                }else{
                    ViewModel.SetLocationWithAdress();
                }
            };
        }


        protected override bool OnBackButtonPressed()
        {
            ViewModel.RemoveToolbarItem();
            return base.OnBackButtonPressed();
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            
            SuggestionListView.ItemTapped += (sender, e) =>
            {
                AdressEntry.Text = e.Item.ToString();
            };

            ViewModel.map = MyMap2;
            ViewModel.DialogView = PopUpDialog;
           
        }
    }
}
