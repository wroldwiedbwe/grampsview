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
    public class HLinkBLModelCollection : CardGroupBase<object>
    {
        public HLinkBLModelCollection()
        {
            Title = "Backlink Collection";
        }

        //public CardGroup GetCardGroup()
        //{
        //    CardGroup t = new CardGroup
        //    {
        //        Title = "Backlink Collection"
        //    };

        // foreach (var item in Items) { t.Add(item.HLink()); }

        //    return t;
        //}
    }
}