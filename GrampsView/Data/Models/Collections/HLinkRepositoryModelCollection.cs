// <copyright file="HLinkRepositoryModelCollection.cs" company="PlaceholderCompany">
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
    public class HLinkRepositoryModelCollection : HLinkBaseCollection<HLinkRepositoryModel>
    {
        public new CardGroup GetCardGroup
        {
            get
            {
                CardGroup t = new CardGroup
                {
                    Title = "Repository Collection",
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