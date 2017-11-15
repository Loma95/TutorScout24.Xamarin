using System;
using Android.Graphics;
using TutorScout24.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Label), typeof(CustomLabelRenderer))]
namespace TutorScout24.Droid
{
    public class CustomLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (!string.IsNullOrEmpty(e.NewElement?.FontFamily))
            {
                    
                Control.Typeface = Typeface.CreateFromAsset(Forms.Context.ApplicationContext.Assets,e.NewElement.FontFamily + ".ttf");

            }
        }
    }
}
