// <copyright file="PersonDataView.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Data.DataView
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.Serialization;

    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using GrampsView.Data.Model;

    using GrampsView.Data.Repositories;

    using GrampsView.Data.Repository;

    public class PersonDataView : DataViewBase<PersonModel, HLinkPersonModel, HLinkPersonModelCollection>, IPersonDataView
    {
        /// <summary>
        /// The local current person h link key.
        /// </summary>
        private string localCurrentPersonHLinkKey = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonDataView"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The ioc common logging.
        /// </param>
        public PersonDataView()
        {
            // TODO fix this force to gilbert handcock for now
            localCurrentPersonHLinkKey = "_c47a1a91aec0a6220a5";
        }

        /// <summary>
        /// Gets or sets the current.
        /// </summary>
        /// <value>
        /// The current.
        /// </value>
        public HLinkPersonModel Current
        {
            get
            {
                HLinkPersonModel t = new HLinkPersonModel
                {
                    HLinkKey = localCurrentPersonHLinkKey,
                };

                return t;
            }

            set
            {
                localCurrentPersonHLinkKey = value.HLinkKey;

                // TODO set property
            }
        }

        /// <summary>
        /// Gets the data default sort.
        /// </summary>
        /// <value>
        /// The data default sort.
        /// </value>
        public override IReadOnlyList<PersonModel> DataDefaultSort
        {
            get
            {
                return DataViewData.Items.OrderBy(PersonModel => PersonModel.GBirthName.SortName).ToList();
            }
        }

        /// <summary>
        /// Gets the data view data.
        /// </summary>
        /// <value>
        /// The data view data.
        /// </value>
        public override RepositoryModelType<PersonModel, HLinkPersonModel> DataViewData
        {
            get
            {
                return PersonData;
            }
        }

        /// <summary>
        /// Gets the groups by letter.
        /// </summary>
        /// <returns>
        /// </returns>
        public override List<CommonGroupInfoCollection<PersonModel>> GetGroupsByLetter
        {
            get
            {
                List<CommonGroupInfoCollection<PersonModel>> groups = new List<CommonGroupInfoCollection<PersonModel>>();

                var query = from item in PersonData.Items
                            orderby item.GBirthName.SortName
                            group item by (item.GBirthName.GSurName + " ").ToUpper(CultureInfo.CurrentCulture).Substring(0, 1) into g
                            select new { GroupName = g.Key, Items = g };

                foreach (var g in query)
                {
                    CommonGroupInfoCollection<PersonModel> info = new CommonGroupInfoCollection<PersonModel>
                    {
                        Key = g.GroupName,
                    };

                    foreach (var item in g.Items)
                    {
                        info.Add(item);
                    }

                    groups.Add(info);
                }

                return groups;
            }
        }

        /// <summary>
        /// Gets or sets the person data.
        /// </summary>
        /// <value>
        /// The person data.
        /// </value>
        [DataMember]
        public RepositoryModelType<PersonModel, HLinkPersonModel> PersonData
        {
            get
            {
                return DataStore.DS.localPersonData;
            }

            set
            {
                SetProperty(ref DataStore.DS.localPersonData, value: value);
            }
        }

        /// <summary>
        /// Collections the sort birth date asc.
        /// </summary>
        /// <param name="collectionArg">
        /// The collection argument.
        /// </param>
        /// <returns>
        /// </returns>
        public static ObservableCollection<PersonModel> CollectionSortBirthDateAsc(ObservableCollection<PersonModel> collectionArg)
        {
            if (collectionArg == null)
            {
                return null;
            }

            // sort the list
            IEnumerable<PersonModel> sortedList = collectionArg.OrderBy(PersonModel => PersonModel.BirthDate);

            return new ObservableCollection<PersonModel>(sortedList);
        }

        /// <summary>
        /// Gets all as hlink.
        /// </summary>
        /// <returns>
        /// </returns>
        public HLinkPersonModelCollection GetAllAsHLink()
        {
            HLinkPersonModelCollection t = new HLinkPersonModelCollection();

            foreach (var item in DataDefaultSort)
            {
                t.Add(item.HLink);
            }

            t = HLinkCollectionSort(t);

            return t;
        }

        /// <summary>
        /// Gets the default image from collection.
        /// </summary>
        /// <param name="argModel">
        /// The argument ViewModel.
        /// </param>
        /// <returns>
        /// </returns>
        public HLinkMediaModel GetDefaultImageFromCollection(PersonModel argModel)
        {
            HLinkMediaModel returnHLink = new HLinkMediaModel();

            // ???? TODO "Default Image Citation"
            return returnHLink;
        }

        /// <summary>
        /// Gets the latest changes for the Person Data View.
        /// </summary>
        /// <returns>
        /// </returns>
        public override CardGroup GetLatestChanges()
        {
            DateTime lastSixtyDays = DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0, 0));

            IEnumerable tt = DataViewData.Items.OrderByDescending(GetLatestChangest => GetLatestChangest.Change).Where(GetLatestChangestt => GetLatestChangestt.Change > lastSixtyDays).Take(3);

            CardGroup returnCardGroup = new CardGroup();

            foreach (PersonModel item in tt)
            {
                returnCardGroup.Cards.Add(item.HLink);
            }

            returnCardGroup.Title = "Latest Person Changes";

            return returnCardGroup;
        }

        /// <summary>
        /// Gets the specified h link string.
        /// </summary>
        /// <param name="HLinkString">
        /// The h link string.
        /// </param>
        /// <returns>
        /// </returns>
        public override PersonModel GetModelFromHLinkString(string HLinkString)
        {
            return PersonData[HLinkString];
        }

        //    return groups;
        //}
        /// <summary>
        /// Gets the person plus family events.
        /// </summary>
        /// <param name="argPerson">
        /// The argument person.
        /// </param>
        /// <returns>
        /// Person and where parent in families events.
        /// </returns>
        public ObservableCollection<EventModel> GetPersonPlusFamilyEvents(PersonModel argPerson)
        {
            if (argPerson is null)
            {
                return new ObservableCollection<EventModel>();
            }

            ObservableCollection<EventModel> t = argPerson.GEventRefCollection.DeRef;

            foreach (HLinkFamilyModel theFamily in argPerson.GParentInRefCollection)
            {
                foreach (EventModel theFamilyEvent in theFamily.DeRef.GEventRefCollection.DeRef)
                {
                    t.Add(theFamilyEvent);
                }
            }

            return t;
        }

        // groups.Add(info); }
        /// <summary>
        /// hes the link collection sort.
        /// </summary>
        /// <param name="collectionArg">
        /// The collection argument.
        /// </param>
        /// <returns>
        /// Sorted hlink collection.
        /// </returns>
        public override HLinkPersonModelCollection HLinkCollectionSort(HLinkPersonModelCollection collectionArg)
        {
            if (collectionArg == null)
            {
                return null;
            }

            IOrderedEnumerable<HLinkPersonModel> t = collectionArg.OrderBy(HLinkPersonModel => HLinkPersonModel.DeRef.GBirthName.SortName);

            HLinkPersonModelCollection tt = new HLinkPersonModelCollection();

            foreach (HLinkPersonModel item in t)
            {
                tt.Add(item);
            }

            return tt;
        }

        // var query = from item in PersonData.Items orderby ((PersonModel)item).GBirthName.SortName
        // group item by ((PersonModel)item).GBirthName.SortName into g select new { GroupName =
        // g.Key, Items = g, }; foreach (var g in query) { CommonGroupInfoCollection info = new
        // CommonGroupInfoCollection { Key = g.GroupName, }; foreach (var item in g.Items) {
        // info.Add(item); }
        /// <summary>
        /// Searches the items.
        /// </summary>
        /// <param name="queryString">
        /// The query string.
        /// </param>
        /// <returns>
        /// List of Serch HLinks.
        /// </returns>
        public override List<SearchItem> Search(string queryString)
        {
            List<SearchItem> itemsFound = new List<SearchItem>();
            queryString = queryString.ToLower(CultureInfo.CurrentCulture);

            // Search by Full Name
            var temp = PersonData.Items.Where(x => x.GBirthName.FullName.ToLower(CultureInfo.CurrentCulture).Contains(queryString));

            foreach (PersonModel tempMO in temp)
            {
                itemsFound.Add(new SearchItem
                {
                    HLink = tempMO.HLink,
                    Text = tempMO.GetDefaultText,
                });
            }

            // Search by Called By
            temp = PersonData.Items.Where(x => x.GBirthName.GCall.ToLower(CultureInfo.CurrentCulture).Contains(queryString));

            foreach (PersonModel tempMO in temp)
            {
                itemsFound.Add(new SearchItem
                {
                    HLink = tempMO.HLink,
                    Text = tempMO.GetDefaultText,
                });
            }

            // Search by Nick Name
            temp = PersonData.Items.Where(x => x.GBirthName.GNick.ToLower(CultureInfo.CurrentCulture).Contains(queryString));

            foreach (PersonModel tempMO in temp)
            {
                itemsFound.Add(new SearchItem
                {
                    HLink = tempMO.HLink,
                    Text = tempMO.GetDefaultText,
                });
            }

            return itemsFound;
        }

        //public override IReadOnlyList<PersonModel> VirtualReader(int startItem, int itemCount)
        //{
        //    return DataDefaultSort.Skip(startItem).Take(itemCount).ToList();
        //}

        ///// <summary>
        ///// Gets the groups by category.
        ///// </summary>
        ///// <returns>
        ///// </returns>
        //public CommonGroupInfoCollection GetGroupsByCategory()
        //{
        //    CommonGroupInfoCollection groups = new CommonGroupInfoCollection();
    }
}