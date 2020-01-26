﻿// <copyright file="HLinkBackLinkModelCollection.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

/// <summary>
/// </summary>
namespace GrampsView.Data.Collections
{
    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;

    using GrampsView.Common;
    using GrampsView.Data.Model;

    /// <summary>
    /// </summary>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<HLinkBackLink>))]
    public class HLinkBackLinkModelCollection
    {
        private ObservableCollection<HLinkBackLink> Items = new ObservableCollection<HLinkBackLink>();

        public void Add(HLinkBackLink ArgHLink)
        {
            Items.Add(ArgHLink);
        }

        public CardGroup GetCardGroup()
        {
            var tt = this;

            CardGroup t = new CardGroup
            {
                Title = "Backlink Collection",
            };

            foreach (var item in Items)
            {
                t.Cards.Add(item.GetHLink());
            }

            return t;
        }
    }
}