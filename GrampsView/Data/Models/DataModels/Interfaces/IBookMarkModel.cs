//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="IBookMarkModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Model
{
    /// <summary>
    /// Public interfaces for the Tag elements.
    /// </summary>
    public interface IBookMarkModel : IModelBase
    {
        /// <summary>
        /// Sets the book mark h link.
        /// </summary>
        /// <value>
        /// The book mark h link.
        /// </value>
        HLinkBackLink HLinkBookMarkTarget
        {
            get;
            set;
        }
    }
}