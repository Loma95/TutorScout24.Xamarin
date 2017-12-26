using MvvmNano;
using TutorScout24.ViewModels;
using Xamarin.Forms;

namespace TutorScout24.Pages
{
    public partial class FeedTabPage
    {
        public FeedTabPage()
        {


            InitializeComponent();


            var presenter = (MvvmNano.Forms.MvvmNanoFormsPresenter)MvvmNanoIoC.Resolve<IPresenter>();
            var view = (Page)presenter.CreateViewFor<FeedListViewModel>();
            view.BindingContext = MvvmNanoIoC.Resolve<FeedListViewModel>();
            view.Title = "Feed";
            Children.Add(view);

            var view2 = (Page)presenter.CreateViewFor<FeedMapViewModel>();
            view2.BindingContext = MvvmNanoIoC.Resolve<FeedMapViewModel>();
            view2.Title = "Karte";
            Children.Add(view2);


         

        }

       protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel.AddToolBarItem();
        }

    
    }
}