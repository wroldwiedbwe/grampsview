//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="ICitationModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Model
{
    /// <summary>
    /// Public interfaces for the Note elements.
    /// </summary>
    public interface ICitationModel : IModelBase
    {
        /// <summary>
        /// Gets the get default text.
        /// </summary>
        /// <value>
        /// The get default text.
        /// </value>
        new string GetDefaultText { get; }
    }
}