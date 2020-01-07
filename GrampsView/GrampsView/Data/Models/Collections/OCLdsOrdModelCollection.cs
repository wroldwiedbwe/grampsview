// <copyright file="OCLdsOrdModelCollection.cs" company="PlaceholderCompany">
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
    [KnownType(typeof(ObservableCollection<LdsOrdModel>))]
    public class OCLdsOrdModelCollection : ObservableCollection<LdsOrdModel>
    {
        public CardGroup GetCardGroup
        {
            get
            {
                CardGroup t = new CardGroup
                {
                    Title = "LDS Ordination Collection",
                };

                t.Cards.AddRange(new ObservableCollection<object>(Items));

                return t;
            }
        }
    }
}