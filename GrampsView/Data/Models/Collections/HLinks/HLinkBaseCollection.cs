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
    using GrampsView.Common;

    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;

    /// <summary>
    /// GRAMPS $$(Hlink)$$ element class.
    /// </summary>
    [CollectionDataContract]
    public class HLinkBaseCollection<T> : ObservableCollection<T>, IHLinkCollectionBase<T>   // , IEnumerable, IEnumerator
         where T : HLinkBase, new()
    {
        // TODO Handle HLink collections properly by handling all their data

        //private int Position = -1;

        //public object Current
        //{
        //    get
        //    {
        //        return this[Position];
        //    }
        //}

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

            foreach (T item in Items)
            {
                t.Cards.Add(item);
            }

            return t;
        }

        //public IEnumerator GetCardGroupEnumerator()
        //{
        //    return (IEnumerator)this;
        //}

        //public bool MoveNext()
        //{
        //    if (Position < this.Count - 1)
        //    {
        //        ++Position;
        //        return true;
        //    }

        //    return false;
        //}

        //public void Reset()
        //{
        //    Position = -1;
        //}
    }
}