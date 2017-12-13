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
            MessageViewModel VM = (MessageViewModel)BindingContext;
            VM.AddToolBarButton();
            base.OnAppearing();
        }

        Boolean isRunning = false;


        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            MessageViewModel VM = (MessageViewModel)BindingContext;

            List.ItemTapped += new SingleClick(VM.GoToChat).Click;

            }
    }
}
