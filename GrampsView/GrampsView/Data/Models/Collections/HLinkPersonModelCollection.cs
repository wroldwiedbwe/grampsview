// <copyright file="HLinkPersonModelCollection.cs" company="PlaceholderCompany">
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
    /// Contains pointers to family models.
    /// </summary>
    /// <seealso cref="GrampsView.Data.ViewModel.HLinkBaseCollection{GrampsView.Data.ViewModel.HLinkPersonModel}" />
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<HLinkPersonModel>))]
    public class HLinkPersonModelCollection : HLinkBaseCollection<HLinkPersonModel>
    {
        /// <summary>
        /// Gets the dereferenced person models.
        /// </summary>
        /// <value>
        /// The de reference.
        /// </value>
        public ObservableCollection<PersonModel> DeRef
        {
            get
            {
                ObservableCollection<PersonModel> t = new ObservableCollection<PersonModel>();

                foreach (HLinkPersonModel item in Items)
                {
                    t.Add(item.DeRef);
                }

                return t;
            }
        }

        public new CardGroup GetCardGroup
        {
            get
            {
                CardGroup t = new CardGroup
                {
                    Title = "People Collection",
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
            PersonModel tempModel = new PersonModel();

            if (Count > 0)
            {
                // Step through each citationmodel hlink in the collection
                for (int i = 0; i < Count; i++)
                {
                    tempModel = DV.PersonDV.PersonData.GetModelFromHLink(this[i]);

                    if (tempModel.HomeImageHLink.HomeUseImage)
                    {
                        FirstHLink = tempModel.HomeImageHLink;
                        break;
                    }
                }

                // Sort the collection
                List<HLinkPersonModel> t = this.OrderBy(HLinkEventModel => HLinkEventModel.DeRef.GBirthName.SortName).ToList();

                Items.Clear();

                foreach (HLinkPersonModel item in t)
                {
                    Items.Add(item);
                }
            }
        }
    }
}