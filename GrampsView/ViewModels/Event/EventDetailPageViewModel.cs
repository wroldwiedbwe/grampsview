//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="EventDetailPageViewModel.cs" company="MeMyselfAndI">
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
    /// <seealso cref="Prism.Mvvm.ViewModelBase" />
    public class EventDetailViewModel : ViewModelBase
    {
        /// <summary>
        /// Holds the Event ViewModel.
        /// </summary>
        private EventModel localEventObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventDetailViewModel" /> class.
        /// </summary>
        /// <param name="iocCommonModelGridBuilder">
        /// The ioc common model grid builder.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The event aggregator.
        /// </param>
        /// <param name="iocCommonLogging">
        /// Common Logging interface.
        /// </param>
        public EventDetailViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
            : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
        }

        /// <summary>
        /// Gets or sets the public Event ViewModel.
        /// </summary>
        /// <value>
        /// The current event ViewModel.
        /// </value>
        public EventModel EventObject
        {
            get
            {
                return localEventObject;
            }

            set
            {
                SetProperty(ref localEventObject, value);
            }
        }

        /// <summary>
        /// Populates the view ViewModel.
        /// </summary>
        public override void PopulateViewModel()
        {
            EventObject = DV.EventDV.GetModel(BaseNavParamsHLink);

            if (!(EventObject is null))
            {
                BaseTitle = EventObject.GDescription;
                BaseTitleIcon = CommonConstants.IconEvents;

                // Get basic details
                CardGroup t = new CardGroup { Title = "Header Details" };

                t.Cards.Add(new CardListLineCollection
                    {
                        new CardListLine("Card Type:", "Event Detail"),
                        new CardListLine("Type:", EventObject.GType),
                        new CardListLine("Date:", EventObject.GDate.GetLongDateAsString),
                        new CardListLine("Event Age:", EventObject.GDate.GetAge),
                        new CardListLine("Description:", EventObject.GDescription),
                        new CardListLine("Place:", EventObject.GPlace.HLinkKey),
                    });

                // Add Model details
                t.Cards.Add(DV.EventDV.GetModelInfoFormatted(localEventObject));

                BaseHeader.Add(t);

                BaseDetail.Add(EventObject.GAttribute.GetCardGroup);
                BaseDetail.Add(EventObject.GCitationRefCollection.GetCardGroup);
                BaseDetail.Add(EventObject.GMediaRefCollection.GetCardGroup);
                BaseDetail.Add(EventObject.GNoteRefCollection.GetCardGroup);
                BaseDetail.Add(EventObject.GTagRefCollection.GetCardGroup);

                BaseDetail.Add(EventObject.BackHLinkReferenceCollection.GetCardGroup);
            }
        }
    }
}