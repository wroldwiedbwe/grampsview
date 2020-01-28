// <copyright file="BookMarkDataView.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

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
    /// Event repository.
    /// </summary>
    /// <seealso cref="GrampsView.Data.DataView.DataViewBase{GrampsView.Data.ViewModel.BookMarkModel, GrampsView.Data.ViewModel.HLinkBookMarkModel, GrampsView.Data.Collections.HLinkBookMarkModelCollection}" />
    /// /// /// /// /// /// /// /// /// /// ///
    /// <seealso cref="GrampsView.Data.DataView.IBookMarkDataView" />
    public class BookMarkDataView : DataViewBase<BookMarkModel, HLinkBookMarkModel, HLinkBookMarkModelCollection>, IBookMarkDataView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookMarkDataView" /> class.
        /// </summary>
        /// iocDat
        public BookMarkDataView()
        {
        }

        /// <summary>
        /// Gets the citation data.
        /// </summary>
        /// <value>
        /// The citation data.
        /// </value>
        [DataMember]
        public RepositoryModelType<BookMarkModel, HLinkBookMarkModel> BookMarkData
        {
            get
            {
                return DataStore.DS.BookMarkData;
            }

            // set { SetProperty(ref DataStore.DS.BookMarkData, value); }
        }

        /// <summary>
        /// Gets the data default sort.
        /// </summary>
        /// <value>
        /// The data default sort.
        /// </value>
        public override IReadOnlyList<BookMarkModel> DataDefaultSort
        {
            get
            {
                return DataViewData.Items.OrderBy(BookMarkModel => BookMarkModel.GTarget).ToList();
            }
        }

        /// <summary>
        /// Gets the data view data.
        /// </summary>
        /// <value>
        /// The data view data.
        /// </value>
        public override RepositoryModelType<BookMarkModel, HLinkBookMarkModel> DataViewData
        {
            get
            {
                return BookMarkData;
            }
        }

        /// <summary>
        /// Gets all as hlink.
        /// </summary>
        /// <returns>
        /// </returns>
        public HLinkBookMarkModelCollection GetAllAsHLink
        {
            get
            {
                HLinkBookMarkModelCollection t = new HLinkBookMarkModelCollection();

                foreach (var item in DataDefaultSort)
                {
                    t.Add(item.HLink);
                }

                return t;
            }
        }

        /// <summary>
        /// Gets all as hlink base so that they can be used in a modelgridview. Takes advantage of
        /// the ability to select a XAML summary item based on the HLink type. Code cheats and
        /// pretends it is a BackLink HLink collection.
        /// </summary>
        /// <returns>
        /// </returns>
        public HLinkBaseCollection<HLinkBase> GetAllAsHLinkBase()
        {
            HLinkBaseCollection<HLinkBase> t = new HLinkBaseCollection<HLinkBase>();

            foreach (var item in DataDefaultSort)
            {
                t.Add(item.GetBookMarkHLink);
            }

            return t;
        }

        public override CardGroup GetLatestChanges()
        {
            IEnumerable tt = DataViewData.Items.OrderByDescending(t => t.Change).Take(3);

            CardGroup returnCardGroup = new CardGroup();

            foreach (BookMarkModel item in tt)
            {
                returnCardGroup.Cards.Add(item.HLink);
            }

            returnCardGroup.Title = "Latest Bookmark Changes";

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
        public override HLinkBookMarkModelCollection HLinkCollectionSort(HLinkBookMarkModelCollection collectionArg)
        {
            if (collectionArg == null)
            {
                return null;
            }

            IOrderedEnumerable<HLinkBookMarkModel> t = collectionArg.OrderBy(HLinkBookMarkModel => HLinkBookMarkModel.DeRef.GTarget);

            HLinkBookMarkModelCollection tt = new HLinkBookMarkModelCollection();

            foreach (HLinkBookMarkModel item in t)
            {
                tt.Add(item);
            }

            return tt;
        }

        public override List<SearchItem> Search(string queryString)
        {
            List<SearchItem> itemsFound = new List<SearchItem>();

            var temp = BookMarkData.Items.Where(x => x.GetDefaultText.ToLower(CultureInfo.CurrentCulture).Contains(queryString));

            foreach (BookMarkModel tempMO in temp)
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