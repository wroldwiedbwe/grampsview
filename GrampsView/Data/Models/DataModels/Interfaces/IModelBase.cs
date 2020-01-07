//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="IModelBase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Model
{
    using System;

    /// <summary>
    /// Public interfaces for the Event elements.
    /// </summary>
    public interface IModelBase : IComparable<ModelBase>
    {
        /// <summary>
        /// Gets the default text.
        /// </summary>
        /// <value>
        /// The get default text.
        /// </value>
        string GetDefaultText { get; }

        /// <summary>
        /// Gets or sets the h link key.
        /// </summary>
        /// <value>
        /// The h link key.
        /// </value>
        string HLinkKey
        {
            get; set;
        }

        ///// <summary>
        ///// Gets or sets the model user activity.
        ///// </summary>
        ///// <value>
        ///// The model user activity.
        ///// </value>
        //UserActivity ModelUserActivity { get; set; }
    }
}