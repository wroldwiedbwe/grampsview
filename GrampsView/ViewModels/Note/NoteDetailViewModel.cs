//-----------------------------------------------------------------------
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

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteDetailViewModel" /> class. Common logging.
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
        }

        /// <summary>
        /// Gets or sets the note object.
        /// </summary>
        /// <value>
        /// The note object.
        /// </value>
        // [RestorableState]
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
        /// Handles navigation in wards and sets up the event model parameter.
        /// </summary>
        /// <param name="e">
        /// The <see cref="NavigatedToEventArgs" /> instance containing the event data.
        /// </param>
        /// <param name="viewModelState">
        /// The parameter is not used.
        /// </param>
        public override void PopulateViewModel()
        {
            HLinkNote = BaseNavParamsHLink as HLinkNoteModel;

            if (HLinkNote.Valid)
            {
                NoteModel NoteModel = DV.NoteDV.GetModelFromHLink(BaseNavParamsHLink);

                BaseTitle = NoteModel.GetDefaultText;
                BaseTitleIcon = CommonConstants.IconNotes;

                // Get basic details
                CardGroup t = new CardGroup { Title = "Header Details" };

                t.Cards.Add(new CardListLineCollection
                {
                    new CardListLine("Card Type:", "Note Detail"),
                    new CardListLine("Type:", NoteModel.GType),
                    new CardListLine("Private:", NoteModel.PrivAsString),
                });

                // Add Model details
                t.Cards.Add(DV.NoteDV.GetModelInfoFormatted(NoteModel));

                BaseHeader.Add(t);

                BaseDetail.Add(NoteModel.GTagRefCollection.GetCardGroup());
                BaseDetail.Add(NoteModel.BackHLinkReferenceCollection.GetCardGroup());
            }
        }
    }
}