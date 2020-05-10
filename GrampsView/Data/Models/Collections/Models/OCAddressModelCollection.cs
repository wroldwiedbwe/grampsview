// <copyright file="OCAddressModelCollection.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

/// <summary>
/// Collection of Address models.
/// </summary>
namespace GrampsView.Data.Collections
{
    using System;
    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;

    using GrampsView.Common;
    using GrampsView.Data.Model;

    /// <summary>
    /// Collection of Address models.
    /// </summary>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<AddressModel>))]
    public class OCAddressModelCollection : CardGroupBase<AddressModel>
    {
        public OCAddressModelCollection()
        {
            Title = "Address Collection";
        }
    }
}