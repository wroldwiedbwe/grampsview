﻿//-----------------------------------------------------------------------
//
// Common routines for the CommonProgressRoutines
//
// <copyright file="CommonNotifications.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Common
{
    using GrampsView.Data.Repository;
    using GrampsView.Events;

    using Prism.Events;

    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    /// Common Progress routines.
    /// </summary>
    [DataContract]
    public class CommonNotifications : CommonBindableBase, ICommonNotifications
    {
        /// <summary>
        /// Common logging routines.
        /// </summary>
        private readonly ICommonLogging _CL;

        private string _MajorStatusMessage = string.Empty;

        /// <summary>
        /// Injected Event Aggregator.
        /// </summary>
        private readonly IEventAggregator _EventAggregator;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonNotifications" /> class.
        /// </summary>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        public CommonNotifications(IEventAggregator iocEventAggregator, ICommonLogging iocCommonLogging)
        {
            _EventAggregator = iocEventAggregator;

            _CL = iocCommonLogging;
        }

        public string MajorStatusMessage
        {
            get
            {
                return _MajorStatusMessage;
            }

            set
            {
                SetProperty(ref _MajorStatusMessage, value);
            }
        }

        /// <summary>
        /// Changes the loading message.
        /// </summary>
        /// <param name="strMessage">
        /// The string message.
        /// </param>
        /// <returns>
        /// </returns>
        public async Task ChangeLoadingMessage(string strMessage)
        {
            _EventAggregator.GetEvent<GVProgressLoading>().Publish(strMessage);

            _CL.LogVariable("ChangeLoadingMessage", strMessage);

            await MajorStatusAdd(strMessage).ConfigureAwait(false);

            return;
        }

        /// <summary>
        /// Notifies the general status.
        /// </summary>
        /// <param name="strMessage">
        /// The string message.
        /// </param>
        /// <returns>
        /// </returns>
        public async Task MajorStatusAdd(string strMessage)
        {
            await MajorStatusAdd(strMessage, false).ConfigureAwait(false);
        }

        /// <summary>
        /// Majors the status add.
        /// </summary>
        /// <param name="argMessage">
        /// The string message.
        /// </param>
        /// <param name="argShowProgressRing">
        /// if set to <c> true </c> [show progress ring].
        /// </param>
        /// <returns>
        /// </returns>
        public async Task MajorStatusAdd(string argMessage, bool argShowProgressRing)
        {
            await Task.Run(() => _EventAggregator.GetEvent<GVProgressMajorTextUpdate>().Publish(argMessage)).ConfigureAwait(false);

            _CL.LogVariable("MajorStatusAdd", argMessage);

            MajorStatusMessage = argMessage;

            // majorStatusQueue.Enqueue(new QueueItem { Text = strMessage, showProgressRing = false });
            return;
        }

        /// <summary>
        /// Majors the status delete.
        /// </summary>
        /// <returns>
        /// </returns>
        public async Task MajorStatusDelete()
        {
            MajorStatusMessage = string.Empty;

            //// Pop top item
            // if (majorStatusQueue.Count > 0) { QueueItem oldItem = majorStatusQueue.Dequeue();

            // localCL.LogVariable("NotifyUserGeneral", "Major Status Delete => " + oldItem.Text); }

            //// Display current item
            // if (majorStatusQueue.Count > 0) { QueueItem currentItem = majorStatusQueue.Peek();

            // await Task.Run(() =>
            // localEventAggregator.GetEvent<GVProgressMajorTextUpdate>().Publish(currentItem.Text)).ConfigureAwait(false);
            // } else { await Task.Run(() =>
            // localEventAggregator.GetEvent<GVProgressMajorTextUpdate>().Publish(null)).ConfigureAwait(false); }
        }

        /// <summary>
        /// Handle DialogBox messages.
        /// </summary>
        /// <param name="argADA">
        /// The message text.
        /// </param>
        public void NotifyDialogBox(ActionDialogArgs argADA)
        {
            // TODO not very clean but what to do when displaying messages before hub page is loaded
            _EventAggregator.GetEvent<GRAMPSDialogBoxEvent>().Publish(argADA);
        }

        /// <summary>
        /// Notifies the error.
        /// </summary>
        /// <param name="strMessage">
        /// The string message.
        /// </param>
        public void NotifyError(string strMessage)
        {
            Dictionary<string, string> argErrorDetail = new Dictionary<string, string>();

            NotifyError(strMessage, argErrorDetail);
        }

        /// <summary>Notifies the user of an error and logs it for further analysis.</summary>
        /// <param name="argMessage">The argument message.</param>
        /// <param name="argErrorDetail">The argument error detail.</param>
        public void NotifyError(string argMessage, Dictionary<string, string> argErrorDetail)
        {
            ActionDialogArgs t = new ActionDialogArgs
            {
                Name = "Error",
                Text = argMessage
            };

            NotifyDialogBox(t);

            MajorStatusAdd(argMessage);

            argErrorDetail.Add("Error", argMessage);

            CommonLogging.LogError(argMessage, argErrorDetail);
        }

        /// <summary>
        /// notify the user about an Exception.
        /// </summary>
        /// <param name="strMessage">
        /// general description of where the Exception occurred.
        /// </param>
        /// <param name="ex">
        /// Exception object.
        /// </param>
        public void NotifyException(string strMessage, Exception ex)
        {
            if (ex is null)
            {
                throw new ArgumentNullException(nameof(ex));
            }

            string exceptionMessage = strMessage + " - Exception:" + ex.Message + " - " + ex.Source + " - " + ex.InnerException + " - " + ex.StackTrace;

            NotifyError(exceptionMessage);

            CommonLogging.LogException(strMessage, ex);

            // Remove serialised data in case it is the issue
            CommonLocalSettings.DataSerialised = false;
        }
    }
}