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
        public CardGroup GetCardGroup()
        {
            return base.GetCardGroup("Source Collection");
        }
    }
}