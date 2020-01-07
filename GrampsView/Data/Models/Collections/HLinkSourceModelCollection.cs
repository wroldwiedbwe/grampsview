// <copyright file="HLinkSourceModelCollection.cs" company="PlaceholderCompany">
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
    [KnownType(typeof(ObservableCollection<HLinkSourceModel>))]
    public class HLinkSourceModelCollection : HLinkBaseCollection<HLinkSourceModel>
    {
        public new CardGroup GetCardGroup
        {
            get
            {
                CardGroup t = new CardGroup
                {
                    Title = "Source Collection",
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