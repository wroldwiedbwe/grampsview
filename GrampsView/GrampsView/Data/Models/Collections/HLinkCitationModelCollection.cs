// <copyright file="HLinkCitationModelCollection.cs" company="PlaceholderCompany">
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
    /// Observable collection of Citation HLinks.
    /// </summary>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<HLinkCitationModel>))]
    public class HLinkCitationModelCollection : HLinkBaseCollection<HLinkCitationModel>
    {
        /// <summary>
        /// Gets getCardGroup for HLink collection.
        /// </summary>
        public new CardGroup GetCardGroup
        {
            get
            {
                CardGroup t = new CardGroup
                {
                    Title = "Citation Collection",
                };

                foreach (var item in Items)
                {
                    t.Cards.Add(item.DeRef);
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
            CitationModel tempCitationModel = new CitationModel();

            if (Count > 0)
            {
                // Step through each citationmodel hlink in the collection
                for (int i = 0; i < Count; i++)
                {
                    tempCitationModel = DV.CitationDV.CitationData.GetModelFromHLink(this[i]);

                    if (tempCitationModel.HomeImageHLink.HomeUseImage)
                    {
                        FirstHLink = tempCitationModel.HomeImageHLink;
                        break;
                    }
                }

                // Sort the collection
                List<HLinkCitationModel> t = this.OrderBy(HLinkCitationModel => HLinkCitationModel.DeRef.GDateContent).ToList();

                Items.Clear();

                foreach (HLinkCitationModel item in t)
                {
                    Items.Add(item);
                }
            }
        }
    }
}