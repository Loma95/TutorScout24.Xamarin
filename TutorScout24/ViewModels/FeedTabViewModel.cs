using MvvmNano;
using MvvmNano.Forms;
using TutorScout24.Utils;
using Xamarin.Forms;
using MasterDetailPage = TutorScout24.Pages.MasterDetailPage;

namespace TutorScout24.ViewModels
{
    public class FeedTabViewModel : MvvmNanoViewModel, IThemeable, IToolBarItem
    {
        private Color _themeColor;

        private readonly ToolbarItem switchI = new ToolbarItem
        {
            Text = "\uf0ec"
        };

        public FeedTabViewModel()
        {
            _themeColor = (Color) Application.Current.Resources["MainColor"];
        }

        public Color ThemeColor
        {
            get => _themeColor;
            set
            {
                var master = (MasterDetailPage) Application.Current.MainPage;
                var navigation = (MvvmNanoNavigationPage) master.Detail;
                var p = (TabbedPage) navigation.RootPage;
                var themable = (IThemeable) p.CurrentPage.BindingContext;
                themable.ThemeColor = value;
                _themeColor = value;
                _themeColor = value;
                NotifyPropertyChanged("ThemeColor");
            }
        }


        public void AddToolBarItem()
        {
            var master = (MasterDetailPage) Application.Current.MainPage;
            if (master != null)
            {
                master.ToolbarItems.Clear();
                master.ToolbarItems.Add(switchI);
                switchI.SetBinding(MenuItem.CommandProperty, nameof(MasterDetailViewModel.ChangeCommand));
            }
        }
    }
}