// <copyright file="PersonDetailViewModel.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;
    using GrampsView.UserControls;

    using Prism.Events;
    using Prism.Navigation;

    using System.Diagnostics.Contracts;
    using System.Threading.Tasks;

    /// <summary>
    /// ViewModel for the Person Detail page.
    /// </summary>
    public class PersonDetailViewModel : ViewModelBase
    {
        /// <summary>
        /// The current person.
        /// </summary>
        private PersonModel _PersonObject = new PersonModel();

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonDetailViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The common logging service.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The event aggregator.
        /// </param>
        /// <param name="iocNavigationService">
        /// Prism Navigation Service
        /// </param>
        public PersonDetailViewModel(ICommonLogging iocCommonLogging)
            : base(iocCommonLogging)
        {
            BaseTitle = "Person Detail";
            BaseTitleIcon = CommonConstants.IconPeople;
        }

        /// <summary>
        /// Gets or sets the View Current Person.
        /// </summary>
        /// <value>
        /// The current person ViewModel.
        /// </value>
        public PersonModel PersonObject
        {
            get
            {
                return _PersonObject;
            }

            set
            {
                SetProperty(ref _PersonObject, value);
            }
        }

        /// <summary>
        /// Called when [navigating from].
        /// </summary>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        public void OnNavigatingFrom(INavigationParameters parameters)
        {
            OnNavigatedFrom(parameters);

            // TODO CommonTimeline.FinishActivitySessionAsync(localActivitySession);
        }

        /// <summary>
        /// Populates the view ViewModel.
        /// </summary>
        /// <returns>
        /// </returns>
        public override async Task<bool> PopulateViewModelAsync()
        {
            BaseCL.LogRoutineEntry("PersonDetailViewModel");

            PersonObject = DV.PersonDV.GetModelFromHLink(BaseNavParamsHLink);

            if (!(PersonObject is null))
            {
                BaseTitle = PersonObject.GPersonNamesCollection.GetPrimaryName.GetDefaultText;

                // Get media image
                HLinkHomeImageModel personImage = PersonObject.HomeImageHLink;
                Contract.Assert(PersonObject.HomeImageHLink != null, PersonObject.Id);
                personImage.CardType = DisplayFormat.MediaCardLarge;
                BaseDetail.Add(personImage);

                // Get Header Details
                CardGroup headerCardGroup = new CardGroup { Title = "Header Details" };

                // Get the Name Details
                CardListLineCollection nameDetails = new CardListLineCollection
                {
                    new CardListLine("Card Type:", "Person Detail"),
                };

                headerCardGroup.Add(nameDetails);

                // Handle the common case where there is only one name
                if (PersonObject.GPersonNamesCollection.Count == 1)
                {
                    headerCardGroup.Add(PersonObject.GPersonNamesCollection[0]);
                }

                // Get extra details
                CardListLineCollection extraDetailsCard = new CardListLineCollection
                {
                        new CardListLine("Gender:", PersonObject.GGenderAsString),
                };

                if (PersonObject.BirthDate != null)
                {
                    extraDetailsCard.Add(new CardListLine("Birth Date:", PersonObject.BirthDate.GetLongDateAsString));

                    if (PersonObject.IsLiving)
                    {
                        extraDetailsCard.Add(new CardListLine("Age:", PersonObject.BirthDate.GetAge));
                    }
                    else
                    {
                        extraDetailsCard.Add(new CardListLine("Years Since Birth:", PersonObject.BirthDate.GetAge));

                        EventModel ageAtDeath = DV.EventDV.GetEventType(PersonObject.GEventRefCollection, "Death");
                        if (ageAtDeath.Valid)
                        {
                            extraDetailsCard.Add(new CardListLine("Age at Death:", ageAtDeath.GDate.DateDifferenceDecoded(PersonObject.BirthDate)));
                        }
                    }
                }
                else
                {
                    extraDetailsCard.Add(new CardListLine("Birth Date:", "Unknown"));
                }

                extraDetailsCard.Add(new CardListLine("Is Living:", PersonObject.IsLivingAsString));

                headerCardGroup.Add(extraDetailsCard);

                // Get parent details
                headerCardGroup.Add(
                    new ParentLinkModel
                    {
                        Parents = PersonObject.GChildOf.DeRef,
                    });

                // Add Standard details
                headerCardGroup.Add(DV.PersonDV.GetModelInfoFormatted(PersonObject));

                BaseDetail.Add(headerCardGroup);

                // Handle the uncommon case where there is more than one name
                if (PersonObject.GPersonNamesCollection.Count > 1)
                {
                    // Add extra name details
                    BaseDetail.Add(PersonObject.GPersonNamesCollection.GetCardGroup1());
                }

                // Get Bio
                HLinkNoteModel bioCard = PersonObject.GNoteRefCollection.GetBio;
                bioCard.CardType = DisplayFormat.NoteCardFull;
                BaseDetail.Add(bioCard);

                // Add details
                BaseDetail.Add(PersonObject.GParentInRefCollection.GetCardGroup());
                BaseDetail.Add(PersonObject.GEventRefCollection.GetCardGroup());
                BaseDetail.Add(PersonObject.GCitationRefCollection.GetCardGroup());
                BaseDetail.Add(PersonObject.GNoteRefCollection.GetCardGroup());
                BaseDetail.Add(PersonObject.GMediaRefCollection.GetCardGroup());
                BaseDetail.Add(PersonObject.GAttributeCollection);
                BaseDetail.Add(PersonObject.GAddress);
                BaseDetail.Add(PersonObject.GTagRefCollection.GetCardGroup());
                BaseDetail.Add(PersonObject.GURLCollection);
                BaseDetail.Add(PersonObject.GLDSCollection);
                BaseDetail.Add(PersonObject.GPersonRefCollection);

                BaseDetail.Add(PersonObject.GPersonNamesCollection.GetPrimaryName.GCitationRefCollection.GetCardGroup("Name Citations"));
                BaseDetail.Add(PersonObject.GPersonNamesCollection.GetPrimaryName.GNoteReferenceCollection.GetCardGroup("Name Notes"));

                BaseDetail.Add(PersonObject.BackHLinkReferenceCollection.GetCardGroup());

                // TODO localActivitySession = await CommonTimeline.AddToTimeLine("Person",
                // PersonObject, PersonObject.HomeImageHLink.DeRef.MediaStorageFilePath, "Person: "
                // + PersonObject.BirthName.FullName, "Born: " + PersonObject.BirthDate.GetShortDateAsString).ConfigureAwait(false);
            }

            return true;
        }
    }
}