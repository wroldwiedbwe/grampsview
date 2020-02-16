//-----------------------------------------------------------------------
//
// Various routines used by the Media object Summary Page View Model
//
// <copyright file="SetupStorageViewModel.cs" company="MeMyselfAndI">
// Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.ViewModels
{
    using System.IO;
    using System.Reflection;

    using GrampsView.Common;
    using GrampsView.Data.Repository;
    using GrampsView.Events;
    using GrampsView.Views;

    using Prism.Commands;
    using Prism.Events;
    using Prism.Navigation;

    using Xam.Forms.Markdown;

    /// <summary>
    /// View model for WHats New Page.
    /// </summary>
    public partial class WhatsNewViewModel : ViewModelBase
    {
        /// <summary>
        /// The local data repsitory.
        /// </summary>
        private readonly IDataRepositoryManager _DataRepositoryManager;

        private string _WhatsNewText = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="SetupStorageViewModel"/> class.
        /// </summary>
        /// <param name="iocDataRepositoryManager">
        /// </param>
        /// <param name="iocCommonLogging">
        /// The common logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The event aggregator.
        /// </param>
        /// <param name="iocNavigationService">
        /// The navigation service.
        /// </param>
        public WhatsNewViewModel(IDataRepositoryManager iocDataRepositoryManager, ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
            : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
            _DataRepositoryManager = iocDataRepositoryManager;

            LoadDataCommand = new DelegateCommand(LoadDataAction);

            BaseTitle = "What's new";
        }

        public DelegateCommand LoadDataCommand { get; private set; }

        /// <summary>
        /// Gets or sets the whats new text.
        /// </summary>
        /// <value>
        /// Whats New text
        /// </value>
        public string WhatsNewText
        {
            get
            {
                return _WhatsNewText;
            }

            set
            {
                SetProperty(ref _WhatsNewText, value);
            }
        }

        public void LoadDataAction()
        {
            BaseEventAggregator.GetEvent<AppMainViewStart>().Publish();

            //// Start data load
            //BaseEventAggregator.GetEvent<PageNavigateEvent>().Publish(nameof(MessageLogPage));
        }

        /// <summary>
        /// Load the data.
        /// </summary>
        public override void PopulateViewModel()
        {
            // Set MarkdownView information that is not easily set in XAML
            MarkdownTheme t = (MarkdownTheme)new DarkMarkdownTheme();
            t.BackgroundColor = Common.CommonRoutines.ResourceColourGet("CardBackGroundNote");

            //this.mdview.Theme = t;

            // Load Resource
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "GrampsView.releasenotes.md";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                WhatsNewText = reader.ReadToEnd();
            }
        }
    }
}