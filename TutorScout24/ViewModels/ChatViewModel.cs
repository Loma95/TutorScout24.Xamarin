using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using TutorScout24.Models.Chat;
using TutorScout24.Utils;
using Xamarin.Forms;

namespace TutorScout24.ViewModels
{
    public class ChatViewModel : MvvmNano.MvvmNanoViewModel<List<Message>>, IThemeable
    {
        
        public ChatViewModel()
        {

            _themeColor = (Xamarin.Forms.Color)Application.Current.Resources["MainColor"];

        }


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

        private Message _selectedItem;
        public Message SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value;
               
            }
        } 

        public override void Initialize(List<Message> parameter)
        {
            base.Initialize(parameter);
  
            if (parameter != null)
            {
                
                foreach (var item in parameter)
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
