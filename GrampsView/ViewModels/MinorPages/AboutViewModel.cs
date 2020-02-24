//-----------------------------------------------------------------------
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
    using GrampsView.UserControls;
    using Prism.Commands;
    using Prism.Events;
    using Prism.Navigation;

    using System.IO;
    using System.Reflection;
    using System.Threading.Tasks;

    using Xam.Forms.Markdown;

    using Xamarin.Essentials;
    using Xamarin.Forms;

    public class AboutViewModel : ViewModelBase
    {
        private CardListLineCollection _ApplicationVersionList = new CardListLineCollection();

        private RadioItems _daRadioItems = new RadioItems();

        private string _DaText = string.Empty;

        private CardGroup _HeaderDetailList = new CardGroup();

        private HeaderModel _HeaderModel = null;

        public AboutViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
                                            : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
            BaseTitle = "About";
            BaseTitleIcon = CommonConstants.IconSettings;

            //Setup Theme radio buttons
            RadioButtonToggledCommand = new DelegateCommand<RadioItemToggledEventArgs>(HandleRadioToggle);

            DaRadioItems.Add(new RadioItem
            {
                Text = "Light Theme",
                Toggled = false
            });

            DaRadioItems.Add(new RadioItem
            {
                Text = "Dark Theme",
                Toggled = false
            });

            DaRadioItems.Add(new RadioItem
            {
                Text = "System Theme",
                Toggled = false
            });

            switch (CommonLocalSettings.ApplicationTheme)
            {
                case AppTheme.Light:
                    {
                        DaRadioItems[0].Toggled = true;
                        break;
                    }

                case AppTheme.Dark:
                    {
                        DaRadioItems[1].Toggled = true;
                        break;
                    }

                default:
                    {
                        DaRadioItems[2].Toggled = true;
                        break;
                    }
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

        public string AppName
        {
            get
            {
                return AppInfo.Name;
            }
        }

        public RadioItems DaRadioItems
        {
            get
            {
                return _daRadioItems;
            }

            set
            {
                SetProperty(ref _daRadioItems, value);
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

        public DelegateCommand<RadioItemToggledEventArgs> RadioButtonToggledCommand { get; private set; }

        public void HandleRadioToggle(RadioItemToggledEventArgs argRadioItem)
        {
            switch (argRadioItem.SelectedItem.Text)
            {
                case "Dark Theme":
                    {
                        CommonTheming.SetThemeDark();
                        break;
                    }

                case "Light Theme":
                    {
                        CommonTheming.SetThemeLight();
                        break;
                    }

                default:
                    {
                        CommonTheming.SetThemeSystem();
                        break;
                    }
            }
        }

        //public bool UseDarkTheme
        //{
        //    get
        //    {
        //        return _AppDarkTheme;
        //    }

        // set { if (SetRadioButton(AppTheme.Dark,value)) { SetProperty(ref _AppDarkTheme, value);

        //            CommonTheming.SetThemeDark();
        //        }
        //    }
        //}

        //private bool _UseDarkThemeChecked = false;

        //public bool UseDarkThemeChecked
        //{
        //    get
        //    {
        //        return _UseDarkThemeChecked;
        //    }

        //    set
        //    {
        //        SetProperty(ref _UseDarkThemeChecked, value);
        //    }
        //}

        //private bool _UseLightThemeChecked = false;

        //public bool UseLightThemeChecked
        //{
        //    get
        //    {
        //        return _UseLightThemeChecked;
        //    }

        //    set
        //    {
        //        SetProperty(ref _UseLightThemeChecked, value);
        //    }
        //}

        //private bool _UseSystemThemeChecked = false;

        //public bool UseSystemThemeChecked
        //{
        //    get
        //    {
        //        return _UseSystemThemeChecked;
        //    }

        //    set
        //    {
        //        SetProperty(ref _UseSystemThemeChecked, value);
        //    }
        //}

        //public bool UseLightTheme
        //{
        //    get
        //    {
        //        return _AppLightTheme;
        //    }

        // set { if (SetRadioButton(AppTheme.Light,value)) { SetProperty(ref _AppLightTheme, value);

        //            CommonTheming.SetThemeLight();
        //        }
        //    }
        //}

        //public bool UseSystemTheme
        //{
        //    get
        //    {
        //        return _AppSystemTheme;
        //    }

        // set { if (SetRadioButton(AppTheme.Unspecified,value)) { SetProperty(ref _AppSystemTheme, value);

        //            CommonTheming.SetThemeSystem();
        //        }
        //    }
        //}

        /// <summary>
        /// Raises the <see cref="NavigatedTo"/> event.
        /// </summary>
        /// <param name="e">
        /// The <see cref="NavigatedToEventArgs"/> instance containing the event data.
        /// </param>
        /// <param name="viewModelState">
        /// State of the view ViewModel.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
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
            tt.BackgroundColor = CommonRoutines.ResourceColourGet("CardBackGroundNote");

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

        //private bool SetRadioButton(AppTheme ArgThemeSelected, bool argNewValue)
        //{
        //    switch (ArgThemeSelected)
        //    {
        //        case AppTheme.Unspecified:
        //            {
        //                if (UseSystemTheme == true)
        //                {
        //                    return false;
        //                }

        // UseDarkTheme = false; UseLightTheme = false;

        // return true; }

        // case AppTheme.Light: { if (UseLightTheme == true) { return false; }

        // UseDarkTheme = false; UseSystemTheme = false;

        // return true; }

        // case AppTheme.Dark: { if (UseDarkTheme == argNewValue) { return false; }

        // if (UseDarkTheme == true) { return false; }

        // UseSystemTheme = false; UseLightTheme = false;

        //                return true;
        //            }
        //        default:
        //            {
        //                return true;
        //            }
        //    }
        //}
    }
}