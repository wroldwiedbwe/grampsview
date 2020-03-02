﻿// <copyright file="OCLdsOrdModelCollection.cs" company="PlaceholderCompany">
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
    public class OCLdsOrdModelCollection : ModelBaseCollection<LdsOrdModel>
    {
        public override CardGroup GetCardGroup()
        {
            return base.GetCardGroup("LDS Ordination Collection");
        }
    }
}