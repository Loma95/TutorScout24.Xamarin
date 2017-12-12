using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
    public class ChatViewModel : MvvmNano.MvvmNanoViewModel<Conversation>, IThemeable,ConversationObserver
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

        public ICommand SendCommand => new Command(SendMessageAsync);




        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                NotifyPropertyChanged("IsRefreshing");
            }
        }


        public ICommand RefreshCommand
        {
            get
            {
                return new Command( ()=>
                {
                   
                     Reload();

                });
            }
        }


        private async void SendMessageAsync()
        {
            if (CurrentMessage != "")
            {
                SendMessage m = new SendMessage();
                m.toUserId = conversation.id;
                m.text = CurrentMessage;
             
                await MvvmNano.MvvmNanoIoC.Resolve<TutorScout24.Services.TutorScoutRestService>().SendMessage(m);
           
                Reload();
                CurrentMessage = "";
                NotifyPropertyChanged("CurrentMessage");

            }
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
        private ToolbarItem _reload = new ToolbarItem
        {
            Text = "\uf021"
        };

        public override void Initialize(Conversation parameter)
        {
            base.Initialize(parameter);

            if (parameter != null)
            {
                conversation = parameter;
                foreach (var item in parameter.Messages)
                {
                    Messages.Add(item);
                }
                NotifyPropertyChanged("Messages");
                 
            }

     

            var master = (Pages.MasterDetailPage)Application.Current.MainPage;

            _reload.Clicked += (sender, e) => {
                Reload();
            };
            master.ToolbarItems.Clear();
            master.ToolbarItems.Add(_reload);
            MvvmNano.MvvmNanoIoC.Resolve<MessageService>().Subscribe(this);
          

        }

        private  void Reload()
        {
            IsRefreshing = true;
            MvvmNano.MvvmNanoIoC.Resolve<TutorScout24.Services.MessageService>().GetMessages();

        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(Conversation value)
        {
            IsRefreshing = false;
            Messages = new ObservableCollection<Message>(value.Messages);
            NotifyPropertyChanged("Messages");
            if(Messages.Count > 1)
            OnMessageAdded?.DynamicInvoke(Messages[Messages.Count - 1]);
        }


        public void RemoveToolbarItem()
        {
            var master = (Pages.MasterDetailPage)Application.Current.MainPage;
            master.ToolbarItems.Remove(_reload);
        }

        private Color _themeColor;
        public Color ThemeColor { get { return _themeColor; } set { _themeColor = value; NotifyPropertyChanged("ThemeColor"); } }

        public string ConversationId { get => conversation.id; set => conversation.id = value; }
    }
}
