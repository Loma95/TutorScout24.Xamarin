using System;
using System.Diagnostics;
using System.Windows.Input;
using MvvmNano;
using TutorScout24.Models.Chat;
using TutorScout24.Models.Tutorings;
using TutorScout24.Services;
using TutorScout24.Utils;
using Xamarin.Forms;
using MasterDetailPage = TutorScout24.Pages.MasterDetailPage;

namespace TutorScout24.ViewModels
{
    public class TutoringDetailViewModel : MvvmNanoViewModel<Tutoring>, IToolBarItem
    {
        private Tutoring _tutoring;

        public Tutoring Tutoring
        {
            get => _tutoring;
            set
            {
                _tutoring = value;
                NotifyPropertyChanged();
            }
        }


        public ICommand GoToChatCommand => new Command(GoToChat);

        public void AddToolBarItem()
        {
            var master = (MasterDetailPage) Application.Current.MainPage;
            if (master != null)
                master.ToolbarItems.Clear();
        }

        /// <summary>
        ///     On UserFrame Press, navigate to that user's profile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ToProfile(object sender, EventArgs e)
        {
            NavigateTo<ForeignProfileViewModel, string>(Tutoring.userName);
        }

        public override void Initialize(Tutoring parameter)
        {
            base.Initialize(parameter);
            Tutoring = parameter;
        }

        /// <summary>
        ///     Navigate to chat on button press
        /// </summary>
        private void GoToChat()
        {
            MvvmNanoIoC.Resolve<MessageService>().ReloadMessages();
            Debug.WriteLine(Tutoring.userName);

            var conn = MvvmNanoIoC.Resolve<MessageService>().GetConversationById(Tutoring.userName);
            NavigateToAsync<ChatViewModel, Conversation>(conn);
        }
    }
}