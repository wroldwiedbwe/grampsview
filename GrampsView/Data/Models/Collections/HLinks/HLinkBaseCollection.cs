﻿//-----------------------------------------------------------------------
//
// Various data modesl to small to be worth putting in their own file
// is first launched.
//
// <copyright file="HLinkBaseCollection.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Model
{
    using GrampsView.Common;

    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;

    /// <summary>
    /// GRAMPS $$(Hlink)$$ element class.
    /// </summary>
    [CollectionDataContract]
    public class HLinkBaseCollection<T> : CardGroupBase<T>, IHLinkCollectionBase<T>
         where T : HLinkBase, new()
    {
        // TODO Handle HLink collections properly by handling all their data

        /// <summary>
        /// Gets or sets the first image hlink.
        /// </summary>
        public HLinkHomeImageModel FirstHLinkHomeImage { get; set; } = new HLinkHomeImageModel();

        //public virtual CardGroup GetCardGroup(string argTitle = "")
        //{
        //    CardGroup t = new CardGroup();

        // if (!string.IsNullOrEmpty(argTitle)) { t.Title = argTitle; };

        // foreach (T item in Items) { t.Add(item); }

        //    return t;
        //}
    }
}