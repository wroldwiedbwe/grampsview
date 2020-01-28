//-----------------------------------------------------------------------
//
// Various data modesl to small to be worth putting in their own file
// is first launched.
//
// <copyright file="PlaceDataView.cs" company="PlaceholderCompany">
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
    /// View of Place data.
    /// </summary>
    /// <seealso cref="GrampsView.Data.DataView.DataViewBase{GrampsView.Data.ViewModel.PlaceModel, GrampsView.Data.ViewModel.HLinkPlaceModel, GrampsView.Data.Collections.HLinkPlaceModelCollection}" />
    /// /// /// /// /// ///
    /// <seealso cref="GrampsView.Data.DataView.IPlaceDataView" />
    public class PlaceDataView : DataViewBase<PlaceModel, HLinkPlaceModel, HLinkPlaceModelCollection>, IPlaceDataView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaceDataView" /> class.
        /// </summary>
        public PlaceDataView()
        {
        }

        public override IReadOnlyList<PlaceModel> DataDefaultSort
        {
            get
            {
                return DataViewData.Items.OrderBy(PlaceModel => PlaceModel.GName).ToList();
            }
        }

        /// <summary>
        /// Gets the local place data.
        /// </summary>
        /// <summary>
        /// Gets or sets the data view data.
        /// </summary>
        /// <value>
        /// The data view data.
        /// </value>
        public override RepositoryModelType<PlaceModel, HLinkPlaceModel> DataViewData
        {
            get
            {
                return PlaceData;
            }
        }

        /// <summary>
        /// Gets or sets the person data.
        /// </summary>
        /// <value>
        /// The person data.
        /// </value>
        [DataMember]
        public RepositoryModelType<PlaceModel, HLinkPlaceModel> PlaceData
        {
            get
            {
                return DataStore.DS.localPlaceData;
            }

            set
            {
                SetProperty(ref DataStore.DS.localPlaceData, value);
            }
        }

        /// <summary>
        /// Gets all as hlink.
        /// </summary>
        /// <returns>
        /// </returns>
        public HLinkPlaceModelCollection GetAllAsHLink()
        {
            HLinkPlaceModelCollection t = new HLinkPlaceModelCollection();

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

            foreach (PlaceModel item in tt)
            {
                returnCardGroup.Cards.Add(item.HLink);
            }

            returnCardGroup.Title = "Latest Place Changes";

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
        public override HLinkPlaceModelCollection HLinkCollectionSort(HLinkPlaceModelCollection collectionArg)
        {
            if (collectionArg == null)
            {
                return null;
            }

            IOrderedEnumerable<HLinkPlaceModel> t = collectionArg.OrderBy(HLinkPlaceModel => HLinkPlaceModel.DeRef.GPTitle);

            HLinkPlaceModelCollection tt = new HLinkPlaceModelCollection();

            foreach (HLinkPlaceModel item in t)
            {
                tt.Add(item);
            }

            return tt;
        }

        public override List<SearchItem> Search(string queryString)
        {
            List<SearchItem> itemsFound = new List<SearchItem>();

            var temp = PlaceData.Items.Where(x => x.GetDefaultText.ToLower(CultureInfo.CurrentCulture).Contains(queryString));

            foreach (PlaceModel tempMO in temp)
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