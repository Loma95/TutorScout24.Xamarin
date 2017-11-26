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

      

        }
        protected override void OnStart()
        {
            base.OnStart();
            SetupDependencies();

            TryToPerformAutoLogin();

    

            SetUpMainPage<LoginViewModel>();
          

        }

        private async void TryToPerformAutoLogin(){
            CredentialService CService = MvvmNanoIoC.Resolve<CredentialService>();
            if (CService.DoCredentialsExist())
            {
                CheckAuthentication auth = new CheckAuthentication();
                auth.authentication = new Authentication();
                auth.authentication.password = CService.Password;
                auth.authentication.userName = CService.UserName;


                bool result = await IsValidAuthentication(auth);
                if (result)
                {
                    MvvmNanoIoC.RegisterAsSingleton<Authentication>(auth.authentication);
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

        protected override void SetUpPresenter()
        {
            MvvmNanoIoC.RegisterAsSingleton<IPresenter>(new CustomPresenter(this));
        }
         
        private static void SetupDependencies(){
            MvvmNanoIoC.Register<IMessenger,MvvmNano.Forms.MvvmNanoFormsMessenger>();
            MvvmNanoIoC.Register<TutorScoutRestService,TutorScoutRestService>();
            MvvmNanoIoC.Register<CredentialService,CredentialService>();
        }

    }
}
