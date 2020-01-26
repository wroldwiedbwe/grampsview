﻿//-----------------------------------------------------------------------
//
// Interface defintion for MediaObjectDetailViewModel.cs
//
// <copyright file="MediaDataView.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.DataView
{
    using System;
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
    /// Datamodel for media object files.
    /// </summary>
    public class MediaDataView : DataViewBase<MediaModel, HLinkMediaModel, HLinkMediaModelCollection>, IMediaDataView
    {
        /// <summary>
        /// The local media data.
        /// </summary>

        /// <summary>
        /// The local common logging
        /// </summary>
        readonly private ICommonLogging localCL = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaDataView" /> class.
        /// </summary>
        public MediaDataView()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaDataView" /> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The ioc common logging.
        /// </param>
        /// <param name="iocCommonProgress">
        /// The ioc common progress.
        /// </param>
        public MediaDataView(ICommonLogging iocCommonLogging)
        {
            localCL = iocCommonLogging;
        }

        /// <summary>
        /// Gets the data default sort.
        /// </summary>
        /// <value>
        /// The data default sort.
        /// </value>
        public override IReadOnlyList<MediaModel> DataDefaultSort
        {
            get
            {
                return DataViewData.Items.OrderBy(MediaModel => MediaModel.GDescription).ToList();
            }
        }

        /// <summary>
        /// Gets the data view data.
        /// </summary>
        /// <value>
        /// The data view data.
        /// </value>
        public override RepositoryModelType<MediaModel, HLinkMediaModel> DataViewData
        {
            get
            {
                return MediaData;
            }
        }

        /// <summary>
        /// Gets the get groups by letter.
        /// </summary>
        /// <value>
        /// The get groups by letter.
        /// </value>
        public override List<CommonGroupInfoCollection<MediaModel>> GetGroupsByLetter
        {
            get
            {
                List<CommonGroupInfoCollection<MediaModel>> groups = new List<CommonGroupInfoCollection<MediaModel>>();

                var query = from item in MediaData.Items
                            orderby item.GDescription
                            group item by (item.GDescription + " ").ToUpper(CultureInfo.CurrentCulture).Substring(0, 1) into g
                            select new { GroupName = g.Key, Items = g };

                foreach (var g in query)
                {
                    CommonGroupInfoCollection<MediaModel> info = new CommonGroupInfoCollection<MediaModel>
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
        /// Gets or sets the media data.
        /// </summary>
        /// <value>
        /// The media data.
        /// </value>
        [DataMember]
        public RepositoryModelType<MediaModel, HLinkMediaModel> MediaData
        {
            get
            {
                return DataStore.DS.localMediaData;
            }

            set
            {
                SetProperty(ref DataStore.DS.localMediaData, value);
            }
        }

        /// <summary>
        /// Gets all as hlink.
        ///
        /// Skip first few which are HLink Defaults.
        /// </summary>
        /// <returns>
        /// </returns>
        public HLinkMediaModelCollection GetAllAsHLink()
        {
            HLinkMediaModelCollection t = new HLinkMediaModelCollection();

            for (int i = 0; i < DataDefaultSort.Count; i++)
            {
                t.Add(MediaData.Get(i).GetHLink);
            }

            t = HLinkCollectionSort(t);

            return t;
        }

        /// <summary>
        /// Gets the first icon from collection.
        /// </summary>
        /// <param name="theCollection">
        /// The collection.
        /// </param>
        /// <returns>
        /// HLink Media ViewModel.
        /// </returns>
        public HLinkMediaModel GetFirstIconFromCollection(HLinkMediaModelCollection theCollection)
        {
            // handle null argument
            if (theCollection == null)
            {
                theCollection = GetAllAsHLink();
            }

            IEnumerable<HLinkMediaModel> t = theCollection.Where(HLinkMediaModel => HLinkMediaModel.DeRef.IsMediaFile == true);

            return t.FirstOrDefault();
        }

        /// <summary>
        /// Gets the first image from collection or null if none found.
        /// </summary>
        /// <param name="argCollection">
        /// The collection. If null then the whole Media Repository is used.
        /// </param>
        /// <param name="DefaultHLink">
        /// The default h link.
        /// </param>
        /// <returns>
        /// </returns>
        public new HLinkMediaModel GetFirstImageFromCollection(HLinkMediaModelCollection argCollection)
        {
            // handle null argument
            if (argCollection == null)
            {
                argCollection = GetAllAsHLink();
            }

            HLinkMediaModel returnMediaModel = null;
            MediaModel tempMediaModel;

            if (argCollection.Count > 0)
            {
                // step through each mediamodel hlink in the collection
                for (int i = 0; i < argCollection.Count; i++)
                {
                    tempMediaModel = MediaData.GetModelFromHLink(argCollection[i]);

                    if (tempMediaModel.IsMediaFile)
                    {
                        returnMediaModel = argCollection[i];
                        break;
                    }
                }
            }

            // return the image
            return returnMediaModel;
        }

        /// <summary>
        /// Gets the random from collection.
        /// </summary>
        /// <param name="argCollection">
        /// The collection.
        /// </param>
        /// <param name="DefaultHLink">
        /// The default h link.
        /// </param>
        /// <returns>
        /// </returns>
        public HLinkMediaModel GetRandomFromCollection(HLinkMediaModelCollection argCollection)
        {
            // handle null argument
            if (argCollection == null)
            {
                argCollection = GetAllAsHLink();
            }

            HLinkMediaModel tt = new HLinkMediaModel
            {
                // HLinkKey = DefaultHLink
            };

            Random randomValue = new Random();

            if (argCollection.Count > 0)
            {
                // get a random value
                int q = randomValue.Next(0, argCollection.Count);

                // get the next image starting at the random value
                for (int i = q; i < argCollection.Count; i++)
                {
                    HLinkMediaModel tempHLinkMediaModel = argCollection[i] as HLinkMediaModel;
                    if (MediaData.GetModelFromHLink(tempHLinkMediaModel).IsMediaFile)
                    {
                        tt = tempHLinkMediaModel;
                        break;
                    }
                }
            }

            // return the image hlink
            return tt;
        }

        //    return returnModel;
        //}
        /// <summary>
        /// hes the link collection sort.
        /// </summary>
        /// <param name="argCollection">
        /// The collection argument.
        /// </param>
        /// <returns>
        /// </returns>
        public override HLinkMediaModelCollection HLinkCollectionSort(HLinkMediaModelCollection argCollection)
        {
            if (argCollection == null)
            {
                return null;
            }

            IOrderedEnumerable<HLinkMediaModel> t = argCollection.OrderBy(hLinkMediaModel => hLinkMediaModel.DeRef.GDescription);

            HLinkMediaModelCollection tt = new HLinkMediaModelCollection();

            foreach (HLinkMediaModel item in t)
            {
                tt.Add(item);
            }

            return tt;
        }

        /// <summary>
        /// News this instance.
        /// </summary>
        /// <returns>
        /// </returns>
        public override MediaModel NewModel()
        {
            MediaModel t = base.NewModel();

            t.ModelCommonLogging = localCL;

            return t;
        }

        /// <summary>
        /// Searches the media items.
        /// </summary>
        /// <param name="argQueryString">
        /// The query string.
        /// </param>
        /// <returns>
        /// </returns>
        public override List<SearchItem> Search(string argQueryString)
        {
            List<SearchItem> itemsFound = new List<SearchItem>();

            var temp = MediaData.Items.Where(x => x.GDescription.ToLower(CultureInfo.CurrentCulture).Contains(argQueryString));

            foreach (MediaModel tempMO in temp)
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