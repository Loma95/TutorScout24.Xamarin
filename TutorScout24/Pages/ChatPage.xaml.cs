using System;
using System.Collections.Generic;
using System.Diagnostics;
using TutorScout24.Models.Chat;
using TutorScout24.ViewModels;
using Xamarin.Forms;
using TutorScout24.Utils;

namespace TutorScout24.Pages
{
    public partial class ChatPage
    {
        public IList<ToolbarItem> ToolBarItems { get => new List<ToolbarItem> { ViewModel._reload }; }

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
            ViewModel.RemoveToolBarItem();
        }

        void OnDelete(object sender, EventArgs e)
        {
            var item = (MenuItem)sender;
            Message selMess = (Message)item.CommandParameter;
            ViewModel.DeleteSelectedItem(selMess.ID);
        }

    

        public override void OnViewModelSet()
        {
            base.OnViewModelSet();

            ChatViewModel vM = (ChatViewModel)BindingContext;

            vM.OnMessageAdded = message =>
            {

                Device.BeginInvokeOnMainThread(() =>
                {


                    MessagesList.ScrollTo(vM.Messages[vM.Messages.Count - 1], ScrollToPosition.MakeVisible, true);

                });

            };

            if (vM.Messages.Count > 0)
                MessagesList.ScrollTo(vM.Messages[vM.Messages.Count - 1], ScrollToPosition.MakeVisible, false);
        }
    }
}
