//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="SourceDetailViewModel.cs" company="MeMyselfAndI">
// Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using Prism.Events;
    using Prism.Navigation;

    /// <summary>
    /// Defines the EVent Detail Page View ViewModel.
    /// </summary>
    public class SourceDetailViewModel : ViewModelBase
    {
        /// <summary>
        /// The local book mark object.
        /// </summary>
        private SourceModel localSourcesObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceDetailViewModel" /> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// </param>
        /// <param name="iocCommonModelGridBuilder">
        /// </param>
        /// <param name="iocEventAggregator">
        /// </param>
        public SourceDetailViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
            : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
            BaseTitle = "Source Detail";
            BaseTitleIcon = CommonConstants.IconSource;
        }

        ///// <summary>
        ///// Initializes a new instance of the <see cref="SourceDetailViewModel" /> class.
        ///// </summary>
        //public SourceDetailViewModel()
        //{
        //}

        /// <summary>
        /// Gets or sets the public Event ViewModel.
        /// </summary>
        /// <value>
        /// The source object.
        /// </value>
        // [RestorableState]
        public SourceModel SourceObject
        {
            get
            {
                return localSourcesObject;
            }

            set
            {
                SetProperty(ref localSourcesObject, value);
            }
        }

        /// <summary>
        /// Populates the view ViewModel.
        /// </summary>
        /// <returns>
        /// </returns>
        public override void PopulateViewModel()
        {
            // cache the Note model
            SourceObject = DV.SourceDV.GetModelFromHLink(BaseNavParamsHLink);

            if (!(SourceObject is null))
            {
                // Get basic details
                BaseTitle = SourceObject.GetDefaultText;
                BaseTitleIcon = CommonConstants.IconSource;

                // Header Card
                CardGroup t = new CardGroup { Title = "Header Details" };

                t.Cards.Add(new CardListLineCollection
                    {
                       new CardListLine("Card Type:", "Source Detail"),
                       new CardListLine("Title:", SourceObject.GSTitle),
                       new CardListLine("Author:", SourceObject.GSAuthor),
                       new CardListLine("Pub Info:", SourceObject.GSPubInfo),
                       new CardListLine("Abbrev:", SourceObject.GSAbbrev),
                    });

                // Add Model details
                t.Cards.Add(DV.SourceDV.GetModelInfoFormatted(SourceObject));

                BaseHeader.Add(t);

                //// Add Repository Refs
                // TODO? foreach (HLinkRepositoryModel item in SourceObject.GRepositoryRefCollection)
                // { ModelDetail.Add(item); }

                // Add bulk items
                BaseDetail.Add(SourceObject.GMediaRefCollection.GetCardGroup());
                BaseDetail.Add(SourceObject.GNoteRefCollection.GetCardGroup());
                BaseDetail.Add(SourceObject.GTagRefCollection.GetCardGroup());
                BaseDetail.Add(SourceObject.GRepositoryRefCollection.GetCardGroup());
                BaseDetail.Add(SourceObject.GSourceAttributeCollection.GetCardGroup());
                BaseDetail.Add(SourceObject.BackHLinkReferenceCollection.GetCardGroup());
            }
        }
    }
}