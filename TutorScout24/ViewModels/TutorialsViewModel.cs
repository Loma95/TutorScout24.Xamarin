using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using MvvmNano;
using TutorScout24.CustomData;
using TutorScout24.Models.Tutorings;
using TutorScout24.Services;
using TutorScout24.Utils;
using Xamarin.Forms;
using MasterDetailPage = TutorScout24.Pages.MasterDetailPage;

namespace TutorScout24.ViewModels
{
    public class TutorialsViewModel : MvvmNanoViewModel, IThemeable, IToolBarItem
    {
        private readonly ToolbarItem switchI = new ToolbarItem
        {
            Text = "\uf0ec"
        };

        private Color _themeColor;

        private List<MyTutoring> _tutorings = new List<MyTutoring>();

        public TutorialsViewModel()
        {
            _themeColor = (Color) Application.Current.Resources["MainColor"];
            SetTutorings();
            MvvmNanoIoC.Resolve<IMessenger>()
                .Subscribe(this, (object arg1, ChangeModeMessage arg2) => { SetTutorings(); });
        }

        public List<MyTutoring> Tutorings
        {
            get => _tutorings;
            set
            {
                _tutorings = value;
                NotifyPropertyChanged();
            }
        }


        public ICommand FabCommand => new Command(Fab);

        public Color ThemeColor
        {
            get => _themeColor;
            set
            {
                _themeColor = value;
                NotifyPropertyChanged("ThemeColor");
            }
        }

        /// <summary>
        /// Add Change mode button to page
        /// </summary>
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

        public async void SetTutorings()
        {
            Tutorings = await MvvmNanoIoC.Resolve<TutorScoutRestService>().GetMyTutorings();
        }

        private void Fab()
        {
            NavigateTo<CreateViewModel, CreateTutoring>(new CreateTutoring());
            Debug.WriteLine("Command");
        }
    }
}