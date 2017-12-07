using System;
using System.Collections.Generic;
using System.Diagnostics;
using TutorScout24.ViewModels;
using Xamarin.Forms;

namespace TutorScout24.Pages
{
    public partial class ChatPage 
    {
        public ChatPage()
        {
            InitializeComponent();

            send.FontFamily = "fontawesome";
            send.Text = "\xf1d8";
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

          
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
           
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ChatViewModel vM = (ChatViewModel)BindingContext;
            vM.RemoveToolbarItem();
        }
        public override void OnViewModelSet()
        {
            base.OnViewModelSet();

            ChatViewModel vM = (ChatViewModel)BindingContext;


            vM.OnMessageAdded = message =>
            {
                Debug.WriteLine("OnMessageAdded");
                Debug.WriteLine(message.Text);
                MessagesList.ScrollTo(message, ScrollToPosition.End, true);
            };

            MessagesList.ScrollTo(vM.Messages[vM.Messages.Count-1], ScrollToPosition.MakeVisible, false);
        }
    }
}
