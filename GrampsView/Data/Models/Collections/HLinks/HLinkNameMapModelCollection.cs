// <copyright file="HLinkNameMapModelCollection.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

/// <summary>
/// </summary>
namespace GrampsView.Data.Collections
{
    using GrampsView.Data.Model;

    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;

    /// <summary>
    /// </summary>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<HLinkCitationModel>))]
    public class HLinkNameMapModelCollection : HLinkBaseCollection<HLinkNameMapModel>
    {
        public HLinkNameMapModelCollection()
        {
            Title = "NameMap Collection";
        }
    }
}