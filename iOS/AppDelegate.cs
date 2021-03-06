﻿using System;
using System.Collections.Generic;
using System.Linq;
using TutorScout24;
using Foundation;
using UIKit;
using NControl.iOS;
using IconEntry.FormsPlugin.iOS;

namespace TutorScout24.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            NControlViewRenderer.Init();
            Xamarin.FormsMaps.Init();
            IconEntryRenderer.Init();
            LoadApplication(new App());
         
            return base.FinishedLaunching(app, options);
        }
    }
}
