using System;
using MvvmNano.Forms.MasterDetail;
using TutorScout24.ViewModels;
using Xamarin.Forms;

namespace TutorScout24.Pages
{
    public class MasterDetailPage : MvvmNanoDefaultMasterDetailPage<MasterDetailViewModel>
    {

        public MasterDetailPage()
        {
            AddDetailData<SearchWeatherViewModel>(new MvvmNanoMasterDetailData("Suche"));
            AddDetailData<CurrentLocationWeatherViewModel>(new MvvmNanoMasterDetailData("Mein Wetter"));

        }

        protected override Page CreateMasterPage()
        {
          
            return base.CreateMasterPage();
        }


    

    }
}
