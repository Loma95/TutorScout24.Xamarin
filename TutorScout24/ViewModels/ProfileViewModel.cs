﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmNano;
using TutorScout24.Models;
using TutorScout24.Services;
using TutorScout24.Utils;
using Xamarin.Forms;

namespace TutorScout24.ViewModels
{
    public class ProfileViewModel : MvvmNanoViewModel,IThemeable
    {

        private ToolbarItem _EditSwitch;

        public ProfileViewModel(){
            _themeColor = (Xamarin.Forms.Color)Application.Current.Resources["MainColor"];

            GetMyUserInfo();

            CredentialService CService = MvvmNanoIoC.Resolve<CredentialService>();
           
            PasswordWasSaved = CService.DoCredentialsExist();

            var master = (Pages.MasterDetailPage)Application.Current.MainPage;

            _EditSwitch = new ToolbarItem
            {
                Text = "\uf044"
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
                    UpdateProfile updateUser = new UpdateProfile();
                    updateUser.firstName = _userInfo.firstName;
                    updateUser.lastName = _userInfo.lastName;
                    updateUser.gender = _userInfo.gender;
                    updateUser.note = _userInfo.description;
                    updateUser.email = _userInfo.email;
                    updateUser.maxGraduation = _userInfo.maxGraduation;
                    await MvvmNanoIoC.Resolve<TutorScoutRestService>().UpdateUser(updateUser);
                }
                EditMode = !EditMode;
                ViewMode = !EditMode;

            };

            master.ToolbarItems.Add(_EditSwitch);
        }



        public ICommand RemovePassword => new Command(RemovePass);

        private void RemovePass()
        {
            CredentialService CService = MvvmNanoIoC.Resolve<CredentialService>();
            CService.DeleteCredentials();
            PasswordWasSaved = false;

            MvvmNanoIoC.Resolve<IMessenger>().Send(new DialogMessage("Erfolgreich","Sie haben das gespeicherte Passwort entfernt"));
        }



        private bool _editMode;
        public bool EditMode
        {
            get { return _editMode; }
            set
            {
                _editMode = value;
                NotifyPropertyChanged("EditMode");
            }
        }

        private bool _viewMode = true;
        public bool ViewMode
        {
            get { return _viewMode; }
            set
            {
                _viewMode = value;
                NotifyPropertyChanged("ViewMode");
            }
        }

        private bool _passwordWasSaved;
        public bool PasswordWasSaved
        {
            get { return _passwordWasSaved; }
            set { _passwordWasSaved = value;
                NotifyPropertyChanged("PasswordWasSaved");}
        }

        private MyUserInfo _userInfo;
        public MyUserInfo UserInfo
        {
            get { return _userInfo; }
            set { _userInfo = value; 

                Debug.WriteLine(_userInfo.description);
                NotifyPropertyChanged("UserInfo");
            }
        }

        public List<string> Gender
        {
            get
            {
                return Enum.GetNames(typeof(Genders)).Select(b => b).ToList();
            }
        }

 

        private DateTime _birthdate;
        public DateTime BirthDate
        {
            get { return _birthdate; }
            set
            {
                _birthdate = value;
                NotifyPropertyChanged("BirthDate");
            }
        }

        private async void GetMyUserInfo(){
                  
            UserInfo = await MvvmNanoIoC.Resolve<TutorScoutRestService>().GetMyUserInfo();


          
        }

        private Color _themeColor;
        public Color ThemeColor { get { return _themeColor; } set { _themeColor = value; NotifyPropertyChanged("ThemeColor"); } }

        public override void Dispose()
        {
            
            var master = (Pages.MasterDetailPage)Application.Current.MainPage;
            master.ToolbarItems.Remove(_EditSwitch);
            base.Dispose();
        }
    }
}
