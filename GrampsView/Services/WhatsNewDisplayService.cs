// <copyright file="WhatsNewDisplayService.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Services
{
    using GrampsView.Events;
    using GrampsView.Views;
    using Prism.Events;
    using Prism.Navigation;

    using Xamarin.Essentials;

    public class WhatsNewDisplayService : IWhatsNewDisplayService
    {
        public WhatsNewDisplayService()
        {
        }

        public bool ShowIfAppropriate(IEventAggregator iocEventAggregator)
        {
            if (iocEventAggregator is null)
            {
                return false;
            }

            if (VersionTracking.IsFirstLaunchForCurrentBuild)
            {
                //Common.CommonLocalSettings.WhatsNewDisplayed = true;
                iocEventAggregator.GetEvent<PageNavigateEvent>().Publish(nameof(WhatsNewPage));

                return true;
            }

            return false;
        }
    }
}