using System;
using System.Diagnostics;
using TutorScout24.Utils;
using TutorScout24.ViewModels;

namespace TutorScout24.Pages
{
    public partial class FeedListPage
    {
        public FeedListPage()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     When Binding COntext has changed, i.e. the ViewModel has been created, add NavigationMethod to the FeedList's
        ///     ItemTapped Event.
        /// </summary>
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var vm = (FeedListViewModel) BindingContext;
            try
            {

                MyListView.ItemTapped += new SingleClick(vm.GoToDetailPage).Click;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }
    }
}