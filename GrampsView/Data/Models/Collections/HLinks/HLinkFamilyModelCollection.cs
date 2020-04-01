﻿// <copyright file="HLinkFamilyModelCollection.cs" company="PlaceholderCompany">
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
    /// Colelction of Family hLinks.
    /// </summary>
    /// <seealso cref="GrampsView.Data.ViewModel.HLinkBaseCollection{GrampsView.Data.ViewModel.HLinkFamilyModel}"/>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<HLinkFamilyModel>))]
    public class HLinkFamilyModelCollection : HLinkBaseCollection<HLinkFamilyModel>
    {
        /// <summary>
        /// Gets the dereferenced Family Models.
        /// </summary>
        /// <value>
        /// The de reference.
        /// </value>
        public ObservableCollection<FamilyModel> DeRef
        {
            get
            {
                ObservableCollection<FamilyModel> t = new ObservableCollection<FamilyModel>();

                foreach (HLinkFamilyModel item in Items)
                {
                    t.Add(item.DeRef);
                }

                return t;
            }
        }

        public CardGroup GetCardGroup()
        {
            return base.GetCardGroup("Family Collection");
        }

        /// <summary>
        /// Helper method to sort and set the firt image link.
        /// </summary>
        public void SortAndSetFirst()
        {
            // Set the first image link. Assumes main image is manually set to the first image in
            // Gramps if we need it to be, e.g. Citations.
            FamilyModel tempModel = new FamilyModel();

            if (Count > 0)
            {
                // Step through each citationmodel hlink in the collection
                for (int i = 0; i < Count; i++)
                {
                    tempModel = DV.FamilyDV.FamilyData.GetModelFromHLink(this[i]);

                    if (tempModel.HomeImageHLink.LinkToImage)
                    {
                        FirstHLinkHomeImage = tempModel.HomeImageHLink;
                        break;
                    }
                }

                // Sort the collection
                List<HLinkFamilyModel> t = this.OrderBy(HLinkCitationModel => HLinkCitationModel.DeRef.FamilyDisplayNameSort).ToList();

                Items.Clear();

                foreach (HLinkFamilyModel item in t)
                {
                    Items.Add(item);
                }
            }
        }
    }
}