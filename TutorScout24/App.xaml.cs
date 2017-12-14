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
using System.Reflection;
using TutorScout24.Resources;
using System.Globalization;

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

            AppResources.Culture = new CultureInfo("de");

        }

        private async void TryToPerformAutoLogin(){
            CredentialService CService = MvvmNanoIoC.Resolve<CredentialService>();
            try
            {
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
                }else{
                    SetUpMainPage<LoginViewModel>();
                }
            }else{
                SetUpMainPage<LoginViewModel>();
            }
            }
            catch (Exception ex)
            {
                SetUpMainPage<LoginViewModel>();
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
            MvvmNanoIoC.RegisterAsSingleton<MessageService>(new MessageService());
        }
         
        private static void SetupDependencies(){
            MvvmNanoIoC.Register<IMessenger,MvvmNano.Forms.MvvmNanoFormsMessenger>();
            MvvmNanoIoC.Register<TutorScoutRestService,TutorScoutRestService>();
            MvvmNanoIoC.RegisterAsSingleton<GeocodeService, GeocodeService>();
            MvvmNanoIoC.RegisterAsSingleton<GeocodeAutocompleteService, GeocodeAutocompleteService>();

            MvvmNanoIoC.Register<CredentialService,CredentialService>();
        }

    }
}
