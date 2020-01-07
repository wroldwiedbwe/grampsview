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
        private NoteModel localNoteObject;

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
        public NoteModel NoteObject
        {
            get
            {
                return localNoteObject;
            }

            set
            {
                SetProperty(ref localNoteObject, value);
            }
        }

        /// <summary>
        /// Gets or sets the note text.
        /// </summary>
        /// <value>
        /// The note text.
        /// </value>
        // [RestorableState]
        public TextDetailCardModel NoteText { get; set; } = new TextDetailCardModel();

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
            NoteObject = DV.NoteDV.GetModel(BaseNavParamsHLink);

            if (NoteObject != null)
            {
                BaseTitle = NoteObject.GetDefaultText;
                BaseTitleIcon = CommonConstants.IconNotes;

                // Get basic details
                CardGroup t = new CardGroup { Title = "Header Details" };

                t.Cards.Add(new CardListLineCollection
                {
                    new CardListLine("Card Type:", "Note Detail"),
                    new CardListLine("Type:", NoteObject.GType),
                    new CardListLine("Private:", NoteObject.PrivAsString),
                });

                // Add Model details
                t.Cards.Add(DV.NoteDV.GetModelInfoFormatted(NoteObject));

                BaseHeader.Add(t);

                BaseDetail.Add(NoteObject.BackHLinkReferenceCollection.GetCardGroup);
            }
        }
    }
}