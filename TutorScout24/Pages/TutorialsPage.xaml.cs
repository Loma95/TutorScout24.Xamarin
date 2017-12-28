using TutorScout24.ViewModels;

namespace TutorScout24
{
    public partial class TutorialsPage
    {
        public TutorialsPage()
        {
            InitializeComponent();
        


            BindingContext = new TutorialsViewModel();
        }


        public override void OnViewModelSet()
        {
            base.OnViewModelSet();
            ViewModel.AddToolBarItem();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.AddToolBarItem();
            ViewModel.SetTutorings();
        }
    }
}