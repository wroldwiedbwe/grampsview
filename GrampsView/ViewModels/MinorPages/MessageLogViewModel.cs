//-----------------------------------------------------------------------
// <copyright file="MessageLogViewModel.cs" company="MeMyselfAndI">
// Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Message Log routines.
/// </summary>
namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.Repository;

    using System.Collections.ObjectModel;

    /// <summary>
    /// <c>viewmodel</c> for the Message Log Page.
    /// </summary>
    public class MessageLogViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageLogViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The ioc common logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        /// <param name="iocNavigationService">
        /// </param>
        public MessageLogViewModel()
        {
            BaseTitle = "Message Log";

            BaseTitleIcon = CommonConstants.IconLog;
        }

        /// <summary>
        /// Gets the data load log.
        /// </summary>
        /// <value>
        /// The data load log.
        /// </value>

        public ObservableCollection<DataLogEntry> DataLoadLog
        {
            get
            {
                return DataStore.CN.DataLoadLog;
            }
        }
    }
}