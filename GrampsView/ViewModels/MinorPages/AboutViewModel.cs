﻿//-----------------------------------------------------------------------
//
// View model for the fly-out page view
//
// <copyright file="AboutViewModel.cs" company="MeMyselfAndI">
// Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;
    using GrampsView.Views;

    using Prism.Commands;
    using Prism.Events;
    using Prism.Navigation;

    using System.IO;
    using System.Reflection;
    using System.Threading.Tasks;

    using Xam.Forms.Markdown;

    using Xamarin.Essentials;

    public class AboutViewModel : ViewModelBase
    {
        private string _DaText = string.Empty;

        private CardListLineCollection _ApplicationVersionList = new CardListLineCollection();

        private CardGroup _HeaderDetailList = new CardGroup();

        private HeaderModel _HeaderModel = null;

        public AboutViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
                                    : base(iocCommonLogging, iocEventAggregator, iocNavigationService)

        {
            BaseTitle = "About";
            BaseTitleIcon = CommonConstants.IconSettings;

            TestPageCommand = new DelegateCommand(TestPage);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AboutViewModel" /> class.
        /// </summary>
        public AboutViewModel()
        {
        }

        public static string AppName
        {
            get
            {
                return AppInfo.Name;
            }
        }

        /// <summary>
        /// Gets the application version list.
        /// </summary>
        /// <value>
        /// The application version list.
        /// </value>
        // [RestorableState]
        public CardListLineCollection ApplicationVersionList
        {
            get
            {
                return _ApplicationVersionList;
            }

            set
            {
                SetProperty(ref _ApplicationVersionList, value);
            }
        }

        public string DaText
        {
            get
            {
                return _DaText;
            }

            set
            {
                SetProperty(ref _DaText, value);
            }
        }

        /// <summary>
        /// Gets or sets the header data.
        /// </summary>
        /// <value>
        /// The header data.
        /// </value>
        // [RestorableState]
        public HeaderModel HeaderData
        {
            get
            {
                return _HeaderModel;
            }

            set
            {
                SetProperty(ref _HeaderModel, value);
            }
        }

        /// <summary>
        /// Gets the header detail list.
        /// </summary>
        /// <value>
        /// The header detail list.
        /// </value>
        // [RestorableState]
        public CardGroup HeaderDetailList
        {
            get
            {
                return _HeaderDetailList;
            }

            set
            {
                SetProperty(ref _HeaderDetailList, value);
            }
        }

        public DelegateCommand TestPageCommand { get; private set; }

        /// <summary>
        /// Raises the <see cref="NavigatedTo" /> event.
        /// </summary>
        /// <param name="e">
        /// The <see cref="NavigatedToEventArgs" /> instance containing the event data.
        /// </param>
        /// <param name="viewModelState">
        /// State of the view ViewModel.
        /// </param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public override void PopulateViewModel()
        {
            // cache Header Data record
            HeaderData = DV.HeaderDV.HeaderDataModel;

            // Assembly level stuff
            var assembly = GetType().GetTypeInfo().Assembly;
            var assemblyName = new AssemblyName(assembly.FullName);

            CardListLineCollection t = new CardListLineCollection
                {
            new CardListLine("Application Name", AppInfo.Name),

            new CardListLine("Package Name", AppInfo.PackageName),

            new CardListLine("First Launch Ever?", VersionTracking.IsFirstLaunchEver),

            new CardListLine("First Launch Current Version?", VersionTracking.IsFirstLaunchForCurrentVersion),

            new CardListLine("First Launch Current Build?", VersionTracking.IsFirstLaunchForCurrentBuild),

            new CardListLine("Current Version", VersionTracking.CurrentVersion),

            new CardListLine("Current Build", VersionTracking.CurrentBuild),

            new CardListLine("Previous Version", VersionTracking.PreviousVersion),

            new CardListLine("Previous Build", VersionTracking.PreviousBuild),

            new CardListLine("First Version Installed", VersionTracking.FirstInstalledVersion),

            new CardListLine("First Build Installed", VersionTracking.FirstInstalledBuild),

            // // TODO new CardListLine("versionHistory", VersionTracking.VersionHistory),

            // // TODO new CardListLine("buildHistory", VersionTracking.BuildHistory),

            new CardListLine("Major Version", assemblyName.Version.Major),

            new CardListLine("Minor Version", assemblyName.Version.Minor),

            new CardListLine("Major Revision", assemblyName.Version.MajorRevision),

            new CardListLine("Middle Revision", assemblyName.Version.Revision),

            new CardListLine("Minor Revision", assemblyName.Version.MinorRevision),
            };

            t.Title = "Application Versions";

            ApplicationVersionList = t;       // TODO Ugly -  Trigger SetProperty

            // Set WhatsNew text Set MarkdownView information that is not easily set in XAML
            MarkdownTheme tt = (MarkdownTheme)new DarkMarkdownTheme();
            tt.BackgroundColor = Common.CommonRoutines.ResourceColourGet("CardBackGroundNote");

            //this.mdview.Theme = t;

            // Load Resource
            var assemblyExec = Assembly.GetExecutingAssembly();
            var resourceName = "GrampsView.releasenotes.md";

            using (Stream stream = assemblyExec.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                DaText = reader.ReadToEnd();
            }
        }

        public void TestPage()
        {
            BaseNavigationService.NavigateAsync(nameof(TestPage));

            //BaseNavigationService.NavigateAsync(nameof(AShellPage));
        }
    }
}