using MvvmNano;
using MvvmNano.Forms;
using TutorScout24.ViewModels;
using Xamarin.Forms;

namespace TutorScout24.Pages
{
    public partial class FeedTabPage
    {
        public FeedTabPage()
        {
            InitializeComponent();

            //Add FeedListPage as one tab of this page
            var presenter = (MvvmNanoFormsPresenter) MvvmNanoIoC.Resolve<IPresenter>();
            var view = (Page) presenter.CreateViewFor<FeedListViewModel>();
            view.BindingContext = MvvmNanoIoC.Resolve<FeedListViewModel>();
            view.Title = "Feed";
            Children.Add(view);

            //Add FeedMapPage as one tab of this page
            var view2 = (Page) presenter.CreateViewFor<FeedMapViewModel>();
            view2.BindingContext = MvvmNanoIoC.Resolve<FeedMapViewModel>();
            view2.Title = "Karte";
            Children.Add(view2);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.AddToolBarItem();
        }

        public override void OnViewModelSet()
        {
            base.OnViewModelSet();
            ViewModel.AddToolBarItem();
        }
    }
}