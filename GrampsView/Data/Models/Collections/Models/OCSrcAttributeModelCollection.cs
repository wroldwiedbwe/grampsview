﻿// <copyright file="OCSrcAttributeModelCollection.cs" company="PlaceholderCompany">
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
    /// <seealso cref="System.Collections.ObjectViewModel.ObservableCollection{GrampsView.Data.ViewModel.SrcAttributeModel}"/>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<SrcAttributeModel>))]
    public class OCSrcAttributeModelCollection : ModelBaseCollection<SrcAttributeModel>
    {
        public override CardGroup GetCardGroup()
        {
            return base.GetCardGroup("Attribute Collection");
        }
    }
}