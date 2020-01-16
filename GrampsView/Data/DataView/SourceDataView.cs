//-----------------------------------------------------------------------
//
// Various data modesl to small to be worth putting in their own file
// is first launched.
//
// <copyright file="SourceDataView.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

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
    public class SourceDataView : DataViewBase<SourceModel, HLinkSourceModel, HLinkSourceModelCollection>, ISourceDataView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SourceDataView" /> class.
        /// </summary>
        public SourceDataView()
        {
        }

        public override IReadOnlyList<SourceModel> DataDefaultSort
        {
            get
            {
                return DataViewData.Items.OrderBy(SourceModel => SourceModel.GSTitle).ToList();
            }
        }

        /// <summary>
        /// Gets the local source data.
        /// </summary>
        /// <summary>
        /// Gets or sets the data view data.
        /// </summary>
        /// <value>
        /// The data view data.
        /// </value>
        public override RepositoryModelType<SourceModel, HLinkSourceModel> DataViewData
        {
            get
            {
                return SourceData;
            }
        }

        /// <summary>
        /// Gets the person data.
        /// </summary>
        /// <value>
        /// The person data.
        /// </value>
        [DataMember]
        public RepositoryModelType<SourceModel, HLinkSourceModel> SourceData
        {
            get
            {
                return DataStore.DS.SourceData;
            }

            // set { this.SetProperty(ref DataStore.DS.SourceData, value); }
        }

        /// <summary>
        /// Gets all as hlink.
        /// </summary>
        /// <returns>
        /// </returns>
        public HLinkSourceModelCollection GetAllAsHLink()
        {
            HLinkSourceModelCollection t = new HLinkSourceModelCollection();

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
        public override HLinkSourceModelCollection HLinkCollectionSort(HLinkSourceModelCollection collectionArg)
        {
            if (collectionArg == null)
            {
                return null;
            }

            IOrderedEnumerable<HLinkSourceModel> t = collectionArg.OrderBy(HLinkSourceModel => HLinkSourceModel.DeRef.GSTitle);

            HLinkSourceModelCollection tt = new HLinkSourceModelCollection();

            foreach (HLinkSourceModel item in t)
            {
                tt.Add(item);
            }

            return tt;
        }

        public override List<SearchItem> Search(string queryString)
        {
            List<SearchItem> itemsFound = new List<SearchItem>();

            var temp = SourceData.Items.Where(x => x.GetDefaultText.ToLower(CultureInfo.CurrentCulture).Contains(queryString));

            foreach (SourceModel tempMO in temp)
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