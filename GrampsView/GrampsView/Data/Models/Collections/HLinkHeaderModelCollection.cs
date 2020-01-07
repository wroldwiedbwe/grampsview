//-----------------------------------------------------------------------
//
// Storage routines for the FamilyModel
//
// <copyright file="HLinkHeaderModelCollection.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Collections
{
    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;

    using GrampsView.Data.Model;

    /// <summary>
    /// Collection of EVent $$(HLinks)$$.
    /// </summary>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<HLinkHeaderModel>))]
    public class HLinkHeaderModelCollection : HLinkBaseCollection<HLinkHeaderModel>
    {
    }
}