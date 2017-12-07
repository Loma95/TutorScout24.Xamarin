using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using MvvmNano;
using TutorScout24.Models;
using TutorScout24.Models.Chat;

namespace TutorScout24.Services
{
    public class MessageService  
    {
        public MessageService()
        {


        }



        private List<ConversationObserver> observers = new List<ConversationObserver>();
        public ObservableCollection<Conversation> Conversations = new ObservableCollection<Conversation>();

        public async Task<ObservableCollection<Conversation>> GetAllAsync()
        {
            Debug.WriteLine("Lets start");
            Conversations.Clear();

            List<RestMessage> AllMessages = new System.Collections.Generic.List<RestMessage>();
            List<RestMessage> sentM = await GetSent();
            List<RestMessage> recM = await GetReceived();
            AllMessages.AddRange((System.Collections.Generic.IEnumerable<TutorScout24.Models.RestMessage>)recM);
            AllMessages.AddRange((System.Collections.Generic.IEnumerable<TutorScout24.Models.RestMessage>)sentM);

            Debug.WriteLine("I got " + AllMessages.Count);
            if (AllMessages != null)
            {
                foreach (RestMessage item in AllMessages)
                {

                    Conversation toAdd;
                    Message m;
                    if (item.fromUserId == MvvmNanoIoC.Resolve<Authentication>().userName)
                    {
                        toAdd = GetConversationById(item.toUserId);
                        m = new SentMessage();
                        toAdd.id = item.toUserId;
                    }
                    else
                    {
                        toAdd = GetConversationById(item.fromUserId);
                        m = new ReceivedMessage();
                        toAdd.id = item.fromUserId;
                    }

                    m.Text = item.text;
                    m.Time = item.datetime;
                    m.ToUser = item.toUserId;
                    m.FromUser = item.fromUserId;

                    toAdd.Messages.Add(m);

                    if (CheckIfConversationIsNew(item.fromUserId))
                    {
                        Conversations.Add(toAdd);

                    }
                }


            }


            foreach (Conversation item in Conversations)
            {
                item.Messages.Sort(delegate (Message m1, Message m2) { return m1.Time.CompareTo(m2.Time); });
            }

            Debug.WriteLine(observers.Count);
            foreach (var obs in observers)
            {
                Debug.WriteLine("OnNext");
                obs.OnNext(GetConversationById(obs.ConversationId));
            }
            Debug.WriteLine("Im finished");
            return Conversations;
        }

        public Conversation GetConversationById(string id)
        {
            foreach (Conversation item in Conversations)
            {
                if (item.id == id)
                {
                    return item;
                }
            }
            return new Conversation();
        }


        private bool CheckIfConversationIsNew(string id)
        {
            foreach (Conversation item in Conversations)
            {
                if (item.id == id)
                {
                    return false;
                }
            }
            return true;
        }


        private async Task<List<RestMessage>> GetSent()
        {
            return await MvvmNano.MvvmNanoIoC.Resolve<TutorScoutRestService>().GetSentMessages();
        }

        private async Task<List<RestMessage>> GetReceived()
        {
            return await MvvmNano.MvvmNanoIoC.Resolve<TutorScoutRestService>().GetReceivedMessages();
        }

        public IDisposable Subscribe(ConversationObserver observer)
        {
            observers.Add(observer);
            return null;
        }
    }
}
