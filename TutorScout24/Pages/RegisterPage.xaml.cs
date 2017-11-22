using System;
using System.Collections.Generic;
using MvvmNano;
using Xamarin.Forms;

namespace TutorScout24.Pages
{
    public partial class RegisterPage 
    {
        public RegisterPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            MvvmNanoIoC.Resolve<IMessenger>().Subscribe<DialogMessage>(this, (object arg1, DialogMessage arg2) =>
            {
                DisplayAlert("Alert", arg2.Text, "ok");
            });
        }
    }
}
