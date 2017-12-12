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
using Xamarin.Forms;

namespace TutorScout24.ViewModels
{
    public class TutoringDetailViewModel : MvvmNanoViewModel<Tutoring>
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

        public override void Initialize(Tutoring parameter)
        {
            base.Initialize(parameter);
            Tutoring = parameter;
        }


        public ICommand GoToChatCommand => new Command(GoToChat);

        private void GoToChat(){
            MvvmNanoIoC.Resolve<MessageService>().GetMessages();
            Debug.WriteLine(Tutoring.userName);
          
           Conversation conn = MvvmNanoIoC.Resolve<MessageService>().GetConversationById(Tutoring.userName);
            Debug.WriteLine(conn.Messages.Count);
            NavigateToAsync<ChatViewModel, Conversation>(conn);
        }



    }
}
