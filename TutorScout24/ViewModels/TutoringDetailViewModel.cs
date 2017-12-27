using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmNano;
using TutorScout24.Models;
using TutorScout24.Models.Chat;
using TutorScout24.Services;
using TutorScout24.Utils;
using Xamarin.Forms;

namespace TutorScout24.ViewModels
{
    public class TutoringDetailViewModel : MvvmNanoViewModel<Tutoring>,IToolBarItem
    {
        private Tutoring _tutoring;

        public Tutoring Tutoring
        {
            get { return _tutoring; }
            set
            {
                _tutoring = value;
                NotifyPropertyChanged();
            }
        }
        

        public void ToProfile(object sender, EventArgs e)
        {
            NavigateTo<ForeignProfileViewModel, string>(Tutoring.userName);
        }

        public override void Initialize(Tutoring parameter)
        {
            base.Initialize(parameter);
            Tutoring = parameter;
        }


        public ICommand GoToChatCommand => new Command(GoToChat);

        private void GoToChat(){
            MvvmNanoIoC.Resolve<MessageService>().ReloadMessages();
            Debug.WriteLine(Tutoring.userName);
          
            Conversation conn = MvvmNanoIoC.Resolve<MessageService>().GetConversationById(Tutoring.userName);
            NavigateToAsync<ChatViewModel, Conversation>(conn);
        }

        public void AddToolBarItem()
        {
            var master = (Pages.MasterDetailPage)Application.Current.MainPage;
            if (master != null)
            {
                master.ToolbarItems.Clear();
            }

        }
    }
}
