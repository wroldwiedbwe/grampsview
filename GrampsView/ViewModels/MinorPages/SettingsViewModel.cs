//-----------------------------------------------------------------------
//
// View model for the fly-out page view
//
// <copyright file="AboutViewModel.cs" company="MeMyselfAndI">
// Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.ViewModels
{
    using GrampsView.Common;

    using Prism.Events;
    using Prism.Navigation;

    public class SettingsViewModel : ViewModelBase
    {
        public SettingsViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
                                                                    : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
            BaseTitle = "About";
            BaseTitleIcon = CommonConstants.IconSettings;
        }
    }
}