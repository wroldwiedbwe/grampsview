//-----------------------------------------------------------------------
//
// Storage routines for the FamilyModel
//
// <copyright file="HLinkEventModelCollection.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

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
    /// Collection of EVent $$(HLinks)$$.
    /// </summary>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<HLinkEventModel>))]
    public class HLinkEventModelCollection : HLinkBaseCollection<HLinkEventModel>
    {
        /// <summary>
        /// Gets the de reference.
        /// </summary>
        /// <value>
        /// The de reference.
        /// </value>
        public ObservableCollection<EventModel> DeRef
        {
            get
            {
                ObservableCollection<EventModel> t = new ObservableCollection<EventModel>();

                foreach (HLinkEventModel item in Items)
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
            EventModel tempModel = new EventModel();

            if (Count > 0)
            {
                // Step through each citationmodel hlink in the collection
                for (int i = 0; i < Count; i++)
                {
                    tempModel = DV.EventDV.EventData.GetModelFromHLink(this[i]);

                    if (tempModel.HomeImageHLink.HomeUseImage)
                    {
                        FirstHLink = tempModel.HomeImageHLink;
                        break;
                    }
                }

                // Sort the collection
                List<HLinkEventModel> t = this.OrderBy(HLinkEventModel => HLinkEventModel.DeRef.GDate).ToList();

                Items.Clear();

                foreach (HLinkEventModel item in t)
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
                    Title = "Event Collection",
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