﻿// <copyright file="HLinkFamilyModelCollection.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

/// <summary>
/// </summary>
namespace GrampsView.Data.Collections
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.Serialization;

    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    /// <summary>
    /// Colelction of Family hLinks.
    /// </summary>
    /// <seealso cref="GrampsView.Data.ViewModel.HLinkBaseCollection{GrampsView.Data.ViewModel.HLinkFamilyModel}" />
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

        /// <summary>Helper method to sort and set the firt image link.</summary>
        public void SortAndSetFirst()
        {
            // Return if null
            if (this == null)
            {
                return;
            }

            // Set the first image link. Assumes main image is manually set to the first image in
            // Gramps if we need it to be, e.g. Citations.
            FamilyModel tempModel = new FamilyModel();

            if (Count > 0)
            {
                // Step through each citationmodel hlink in the collection
                for (int i = 0; i < Count; i++)
                {
                    tempModel = DV.FamilyDV.FamilyData.GetModelFromHLink(this[i]);

                    if (tempModel.HomeImageHLink.HomeUseImage)
                    {
                        FirstHLink = tempModel.HomeImageHLink;
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

        public new CardGroup GetCardGroup
        {
            get
            {
                CardGroup t = new CardGroup
                {
                    Title = "Family Collection",
                };

                foreach (var item in Items)
                {
                    t.Cards.Add(item.DeRef);
                }

                return t;
            }
        }
    }
}