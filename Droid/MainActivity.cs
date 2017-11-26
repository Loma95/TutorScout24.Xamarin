using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using TutorScout24;
using Plugin.Permissions;
using NControl.Droid;
using Android.Util;
using Java.Lang;
using Java.Lang.Reflect;
using Android.Graphics;
using TutorScout24.Services;
using Xamarin.Auth;
using IconEntry.FormsPlugin.Android;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace TutorScout24.Droid
{
    [Activity(Label = "TutorScout24.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);
           TabLayoutResource = Resource.Layout.Tabbar;
           ToolbarResource = Resource.Layout.Toolbar;

      
            global::Xamarin.Forms.Forms.Init(this, bundle);
            Xamarin.FormsMaps.Init(this, bundle);
            IconEntryRenderer.Init();
            AppCenter.Start("b0585775-e91e-4607-93e8-d36b1dcc2273",
                   typeof(Analytics), typeof(Crashes));

            NControlViewRenderer.Init();
            try
            {
                LoadApplication(new App());

            }catch(System.Exception ex){
                Console.WriteLine(ex);
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }


        /**** thanks to https://github.com/daniel-luberda/XamarinFormsToolbarCustomFont**/
        static Class ActionMenuItemViewClass = null;
        static Constructor ActionMenuItemViewConstructor = null;

        static Typeface typeface = null;
        public static Typeface Typeface
        {
            get
            {
                if (typeface == null)
                    typeface = Typeface.CreateFromAsset(Xamarin.Forms.Forms.Context.ApplicationContext.Assets, "fontawesome.ttf");

                return typeface;
            }
        }

        public override View OnCreateView(string name, Context context, IAttributeSet attrs)
        {
            if (name.Equals("android.support.v7.view.menu.ActionMenuItemView", StringComparison.InvariantCultureIgnoreCase))
            {
                System.Diagnostics.Debug.WriteLine(name);
                var customLoginIfNeeded = CreateCustomToolbarItem(name, context, attrs);
                if (customLoginIfNeeded != null)
                    return customLoginIfNeeded;
            }

            return base.OnCreateView(name, context, attrs);
        }

        public override View OnCreateView(View parent, string name, Context context, IAttributeSet attrs)
        {
            if (name.Equals("android.support.v7.view.menu.ActionMenuItemView", StringComparison.InvariantCultureIgnoreCase))
            {
                System.Diagnostics.Debug.WriteLine(name);
                var customLoginIfNeeded = CreateCustomToolbarItem(name, context, attrs);
                if (customLoginIfNeeded != null)
                    return customLoginIfNeeded;
            }

            return base.OnCreateView(parent, name, context, attrs);
        }

        private View CreateCustomToolbarItem(string name, Context context, IAttributeSet attrs)
        {
            // android.support.v7.widget.Toolbar
            // android.support.v7.view.menu.ActionMenuItemView
            View view = null;

            try
            {
                if (ActionMenuItemViewClass == null)
                    ActionMenuItemViewClass = ClassLoader.LoadClass(name);
            }
            catch (ClassNotFoundException )
            {
                return null;
            }

            if (ActionMenuItemViewClass == null)
                return null;

            if (ActionMenuItemViewConstructor == null)
            {
                try
                {
                    ActionMenuItemViewConstructor = ActionMenuItemViewClass.GetConstructor(new Class[] {
                            Class.FromType(typeof(Context)),
                                 Class.FromType(typeof(IAttributeSet))
                        });
                }
                catch (SecurityException)
                {
                    return null;
                }
                catch (NoSuchMethodException)
                {
                    return null;
                }
            }
            if (ActionMenuItemViewConstructor == null)
                return null;

            try
            {
                Java.Lang.Object[] args = new Java.Lang.Object[] { context, (Java.Lang.Object)attrs };
                view = (View)(ActionMenuItemViewConstructor.NewInstance(args));
            }
            catch (IllegalArgumentException)
            {
                return null;
            }
            catch (InstantiationException)
            {
                return null;
            }
            catch (IllegalAccessException)
            {
                return null;
            }
            catch (InvocationTargetException)
            {
                return null;
            }
            if (null == view)
                return null;

            View v = view;

            new Handler().Post(() =>
            {

                try
                {
                    if (v is LinearLayout)
                    {
                        var ll = (LinearLayout)v;
                        for (int i = 0; i < ll.ChildCount; i++)
                        {
                            var button = ll.GetChildAt(i) as Button;

                            if (button != null)
                            {
                                var title = button.Text;

                                if (!string.IsNullOrEmpty(title) && title.Length == 1)
                                {
                                    button.SetTypeface(Typeface, TypefaceStyle.Normal);
                                }
                            }
                        }
                    }
                    else if (v is TextView)
                    {
                        var tv = (TextView)v;
                        string title = tv.Text;

                        if (!string.IsNullOrEmpty(title) && title.Length == 1)
                        {
                            tv.SetTypeface(Typeface, TypefaceStyle.Normal);
                        }
                    }
                }
                catch (ClassCastException)
                {
                }
            });

            return view;
        }
        /**** thanks to https://github.com/daniel-luberda/XamarinFormsToolbarCustomFont**/
    }
}
