using System;
using System.Collections.Generic;
using System.Diagnostics;
using TutorScout24.Models;
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
            MessageViewModel VM = (MessageViewModel)BindingContext;
            VM.AddToolBarButton();
            base.OnAppearing();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            MessageViewModel VM = (MessageViewModel)BindingContext;

            List.ItemTapped += (sender, e) => {
                VM.GoToChat();
            };
        }
    }
}
