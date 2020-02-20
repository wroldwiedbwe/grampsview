﻿// <copyright file="CitationDetailPageViewModel.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

/// <summary>
/// Defines the Citation Detail Page View Model
/// </summary>
namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using Prism.Events;
    using Prism.Navigation;

    using System.Globalization;

    /// <summary>
    /// Defines the Citation Detail Page View ViewModel.
    /// </summary>
    public class CitationDetailViewModel : ViewModelBase
    {
        private HLinkSourceModel _SourceObject = new HLinkSourceModel();

        /// <summary>
        /// Holds the Note ViewModel.
        /// </summary>
        private CitationModel localCitationObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="CitationDetailViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// </param>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        /// <param name="iocNavigationService">
        /// Navigation Service
        /// </param>
        public CitationDetailViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
            : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
        }

        /// <summary>
        /// Gets or sets the citation object.
        /// </summary>
        /// <value>
        /// The citation object.
        /// </value>
        // [RestorableState]
        public CitationModel CitationObject
        {
            get
            {
                return localCitationObject;
            }

            set
            {
                SetProperty(ref localCitationObject, value);
            }
        }

        /// <summary>
        /// Gets or sets the source object.
        /// </summary>
        /// <value>
        /// The source object.
        /// </value>
        public HLinkSourceModel SourceObject
        {
            get
            {
                return _SourceObject;
            }
            set
            {
                SetProperty(ref _SourceObject, value);
            }
        }

        /// <summary>
        /// Handles navigation in wards and sets up the event model parameter.
        /// </summary>
        /// <param name="e">
        /// The <see cref="NavigatedToEventArgs"/> instance containing the event data.
        /// </param>
        /// <param name="viewModelState">
        /// The parameter is not used.
        /// </param>
        public override void PopulateViewModel()
        {
            // Handle HLinkKeys

            CitationObject = DV.CitationDV.GetModelFromHLinkString(BaseNavParamsHLink.HLinkKey);

            if (CitationObject != null)
            {
                BaseTitle = CitationObject.GetDefaultText;
                BaseTitleIcon = CommonConstants.IconCitation;

                // Get basic details
                CardGroup t = new CardGroup { Title = "Header Details" };

                t.Cards.Add(new CardListLineCollection
            {
                    new CardListLine("Card Type:", "Citation Detail"),
                    new CardListLine("Date:", CitationObject.GDateContent.GetLongDateAsString),
                    new CardListLine("Page:", CitationObject.GPage),
                    new CardListLine("Confidence:", CitationObject.GConfidence.ToString(CultureInfo.CurrentCulture)),
            });

                t.Cards.Add(DV.CitationDV.GetModelInfoFormatted(CitationObject));

                BaseHeader.Add(t);

                // Add Source details
                SourceObject = CitationObject.GSourceRef;

                // If only one note (the most common case) just display it in a large format,
                // otherwise setup a list of them.
                if (CitationObject.GNoteRefCollection.Count > 0)
                {
                    // NoteObject = CitationObject.GNoteRefCollection[0].DeRef;
                }

                // Add remaining details
                BaseDetail.Add(CitationObject.GMediaRefCollection.GetCardGroup());
                BaseDetail.Add(CitationObject.GNoteRefCollection.GetCardGroup());
                BaseDetail.Add(CitationObject.GTagRef.GetCardGroup());
                BaseDetail.Add(CitationObject.GSourceAttributeCollection.GetCardGroup());
                BaseDetail.Add(CitationObject.BackHLinkReferenceCollection.GetCardGroup());
            }
        }
    }
}