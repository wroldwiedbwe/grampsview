// <copyright file="IDatabaseReloadDisplayService.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Prism.Navigation;

namespace GrampsView.Services
{
    /// <summary>
    /// Display the database reload view if required.
    /// </summary>
    public interface IDatabaseReloadDisplayService
    {
        bool ShowIfAppropriate(INavigationService iocNavigationService, bool shown);
    }
}