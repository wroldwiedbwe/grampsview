﻿// <copyright file="SurnameModelCollection.cs" company="PlaceholderCompany">
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
    /// Attribute model collection.
    /// </summary>
    /// <seealso cref="System.Collections.ObjectViewModel.ObservableCollection{GrampsView.Data.ViewModel.AttributeModel}" />
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<SurnameModel>))]
    public class SurnameModelCollection : ModelBaseCollection<SurnameModel>
    {
        public string GetPrimarySurname
        {
            get
            {
                // TODO Handle multiple surnames

                if (Items.Count > 0)
                {
                    return Items[0].GText;
                }

                return string.Empty;
            }
        }

        public CardGroup GetCardGroup()
        {
            return base.GetCardGroup("Surname Model Collection");
        }
    }
}