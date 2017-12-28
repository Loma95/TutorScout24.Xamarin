using System;
using System.Diagnostics;
using MvvmNano.Forms;
using TutorScout24.Pages;
using TutorScout24.ViewModels;
using Xamarin.Forms;

namespace TutorScout24
{
    public class CustomPresenter : MvvmNano.Forms.MvvmNanoFormsPresenter
    {
        public CustomPresenter(MvvmNano.Forms.MvvmNanoApplication application) : base(application)
        {

        }

        protected override void OpenPage(Page page)
        {
            if (page is LoginPage)
            {
                //Open LoginPage in a Navigationpage
                Application.MainPage = new MvvmNano.Forms.MvvmNanoNavigationPage(page);
            }
            else if (page is Pages.MasterDetailPage)
            {
                // Set Masterdetailpage as RootPage
                Application.MainPage = page;
                NavigateToViewModel<FeedTabViewModel>();
            }else{
                base.OpenPage(page);
            }
        }
        protected override System.Threading.Tasks.Task OpenPageAsync(Page page)
        {
            return base.OpenPageAsync(page);
        }
    }
}
