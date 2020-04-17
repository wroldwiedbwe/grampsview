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
    using Microsoft.Extensions.Logging;

    using System;
    using System.Collections.Generic;

    public class CommonLogging : ICommonLogging
    {
        private static ILogger Log;
        private readonly ILoggerFactory LogFactory = LoggerFactory.Create(builder => { builder.SetMinimumLevel(LogLevel.Trace).AddConsole().AddDebug(); });

        public CommonLogging()
        {
            Log = LogFactory.CreateLogger("GrampsView");

            Log.LogInformation("Log started");
        }

        public static void LogError(string argMessage, Dictionary<string, string> argErrorDetail)
        {
            Log.LogError(argMessage, argErrorDetail);

            // Only Start App Center if there
            if (!Common.CommonRoutines.IsEmulator())
            {
                Crashes.TrackError(null, argErrorDetail);

                Analytics.TrackEvent(argMessage, argErrorDetail);
            }
        }

        public static void LogException(string strMessage, Exception ex)
        {
            if (ex is null)
            {
                throw new ArgumentNullException(nameof(ex));
            }

            Dictionary<string, string> errorDetail = new Dictionary<string, string>
            {
                { "Message", ex.Message },
                { "Source", ex.Source },
                { "Inner Exception", ex.InnerException.Message },
                { "StackTrace", ex.StackTrace }
            };

            Log.LogCritical(strMessage, errorDetail);

            // Only Start App Center if there
            string exceptionMessage = strMessage + " - Exception:" + ex.Message + " - " + ex.Source + " - " + ex.InnerException + " - " + ex.StackTrace;

            if (!Common.CommonRoutines.IsEmulator())
            {
                Crashes.TrackError(ex,
                new Dictionary<string, string>{
                { "Message", strMessage },
                { "Exception Message", exceptionMessage },
                });
            }
        }

        public void LogGeneral(string argMessage)
        {
            Dictionary<string, string> errorDetail = new Dictionary<string, string>
            {
                //{ "Message", ex.Message },
            };

            LogGeneral(argMessage, errorDetail);
        }

        public void LogGeneral(string argMessage, Dictionary<string, string> argDetails)
        {
            Log.LogDebug(argMessage, argDetails);
        }

        public void LogProgress(string value)
        {
            Log.LogTrace(value);
        }

        public void LogRoutineEntry(string routine)
        {
            Log.LogTrace("Start=> " + routine);
        }

        public void LogRoutineExit(string message)
        {
            Log.LogTrace("End <= " + message);
        }

        public void LogVariable(string name, string value)
        {
            Dictionary<string, string> moreDetail = new Dictionary<string, string>
            {
                //{ "Message", ex.Message },
            };

            Log.LogDebug(name + " = " + value, moreDetail);
        }

        // TODO detect redundant calls
    }
}