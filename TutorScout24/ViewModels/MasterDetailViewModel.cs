using MvvmNano;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using TutorScout24.Models;
using TutorScout24.Services;
using Xamarin.Forms;

namespace TutorScout24.ViewModels
{
    public class MasterDetailViewModel:MvvmNano.MvvmNanoViewModel
    {
        public ICommand ChangeCommand => new Command(async () => await Change());

        private async Task Change()
        {
            UserInfos userI = await MvvmNanoIoC.Resolve<TutorScoutRestService>().GetUserInfo();
            MvvmNanoIoC.Resolve<IMessenger>().Send(new DialogMessage(userI.UserCount.ToString()));
        }
    }
}
