// <copyright file="WhatsNewDisplayService.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Prism.Navigation;

using Xamarin.Essentials;

namespace GrampsView.Services
{
    public class WhatsNewDisplayService : IWhatsNewDisplayService
    {
        private static bool shown = false;

        public WhatsNewDisplayService()
        {
        }

        public bool ShowIfAppropriate(INavigationService iocNavigationService)
        {
            if (VersionTracking.IsFirstLaunchForCurrentBuild && !shown)
            {
                shown = true;
            }

            return shown;
        }
    }
}