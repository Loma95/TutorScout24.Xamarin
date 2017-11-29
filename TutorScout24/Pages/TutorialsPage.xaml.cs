using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using TutorScout24.Controls;
using TutorScout24.ViewModels;
using Xamarin.Forms;

namespace TutorScout24
{
    public partial class TutorialsPage
    {
        public TutorialsPage()
        {

            InitializeComponent();


       BindingContext = new TutorialsViewModel();

    }
      

    }
}
