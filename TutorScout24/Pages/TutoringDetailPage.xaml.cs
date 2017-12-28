using TutorScout24.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutorScout24.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TutoringDetailPage
    {
        /// <summary>
        ///     Initialize and add label with icon to grid.
        /// </summary>
        public TutoringDetailPage()
        {
            InitializeComponent();
            var label = new Label
            {
                Text = "\uf08e",
                FontFamily = "fontawesome",
		        HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
   
		    };
		    UserGrid.Children.Add(label, 1, 0);
		}


        /// <summary>
        ///     Make UserFrame navigate to Profile of that user.
        /// </summary>
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            var gr = new TapGestureRecognizer();
            gr.Tapped += new SingleClick(ViewModel.ToProfile).Click;
            UserFrame.GestureRecognizers.Add(gr);
            ViewModel.AddToolBarItem();
        }
    }
}