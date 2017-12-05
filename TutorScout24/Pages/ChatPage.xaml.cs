using System;
using System.Collections.Generic;
using TutorScout24.ViewModels;
using Xamarin.Forms;

namespace TutorScout24.Pages
{
    public partial class ChatPage 
    {
        public ChatPage()
        {
            InitializeComponent();


        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

          
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
           
        }
        public override void OnViewModelSet()
        {
            base.OnViewModelSet();

            ChatViewModel vM = (ChatViewModel)BindingContext;
            vM.OnMessageAdded = message =>
            { 
                MessagesList.ScrollTo(message, ScrollToPosition.MakeVisible, true);
            };

            MessagesList.ScrollTo(vM.Messages[vM.Messages.Count-1], ScrollToPosition.MakeVisible, true);
        }
    }
}
