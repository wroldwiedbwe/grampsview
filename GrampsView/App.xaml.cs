﻿using GrampsView.Common;
using GrampsView.Common.CustomClasses;
using GrampsView.Data;
using GrampsView.Data.External.StoreSerial;
using GrampsView.Data.ExternalStorageNS;
using GrampsView.Data.Repository;
using GrampsView.Services;
using GrampsView.ViewModels;
using GrampsView.Views;

using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;

using Prism;
using Prism.Events;
using Prism.Ioc;
using Prism.Logging;
using Prism.Modularity;

using System.Diagnostics;
using System.Reflection;

using Unity;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace GrampsView
{
    public sealed partial class App
    {
        public App()
            : this(null)
        {
            Debug.WriteLine("====== resource debug info =========");

            var assembly = typeof(App).GetTypeInfo().Assembly;

            foreach (var res in assembly.GetManifestResourceNames())

                Debug.WriteLine("found resource: " + res);

            Debug.WriteLine("====================================");

            // This lookup NOT required for Windows platforms - the Culture will be automatically set
            if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.iOS || Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.Android)
            {
                // determine the correct, supported .NET culture
                DependencyService.Get<ILocalize>();

                var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();

                // Assets.Strings.AppResources.Culture = ci; // ODO set the RESX for resource localization
                DependencyService.Get<Common.ILocalize>().SetLocale(ci); // set the Thread for locale-aware methods
            }
        }

        public App(IPlatformInitializer initializer)
                            : this(initializer, true)
        {
        }

        public App(IPlatformInitializer initializer, bool setFormsDependencyResolver)
                            : base(initializer, setFormsDependencyResolver)
        {
        }

        public static AppTheme AppTheme { get; set; }

        /// <summary>
        /// Creates the logger.
        /// </summary>
        /// <returns>
        /// </returns>
        public static ILoggerFacade CreateLogger
        {
            get
            {
                return new CommonLogPrism();
            }
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            VersionTracking.Track();
        }

        protected override void OnResume()
        {
            // Support IApplicationLifecycleAware

            base.OnResume();

            // Handle when your app resumes
        }

        protected override void OnSleep()
        {
            // Support IApplicationLifecycleAware

            base.OnSleep();

            // Handle when your app sleeps
        }

        protected override void OnStart()
        {
            string StartPage = string.Empty;

            //// Only Start App Center if physical
            //if (!CommonRoutines.IsEmulator())
            //{
            AppCenterInit();
            //}

            CommonTheming.SetAppTheme();

            IPlatformSpecific ps = Container.Resolve<IPlatformSpecific>();

            DataStore.CN = Container.Resolve<ICommonNotifications>();

            DataStore.NV = new NavCmd(Container.Resolve<IEventAggregator>());

            IDataRepositoryManager temp = Container.Resolve<IDataRepositoryManager>();

            // Start at the MessageLog Page and work from there
            StartPage = nameof(MessageLogPage);

            NavigationService.NavigateAsync("MainPage/NavigationPage/" + StartPage);
        }

        protected override void RegisterTypes(IContainerRegistry container)
        {
            container.RegisterForNavigation<AboutPage, AboutViewModel>();
            //container.RegisterForNavigation<BookMarkDetailPage, BookMarkDetailViewModel>();
            container.RegisterForNavigation<BookMarkListPage, BookMarkListViewModel>();
            container.RegisterForNavigation<CitationDetailPage, CitationDetailViewModel>();
            container.RegisterForNavigation<CitationListPage, CitationListViewModel>();
            container.RegisterForNavigation<EventDetailPage, EventDetailViewModel>();
            container.RegisterForNavigation<EventListPage, EventListViewModel>();
            container.RegisterForNavigation<FamilyDetailPage, FamilyDetailViewModel>();
            container.RegisterForNavigation<FamilyListPage, FamilyListViewModel>();
            container.RegisterForNavigation<FileInputHandlerPage, FileInputHandlerViewModel>();
            container.RegisterForNavigation<FirstRunPage, FirstRunViewModel>();
            container.RegisterForNavigation<HubPage, HubViewModel>();
            container.RegisterForNavigation<MediaDetailPage, MediaDetailViewModel>();
            container.RegisterForNavigation<MediaListPage, MediaListViewModel>();
            container.RegisterForNavigation<MessageLogPage, MessageLogViewModel>();
            container.RegisterForNavigation<NeedDatabaseReloadPage, NeedDatabaseReloadViewModel>();
            container.RegisterForNavigation<NoteDetailPage, NoteDetailViewModel>();
            container.RegisterForNavigation<NoteListPage, NoteListViewModel>();
            container.RegisterForNavigation<PeopleGraphPage, PeopleGraphViewModel>();
            container.RegisterForNavigation<PersonDetailPage, PersonDetailViewModel>();
            container.RegisterForNavigation<PersonListPage, PersonListViewModel>();
            container.RegisterForNavigation<PlaceDetailPage, PlaceDetailViewModel>();
            container.RegisterForNavigation<PlaceListPage, PlaceListViewModel>();
            container.RegisterForNavigation<RepositoryDetailPage, RepositoryDetailViewModel>();
            container.RegisterForNavigation<RepositoryListPage, RepositoryListViewModel>();
            container.RegisterForNavigation<SearchPage, SearchViewModel>();

            //container.RegisterForNavigation<SettingsPage, SettingsViewModel>();
            container.RegisterForNavigation<SetupStoragePage, SetupStorageViewModel>();
            container.RegisterForNavigation<SourceDetailPage, SourceDetailViewModel>();
            container.RegisterForNavigation<SourceListPage, SourceListViewModel>();
            container.RegisterForNavigation<TagDetailPage, TagDetailViewModel>();
            container.RegisterForNavigation<TagListPage, TagListViewModel>();

            container.RegisterForNavigation<NameMapDetailView, NameMapDetailViewModel>();
            container.RegisterForNavigation<NameMapListView, NameMapListViewModel>();

            //container.RegisterForNavigation<TagListView, TagListViewModel>();
            //container.RegisterForNavigation<TestCacheView, TestCacheViewModel>();

            container.RegisterForNavigation<NavigationPage>();

            //container.RegisterForNavigation<MyNavigationPage>();
            container.RegisterForNavigation<MainPage, MainPageViewModel>();

            container.RegisterForNavigation<AShellPage>();
            container.RegisterForNavigation<TestPage>();
            container.RegisterForNavigation<CropTransformationPage, CropTransformationViewModel>();

            container.RegisterDialog<ErrorDialog, ErrorDialogViewModel>();

            // container.Register<ICommonModelGridBuilder, CommonModelGridBuilder>();
            //container.RegisterSingleton<ICommonCircularLog, CommonLogCircular>();
            container.RegisterSingleton<ICommonLogging, CommonLogging>();
            container.RegisterSingleton<ICommonNotifications, CommonNotifications>();
            container.RegisterSingleton<IDataRepositoryManager, DataRepositoryManager>();
            container.RegisterSingleton<IEventAggregator, EventAggregator>();
            container.RegisterSingleton<IStorePostLoad, GrampsStorePostLoad>();
            container.RegisterSingleton<IGrampsStoreSerial, GrampsStoreSerial>();
            container.RegisterSingleton<IGrampsStoreXML, GrampsStoreXML>();
            container.RegisterSingleton<IStoreFile, StoreFile>();

            container.RegisterSingleton<WhatsNewDisplayService>();
            container.RegisterForNavigation<WhatsNewPage, WhatsNewViewModel>();

            container.RegisterSingleton<FirstRunDisplayService>();
            container.RegisterForNavigation<FirstRunPage>();

            container.RegisterSingleton<DatabaseReloadDisplayService>();
            container.RegisterForNavigation<NeedDatabaseReloadPage>();

            // Set a factory for the ViewModelLocator to use the container to construct view models
            // so their dependencies get injected by the container TODO
            // ViewModelLocationProvider.SetDefaultViewModelFactory((viewModelType) => container.Resolve(viewModelType));

            // Sections of code should not be "commented out" // set the convention used to match
            // views and viewmodels
            // ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) => {
            // string ViewNamespace = "Views"; string ViewModelNamespace = "ViewModels"; var
            // friendlyName = viewType.FullName; friendlyName = friendlyName.Replace(ViewNamespace,
            // ViewModelNamespace); var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            // var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}ViewModel, {1}",
            // friendlyName, viewAssemblyName); return Type.GetType(viewModelName); }); Sections of
            // code should not be "commented out"
        }

        /// <summary>
        /// Initialize App Center.
        /// </summary>
        private static void AppCenterInit()
        {
            string initString = "uwp=" + Secret.UWPSecret + ";" +
                                "android=" + Secret.AndroidSecret + ";" +
                                "ios=" + Secret.IOSSecret;

            Debug.WriteLine(initString, "AppCenterInit");

            AppCenter.LogLevel = LogLevel.Verbose;

            AppCenter.Start(initString,
                            typeof(Analytics), typeof(Crashes), typeof(Distribute));

            Distribute.SetEnabledAsync(true);

            var t = Distribute.UpdateTrack;
        }
    }
}