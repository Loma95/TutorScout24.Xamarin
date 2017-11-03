using System.Diagnostics;
using Xamarin.Forms;
using MvvmNano.Forms;
using MvvmNano;
using MvvmNano.Ninject;
using TutorScout24.ViewModels;

namespace TutorScout24
{
    public partial class App
    {
        public App()
        {
            InitializeComponent();
            MvvmNanoIoC.SetUp(GetIoCAdapter());
            SetupDependencies();

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
         
        private static void SetupDependencies(){
            MvvmNanoIoC.Register<IMessenger,MvvmNano.Forms.MvvmNanoFormsMessenger>();
        }

    }
}
