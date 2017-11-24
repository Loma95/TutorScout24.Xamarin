using MvvmNano;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using TutorScout24.Models;
using TutorScout24.Services;
using Xamarin.Forms;
using MvvmNano.Forms;
using MvvmNano.Forms.MasterDetail;
using System.Reflection;
using System.Diagnostics;
using System.Linq;
using System.Diagnostics.Contracts;
using TutorScout24.Utils;

namespace TutorScout24.ViewModels
{
    public class MasterDetailViewModel : MvvmNano.MvvmNanoViewModel
    {
        public enum Mode  { TUTOR,STUDENT };
        public static Mode CurrentMode;
        public ICommand ChangeCommand => new Command(Change);

        private  void Change()
        {


            if(CurrentMode == Mode.STUDENT){
                CurrentMode = Mode.TUTOR;
                Application.Current.Resources["MainColor"] = Color.FromHex("#EF5350");
            }else{
                CurrentMode = Mode.STUDENT;
                Application.Current.Resources["MainColor"] = Color.FromHex("#78909c");
            }


            var master = (Pages.MasterDetailPage)Application.Current.MainPage;
            var navigation = (MvvmNanoNavigationPage)master.Detail;
            IThemeable vc =  (IThemeable)navigation.RootPage.BindingContext;
            vc.ThemeColor = (Xamarin.Forms.Color)Application.Current.Resources["MainColor"];


           // RefreshCurrentPage();
            SetBarColor();

           // var master = (Pages.MasterDetailPage)Application.Current.MainPage;
            
        
        }




        /// <summary>
        /// Sets the color of the bar.
        /// </summary>
        private void SetBarColor(){
            var master = (Pages.MasterDetailPage)Application.Current.MainPage;
            var navigation = (MvvmNanoNavigationPage)master.Detail;
            navigation.BarBackgroundColor = (Xamarin.Forms.Color)Application.Current.Resources["MainColor"];
        }

        /// <summary>
        /// Refreshs the current page.
        /// </summary>
        private void RefreshCurrentPage(){
            var master = (Pages.MasterDetailPage)Application.Current.MainPage;
            var navigation = (MvvmNanoNavigationPage)master.Detail;
            Type type = navigation.RootPage.BindingContext.GetType();
            var presenter = (MvvmNano.Forms.MvvmNanoFormsPresenter)MvvmNanoIoC.Resolve<IPresenter>();
            IViewModel model = GetViewModel(type);
            var view = (Page)MvvmNanoIoC.Resolve<IPresenter>().CreateViewFor(type);
            view.BindingContext = model;
            view.Title = navigation.RootPage.Title;
            master.Detail = new MvvmNanoNavigationPage(view);
        }

        /**** Code was stolen from MVVMNano GetViewModel && ResolveViewModel  :) ***/

        /// <summary>
        /// Gets the view model.
        /// </summary>
        /// <returns>The view model.</returns>
        /// <param name="viewModelType">View model type.</param>
        private IViewModel GetViewModel(Type viewModelType)
        {
            try
            {
                var allMethods = typeof(MasterDetailViewModel).GetRuntimeMethods();
                MethodInfo method = allMethods.First(x => x.Name == nameof(ResolveViewModel));
                MethodInfo genericMethod = method.MakeGenericMethod(new[] { viewModelType });
                var viewModel = (IViewModel)genericMethod.Invoke(this, null);
                return viewModel;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }
        /// <summary>
        /// Gets an instance of the given <see cref="TViewModel"/>.
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <returns></returns>
        /// <exception cref="T:MvvmNano.Forms.MvvmNanoFormsPresenterException"></exception>
        private IViewModel ResolveViewModel<TViewModel>()
        {
            var viewModel = MvvmNanoIoC.Resolve<TViewModel>() as IViewModel;
            if (viewModel == null)
            {
                throw new Exception();
            }

            return viewModel;
        }


        /**** Code was stolen from MVVMNano  :) ***/

    }
}
