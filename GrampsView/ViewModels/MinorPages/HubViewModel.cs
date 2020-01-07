//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="HubViewModel.cs" company="MeMyselfAndI">
// Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;
    using GrampsView.Events;
    using Prism.Events;
    using Prism.Navigation;

    /// <summary>
    /// View model for the Hub Page.
    /// </summary>
    public class HubViewModel : ViewModelBase
    {
        /// <summary>
        /// The local header ViewModel.
        /// </summary>
        private HeaderModel localHeaderModel = new HeaderModel();

        /// <summary>
        /// Media object for the Hub 'Hero' image.
        /// </summary>
        private MediaModel localHeroImage;

        /// <summary>
        /// Initializes a new instance of the <see cref="HubViewModel" /> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The ioc common logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        /// <param name="iocNavigationService">
        /// </param>
        public HubViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
       : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
            BaseTitle = "Hub";
            BaseTitleIcon = Common.CommonConstants.IconHub;

            BaseEventAggregator.GetEvent<DataLoadCompleteEvent>().Subscribe(CheckHeroImageLoad, ThreadOption.BackgroundThread);
        }

        public static void CheckHeroImageLoad(object value)
        {
        }

        /// <summary>
        /// Gets or sets the header data.
        /// </summary>
        /// <value>
        /// The header data.
        /// </value>
        public HeaderModel HeaderData
        {
            get
            {
                return localHeaderModel;
            }

            set
            {
                SetProperty(ref localHeaderModel, value);
            }
        }

        /// <summary>
        /// Gets or sets the hero image.
        /// </summary>
        /// <value>
        /// The hero image.
        /// </value>
        // [RestorableState]
        public MediaModel HeroImage
        {
            get
            {
                return localHeroImage;
            }

            set
            {
                SetProperty(ref localHeroImage, value);
            }
        }

        /// <summary>
        /// Gets the todo list.
        /// </summary>
        /// <value>
        /// The todo list.
        /// </value>
        public CardGroup TodoList
        {
            get
            {
                return _TodoList;
            }

            set
            {
                SetProperty(ref _TodoList, value);
            }
        }

        private CardGroup _TodoList = new CardGroup();

        /// <summary>Called when [navigating from].</summary>
        public void OnNavigatingFrom()
        {
            // Clear large Bitmap Image
            if (HeroImage != null)
            {
                HeroImage.FullImageClean();
            }
        }

        /// <summary>
        /// Populate the Hub View.
        /// </summary>
        public override void PopulateViewModel()
        {
            HeaderData = DV.HeaderDV.HeaderDataModel;

            // Load the full bitmap
            HeroImage = DV.MediaDV.GetModel(DV.MediaDV.GetRandomFromCollection(null).HLinkKey);

            if (HeroImage == null)
            {
                DataStore.CN.MajorStatusAdd("No images found in this data.  Consider adding some.");
            }

            // Setup ToDo list
            CardGroup temp = DV.NoteDV.AsCardGroup(DV.NoteDV.GetAllOfType(NoteModel.GTypeToDo));
            temp.Title = "ToDo list";
            TodoList = temp;
        }
    }
}