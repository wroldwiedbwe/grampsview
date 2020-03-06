﻿// <copyright file="HLinkMediaModelCollection.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

/// <summary>
/// </summary>
namespace GrampsView.Data.Collections
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// </summary>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<HLinkMediaModel>))]
    public class HLinkMediaModelCollection : HLinkBaseCollection<HLinkMediaModel>
    {
        public CardGroup GetCardGroup()
        {
            return base.GetCardGroup("Media Collection");
        }

        /// <summary>
        /// Helper method to sort and set the firt image link.
        /// </summary>
        /// <param name="collectionArg">
        /// The collection argument.
        /// </param>
        public void SortAndSetFirst()
        {
            // Set the first image link. Assumes main image is manually set to the first image in
            // Gramps if we need it to be, e.g. Citations.
            MediaModel tempMediaModel = new MediaModel();

            if (Count > 0)
            {
                // Step through each mediamodel hlink in the collection
                for (int i = 0; i < Count; i++)
                {
                    tempMediaModel = DV.MediaDV.MediaData.GetModelFromHLink(this[i]);

                    if (tempMediaModel.IsMediaFile)
                    {
                        FirstHLinkHomeImage.ConvertHLinkMediaModel(this[i]);

                        break;
                    }
                }

                // Sort the collection
                List<HLinkMediaModel> t = this.OrderBy(hLinkMediaModel => hLinkMediaModel.DeRef.GDescription).ToList();

                Items.Clear();

                foreach (HLinkMediaModel item in t)
                {
                    Items.Add(item);
                }
            }
        }
    }
}