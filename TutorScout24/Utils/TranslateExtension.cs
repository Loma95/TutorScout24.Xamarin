using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutorScout24.Utils
{
   
        [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
        {
            public string Text { get; set; }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return null;
            CultureInfo ci =  CultureInfo.CurrentCulture;
            ResourceManager rm = new ResourceManager("TutorScout24.Resources.AppResources", typeof(TranslateExtension).GetTypeInfo().Assembly);
                return rm.GetString(Text, ci);
        }
    }

}
