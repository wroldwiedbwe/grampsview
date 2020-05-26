﻿//-----------------------------------------------------------------------
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

    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.Contracts;
    using System.Threading.Tasks;

    /// <summary>
    /// View model for the Hub Page.
    /// </summary>
    public class HubViewModel : ViewModelBase
    {
        /// <summary>
        /// Media object for the Hub 'Hero' image.
        /// </summary>
        private HLinkMediaModel _HeroImage = new HLinkMediaModel();

        /// <summary>
        /// Initializes a new instance of the <see cref="HubViewModel"/> class.
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
            BaseTitleIcon = CommonConstants.IconHub;

            BaseEventAggregator.GetEvent<DataLoadCompleteEvent>().Subscribe(CheckHeroImageLoad, ThreadOption.BackgroundThread);
        }

        /// <summary>
        /// Gets or sets the hero image.
        /// </summary>
        /// <value>
        /// The hero image.
        /// </value>
        public HLinkMediaModel HeroImage
        {
            get
            {
                return _HeroImage;
            }

            set
            {
                SetProperty(ref _HeroImage, value);
            }
        }

        public static void CheckHeroImageLoad(object value)
        {
        }

        /// <summary>
        /// Called when [navigating from].
        /// </summary>
        public void OnNavigatingFrom()
        {
            // Clear large Bitmap Image
            if (HeroImage != null)
            {
                HeroImage.DeRef.FullImageClean();
            }
        }

        /// <summary>
        /// Populate the Hub View.
        /// </summary>
        public override async Task<bool> PopulateViewModelAsync()
        {
            // Get Header CardLarge
            HLinkHeaderModel HeaderCard = DV.HeaderDV.HeaderDataModel.HLink;
            HeaderCard.CardType = DisplayFormat.HeaderCardLarge;
            BaseDetail.Add(HeaderCard);

            HLinkMediaModel HeroImage = DV.MediaDV.GetRandomFromCollection(null);
            HeroImage.CardType = DisplayFormat.MediaCardLarge;
            BaseDetail.Add(HeroImage);

            if (HeroImage == null)
            {
                DataStore.CN.NotifyAlert("No images found in this data.  Consider adding some.");
            }

            // Setup ToDo list
            ObservableCollection<NoteModel> t = DV.NoteDV.GetAllOfType(NoteModel.GTypeToDo);

            CardGroup temp = new CardGroup();

            foreach (NoteModel item in t)
            {
                temp.Add(item.HLink);
            }

            temp.Title = "ToDo list";
            BaseDetail.Add(temp);

            // Setup Latest Changes list

            // TODO fix this LatestChanges.Add(DV.BookMarkDV.GetLatestChanges());

            BaseDetail.Add(DV.CitationDV.GetLatestChanges());

            BaseDetail.Add(DV.EventDV.GetLatestChanges());

            BaseDetail.Add(DV.FamilyDV.GetLatestChanges());

            BaseDetail.Add(DV.MediaDV.GetLatestChanges());

            BaseDetail.Add(DV.NoteDV.GetLatestChanges());

            BaseDetail.Add(DV.PersonDV.GetLatestChanges());

            BaseDetail.Add(DV.PlaceDV.GetLatestChanges());

            BaseDetail.Add(DV.SourceDV.GetLatestChanges());

            BaseDetail.Add(DV.TagDV.GetLatestChanges());

            return true;
        }
    }
}