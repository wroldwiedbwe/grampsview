//-----------------------------------------------------------------------
//
// Various data modesl to small to be worth putting in their own file
// is first launched.
//
// <copyright file="CitationDataView.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.DataView
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repositories;
    using GrampsView.Data.Repository;

    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    // Event repository </summary>
    public class CitationDataView : DataViewBase<CitationModel, HLinkCitationModel, HLinkCitationModelCollection>, ICitationDataView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CitationDataView" /> class.
        /// </summary>
        public CitationDataView()
        {
        }

        /// <summary>
        /// Gets or sets the citation data.
        /// </summary>
        /// <value>
        /// The citation data.
        /// </value>
        [DataMember]
        public RepositoryModelType<CitationModel, HLinkCitationModel> CitationData
        {
            get
            {
                return DataStore.DS.LocalCitationData;
            }

            set
            {
                SetProperty(ref DataStore.DS.LocalCitationData, value);
            }
        }

        /// <summary>
        /// Gets the data default sort.
        /// </summary>
        /// <value>
        /// The data default sort.
        /// </value>
        public override IReadOnlyList<CitationModel> DataDefaultSort
        {
            get
            {
                return DataViewData.Items.OrderBy(citationModel => citationModel.GSourceRef.DeRef.GSTitle).ToList();
            }
        }

        /// <summary>
        /// Gets the data view data.
        /// </summary>
        /// <value>
        /// The data view data.
        /// </value>
        public override RepositoryModelType<CitationModel, HLinkCitationModel> DataViewData
        {
            get
            {
                return CitationData;
            }
        }

        public override List<CommonGroupInfoCollection<CitationModel>> GetGroupsByLetter
        {
            get
            {
                List<CommonGroupInfoCollection<CitationModel>> groups = new List<CommonGroupInfoCollection<CitationModel>>();

                var query = from item in CitationData.Items
                            orderby item.GDateContent.SortDate
                            group item by item.GDateContent.GetDecade into g
                            select new { GroupName = g.Key, Items = g };

                foreach (var g in query)
                {
                    CommonGroupInfoCollection<CitationModel> info = new CommonGroupInfoCollection<CitationModel>();

                    // Handle 0's
                    if (g.GroupName == 0)
                    {
                        info.Key = "Unknown Date";
                    }
                    else
                    {
                        info.Key = g.GroupName + "'s";
                    }

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
        /// Gets all as hlink.
        /// </summary>
        /// <returns>
        /// </returns>
        public HLinkCitationModelCollection GetAllAsHLink()
        {
            HLinkCitationModelCollection t = new HLinkCitationModelCollection();

            foreach (var item in DataDefaultSort)
            {
                t.Add(item.GetHLink);
            }

            return t;
        }

        /// <summary>
        /// Gets the first image from collection.
        /// </summary>
        /// <param name="theCollection">
        /// The collection.
        /// </param>
        /// <returns>
        /// </returns>
        public new HLinkMediaModel GetFirstImageFromCollection(HLinkCitationModelCollection theCollection)
        {
            // handle null argument
            if (theCollection == null)
            {
                return null;
            }

            HLinkMediaModel returnMediaModel = null;

            if (theCollection.Count > 0)
            {
                // step through each mediamodel hlink in the collection Accept either a direct
                // mediamodel reference or a hlink to a Source media reference bool mediaFoundFlag = false;

                // do { } while (!mediaFoundFlag);
                for (int i = 0; i < theCollection.Count; i++)
                {
                    HLinkCitationModel currentHLink = theCollection[i];

                    returnMediaModel = currentHLink.DeRef.GMediaRefCollection.FirstHLink;

                    //// Handle direct media reference
                    // if (currentHLink.DeRef.GMediaRefCollection.Count > 0) { foreach
                    // (HLinkMediaModel item in currentHLink.DeRef.GMediaRefCollection) {
                    // tempMediaModel = DV.MediaDV.GetHLink(item.HLinkKey);

                    // if (tempMediaModel.IsMediaFile) { returnMediaModel = item; break; } } }

                    // Handle Source Links
                    if (currentHLink.DeRef.HomeImageHLink.HomeUseImage)
                    {
                        returnMediaModel = currentHLink.DeRef.HomeImageHLink;
                    }

                    if (returnMediaModel != null)
                    {
                        break;
                    }
                }
            }

            // return the image
            return returnMediaModel;
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
        public override HLinkCitationModelCollection HLinkCollectionSort(HLinkCitationModelCollection collectionArg)
        {
            if (collectionArg == null)
            {
                return null;
            }

            IOrderedEnumerable<HLinkCitationModel> t = collectionArg.OrderBy(HLinkCitationModel => HLinkCitationModel.DeRef.GDateContent);

            HLinkCitationModelCollection tt = new HLinkCitationModelCollection();

            foreach (HLinkCitationModel item in t)
            {
                tt.Add(item);
            }

            return tt;
        }

        public override List<SearchItem> Search(string queryString)
        {
            List<SearchItem> itemsFound = new List<SearchItem>();

            var temp = CitationData.Items.Where(x => x.GDateContent.GetShortDateAsString.ToLower(CultureInfo.CurrentCulture).Contains(queryString));

            foreach (CitationModel tempMO in temp)
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