// <copyright file="HLinkBackLinkModelCollection.cs" company="PlaceholderCompany">
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
    [KnownType(typeof(ObservableCollection<HLinkBase>))]
    public class HLinkBackLinkModelCollection : ObservableCollection<HLinkBase>
    {
        /// <summary>Gets the model for this hlink. </summary> <returns>ObservableCollection.<PersonModel></returns>
        public ObservableCollection<ModelBase> DeRef
        {
            get
            {
                ObservableCollection<ModelBase> t = new ObservableCollection<ModelBase>();

                foreach (HLinkBase item in Items)
                {
                    t.Add(item.GetActualModel);
                }

                return t;
            }
        }

        public CardGroup GetCardGroup
        {
            get
            {
                CardGroup t = new CardGroup
                {
                    Title = "Backlink Collection",
                };

                foreach (var item in Items)
                {
                    t.Cards.Add(item.GetActualModel);
                }

                return t;
            }
        }
    }
}