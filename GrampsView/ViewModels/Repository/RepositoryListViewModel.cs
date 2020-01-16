//-----------------------------------------------------------------------
// Code behind for Repository List page
//
// <copyright file="RepositoryListViewModel.cs" company="MeMyselfAndI">
// Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.ViewModels
{
    using System.Collections.ObjectModel;

    using GrampsView.Common;
    using GrampsView.Data.DataView;

    using Prism.Events;
    using Prism.Navigation;

    /// <summary>
    /// View Model for the Event Section Page.
    /// </summary>
    public class RepositoryListViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryListViewModel" /> class.
        /// </summary>
        /// <param name="iocNavigationService">
        /// The ioc navigation service.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        public RepositoryListViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
            : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
            BaseTitle = "Repository List";
        }

        public CardGroup RepositorySource
        {
            get
            {
                CardGroup t = new CardGroup();

                t.Cards.AddRange(new ObservableCollection<object>(DV.RepositoryDV.GetAllAsHLink()));

                return t;
            }
        }
    }
}