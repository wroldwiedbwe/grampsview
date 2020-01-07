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
    using System.Collections.ObjectModel;

    using GrampsView.Common;
    using GrampsView.Events;
    using GrampsView.Services;

    using Prism.Events;
    using Prism.Navigation;

    /// <summary>
    /// <c> viewmodel </c> for the About <c> Flyout </c>.
    /// </summary>
    public class MessageLogViewModel : ViewModelBase
    {
        private readonly int maxCount = 10;

        private int currentIndex = -1;

        /// <summary>Initializes a new instance of the <see cref="MessageLogViewModel"/> class.</summary>
        /// <param name="iocCommonLogging">The ioc common logging.</param>
        /// <param name="iocEventAggregator">The ioc event aggregator.</param>
        /// <param name="iocNavigationService"></param>
        public MessageLogViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
            : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
            BaseEventAggregator.GetEvent<GVProgressMajorTextUpdate>().Subscribe(LogAdd, ThreadOption.UIThread);

            BaseTitle = "Message Log";
        }

        /// <summary>
        /// Gets the data load log.
        /// </summary>
        /// <value>
        /// The data load log.
        /// </value>
        public ObservableCollection<DataLogEntry> DataLoadLog { get; } = new ObservableCollection<DataLogEntry>();

        private void LogAdd(string entry)
        {
            DataLogEntry t = default(DataLogEntry);

            if (entry != null)
            {
                t.Label = string.Format("{0:HH: mm:ss}", System.DateTime.Now);
                t.Text = entry;

                lock (this)
                {
                    currentIndex += 1;

                    if (currentIndex > maxCount)
                    {
                        currentIndex = 0;
                    }

                    if (DataLoadLog.Count <= maxCount)
                    {
                        // Add if not full yet
                        DataLoadLog.Add(t);
                    }
                    else
                    {
                        // Replace if full
                        DataLoadLog[currentIndex] = t;
                    }
                }
            }
        }
    }
}