using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using MvvmNano;
using TutorScout24.Models;
using TutorScout24.Models.Chat;
using Xamarin.Forms;

namespace TutorScout24.Services
{
    public class MessageService
    {
        public MessageService()
        {

           
        }
        private List<ConversationObserver> observers = new List<ConversationObserver>();
        private List<IObserver<ObservableCollection<Conversation>>> allObservers = new List<IObserver<ObservableCollection<Conversation>>>();
        public ObservableCollection<Conversation> Conversations = new ObservableCollection<Conversation>();

    
        /// <summary>
        /// reloads the message data and notifies the observers
        /// </summary>
        public async void ReloadMessages()
        {

            Conversations.Clear();

            List<RestMessage> AllMessages = new System.Collections.Generic.List<RestMessage>();
            List<RestMessage> sentM = await GetSent();
            List<RestMessage> recM = await GetReceived();

            if (sentM != null)
            {
                CreateConversationWithSentMessages(sentM);
            }
            if (recM != null)
            {
                CreateConversationWithReceivedMessages(recM);

            }

            SortConversationMessages();

            NotifyConversationObservers();
        }


        /// <summary>
        /// Notify the observers
        /// </summary>
        private void NotifyConversationObservers()
        {
            Device.BeginInvokeOnMainThread(() =>
            {

                foreach (var obs in observers)
                {
                    obs.OnNext(GetConversationById(obs.ConversationId));
                }

                foreach (var item in allObservers)
                {
                    item.OnNext(Conversations);
                }
            });
        }


        /// <summary>
        /// creates the conversation by iterating through the receivedmessages
        /// </summary>
        /// <param name="recM"></param>
        private void CreateConversationWithReceivedMessages(List<RestMessage> recM)
        {
            foreach (var item in recM)
            {

                Conversation con = GetConversationById(item.fromUserId);
                ReceivedMessage recMsg = new ReceivedMessage();
                recMsg.Text = item.text;
                recMsg.ID = item.messageId;
                recMsg.Time = item.datetime;
                recMsg.FromUser = item.fromUserId;
                recMsg.ToUser = item.toUserId;

                con.Messages.Add(recMsg);
                if (CheckIfConversationIsNew(con.id))
                {
                    Conversations.Add(con);
                }
            }
        }

        /// <summary>
        /// creates the conversation by iterating through the sentmessages
        /// </summary>
        /// <param name="sentM"></param>
        private void CreateConversationWithSentMessages(List<RestMessage> sentM)
        {
            foreach (RestMessage item in sentM)
            {

                Conversation con = GetConversationById(item.toUserId);
                SentMessage sentMsg = new SentMessage();
                sentMsg.Text = item.text;
                sentMsg.ID = item.messageId;
                sentMsg.Time = item.datetime;
                sentMsg.FromUser = item.fromUserId;
                sentMsg.ToUser = item.toUserId;

                con.Messages.Add(sentMsg);
                if (CheckIfConversationIsNew(con.id))
                {
                    Conversations.Add(con);
                }
            }
        }

        /// <summary>
        /// sorts the messages by time
        /// </summary>
        private void SortConversationMessages()
        {

            foreach (Conversation item in Conversations)
            {
                item.Messages.Sort(delegate (Message m1, Message m2) { return m1.Time.CompareTo(m2.Time); });
            }
        }


        /// <summary>
        /// returns the conversation with the given id or creates an empty one and returns it
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Conversation GetConversationById(string id)
        {
            foreach (Conversation item in Conversations)
            {
                if (item.id == id)
                {
                    return item;
                }
            }

            Conversation con = new Conversation();
            con.id = id;
            return con;
        }

        /// <summary>
        /// checks if the conversation exists in the current data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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


        public IDisposable Subscribe(IObserver<ObservableCollection<Conversation>> observer)
        {
            allObservers.Add(observer);
            return null;
        }

        public IDisposable Subscribe(ConversationObserver observer)
        {
            observers.Add(observer);
            return null;
        }
    }
}
