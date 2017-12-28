using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmNano;
using TutorScout24.CustomData;
using TutorScout24.Models.Chat;
using TutorScout24.Services;
using TutorScout24.Utils;
using Xamarin.Forms;
using MasterDetailPage = TutorScout24.Pages.MasterDetailPage;

namespace TutorScout24.ViewModels
{
    public class MessageViewModel : MvvmNanoViewModel, IThemeable, IObserver<ObservableCollection<Conversation>>,
        IToolBarItem
    {
        private bool _addMode;


        private ObservableCollection<Conversation> _conversations = new ObservableCollection<Conversation>();

        private readonly ToolbarItem _createChat = new ToolbarItem
        {
            Text = "\uf067"
        };


        private bool _isRefreshing;

        private string _newConversationUser;

        private Color _themeColor;

        public MessageViewModel()
        {
            _themeColor = (Color) Application.Current.Resources["MainColor"];


            AddToolBarItem();

            MvvmNanoIoC.Resolve<MessageService>().Subscribe(this);

            Load();
        }

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

                    Load();

                    IsRefreshing = false;
                });
            }
        }

        public ICommand OpenCreateCommand => new Command(OpenCreateDialog);

        public ICommand AddCommand
        {
            get { return new Command(async () => AddConversation()); }
        }

        public bool AddMode
        {
            get => _addMode;
            set
            {
                _addMode = value;
                NotifyPropertyChanged("AddMode");
            }
        }

        public List<RestMessage> AllMessages { get; set; }

        public Conversation SelectedItem { get; set; } = new Conversation();

        public string NewConversationUser
        {
            get => _newConversationUser;
            set
            {
                _newConversationUser = value;
                NotifyPropertyChanged("NewConversationUser");
            }
        }

        public ObservableCollection<Conversation> Conversations
        {
            get => _conversations;
            set
            {
                _conversations = value;

                foreach (var item in value)
                    Debug.WriteLine(item.id);
                NotifyPropertyChanged("Conversations");
                Debug.WriteLine(value.Count);
            }
        }


        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(ObservableCollection<Conversation> value)
        {
            Conversations = new ObservableCollection<Conversation>(value);
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
            master.ToolbarItems.Add(_createChat);
            _createChat.Command = OpenCreateCommand;
        }


        private void OpenCreateDialog()
        {
            AddMode = !AddMode;
        }


        private async void AddConversation()
        {
            if (await UserExists())
            {
                var newConn = new Conversation();
                newConn.id = NewConversationUser;
                newConn.Messages = new List<Message>();
                Conversations.Add(newConn);
                NotifyPropertyChanged("Conversations");
                AddMode = false;
                NewConversationUser = "";
            }
            else
            {
                MvvmNanoIoC.Resolve<IMessenger>()
                    .Send(new DialogMessage(NewConversationUser, "Benutzer existiert nicht"));
                NewConversationUser = "";
            }
        }


        public void GoToChat(object sender, EventArgs e)
        {
            if (SelectedItem != null)
                NavigateToAsync<ChatViewModel, Conversation>(SelectedItem);
        }

        private void Load()
        {
            MvvmNanoIoC.Resolve<MessageService>().ReloadMessages();
        }


        private async Task<bool> UserExists()
        {
            var UserInfo = await MvvmNanoIoC.Resolve<TutorScoutRestService>().GetUserInfo(NewConversationUser);
            if (UserInfo != null)
                return true;
            return false;
        }

        public override void Dispose()
        {
            var master = (MasterDetailPage) Application.Current.MainPage;
            master.ToolbarItems.Remove(_createChat);
            base.Dispose();
        }
    }
}