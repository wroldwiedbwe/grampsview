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
        private NoteModel _PersonBio = new NoteModel();

        /// <summary>
        /// The current person.
        /// </summary>
        private PersonModel localPersonModel = null;

        /// <summary>Initializes a new instance of the <see cref="PersonDetailViewModel"/> class.</summary>
        /// <param name="iocCommonLogging">The common logging service.</param>
        /// <param name="iocEventAggregator">The event aggregator.</param>
        /// <param name="iocNavigationService">Prism Navigation Service</param>
        public PersonDetailViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
            : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
            BaseTitle = "Person Detail";
            BaseTitleIcon = CommonConstants.IconPeople;
        }

        public bool mediaImageVisible
        {
            get
            {
                return true;
            }
        }

        public NoteModel PersonBio
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
                return localPersonModel;
            }

            set
            {
                SetProperty(ref localPersonModel, value);
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

            PersonObject = DV.PersonDV.GetModel(BaseNavParamsHLink);

            if (!(PersonObject is null))
            {
                BaseTitle = PersonObject.GBirthName.GetDefaultText;

                // Get Header Details
                CardGroup t = new CardGroup { Title = "Header Details" };

                // Get basic details
                t.Cards.Add(new CardListLineCollection
                {
                    new CardListLine("Card Type:", "Person Detail"),
                    new CardListLine("Full Name:", PersonObject.GBirthName.FullName),
                    new CardListLine("First Name:", PersonObject.GBirthName.GFirstName),
                    new CardListLine("Surname:", PersonObject.GBirthName.GSurName.GetPrimarySurname),
                    new CardListLine("Nickname:", PersonObject.GBirthName.GNick),
                    new CardListLine("Called:", PersonObject.GBirthName.GCall),
                });

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
                        if (DV.EventDV.GetEventType(PersonObject.GEventRefCollection, "Death") != null)
                        {
                            tt.Add(new CardListLine("Age at Death:", DV.EventDV.GetEventType(PersonObject.GEventRefCollection, "Death").GDate.DateDifferenceDecoded(PersonObject.BirthDate)));
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

                BaseDetail.Add(PersonObject.GParentInRefCollection.GetCardGroup);
                BaseDetail.Add(PersonObject.GEventRefCollection.GetCardGroup);

                BaseDetail.Add(PersonObject.GCitationRefCollection.GetCardGroup);
                BaseDetail.Add(PersonObject.GNoteRefCollection.GetCardGroup);
                BaseDetail.Add(PersonObject.GMediaRefCollection.GetCardGroup);
                BaseDetail.Add(PersonObject.GAttributeCollection.GetCardGroup);
                BaseDetail.Add(PersonObject.GAddress.GetCardGroup);
                BaseDetail.Add(PersonObject.GTagRefCollection.GetCardGroup);
                BaseDetail.Add(PersonObject.GURLCollection.GetCardGroup);
                BaseDetail.Add(PersonObject.GLDSCollection.GetCardGroup);

                BaseDetail.Add(PersonObject.BackHLinkReferenceCollection.GetCardGroup);

                // TODO localActivitySession = await CommonTimeline.AddToTimeLine("Person",
                // PersonObject, PersonObject.HomeImageHLink.DeRef.MediaStorageFilePath, "Person: "
                // + PersonObject.BirthName.FullName, "Born: " + PersonObject.BirthDate.GetShortDateAsString).ConfigureAwait(false);

                PersonBio = PersonObject.GNoteRefCollection.GetBio;
            }
        }
    }
}