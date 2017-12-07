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
        private List<IObserver<ObservableCollection<Conversation>>> allObservers = new List<IObserver<ObservableCollection<Conversation>>>();
        public ObservableCollection<Conversation> Conversations = new ObservableCollection<Conversation>();

        public async void GetMessages()
        {

            Conversations.Clear();

            List<RestMessage> AllMessages = new System.Collections.Generic.List<RestMessage>();
            List<RestMessage> sentM = await GetSent();
            List<RestMessage> recM = await GetReceived();

            if (sentM != null)
            {

                foreach (RestMessage item in sentM)
                {

                    Conversation con = GetConversationById(item.toUserId);
                    SentMessage sentMsg = new SentMessage();
                    sentMsg.Text = item.text;
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
            if (recM != null)
            {
                foreach (var item in recM)
                {

                    Conversation con = GetConversationById(item.fromUserId);
                    ReceivedMessage recMsg = new ReceivedMessage();
                    recMsg.Text = item.text;
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

            SortConversationMessages();


            foreach (var obs in observers)
            {
                obs.OnNext(GetConversationById(obs.ConversationId));
            }

            foreach (var item in allObservers)
            {
                item.OnNext(Conversations);
            }


        }


        private void SortConversationMessages()
        {

            foreach (Conversation item in Conversations)
            {
                item.Messages.Sort(delegate (Message m1, Message m2) { return m1.Time.CompareTo(m2.Time); });
            }
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

            Conversation con = new Conversation();
            con.id = id;
            return con;
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
