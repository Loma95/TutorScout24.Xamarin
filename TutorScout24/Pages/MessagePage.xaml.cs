using System;
using System.Collections.Generic;
using System.Diagnostics;
using TutorScout24.Models;
using TutorScout24.Utils;
using TutorScout24.ViewModels;
using Xamarin.Forms;

namespace TutorScout24.Pages
{
    public partial class MessagePage 
    {
        public MessagePage()
        {
            InitializeComponent();

          
        }
        protected override void OnAppearing()
        {
            ViewModel.AddToolBarItem();
            base.OnAppearing();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            try
            {
                List.ItemTapped += new SingleClick(ViewModel.GoToChat).Click;
            }catch(Exception){
                
            }
            }
    }
}
