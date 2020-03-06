//-----------------------------------------------------------------------
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
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.Serialization;

    using GrampsView.Common;

    /// <summary>
    /// GRAMPS $$(Hlink)$$ element class.
    /// </summary>
    [CollectionDataContract]
    public class HLinkBaseCollection<T> : ObservableCollection<T>, IHLinkCollectionBase<T>
         where T : HLinkBase, new()
    {
        // TODO Handle HLink collections properly by handling all their data

        /// <summary>
        /// Gets or sets the first image h link.
        /// </summary>
        public HLinkHomeImageModel FirstHLinkHomeImage { get; set; } = new HLinkHomeImageModel();

        public virtual CardGroup GetCardGroup(string argTitle = "")
        {
            CardGroup t = new CardGroup();

            if (!string.IsNullOrEmpty(argTitle))
            {
                t.Title = argTitle;
            };

            t.Cards.AddRange(new ObservableCollection<object>(Items));

            return t;
        }
    }
}