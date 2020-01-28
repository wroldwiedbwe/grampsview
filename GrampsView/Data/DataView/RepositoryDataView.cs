//-----------------------------------------------------------------------
//
// Various data modesl to small to be worth putting in their own file
// is first launched.
//
// <copyright file="RepositoryDataView.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.DataView
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.Serialization;

    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repositories;
    using GrampsView.Data.Repository;

    /// <summary>
    /// </summary>
    /// <seealso cref="GrampsView.Data.DataView.DataViewBase{GrampsView.Data.ViewModel.RepositoryModel, GrampsView.Data.ViewModel.HLinkRepositoryModel, GrampsView.Data.Collections.HLinkRepositoryModelCollection}" />
    /// /// /// /// /// /// ///
    /// <seealso cref="GrampsView.Data.DataView.IRepositoryDataView" />
    public class RepositoryDataView : DataViewBase<RepositoryModel, HLinkRepositoryModel, HLinkRepositoryModelCollection>, IRepositoryDataView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryDataView" /> class.
        /// </summary>
        public RepositoryDataView()
        {
        }

        public override IReadOnlyList<RepositoryModel> DataDefaultSort
        {
            get
            {
                return DataViewData.Items.OrderBy(RepositoryModel => RepositoryModel.GRName).ToList();
            }
        }

        /// <summary>
        /// Gets the local repository data.
        /// </summary>
        /// <summary>
        /// Gets or sets the data view data.
        /// </summary>
        /// <value>
        /// The data view data.
        /// </value>
        public override RepositoryModelType<RepositoryModel, HLinkRepositoryModel> DataViewData
        {
            get
            {
                return RepositoryData;
            }
        }

        /// <summary>
        /// Gets or sets the person data.
        /// </summary>
        /// <value>
        /// The person data.
        /// </value>
        [DataMember]
        public RepositoryModelType<RepositoryModel, HLinkRepositoryModel> RepositoryData
        {
            get
            {
                return DataStore.DS.localRepositoryData;
            }

            set
            {
                SetProperty(ref DataStore.DS.localRepositoryData, value);
            }
        }

        /// <summary>
        /// Gets all as hlink.
        /// </summary>
        /// <returns>
        /// </returns>
        public HLinkRepositoryModelCollection GetAllAsHLink()
        {
            HLinkRepositoryModelCollection t = new HLinkRepositoryModelCollection();

            foreach (var item in DataDefaultSort)
            {
                t.Add(item.HLink);
            }

            return t;
        }

        public override CardGroup GetLatestChanges()
        {
            IEnumerable tt = DataViewData.Items.OrderByDescending(t => t.Change).Take(3);

            CardGroup returnCardGroup = new CardGroup();

            foreach (RepositoryModel item in tt)
            {
                returnCardGroup.Cards.Add(item.HLink);
            }

            returnCardGroup.Title = "Latest Repository Changes";

            return returnCardGroup;
        }

        /// <summary>
        /// hes the link collection sort.
        /// </summary>
        /// <param name="collectionArg">
        /// The collection argument.
        /// </param>
        /// <returns>
        /// Sorted hlink collection.
        /// </returns>
        public override HLinkRepositoryModelCollection HLinkCollectionSort(HLinkRepositoryModelCollection collectionArg)
        {
            if (collectionArg == null)
            {
                return null;
            }

            IOrderedEnumerable<HLinkRepositoryModel> t = collectionArg.OrderBy(HLinkRepositoryModel => HLinkRepositoryModel.DeRef.GRName);

            HLinkRepositoryModelCollection tt = new HLinkRepositoryModelCollection();

            foreach (HLinkRepositoryModel item in t)
            {
                tt.Add(item);
            }

            return tt;
        }

        public override List<SearchItem> Search(string queryString)
        {
            List<SearchItem> itemsFound = new List<SearchItem>();

            var temp = RepositoryData.Items.Where(x => x.GetDefaultText.ToLower(CultureInfo.CurrentCulture).Contains(queryString));

            foreach (RepositoryModel tempMO in temp)
            {
                itemsFound.Add(new SearchItem
                {
                    HLink = tempMO.HLink,
                    Text = tempMO.GetDefaultText,
                });
            }

            return itemsFound;
        }
    }
}