using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using TutorScout24.Models;
using TutorScout24.Models.Chat;
using TutorScout24.Utils;
using Xamarin.Forms;

namespace TutorScout24.ViewModels
{
    public class ChatViewModel : MvvmNano.MvvmNanoViewModel<Conversation>, IThemeable
    {
        Conversation conversation;
        public ListView MessagesList;
        public ChatViewModel()
        {

            _themeColor = (Xamarin.Forms.Color)Application.Current.Resources["MainColor"];

        }

        public Action<Message> OnMessageAdded { get; set; }

        private ObservableCollection<Message> _messages = new ObservableCollection<Message>();
        public ObservableCollection<Message> Messages
        {
            get { return _messages; }
            set
            {
                _messages = value;

             
                NotifyPropertyChanged("Messages");

            }
        }

        public ICommand SendCommand => new Command(async () =>  SendMessageAsync());



        private async void SendMessageAsync()
        {
            SendMessage m = new SendMessage();
            m.toUserId = conversation.id;
            m.text = CurrentMessage;
            Debug.WriteLine(m.text);
            await MvvmNano.MvvmNanoIoC.Resolve<TutorScout24.Services.TutorScoutRestService>().SendMessage(m);
            Message mess = new SentMessage
            {
                FromUser = MvvmNano.MvvmNanoIoC.Resolve<Authentication>().userName,
                Text = m.text,
                ToUser = m.toUserId,
                Time = DateTime.Now
            };
            Messages.Add(mess);
            CurrentMessage = "";
            NotifyPropertyChanged("CurrentMessage");
            NotifyPropertyChanged("Messages");
            OnMessageAdded?.Invoke(mess);
        }

        private string _currentMessage;
        public string CurrentMessage
        {
            get { return _currentMessage; }
            set { _currentMessage = value;
               
            }
        } 

        private Message _selectedItem;
        public Message SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value;
               
            }
        } 

        public override void Initialize(Conversation parameter)
        {
            base.Initialize(parameter);
  
            if (parameter != null)
            {
                conversation = parameter;
                foreach (var item in parameter.Messages)
                {
                    if(item.MyTypeName == typeof(ReceivedMessage).Name){
                        Debug.WriteLine("received" + item.FromUser);
                    }else{
                        Debug.WriteLine("sent" + item.ToUser);
                    }
                    Messages.Add(item);
                }
                NotifyPropertyChanged("Messages");

            }

        }



        private Color _themeColor;
        public Color ThemeColor { get { return _themeColor; } set { _themeColor = value; NotifyPropertyChanged("ThemeColor"); } }
    }
}
