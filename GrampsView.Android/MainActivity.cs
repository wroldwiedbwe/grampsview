using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;

using AndroidX.Core.App;
using AndroidX.Core.Content;

using FFImageLoading.Forms.Platform;

using GrampsView.Assets.Styles;
using GrampsView.Common;
using GrampsView.Common.CustomClasses;
using GrampsView.Data.Repository;
using GrampsView.Droid.Common;

using Microsoft.AppCenter.Distribute;

using Plugin.CurrentActivity;

using Prism;
using Prism.Ioc;

using System;
using System.Threading.Tasks;

using static GrampsView.App;

//using Xamarin.OneDrive;

namespace GrampsView.Droid
{
    [Activity(Label = "GrampsView", Icon = "@mipmap/icon", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            //Connector.SetAuthenticationContinuationEventArgs(requestCode, resultCode, data);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            //TabLayoutResource = Resource.Layout.Tabbar;
            //ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;

            TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;

            // Get read/write permisions
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.WriteExternalStorage) != (int)Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.WriteExternalStorage }, 0);
            }

            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadExternalStorage) != (int)Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.ReadExternalStorage }, 0);
            }

            // Init things
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            CrossCurrentActivity.Current.Init(this, savedInstanceState);

            // Only Start App Center if there
            if (!CommonRoutines.IsEmualator())
            {
                // App Center Distribute
                Distribute.SetEnabledForDebuggableBuild(true);
            }

            // FFImageLoading Init

            CachedImageRenderer.Init(enableFastRenderer: false);

            CachedImageRenderer.InitImageViewHandler();

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            //GrampsView.UserControls.Droid.Renderers.BorderlessEntryRenderer.Init();

            //Connector.Init(this);

            // Load the app
            LoadApplication(new App(new AndroidInitializer()));

            //SetAppTheme();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            UnregisterManagers();
        }

        protected override void OnPause()
        {
            base.OnPause();
            UnregisterManagers();
        }

        protected override void OnResume()
        {
            base.OnResume();

            //CrashManager.Register(this, GrampsView.Common.CommonConstants.HockeyAppId);
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        {
            var newExc = new Exception("CurrentDomainOnUnhandledException", unhandledExceptionEventArgs.ExceptionObject as Exception);
            DataStore.CN.NotifyException("CurrentDomainOnUnhandledException", newExc);
        }

        private static void SetTheme(Xamarin.Essentials.AppTheme mode)
        {
            if (mode == Xamarin.Essentials.AppTheme.Dark)
            {
                if (AppTheme == Xamarin.Essentials.AppTheme.Dark)
                    return;
                PrismApplicationBase.Current.Resources = new DarkTheme();
            }
            else
            {
                if (AppTheme != Xamarin.Essentials.AppTheme.Dark)
                    return;
                PrismApplicationBase.Current.Resources = new LightTheme();
            }
            AppTheme = mode;
        }

        private static void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs unobservedTaskExceptionEventArgs)
        {
            var newExc = new Exception("TaskSchedulerOnUnobservedTaskException", unobservedTaskExceptionEventArgs.Exception);
            DataStore.CN.NotifyException("TaskSchedulerOnUnobservedTaskException", newExc);

            CommonLocalSettings.DataSerialised = false;
        }

        private void SetAppTheme()
        {
            if (Resources.Configuration.UiMode.HasFlag(UiMode.NightYes))
                SetTheme(Xamarin.Essentials.AppTheme.Dark);
            else
                SetTheme(Xamarin.Essentials.AppTheme.Light);
        }

        private void UnregisterManagers()
        {
            //UpdateManager.Unregister();
        }

        public class AndroidInitializer : IPlatformInitializer
        {
            public void RegisterTypes(IContainerRegistry containerRegistry)
            {
                // Register any platform specific implementations
                containerRegistry.RegisterSingleton<IPlatformSpecific, PlatformSpecific>();
            }
        }

        public class CustomLogger : FFImageLoading.Helpers.IMiniLogger

        {
            public void Debug(string message)

            {
                Console.WriteLine(message);
            }

            public void Error(string errorMessage)

            {
                Console.WriteLine(errorMessage);
            }

            public void Error(string errorMessage, Exception ex)

            {
                Error(errorMessage + System.Environment.NewLine + ex.ToString());
            }
        }
    }
}