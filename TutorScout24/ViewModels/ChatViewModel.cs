using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MvvmNano;
using TutorScout24.Models.Chat;
using TutorScout24.Services;
using TutorScout24.Utils;
using Xamarin.Forms;
using MasterDetailPage = TutorScout24.Pages.MasterDetailPage;

namespace TutorScout24.ViewModels
{
    public class ChatViewModel : MvvmNanoViewModel<Conversation>, IThemeable, ConversationObserver, IToolBarItem
    {
        private bool _isRefreshing;

        private ObservableCollection<Message> _messages = new ObservableCollection<Message>();


        public ToolbarItem _reload = new ToolbarItem
        {
            Text = "\uf021"
        };


        private Color _themeColor;
        private Conversation conversation;
        public ListView MessagesList;

        public ChatViewModel()
        {
            _themeColor = (Color) Application.Current.Resources["MainColor"];
        }


        public Action<Message> OnMessageAdded { get; set; }

        public ObservableCollection<Message> Messages
        {
            get => _messages;
            set
            {
                _messages = value;

                NotifyPropertyChanged("Messages");
            }
        }

        public ICommand SendCommand => new Command(SendMessageAsync);

        public bool IsRefreshing
        {
            get => _isRefreshing;
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
                return new Command(() =>
                {
                    IsRefreshing = true;
                    Reload();
                });
            }
        }

        public string CurrentMessage { get; set; }

        public Message SelectedItem { get; set; }

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
            if (Messages.Count > 1)
                OnMessageAdded?.DynamicInvoke(Messages[Messages.Count - 1]);
        }

        public string ConversationId
        {
            get => conversation.id;
            set => conversation.id = value;
        }

        public Color ThemeColor
        {
            get => _themeColor;
            set
            {
                _themeColor = value;
                NotifyPropertyChanged("ThemeColor");
            }
        }

        public void AddToolBarItem()
        {
            var master = (MasterDetailPage) Application.Current.MainPage;
            master.ToolbarItems.Clear();
            master.ToolbarItems.Add(_reload);
        }


        private async void SendMessageAsync()
        {
            if (CurrentMessage != "")
            {
                var m = new SendMessage();
                m.toUserId = conversation.id;
                m.text = CurrentMessage;

                await MvvmNanoIoC.Resolve<TutorScoutRestService>().SendMessage(m);

                Reload();
                CurrentMessage = "";
                NotifyPropertyChanged("CurrentMessage");
            }
        }


        public async void DeleteSelectedItem(int messageID)
        {
            await MvvmNanoIoC.Resolve<TutorScoutRestService>().DeleteMessage(messageID);
            Reload();
        }


        public override void Initialize(Conversation parameter)
        {
            base.Initialize(parameter);

            if (parameter != null)
            {
                conversation = parameter;
                foreach (var item in parameter.Messages)
                    Messages.Add(item);
                NotifyPropertyChanged("Messages");
            }


            var master = (MasterDetailPage) Application.Current.MainPage;

            _reload.Clicked += (sender, e) => { Reload(); };


            AddToolBarItem();
            MvvmNanoIoC.Resolve<MessageService>().Subscribe(this);
        }


        private void Reload()
        {
            MvvmNanoIoC.Resolve<MessageService>().ReloadMessages();
        }


        public void RemoveToolBarItem()
        {
            var master = (MasterDetailPage) Application.Current.MainPage;
            master.ToolbarItems.Clear();
        }
    }
}