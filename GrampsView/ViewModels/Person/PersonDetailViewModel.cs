// <copyright file="PersonDetailViewModel.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using Prism.Events;
    using Prism.Navigation;

    /// <summary>
    /// ViewModel for the Person Detail page.
    /// </summary>
    public class PersonDetailViewModel : ViewModelBase
    {
        private HLinkNoteModel _PersonBio = new HLinkNoteModel();

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
        public PersonDetailViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
            : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
            BaseTitle = "Person Detail";
            BaseTitleIcon = CommonConstants.IconPeople;
        }

        /// <summary>
        /// Gets or sets the person biograqphical details.
        /// </summary>
        /// <value>
        /// The person bio.
        /// </value>
        public HLinkNoteModel PersonBio
        {
            get
            {
                return _PersonBio;
            }

            set
            {
                SetProperty(ref _PersonBio, value);
            }
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
        public override void PopulateViewModel()
        {
            BaseCL.LogRoutineEntry("PersonDetailViewModel");

            PersonObject = DV.PersonDV.GetModelFromHLink(BaseNavParamsHLink);

            if (!(PersonObject is null))
            {
                BaseTitle = PersonObject.GBirthName.GetDefaultText;

                // Get Header Details
                CardGroup t = new CardGroup { Title = "Header Details" };

                // Get the Name Details

                CardListLineCollection nameDetails = new CardListLineCollection
                {
                    new CardListLine("Card Type:", "Person Detail"),
                    new CardListLine("Full Name:", PersonObject.GBirthName.FullName),
                    new CardListLine("Name Title:", PersonObject.GBirthName.GTitle),
                    new CardListLine("First Name:", PersonObject.GBirthName.GFirstName)
                };

                if (PersonObject.GBirthName.GSurName.Count > 0)
                {
                    foreach (SurnameModel item in PersonObject.GBirthName.GSurName)
                    {
                        nameDetails.Add(new CardListLine("Surname:", item.GText));
                        nameDetails.Add(new CardListLine("Surname Connector:", item.GConnector));
                        nameDetails.Add(new CardListLine("Surname Derivation:", item.GDerivation));
                        nameDetails.Add(new CardListLine("Surname Prefix:", item.GPrefix));
                        nameDetails.Add(new CardListLine("Surname SecondaryColor:", item.GPrim));
                    }
                }

                nameDetails.Add(new CardListLine("Nickname:", PersonObject.GBirthName.GNick));
                nameDetails.Add(new CardListLine("Family Nickname:", PersonObject.GBirthName.GFamilyNick));
                nameDetails.Add(new CardListLine("Called:", PersonObject.GBirthName.GCall));

                if (PersonObject.GBirthName.GDate.Valid)
                {
                    nameDetails.Add(new CardListLine("Name Date:", PersonObject.GBirthName.GDate.GetShortDateAsString));
                }

                nameDetails.Add(new CardListLine("Name Display:", PersonObject.GBirthName.GDisplay));
                nameDetails.Add(new CardListLine("Name Group:", PersonObject.GBirthName.GGroup));
                nameDetails.Add(new CardListLine("Name Private:", PersonObject.GBirthName.GPriv));
                nameDetails.Add(new CardListLine("Name Sort:", PersonObject.GBirthName.GSort));
                nameDetails.Add(new CardListLine("Name Suffix:", PersonObject.GBirthName.GSuffix));
                nameDetails.Add(new CardListLine("Name Type:", PersonObject.GBirthName.GType));

                t.Cards.Add(nameDetails);

                // Get extra details
                CardListLineCollection tt = new CardListLineCollection
                {
                        new CardListLine("Gender:", PersonObject.GGenderAsString),
                };

                if (PersonObject.BirthDate != null)
                {
                    tt.Add(new CardListLine("Birth Date:", PersonObject.BirthDate.GetLongDateAsString));

                    if (PersonObject.IsLiving)
                    {
                        tt.Add(new CardListLine("Age:", PersonObject.BirthDate.GetAge));
                    }
                    else
                    {
                        tt.Add(new CardListLine("Years Since Birth:", PersonObject.BirthDate.GetAge));

                        EventModel ageAtDeath = DV.EventDV.GetEventType(PersonObject.GEventRefCollection, "Death");
                        if (ageAtDeath.Valid)
                        {
                            tt.Add(new CardListLine("Age at Death:", ageAtDeath.GDate.DateDifferenceDecoded(PersonObject.BirthDate)));
                        }
                    }
                }
                else
                {
                    tt.Add(new CardListLine("Birth Date:", "Unknown"));
                }

                tt.Add(new CardListLine("Is Living:", PersonObject.IsLivingAsString));

                t.Cards.Add(tt);

                // Get children details
                t.Cards.Add(
                    new ParentLinkModel
                    {
                        Parents = PersonObject.GChildOf.DeRef,
                    });

                // Add Standard details
                t.Cards.Add(DV.PersonDV.GetModelInfoFormatted(PersonObject));

                BaseHeader.Add(t);

                BaseDetail.Add(PersonObject.GParentInRefCollection.GetCardGroup());
                BaseDetail.Add(PersonObject.GEventRefCollection.GetCardGroup());

                BaseDetail.Add(PersonObject.GCitationRefCollection.GetCardGroup());
                BaseDetail.Add(PersonObject.GNoteRefCollection.GetCardGroup());
                BaseDetail.Add(PersonObject.GMediaRefCollection.GetCardGroup());
                BaseDetail.Add(PersonObject.GAttributeCollection.GetCardGroup());
                BaseDetail.Add(PersonObject.GAddress.GetCardGroup());
                BaseDetail.Add(PersonObject.GTagRefCollection.GetCardGroup());
                BaseDetail.Add(PersonObject.GURLCollection.GetCardGroup());
                BaseDetail.Add(PersonObject.GLDSCollection.GetCardGroup());

                BaseDetail.Add(PersonObject.GBirthName.CitationRefCollection.GetCardGroup("Name Citations"));
                BaseDetail.Add(PersonObject.GBirthName.NoteReferenceCollection.GetCardGroup("Name Notes"));

                BaseDetail.Add(PersonObject.BackHLinkReferenceCollection.GetCardGroup());

                // TODO localActivitySession = await CommonTimeline.AddToTimeLine("Person",
                // PersonObject, PersonObject.HomeImageHLink.DeRef.MediaStorageFilePath, "Person: "
                // + PersonObject.BirthName.FullName, "Born: " + PersonObject.BirthDate.GetShortDateAsString).ConfigureAwait(false);

                PersonBio = PersonObject.GNoteRefCollection.GetBio;
            }
        }
    }
}