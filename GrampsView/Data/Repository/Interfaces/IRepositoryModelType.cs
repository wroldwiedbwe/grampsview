//-----------------------------------------------------------------------
//
// Various data modesl to small to be worth putting in their own file
// is first launched.
//
// <copyright file="IRepositoryModelType.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Repositories
{
    using System.Collections.Generic;

    /// <summary>
    /// Interfaces for Base Repository.
    /// </summary>
    /// <typeparam name="T"> Data ViewModel. </typeparam>
    /// <typeparam name="U"> $$(HLink)$$ ViewModel. </typeparam>
    public interface IRepositoryModelType<out T, U>
    {
        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value> The count. </value>
        int Count { get; }

        /// <summary>
        /// Gets the <see cref="T"/> with the specified key.
        /// </summary>
        /// <value> The <see cref="T"/>. </value>
        /// <param name="key"> The key. </param>
        /// <returns></returns>
        T this[string key] { get; }

        /// <summary>
        /// Gets the <see cref="T"/> with the specified key.
        /// </summary>
        /// <value> The <see cref="T"/>. </value>
        /// <param name="key"> The key. </param>
        /// <returns></returns>
        T this[U hLink] { get; }

        /// <summary>
        /// Clear the Repository Data.
        /// </summary>
        void Clear();

        /// <summary>
        /// Gets or sets the <see cref="T"/> with the specified key.
        /// </summary>
        /// <value> The <see cref="T"/>. </value>
        /// <param name="key"> The key. </param>
        /// <returns></returns>
        T Get(string key);

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key"> The key. </param>
        /// <returns></returns>
        T Get(int key);

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns></returns>
        IEnumerator<T> GetEnumerator();

        ///// <summary>
        ///// Gets the h link string collection.
        ///// </summary>
        ///// <returns></returns>
        // ObservableCollection<object> HLinkStringCollection();
        /// <summary>
        /// Randoms the item.
        /// </summary>
        /// <returns></returns>
        T GetRandomItem();
    }
}