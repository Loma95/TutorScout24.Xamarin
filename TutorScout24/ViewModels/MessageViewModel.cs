using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmNano;
using MvvmNano.Forms;
using TutorScout24.Models;
using TutorScout24.Models.Chat;
using TutorScout24.Utils;
using Xamarin.Forms;

namespace TutorScout24.ViewModels
{
    public class MessageViewModel : MvvmNano.MvvmNanoViewModel, IThemeable, IObserver<ObservableCollection<Conversation>>,IToolBarItem
    {
        public MessageViewModel()
        {
            _themeColor = (Xamarin.Forms.Color)Application.Current.Resources["MainColor"];


            AddToolBarItem();

            MvvmNano.MvvmNanoIoC.Resolve<TutorScout24.Services.MessageService>().Subscribe(this);

            Load();



        }


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
                return new Command(() =>
                {
                    IsRefreshing = true;

                    Load();

                    IsRefreshing = false;
                });
            }
        }

   


        private void OpenCreateDialog()
        {
            AddMode = !AddMode;

        }

        public ICommand OpenCreateCommand => new Command(OpenCreateDialog);

        public ICommand AddCommand
        {
            get { return new Command(async () => AddConversation()); }
        }


        private async void AddConversation()
        {
            if (await UserExists())
            {
                Conversation newConn = new Conversation();
                newConn.id = NewConversationUser;
                newConn.Messages = new List<Message>();
                Conversations.Add(newConn);
                NotifyPropertyChanged("Conversations");
                AddMode = false;
                NewConversationUser = "";
            }
            else
            {
                MvvmNanoIoC.Resolve<IMessenger>().Send(new DialogMessage(NewConversationUser, "Benutzer existiert nicht"));
                NewConversationUser = "";
            }
        }

        private ToolbarItem _createChat = new ToolbarItem
        {
            Text = "\uf067"
        };



        private bool _addMode;
        public bool AddMode
        {
            get { return _addMode; }
            set
            {
                _addMode = value;
                NotifyPropertyChanged("AddMode");
            }
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
            set
            {

                _selectedItem = value;


            }
        }

        private string _newConversationUser;
        public string NewConversationUser
        {
            get { return _newConversationUser; }
            set
            {
                _newConversationUser = value;
                NotifyPropertyChanged("NewConversationUser");
            }
        }

    
        public void GoToChat(object sender, EventArgs e)
        {
            if (_selectedItem != null)
            {
                NavigateToAsync<ChatViewModel, Conversation>(SelectedItem);
            }
        }
       

        private ObservableCollection<Conversation> _conversations = new ObservableCollection<Conversation>();
        public ObservableCollection<Conversation> Conversations
        {
            get { return _conversations; }
            set
            {
                _conversations = value;

                foreach (Conversation item in value)
                {
                    Debug.WriteLine(item.id);
                }
                NotifyPropertyChanged("Conversations");
                Debug.WriteLine(value.Count);
            }
        }

        private void Load()
        {

            MvvmNano.MvvmNanoIoC.Resolve<TutorScout24.Services.MessageService>().ReloadMessages();

        }


        private async Task<bool> UserExists()
        {
            var UserInfo = await MvvmNano.MvvmNanoIoC.Resolve<TutorScout24.Services.TutorScoutRestService>().GetUserInfo(NewConversationUser);
            if (UserInfo != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public override void Dispose()
        {
            var master = (Pages.MasterDetailPage)Application.Current.MainPage;
            master.ToolbarItems.Remove(_createChat);
            base.Dispose();
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

        public void AddToolBarItem()
        {
            var master = (Pages.MasterDetailPage)Application.Current.MainPage;

            master.ToolbarItems.Clear();
            master.ToolbarItems.Add(_createChat);
            _createChat.Command = OpenCreateCommand;
        }

        private Color _themeColor;
        public Color ThemeColor
        {
            get { return _themeColor; }
            set
            {
                _themeColor = value;

                NotifyPropertyChanged("ThemeColor");

            }
        }
    }
}
