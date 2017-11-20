using System.Diagnostics;
using Xamarin.Forms;
using MvvmNano.Forms;
using MvvmNano;
using MvvmNano.Ninject;
using TutorScout24.ViewModels;
using TutorScout24.Services;
using TutorScout24.Models;
using System.Threading.Tasks;
using System;

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


            TryToPerformAutoLogin();
        
            SetUpMainPage<LoginViewModel>();

        }

        private void TryToPerformAutoLogin(){
            CredentialService CService = MvvmNanoIoC.Resolve<CredentialService>();
            if (CService.DoCredentialsExist())
            {
                CheckAuthentication auth = new CheckAuthentication();
                auth.authentication = new Authentication();
                auth.authentication.password = CService.Password;
                auth.authentication.userName = CService.UserName;
                if (IsValidAuthentication(auth).Result)
                {
                    SetUpMainPage<MasterDetailViewModel>();
                }
            }
           
        }

        private async Task<bool> IsValidAuthentication(CheckAuthentication auth){
            return await MvvmNanoIoC.Resolve<TutorScoutRestService>().CanAuthenticate(auth);

        }

      protected override IMvvmNanoIoCAdapter GetIoCAdapter()
        {
            
            return new MvvmNanoNinjectAdapter();
        }
         
        private static void SetupDependencies(){
            MvvmNanoIoC.Register<IMessenger,MvvmNano.Forms.MvvmNanoFormsMessenger>();
            MvvmNanoIoC.Register<TutorScoutRestService,TutorScoutRestService>();
            MvvmNanoIoC.Register<CredentialService,CredentialService>();
        }

    }
}
