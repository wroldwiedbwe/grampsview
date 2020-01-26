﻿//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="HeaderDataView.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.DataView
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;

    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repositories;
    using GrampsView.Data.Repository;

    /// <summary>
    /// repository for the XML file header.
    /// </summary>
    public class HeaderDataView : DataViewBase<HeaderModel, HLinkHeaderModel, HLinkHeaderModelCollection>, IHeaderDataView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderDataView" /> class.
        /// </summary>
        public HeaderDataView()
        {
        }

        public override IReadOnlyList<HeaderModel> DataDefaultSort
        {
            get
            {
                return DataViewData.Items.OrderBy(HeaderModel => HeaderModel.GCreatedDate).ToList();
            }
        }

        /// <summary>
        /// Gets or sets the header data.
        /// </summary>
        /// <value>
        /// The header data.
        /// </value>
        [DataMember]
        public RepositoryModelType<HeaderModel, HLinkHeaderModel> HeaderData
        {
            get
            {
                return DataStore.DS.localHeaderData;
            }

            set
            {
                SetProperty(ref DataStore.DS.localHeaderData, value);
            }
        }

        /// <summary>
        /// Gets the header data.
        /// </summary>
        /// <value>
        /// The header data.
        /// </value>
        public HeaderModel HeaderDataModel
        {
            get
            {
                if (HeaderData.Count > 0)
                {
                    return HeaderData.Items[0];
                }
                else
                {
                    return new HeaderModel();
                }
            }
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
        public override HLinkHeaderModelCollection HLinkCollectionSort(HLinkHeaderModelCollection collectionArg)
        {
            if (collectionArg == null)
            {
                return null;
            }

            IOrderedEnumerable<HLinkHeaderModel> t = collectionArg.OrderBy(HLinkHeaderModel => HLinkHeaderModel.DeRef.GMediaPath);

            HLinkHeaderModelCollection tt = new HLinkHeaderModelCollection();

            foreach (HLinkHeaderModel item in t)
            {
                tt.Add(item);
            }

            return tt;
        }

        public override List<SearchItem> Search(string queryString)
        {
            List<SearchItem> itemsFound = new List<SearchItem>();

            return itemsFound;
        }
    }
}