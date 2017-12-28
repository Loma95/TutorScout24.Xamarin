using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using MvvmNano;
using TutorScout24.CustomData;
using TutorScout24.Models.UserData;
using TutorScout24.Services;
using TutorScout24.Utils;
using Xamarin.Forms;
using MasterDetailPage = TutorScout24.Pages.MasterDetailPage;

namespace TutorScout24.ViewModels
{
    public class ProfileViewModel : MvvmNanoViewModel, IThemeable, IToolBarItem
    {
        private string _age;


        private bool _editMode;

        private readonly ToolbarItem _EditSwitch;
        private readonly ToolbarItem _logout;

        private bool _passwordWasSaved;


        private Color _themeColor;

        private MyUserInfo _userInfo;

        private bool _viewMode = true;
        private MyUserInfo OldUserData;

        public ProfileViewModel()
        {
            _themeColor = (Color) Application.Current.Resources["MainColor"];

            GetMyUserInfo();

            var CService = MvvmNanoIoC.Resolve<CredentialService>();

            PasswordWasSaved = CService.DoCredentialsExist();

            var master = (MasterDetailPage) Application.Current.MainPage;

            _EditSwitch = new ToolbarItem
            {
                Text = "\uf044"
            };

            _logout = new ToolbarItem
            {
                Text = "\uf08b"
            };

            _logout.Clicked += (sender, e) =>
            {
                ((ToolbarItem) sender).IsEnabled = false;
                RemovePass();
            };


            _EditSwitch.Clicked += async (sender, e) =>
            {
                if (!EditMode)
                {
                    _EditSwitch.Text = "\uf00c";
                }
                else
                {
                    _EditSwitch.Text = "\uf044";
                    NotifyPropertyChanged("UserInfo");
                    var updateUser = new UpdateProfile();
                    updateUser.firstName = _userInfo.firstName;
                    updateUser.lastName = _userInfo.lastName;
                    updateUser.gender = _userInfo.gender;
                    updateUser.note = _userInfo.description;
                    updateUser.email = _userInfo.email != OldUserData.email ? _userInfo.email : null;
                    updateUser.maxGraduation = _userInfo.maxGraduation;
                    await MvvmNanoIoC.Resolve<TutorScoutRestService>().UpdateUser(updateUser);
                }
                EditMode = !EditMode;
                ViewMode = !EditMode;
            };

            AddToolBarItem();
        }

        public bool EditMode
        {
            get => _editMode;
            set
            {
                _editMode = value;
                NotifyPropertyChanged("EditMode");
            }
        }

        public bool ViewMode
        {
            get => _viewMode;
            set
            {
                _viewMode = value;
                NotifyPropertyChanged("ViewMode");
            }
        }

        public bool PasswordWasSaved
        {
            get => _passwordWasSaved;
            set
            {
                _passwordWasSaved = value;
                NotifyPropertyChanged("PasswordWasSaved");
            }
        }

        public MyUserInfo UserInfo
        {
            get => _userInfo;
            set
            {
                _userInfo = value;

                Debug.WriteLine(_userInfo.description);
                NotifyPropertyChanged("UserInfo");
            }
        }

        public List<string> Gender
        {
            get { return Enum.GetNames(typeof(Genders)).Select(b => b).ToList(); }
        }

        public string Age
        {
            get => _age;
            set
            {
                _age = value;
                NotifyPropertyChanged("Age");
            }
        }

        public Color ThemeColor
        {
            get => _themeColor;
            set
            {
                _themeColor = value;
                NotifyPropertyChanged("ThemeColor");
            }
        }

        public void AddToolBarItem()
        {
            var master = (MasterDetailPage) Application.Current.MainPage;
            master.ToolbarItems.Clear();
            master.ToolbarItems.Add(_EditSwitch);

            if (PasswordWasSaved)
                master.ToolbarItems.Add(_logout);
        }


        private void RemovePass()
        {
            var CService = MvvmNanoIoC.Resolve<CredentialService>();
            CService.DeleteCredentials();
            PasswordWasSaved = false;

            MvvmNanoIoC.Resolve<IMessenger>().Send(new DialogMessage("Autom. Login deaktiviert",
                "Sie haben das gespeicherte Passwort entfernt."));
        }

        private async void GetMyUserInfo()
        {
            var UInfo = await MvvmNanoIoC.Resolve<TutorScoutRestService>().GetMyUserInfo();
            if (UInfo != null)
            {
                UserInfo = UInfo;
                OldUserData = UInfo;
                Age = DateTimeUtils.CalculateAge(DateTime.ParseExact(UserInfo.dayOfBirth,
                    "yyyyMMdd",
                    CultureInfo.InvariantCulture)).ToString();
            }
        }

        public override void Dispose()
        {
            RemoveToolBarItem();
            base.Dispose();
        }

        public void RemoveToolBarItem()
        {
            var master = (MasterDetailPage) Application.Current.MainPage;
            master.ToolbarItems.Remove(_EditSwitch);
            master.ToolbarItems.Remove(_logout);
        }
    }
}