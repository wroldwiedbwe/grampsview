// <copyright file="IBookMarkDataView.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Data.DataView
{
    using System.Collections.Generic;

    using GrampsView.Data.Collections;

    using GrampsView.Data.Model;
    using GrampsView.Data.Repositories;

    /// <summary>
    /// Interface for BookMark data view.
    /// </summary>
    /// <seealso cref="GrampsView.Data.DataView.IDataViewBase{GrampsView.Data.ViewModel.BookMarkModel, GrampsView.Data.ViewModel.HLinkBookMarkModel, GrampsView.Data.Collections.HLinkBookMarkModelCollection}" />
    public interface IBookMarkDataView : IDataViewBase<BookMarkModel, HLinkBookMarkModel, HLinkBookMarkModelCollection>
    {
        /// <summary>
        /// Gets the book mark data.
        /// </summary>
        /// <value>
        /// The book mark data.
        /// </value>
        RepositoryModelType<BookMarkModel, HLinkBookMarkModel> BookMarkData
        {
            get;
        }

        /// <summary>
        /// Gets the data default sort.
        /// </summary>
        /// <value>
        /// The data default sort.
        /// </value>
        new IReadOnlyList<BookMarkModel> DataDefaultSort { get; }

        /// <summary>
        /// Gets the get all as hlink.
        /// </summary>
        /// <value>
        /// The get all as hlink.
        /// </value>
        HLinkBookMarkModelCollection GetAllAsHlink
        {
            get;
        }

        /// <summary>
        /// Gets all as hlink base.
        /// </summary>
        /// <returns>
        /// HLink Base Collection.
        /// </returns>
        HLinkBaseCollection<HLinkBase> GetAllAsHlinkBase();

        /// <summary>
        /// Gets all as ViewModel.
        /// </summary>
        /// <returns>
        /// List of BookMark models.
        /// </returns>
        new List<BookMarkModel> GetAllAsModel();
    }
}