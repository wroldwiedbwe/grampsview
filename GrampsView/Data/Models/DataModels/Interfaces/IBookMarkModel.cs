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
        /// <value> The book mark h link. </value>
        string BookMarkHLink
        {
            set;
        }

        /// <summary>
        /// Gets the get book mark h link.
        /// </summary>
        /// <value> The get book mark h link. </value>
        HLinkBase GetBookMarkHLink
        {
            get;
        }

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        /// <value> The target. </value>
        string GTarget
        {
            get;
            set;
        }
    }
}