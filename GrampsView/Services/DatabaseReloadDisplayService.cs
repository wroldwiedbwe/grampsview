// <copyright file="DatabaseReloadDisplayService.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Services
{
    using GrampsView.Common;
    using GrampsView.Data.Repository;
    using GrampsView.Views;

    using Prism.Navigation;

    // For instructions on testing this service see https://github.com/Microsoft/WindowsTemplateStudio/tree/master/docs/features/whats-new-prompt.md
    public class DatabaseReloadDisplayService : IDatabaseReloadDisplayService
    {
        public DatabaseReloadDisplayService()
        {
        }

        /// <summary>
        /// Shows database reload view if appropriate.
        /// </summary>
        /// <returns>
        /// if the view is displayed.
        /// </returns>
        public bool ShowIfAppropriate(INavigationService iocNavigationService, bool shown)
        {
            if (CommonLocalSettings.DatabaseReloadNeeded && !shown)
            {
                DataStore.NV.Nav(nameof(NeedDatabaseReloadPage));

                shown = true;
            }

            return shown;
        }
    }
}