//-----------------------------------------------------------------------
//
// Various data modesl to small to be worth putting in their own file
// is first launched.
//
// <copyright file="NameMapDataView.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// </summary>
namespace GrampsView.Data.DataView
{
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
    // Event repository </summary>
    public class NameMapDataView : DataViewBase<NameMapModel, HLinkNameMapModel, HLinkNameMapModelCollection>, INameMapDataView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NameMapDataView" /> class.
        /// </summary>
        public NameMapDataView()
        {
        }

        public override IReadOnlyList<NameMapModel> DataDefaultSort
        {
            get
            {
                return DataViewData.Items.OrderBy(NameMapModel => NameMapModel.Id).ToList();
            }
        }

        /// <summary>
        /// Gets the local media data.
        /// </summary>
        /// <summary>
        /// Gets or sets the data view data.
        /// </summary>
        /// <value>
        /// The data view data.
        /// </value>
        public override RepositoryModelType<NameMapModel, HLinkNameMapModel> DataViewData
        {
            get
            {
                return NameMapData;
            }
        }

        /// <summary>
        /// Gets or sets the citation data.
        /// </summary>
        /// <value>
        /// The citation data.
        /// </value>
        [DataMember]
        public RepositoryModelType<NameMapModel, HLinkNameMapModel> NameMapData
        {
            get
            {
                return DataStore.DS.localNameMapData;
            }

            set
            {
                SetProperty(ref DataStore.DS.localNameMapData, value);
            }
        }

        /// <summary>
        /// Gets all as hlink.
        /// </summary>
        /// <returns>
        /// </returns>
        public HLinkNameMapModelCollection getAllAsHlink()
        {
            HLinkNameMapModelCollection t = new HLinkNameMapModelCollection();

            foreach (var item in DataViewData)
            {
                t.Add(item.GetHLink);
            }

            return t;
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
        public override HLinkNameMapModelCollection HLinkCollectionSort(HLinkNameMapModelCollection collectionArg)
        {
            if (collectionArg == null)
            {
                return null;
            }

            IOrderedEnumerable<HLinkNameMapModel> t = collectionArg.OrderBy(HLinkNameMapModel => HLinkNameMapModel.DeRef.HLinkKey);

            HLinkNameMapModelCollection tt = new HLinkNameMapModelCollection();

            foreach (HLinkNameMapModel item in t)
            {
                tt.Add(item);
            }

            return tt;
        }

        public override List<SearchItem> Search(string queryString)
        {
            List<SearchItem> itemsFound = new List<SearchItem>();

            var temp = NameMapData.Items.Where(x => x.GetDefaultText.ToLower(CultureInfo.CurrentCulture).Contains(queryString));

            foreach (NameMapModel tempMO in temp)
            {
                itemsFound.Add(new SearchItem
                {
                    HLink = tempMO.GetHLink,
                    Text = tempMO.GetDefaultText,
                });
            }

            return itemsFound;
        }
    }
}