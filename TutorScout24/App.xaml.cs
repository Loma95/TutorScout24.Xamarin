using System.Diagnostics;
using Xamarin.Forms;
using MvvmNano.Forms;
using MvvmNano;
using MvvmNano.Ninject;
using TutorScout24.ViewModels;
using TutorScout24.Services;

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

            SetUpMainPage<LoginViewModel>();
        }

      protected override IMvvmNanoIoCAdapter GetIoCAdapter()
        {
            
            return new MvvmNanoNinjectAdapter();
        }
         
        private static void SetupDependencies(){
            MvvmNanoIoC.Register<IMessenger,MvvmNano.Forms.MvvmNanoFormsMessenger>();
            MvvmNanoIoC.Register<TutorScoutRestService,TutorScoutRestService>();
            MvvmNanoIoC.Register<CredentialService, CredentialService>();
        }

    }
}
