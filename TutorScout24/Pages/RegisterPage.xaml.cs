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
            MvvmNanoIoC.Resolve<IMessenger>().Subscribe<DialogMessage>(this, (object arg1, DialogMessage arg2) =>
           {
               DisplayAlert(arg2.Header, arg2.Text, "ok");
           });
        }

        public override void Dispose()
        {

            MvvmNanoIoC.Resolve<IMessenger>().Unsubscribe<DialogMessage>(this);
            base.Dispose();
        }
    }
}
