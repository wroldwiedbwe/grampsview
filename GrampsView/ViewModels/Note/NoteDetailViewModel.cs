﻿//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="NoteDetailViewModel.cs" company="MeMyselfAndI">
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
    public class NoteDetailViewModel : ViewModelBase
    {
        /// <summary>
        /// Holds the Note ViewModel.
        /// </summary>
        private HLinkNoteModel _HLinkNote;

        private NoteModel _NoteObject = new NoteModel();

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteDetailViewModel"/> class. Common logging.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// Common logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// Common Event Aggregator.
        /// </param>
        /// <param name="iocNavigationService">
        /// </param>
        public NoteDetailViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
            : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
            BaseTitleIcon = CommonConstants.IconNotes;
        }

        /// <summary>
        /// Gets or sets the note object.
        /// </summary>
        /// <value>
        /// The note object.
        /// </value>
        public HLinkNoteModel HLinkNote
        {
            get
            {
                return _HLinkNote;
            }

            set
            {
                SetProperty(ref _HLinkNote, value);
            }
        }

        /// <summary>
        /// Gets or sets the public Event ViewModel.
        /// </summary>
        /// <value>
        /// The current event ViewModel.
        /// </value>
        public NoteModel NoteObject
        {
            get
            {
                return _NoteObject;
            }

            set
            {
                SetProperty(ref _NoteObject, value);
            }
        }

        /// <summary>
        /// Populates the view ViewModel.
        /// </summary>
        public override void PopulateViewModel()
        {
            HLinkNoteModel HLinkObject = BaseNavParamsHLink as HLinkNoteModel;

            if (!(HLinkObject is null) && (HLinkObject.Valid))
            {
                NoteObject = HLinkObject.DeRef;

                BaseTitle = NoteObject.GetDefaultText;

                // Get basic details
                CardGroup basicHeaderDetails = new CardGroup { Title = "Header Details" };

                basicHeaderDetails.Add(new CardListLineCollection
                {
                    new CardListLine("Card Type:", "Note Detail"),
                    new CardListLine("Type:", NoteObject.GType),
                });

                // Add Model details
                basicHeaderDetails.Add(DV.NoteDV.GetModelInfoFormatted(NoteObject));

                BaseHeader.Add(basicHeaderDetails);

                HLinkNote = NoteObject.HLink;

                BaseDetail.Add(NoteObject.GTagRefCollection.GetCardGroup());

                BaseBackLinks.Add(NoteObject.BackHLinkReferenceCollection.GetCardGroup());
            }
        }
    }
}