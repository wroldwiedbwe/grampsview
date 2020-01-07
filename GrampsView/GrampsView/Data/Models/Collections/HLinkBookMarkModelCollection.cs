// <copyright file="HLinkBookMarkModelCollection.cs" company="PlaceholderCompany">
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
    public class HLinkBookMarkModelCollection : HLinkBaseCollection<HLinkBookMarkModel>
    {
        /// <summary>Gets the model for this hlink. </summary> <returns>ObservableCollection.<PersonModel></returns>
        public ObservableCollection<BookMarkModel> DeRef
        {
            get
            {
                ObservableCollection<BookMarkModel> t = new ObservableCollection<BookMarkModel>();

                foreach (HLinkBookMarkModel item in Items)
                {
                    t.Add(item.DeRef);
                }

                return t;
            }
        }

        public new CardGroup GetCardGroup
        {
            get
            {
                CardGroup t = new CardGroup
                {
                    Title = "Bookmark Collection",
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