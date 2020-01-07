//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="INoteModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Model
{
    /// <summary>
    /// Public interfaces for the Note elements.
    /// </summary>
    public interface INoteModel : IModelBase
    {
        /// <summary>
        /// Gets the get h link Note Model that points to this ViewModel.
        /// </summary>
        /// <value> The get h link. </value>
        HLinkNoteModel GetHLink { get; }
    }
}