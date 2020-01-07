// <copyright file="HLinkPlaceModelCollection.cs" company="PlaceholderCompany">
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
    [KnownType(typeof(ObservableCollection<HLinkCitationModel>))]
    public class HLinkPlaceModelCollection : HLinkBaseCollection<HLinkPlaceModel>
    {
        public new CardGroup GetCardGroup
        {
            get
            {
                return GetCardGroupWithTitle("Place Collection");
            }
        }

        public CardGroup GetCardGroupWithTitle(string title)
        {
            CardGroup t = new CardGroup
            {
                Title = title,
            };

            foreach (var item in Items)
            {
                t.Cards.Add(item.DeRef);
            }

            return t;
        }
    }
}