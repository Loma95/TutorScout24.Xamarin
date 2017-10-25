using System.Diagnostics;
using Xamarin.Forms;
using MvvmNano.Forms;
using MvvmNano;
using MvvmNano.Ninject;
using TutorScout24.ViewModels;

namespace TutorScout24
{
    public partial class App : MvvmNanoApplication
    {
        public App()
        {
            InitializeComponent();


        }
        protected override void OnStart()
        {
            base.OnStart();


            SetUpMainPage<MasterDetailViewModel>();
        }

        protected override IMvvmNanoIoCAdapter GetIoCAdapter()
        {
            return new MvvmNanoNinjectAdapter();
        }

    }
}
