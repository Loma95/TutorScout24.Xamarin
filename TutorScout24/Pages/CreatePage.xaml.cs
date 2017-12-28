namespace TutorScout24.Pages
{
    public partial class CreatePage
    {
        /// <summary>
        ///     When Select Button is pressed, select position from map or from adress field.
        /// </summary>
        public CreatePage()
        {
            InitializeComponent();

            selectButton.Clicked += (sender, e) => {
                //check for the Mode
                if (ViewModel.ShowMap)
                {
                    // save Location
                    ViewModel.PositionSelected(MyMap2.VisibleRegion.Center.Latitude, MyMap2.VisibleRegion.Center.Longitude);
                }else{
                    // get location from adress string
                    ViewModel.SetLocationWithAdress();
                }

            };
        }


        protected override bool OnBackButtonPressed()
        {
            ViewModel.RemoveToolbarItem();
            return base.OnBackButtonPressed();
        }


        /// <summary>
        ///     When ViewModel is bound, add Method to set adress field to suggestion when tapped.
        /// </summary>

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            SuggestionListView.ItemTapped += (sender, e) => { AdressEntry.Text = e.Item.ToString(); };

            ViewModel.map = MyMap2;
            ViewModel.DialogView = PopUpDialog;
        }
    }
}