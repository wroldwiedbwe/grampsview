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
    using GrampsView.Common;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using Prism.Events;
    using Prism.Navigation;

    /// <summary>
    /// View model for File Input Page.
    /// </summary>
    public partial class SetupStorageViewModel : ViewModelBase
    {
        /// <summary>
        /// The local data detail list.
        /// </summary>
        private CardListLineCollection localDataDetailList;

        /// <summary>
        /// The local data repsitory.
        /// </summary>
        private IDataRepositoryManager localDataRepositoryManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="SetupStorageViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The common logging.
        /// </param>
        /// <param name="iocCommonProgress">
        /// The ioc common progress.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The event aggregator.
        /// </param>
        /// <param name="iocNavigationService">
        /// The navigation service.
        /// </param>
        public SetupStorageViewModel(IDataRepositoryManager iocDataRepositoryManager, ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
            : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
            localDataRepositoryManager = iocDataRepositoryManager;

            ButtonUnLocked = true;

            BaseTitleIcon = CommonConstants.IconSettings;

            // BaseEventAggregator.GetEvent<PageTitleChangedEvent>().Publish(new
            // PageTitleChangedEventArgs { PageTitle = "Setup Storage Page", PageIcon =
            // CommonConstants.IconCitation });
        }

        /// <summary>
        /// Gets or sets a value indicating whether [button locked].
        /// </summary>
        /// <value>
        /// <c>true</c> if [button locked]; otherwise, <c>false</c>.
        /// </value>
        public bool ButtonUnLocked
        {
            get;
            set;
        }
= true;

        // [RestorableState]
        public CardListLineCollection DataDetailList
        {
            get
            {
                return localDataDetailList;
            }

            set
            {
                SetProperty(ref localDataDetailList, value);
            }
        }

        ///// <summary>
        ///// Gramps export XML plus media.
        ///// </summary>
        ///// <param name="sender">
        ///// The sender.
        ///// </param>
        ///// <param name="parameter">
        ///// The parameter.
        ///// </param>
        //public async void HandleSetDataFolderToChosen(object sender, object parameter)
        //{
        //    BaseCL.LogProgress("Calling folder picker");

        // //bool allOk = await StoreFileUtility.GetCurrentInputFolder().ConfigureAwait(false);

        // //if (allOk) //{ // StorageFolder t = DataStore.DS.CurrentDataFolder;
        //    //    DataDetailList = new CardListLineCollection
        //    //{
        //    //    new CardListLine("Data Folder:", DataStore.DS.CurrentDataFolder.Path),
        //    //};
        //    //}
        //}

        ///// <summary>
        ///// Gramps export XML plus media.
        ///// </summary>
        ///// <param name="sender">
        ///// The sender.
        ///// </param>
        ///// <param name="parameter">
        ///// The parameter.
        ///// </param>
        //public async void HandleSetDataFolderToLocalStorage(object sender, object parameter)
        //{
        //    await StoreFileNames.DataFolderSetLocalStorageAsync().ConfigureAwait(false);

        //    //DataDetailList = new CardListLineCollection
        //    //{
        //    //    new CardListLine("Data Folder:", DataStore.DS.CurrentDataFolder.Path),
        //    //};
        //}

        /// <summary>
        /// Load the data.
        /// </summary>
        public override void PopulateViewModel()
        {
            //DataDetailList = new CardListLineCollection
            //{
            //    new CardListLine("Data Folder:", DataStore.DS.CurrentDataFolder.Path),
            //};
        }
    }
}