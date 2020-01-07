// <copyright file="PageTitleChangedEventArgs.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsNet.Events
{
    using System;

    /// <summary>
    /// Define sht eargumnets to the page title changed event.
    /// </summary>
    public class PageTitleChangedEventArgs
    {
        /// <summary>
        /// Gets or sets defines the symbol to be used for the page header.
        /// </summary>
        public String PageIcon { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public string PageTitle { get; set; }
    }
}