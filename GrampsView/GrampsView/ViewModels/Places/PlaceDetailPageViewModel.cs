//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="PlaceDetailPageViewModel.cs" company="MeMyselfAndI">
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
    public class PlaceDetailViewModel : ViewModelBase
    {
        /// <summary>
        /// The local book mark object.
        /// </summary>
        private PlaceModel localPlaceObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaceDetailViewModel" /> class.
        /// </summary>
        /// <param name="iocLoggingService">
        /// The ioc logging service.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        /// <param name="iocCommonModelGridBuilder">
        /// The ioc common model grid builder.
        /// </param>
        public PlaceDetailViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
            : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
        }

        /// <summary>
        /// Gets or sets the public Event ViewModel.
        /// </summary>
        // [RestorableState]
        public PlaceModel PlaceObject
        {
            get
            {
                return localPlaceObject;
            }

            set
            {
                SetProperty(ref localPlaceObject, value);
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
            PlaceObject = DV.PlaceDV.GetModel(BaseNavParamsHLink);

            if (PlaceObject != null)
            {
                BaseTitle = PlaceObject.GetDefaultText;
                BaseTitleIcon = CommonConstants.IconPlace;

                // Get Header details
                CardGroup t = new CardGroup { Title = "Header Details" };

                t.Cards.Add(new CardListLineCollection
                    {
                        new CardListLine("Card Type:", "Place Detail"),
                        new CardListLine("Title:", PlaceObject.GPTitle),
                        new CardListLine("Name:", PlaceObject.GName),
                        new CardListLine("Type:", PlaceObject.GType),
                        new CardListLine("Private:", PlaceObject.PrivAsString),
                    });

                t.Cards.Add(DV.PlaceDV.GetModelInfoFormatted(PlaceObject));

                BaseHeader.Add(t);

                // Details
                BaseDetail.Add(PlaceObject.GPlaceRefCollection.GetCardGroupWithTitle("Enclosing Place"));
                BaseDetail.Add(PlaceObject.GCitationRefCollection.GetCardGroup);
                BaseDetail.Add(PlaceObject.GTagRefCollection.GetCardGroup);
                BaseDetail.Add(PlaceObject.GURLCollection.GetCardGroup);

                BaseDetail.Add(PlaceObject.BackHLinkReferenceCollection.GetCardGroup);
            }
        }
    }
}