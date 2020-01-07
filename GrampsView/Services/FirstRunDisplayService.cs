// <copyright file="FirstRunDisplayService.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Prism.Navigation;

using Xamarin.Essentials;

namespace GrampsView.Services
{
    public class FirstRunDisplayService : IFirstRunDisplayService
    {
        private static bool shown = false;

        public FirstRunDisplayService()
        {
        }

        /// <summary>
        /// Shows if appropriate asynchronous.
        /// </summary>
        /// <param name="navService">
        /// The nav service.
        /// </param>
        /// <returns>
        /// </returns>
        public bool ShowIfAppropriate(INavigationService iocNavigationService)
        {
            if (VersionTracking.IsFirstLaunchEver && !shown)
            {
                shown = true;
            }

            return shown;
        }
    }
}