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
    public class MessageViewModel : MvvmNano.MvvmNanoViewModel, IThemeable
    {
        public MessageViewModel()
        {
            _themeColor = (Xamarin.Forms.Color)Application.Current.Resources["MainColor"];
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
                if(value != null)
                NavigateTo<ChatViewModel, Conversation>(SelectedItem);
               
          
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
            Conversations = new ObservableCollection<Conversation>(await MvvmNano.MvvmNanoIoC.Resolve<TutorScout24.Services.MessageService>().GetAllAsync());
        }

        private Color _themeColor;
        public Color ThemeColor { get { return _themeColor; } set { _themeColor = value;

                NotifyPropertyChanged("ThemeColor");

            } }
    }
}
