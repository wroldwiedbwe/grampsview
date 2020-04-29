namespace GrampsView.Droid
{
    using Android;
    using Android.App;
    using Android.Content;
    using Android.Content.PM;
    using Android.OS;
    using Android.Runtime;
    using Android.Support.V4.App;
    using Android.Support.V4.Content;

    using FFImageLoading.Forms.Platform;

    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Repository;
    using GrampsView.Droid.Common;

    using Microsoft.AppCenter.Distribute;
    using Microsoft.Device.Display;

    using Plugin.CurrentActivity;

    using Prism;
    using Prism.Ioc;

    using System;
    using System.Threading.Tasks;

    using Xamarin.Essentials;

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

    [Activity(MainLauncher = false, Label = "GrampsView", Icon = "@mipmap/icon", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.UiMode | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.FullSensor)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        /// <summary>
        /// Called when system configuration is changed.
        /// </summary>
        /// <param name="argNewConfig">
        /// The new configuration.
        /// </param>
        public override void OnConfigurationChanged(Android.Content.Res.Configuration argNewConfig)
        {
            if (!(argNewConfig is null))
            {
                CommonTheming.SetAppTheme();
            }

            base.OnConfigurationChanged(argNewConfig);
        }

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

            // App Center Distribute
            Distribute.SetEnabledForDebuggableBuild(true);

            // FFImageLoading Init

            CachedImageRenderer.Init(enableFastRenderer: false);

            CachedImageRenderer.InitImageViewHandler();

            Platform.Init(this, savedInstanceState);

            ScreenHelper screenHelper = new ScreenHelper();
            bool isDuo = screenHelper.Initialize(this);

            //GrampsView.UserControls.Droid.Renderers.BorderlessEntryRenderer.Init();

            //Connector.Init(this);

            // Load the app
            LoadApplication(new App(new AndroidInitializer()));
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

        private static void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs unobservedTaskExceptionEventArgs)
        {
            var newExc = new Exception("TaskSchedulerOnUnobservedTaskException", unobservedTaskExceptionEventArgs.Exception);
            DataStore.CN.NotifyException("TaskSchedulerOnUnobservedTaskException", newExc);
        }

        private void UnregisterManagers()
        {
            //UpdateManager.Unregister();
        }
    }
}