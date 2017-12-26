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
            VM.AddToolBarItem();
            base.OnAppearing();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            MessageViewModel VM = (MessageViewModel)BindingContext;
            try
            {
                List.ItemTapped += new SingleClick(ViewModel.GoToChat).Click;
            }catch(Exception){
                
            }
            }
    }
}
