﻿// <copyright file="HLinkPlaceModelCollection.cs" company="PlaceholderCompany">
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
        public CardGroup GetCardGroup()
        {
            return base.GetCardGroup("Place Collection");
        }
    }
}