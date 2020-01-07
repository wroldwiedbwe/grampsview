//-----------------------------------------------------------------------
//
// Various data modesl to small to be worth putting in their own file
// is first launched.
//
// <copyright file="CardGroupCollection.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Model
{
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Runtime.Serialization;

    using GrampsView.Common;

    /// <summary>
    /// GRAMPS $$(Hlink)$$ element class.
    /// </summary>
    [CollectionDataContract]
    public class CardGroupCollection : ObservableCollection<CardGroup>
    {
        public CardGroupCollection()
        {
            this.CollectionChanged += FullObservableCollectionCollectionChanged;
        }

        /// <summary>
        /// Adds the specified argument card group.
        /// </summary>
        /// <param name="argCardGroup">
        /// The argument card group.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// argCardGroup
        /// </exception>
        public new void Add(CardGroup argCardGroup)
        {
            if (argCardGroup is null)
            {
                throw new ArgumentNullException(nameof(argCardGroup));
            }

            if (argCardGroup.CardGroupVisible)
            {
                base.Add(argCardGroup);
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        private void FullObservableCollectionCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("CollectionChanged");
        }
    }
}