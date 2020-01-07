// <copyright file="TagDataView.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

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
    /// Tag Data View.
    /// </summary>
    /// <seealso cref="GrampsView.Data.DataView.DataViewBase{GrampsView.Data.ViewModel.TagModel, GrampsView.Data.ViewModel.HLinkTagModel, GrampsView.Data.Collections.HLinkTagModelCollection}" />
    /// /// /// /// ///
    /// <seealso cref="GrampsView.Data.DataView.ITagDataView" />
    public class TagDataView : DataViewBase<TagModel, HLinkTagModel, HLinkTagModelCollection>, ITagDataView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagDataView" /> class.
        /// </summary>
        public TagDataView()
        {
        }

        /// <summary>
        /// Gets the data default sort.
        /// </summary>
        /// <value>
        /// The data default sort.
        /// </value>
        public override IReadOnlyList<TagModel> DataDefaultSort
        {
            get
            {
                return DataViewData.Items.OrderBy(TagModel => TagModel.GName).ToList();
            }
        }

        /// <summary>
        /// Gets the local tag data.
        /// </summary>
        /// <summary>
        /// Gets or sets the data view data.
        /// </summary>
        /// <value>
        /// The data view data.
        /// </value>
        public override RepositoryModelType<TagModel, HLinkTagModel> DataViewData
        {
            get
            {
                return TagData;
            }
        }

        /// <summary>
        /// Gets or sets the person data.
        /// </summary>
        /// <value>
        /// The person data.
        /// </value>
        [DataMember]
        public RepositoryModelType<TagModel, HLinkTagModel> TagData
        {
            get
            {
                return DataStore.DS.localTagData;
            }

            set
            {
                SetProperty(ref DataStore.DS.localTagData, value);
            }
        }

        /// <summary>
        /// Gets all as hlink.
        /// </summary>
        /// <returns>
        /// Tag HLink Collection.
        /// </returns>
        public HLinkTagModelCollection getAllAsHlink()
        {
            HLinkTagModelCollection t = new HLinkTagModelCollection();

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
        /// Sorted HLinks.
        /// </returns>
        public override HLinkTagModelCollection HLinkCollectionSort(HLinkTagModelCollection collectionArg)
        {
            if (collectionArg == null)
            {
                return null;
            }

            IOrderedEnumerable<HLinkTagModel> t = collectionArg.OrderBy(HLinkTagModel => HLinkTagModel.DeRef.Handle);

            HLinkTagModelCollection tt = new HLinkTagModelCollection();

            foreach (HLinkTagModel item in t)
            {
                tt.Add(item);
            }

            return tt;
        }

        public override List<SearchItem> Search(string queryString)
        {
            List<SearchItem> itemsFound = new List<SearchItem>();

            var temp = TagData.Items.Where(x => x.GetDefaultText.ToLower(CultureInfo.CurrentCulture).Contains(queryString));

            foreach (TagModel tempMO in temp)
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