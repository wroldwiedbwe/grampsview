//-----------------------------------------------------------------------
//
// Common routines for the CommonRoutines
//
// <copyright file="CommonLogging.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Common
{
    using Microsoft.AppCenter.Analytics;
    using Microsoft.AppCenter.Crashes;

    using Prism.Logging;

    using System;
    using System.Collections.Generic;

    public class CommonLogging : ICommonLogging
    {
        private readonly CommonLogPrism Log = new CommonLogPrism();

        public CommonLogging()
        {
        }

        void ICommonLogging.CloseLogging()
        {
        }

        public void LogProgress(string value)
        {
            Log.Log(value, Category.Debug, Priority.Low);
        }

        public void LogRoutineEntry(string routine)
        {
            Log.Log("Start=> " + routine, Category.Debug, Priority.Low);
        }

        public void LogRoutineExit(string message)
        {
            Log.Log("End <= " + message, Category.Debug, Priority.Low);
        }

        public void LogVariable(string name, string value)
        {
            Log.Log(name + " = " + value, Category.Debug, Priority.Low);
        }

        // TODO detect redundant calls

        public static void LogException(string strMessage, Exception ex)
        {
            if (ex is null)
            {
                throw new ArgumentNullException(nameof(ex));
            }

            string exceptionMessage = strMessage + " - Exception:" + ex.Message + " - " + ex.Source + " - " + ex.InnerException + " - " + ex.StackTrace;

            Crashes.TrackError(ex,
                new Dictionary<string, string>{
                { "Message", strMessage},
                 { "Exception Message", exceptionMessage},
                });
        }

        public static void LogError(string argMessage, Dictionary<string, string> argErrorDetail)
        {
            //argErrorDetail.Add("Error", argMessage);

            Crashes.TrackError(null, argErrorDetail);

            Analytics.TrackEvent(argMessage, argErrorDetail);
        }
    }
}