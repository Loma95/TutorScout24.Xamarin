using System.Windows.Input;
using MvvmNano;
using MvvmNano.Forms;
using TutorScout24.CustomData;
using TutorScout24.Models.Chat;
using TutorScout24.Services;
using TutorScout24.Utils;
using Xamarin.Forms;
using MasterDetailPage = TutorScout24.Pages.MasterDetailPage;

namespace TutorScout24.ViewModels
{
    public class MasterDetailViewModel : MvvmNanoViewModel
    {
        public enum Mode
        {
            TUTOR,
            STUDENT
        }

        public static Mode CurrentMode = Mode.STUDENT;
        public ICommand ChangeCommand => new Command(Change);

        private void Change()
        {
            if (CurrentMode == Mode.STUDENT)
            {
                CurrentMode = Mode.TUTOR;
                Application.Current.Resources["MainColor"] = Color.FromHex("#78909c");
            }
            else
            {
                CurrentMode = Mode.STUDENT;
                Application.Current.Resources["MainColor"] = Color.FromHex("#EF5350");
            }

            var master = (MasterDetailPage) Application.Current.MainPage;
            var navigation = (MvvmNanoNavigationPage) master.Detail;
            var vc = (IThemeable) navigation.RootPage.BindingContext;
            vc.ThemeColor = (Color) Application.Current.Resources["MainColor"];


            SetBarColor();
            MvvmNanoIoC.Resolve<IMessenger>().Send(new ChangeModeMessage(CurrentMode));
        }


        public void OpenChat(string userName)
        {
            var conn = MvvmNanoIoC.Resolve<MessageService>().GetConversationById(userName);
            NavigateToAsync<ChatViewModel, Conversation>(conn);
        }

        /// <summary>
        ///     Sets the color of the bar.
        /// </summary>
        private void SetBarColor()
        {
            var master = (MasterDetailPage) Application.Current.MainPage;
            var navigation = (MvvmNanoNavigationPage) master.Detail;
            navigation.BarBackgroundColor = (Color) Application.Current.Resources["MainColor"];
        }
    }
}