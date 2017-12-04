using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using MvvmNano;
using MvvmNano.Forms;
using TutorScout24.Models;
using TutorScout24.Models.Chat;
using TutorScout24.Utils;
using Xamarin.Forms;

namespace TutorScout24.ViewModels
{
    public class MessageViewModel : MvvmNano.MvvmNanoViewModel, IThemeable
    {
        public MessageViewModel()
        {
            _themeColor = (Xamarin.Forms.Color)Application.Current.Resources["MainColor"];

            /* SendMessage m = new SendMessage();
         m.toUserId = "Test1234";
         m.text = "Hallo";
         MvvmNano.MvvmNanoIoC.Resolve<TutorScout24.Services.TutorScoutRestService>().SendMessage(m);*/
            GetAll();
        }


        public List<RestMessage> AllMessages
        {
            get;
            set;
        }

        private Conversation _selectedItem = new Conversation();
        public Conversation SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value;
                if (value != null)
                {
                    NavigateTo<ChatViewModel, List<Message>>(value.Messages);
                }
            }
        } 


        private ObservableCollection<Conversation> _conversations = new ObservableCollection<Conversation>();
        public ObservableCollection<Conversation> Conversations 
        {
            get{return _conversations;}
            set{ _conversations = value;

                foreach (Conversation item in value)
                {
                    Debug.WriteLine(item.id);
                }
                NotifyPropertyChanged("Conversations");
                Debug.WriteLine(value.Count);
            }
        }

    


        private async void GetAll(){
            AllMessages = new System.Collections.Generic.List<RestMessage>();
            List<RestMessage>  sentM = await GetSent();
            List<RestMessage>  recM = await GetReceived();
            AllMessages.AddRange((System.Collections.Generic.IEnumerable<TutorScout24.Models.RestMessage>)recM);
            AllMessages.AddRange((System.Collections.Generic.IEnumerable<TutorScout24.Models.RestMessage>)sentM);
            if (AllMessages != null)
            {
                foreach (RestMessage item in AllMessages)
                {
                  
                    Conversation toAdd = GetConversationById(item.fromUserId);
                    Message m;
                    if (item.fromUserId == MvvmNanoIoC.Resolve<Authentication>().userName){
                        m = new SentMessage();
                    }else{
                        m = new ReceivedMessage();
                    }
                        
                    m.Text = item.text;
                    m.Time = item.datetime;
                    m.ToUser = item.toUserId;
                    m.FromUser = item.fromUserId;
                    toAdd.id = item.fromUserId;
                    toAdd.Messages.Add(m);

                    if(CheckIfConversationIsNew(item.fromUserId)){
                        Conversations.Add(toAdd);
                        Conversations = _conversations;
                    }

                }
            }
        }


        private Conversation GetConversationById(string id){
            foreach (Conversation item in _conversations)
            {
                if (item.id == id)
                {
                    return item;
                }
            }
            return new Conversation();
        }


        private bool CheckIfConversationIsNew(string id){
            foreach (Conversation item in _conversations)
            {
                if(item.id == id){
                    return false;
                }
            }
            return true;
        }

        private async Task<List<RestMessage>> GetReceived(){
            return await MvvmNano.MvvmNanoIoC.Resolve<TutorScout24.Services.TutorScoutRestService>().GetReceivedMessages();
        }


        private async Task<List<RestMessage>> GetSent(){
            return await MvvmNano.MvvmNanoIoC.Resolve<TutorScout24.Services.TutorScoutRestService>().GetSentMessages();

        }

        private Color _themeColor;
        public Color ThemeColor { get { return _themeColor; } set { _themeColor = value;

                NotifyPropertyChanged("ThemeColor");

            } }
    }
}
