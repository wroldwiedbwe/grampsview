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
    using System;
    using System.Collections;
    using System.Diagnostics;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    using GrampsView.Data.Repository;

    using Windows.Foundation.Diagnostics;
    using Windows.Storage;

    /// <summary>
    /// LoggingScenario is a central singleton class which contains the logging-specific sample code.
    /// </summary>
    [DataContract]
    public class CommonLogging : ICommonLogging, IDisposable
    {
        /// <summary>
        /// The sample's one this.localFileLogSession.
        /// </summary>
        [DataMember]
        private FileLoggingSession localFileLogSession;

        /// <summary>
        /// SAMPLE CODE This boolean tracks whether or not there are any sessions listening to the
        /// app's channel. This is adjusted as the channel's LoggingEnabled event is raised. Search
        /// for OnChannelLoggingEnabled for more information.
        /// </summary>
        private bool localIsChannelEnabled = false;

        /// <summary>
        /// The sample's one channel.
        /// </summary>
        private LoggingChannel logChannel;

        /// <summary>
        /// This is the current maximum level of listeners of the application's channel. It is
        /// adjusted as the channel's LoggingEnabled event is raised. Search for
        /// OnChannelLoggingEnabled for more information.
        /// </summary>
        private LoggingLevel logChannelLoggingLevel = LoggingLevel.Verbose;

        /// <summary>
        /// Set to 'true' if Dispose() has been called.
        /// </summary>
        private bool loggingSessionDisposed = false;

        /// <summary>
        /// The routine name stack
        /// </summary>
        private Stack RoutineNameStack = new Stack();

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonLogging" /> class.
        /// <para>
        /// Disallow creation of instances beyond the one instance for the process. The one instance
        /// is accessible via GetLoggingScenarioSingleton() (see below).
        /// </para>
        /// </summary>
        public CommonLogging()
        {
            LogFileGeneratedCount = 0;

            logChannel = new LoggingChannel(CommonConstants.LogDefaultChannelName, null);
            logChannel.LoggingEnabled += OnChannelLoggingEnabled;

            //Prism.PrismApplicationBase.Current.Resuming += OnAppResuming;

            //Prism.PrismApplicationBase.Current.Suspending += PrepareToSuspend;

            // If the app is being launched (not resumed), the following call will activate logging
            // if it had been activated during the last suspend.
            ResumeLoggingIfApplicable();
        }

        /// <summary>
        /// Gets a value indicating whether logging is enabled.
        /// </summary>
        /// <value>
        /// <c> true </c> if this instance is logging enabled; otherwise, <c> false </c>.
        /// </value>
        public bool IsLoggingEnabled
        {
            get
            {
                return localFileLogSession != null;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is preparing for suspend.
        /// </summary>
        /// <value>
        /// <c> true </c> if this instance is preparing for suspend; otherwise, <c> false </c>.
        /// </value>
        public bool IsPreparingForSuspend
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the channel.
        /// </summary>
        /// <value>
        /// The channel.
        /// </value>
        public LoggingChannel LogChannel
        {
            get
            {
                return logChannel;
            }
        }

        /// <summary>
        /// Gets the number of times LogFileGeneratedHandler has been called.
        /// </summary>
        public int LogFileGeneratedCount
        {
            get;
            private set;
        }

        /// <summary>
        /// Closes the logging.
        /// </summary>
        public void CloseLogging()
        {
            Task.Run(async () => await CloseSessionSaveFinalLogFile());
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Log a progress message.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        public void LogProgress(string value)
        {
            LogChannel.LogMessage(value, LoggingLevel.Information);

            // this.LogChannel.LogMessage(value);
            Debug.WriteLine(value);
        }

        /// <summary>
        /// Log start of a routine.
        /// </summary>
        /// <param name="argRoutine">
        /// The subroutine name.
        /// </param>
        public void LogRoutineEntry(string argRoutine)
        {
            RoutineNameStack.Push(argRoutine);

            LogChannel.LogMessage(argRoutine + "-> start", LoggingLevel.Information);

            Debug.WriteLine(argRoutine + "-> start");
        }

        /// <summary>
        /// Logs the routine exit.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public void LogRoutineExit(string message = "")
        {
            string routineName = RoutineNameStack.Pop() as string;

            LogChannel.LogMessage(routineName + "<- exit -" + message, LoggingLevel.Information);

            Debug.WriteLine(message + "<- exit -" + message);
        }

        /// <summary>
        /// Log a variable.
        /// </summary>
        /// <param name="name">
        /// The variable name.
        /// </param>
        /// <param name="value">
        /// the value of the variable.
        /// </param>
        public void LogVariable(string name, string value)
        {
            LogChannel.LogMessage(name + " = " + value, LoggingLevel.Information);

            Debug.WriteLine(name + " = " + value);
        }

        /// <summary>
        /// called when logging enabled.
        /// </summary>
        /// <param name="sender">
        /// The log channel.
        /// </param>
        /// <param name="args">
        /// Parameter not used.
        /// </param>
        public void OnChannelLoggingEnabled(ILoggingChannel sender, object args)
        {
            // This method is called when the channel is informing us of channel-related state
            // changes. Save new channel state. These values can be used for advanced logging
            // scenarios where, for example, it's desired to skip blocks of logging code if the
            // channel is not being consumed by any sessions.
            localIsChannelEnabled = sender.Enabled;
            logChannelLoggingLevel = sender.Level;
        }

        /// <summary>
        /// Prepare for suspension.
        /// </summary>
        public void PrepareToSuspend()
        {
            CheckDisposed();

            CommonLocalSettings.LoggingEnabled = (localFileLogSession != null);
        }

#pragma warning disable IDE0060 // Remove unused parameter

        /// <summary>
        /// Prepares to syspend.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        public void PrepareToSuspend(object sender, object e)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            Task.Run(() => PrepareToSuspendAsync());
        }

        /// <summary>
        /// SAMPLE CODE Prepare this scenario for suspend.
        /// </summary>
        /// <returns>
        /// Void Task.
        /// </returns>
        public async Task PrepareToSuspendAsync()
        {
            CheckDisposed();

            if (localFileLogSession != null)
            {
                IsPreparingForSuspend = true;

                try
                {
                    // Before suspend, save any final log file.
                    string finalFileBeforeSuspend = await CloseSessionSaveFinalLogFile();

                    localFileLogSession.Dispose();
                    localFileLogSession = null;

                    // Save values used when the app is resumed or started later. Logging is enabled.
                    CommonLocalSettings.LoggingEnabled = true;

                    // Save the log file name saved at suspend so the sample UI can be updated on
                    // resume with that information.
                    ApplicationData.Current.LocalSettings.Values["LogFileGeneratedBeforeSuspend"] = finalFileBeforeSuspend;
                }
                finally
                {
                    IsPreparingForSuspend = false;
                }
            }
            else
            {
                // Save values used when the app is resumed or started later. Logging is not enabled
                // and no log file was saved.
                CommonLocalSettings.LoggingEnabled = false;
                ApplicationData.Current.LocalSettings.Values["LogFileGeneratedBeforeSuspend"] = null;
            }
        }

        /// <summary>
        /// This is called when the app is either resuming or starting. It will enable logging if the
        /// app has never been started before or if logging had been enabled the last time the app
        /// was running.
        /// </summary>
        public void ResumeLoggingIfApplicable()
        {
            CheckDisposed();

            if (CommonLocalSettings.LoggingEnabled == false)
            {
                CommonLocalSettings.LoggingEnabled = true;
            }

            if (CommonLocalSettings.LoggingEnabled == true)
            {
                StartLogging(true);
            }

            // When the sample suspends, it retains state as to whether or not it had generated a new
            // log file at the last suspension. This allows any UI to be updated on resume to reflect
            // that fact.
            if (ApplicationData.Current.LocalSettings.Values.TryGetValue("LogFileGeneratedBeforeSuspend", out object logFileGeneratedBeforeSuspendObject) &&
                logFileGeneratedBeforeSuspendObject != null &&
                logFileGeneratedBeforeSuspendObject is string)
            {
                ApplicationData.Current.LocalSettings.Values["LogFileGeneratedBeforeSuspend"] = null;
            }

            LogProgress("Resuming Logging");
        }

        /// <summary>
        /// Starts the logging.
        /// </summary>
        /// <param name="logToFile">
        /// if set to <c> true </c> [log to file].
        /// </param>
        public void StartLogging(bool logToFile)
        {
            CheckDisposed();

            // If not this.localFileLogSession exists, create one.
            // NOTE: There are use cases where an application may want to create only a channel for
            // sessions outside of the application itself. See MSDN for details. This sample is the
            // common scenario of an app logging events which it wants to place in its own log file,
            // so it creates a this.localFileLogSession and channel as a pair. The channel is created
            // during construction of this LoggingScenario class so it already exists by the time
            // this function is called.
            if (localFileLogSession == null)
            {
                if (logToFile == true)
                {
                    localFileLogSession = new FileLoggingSession(CommonConstants.LogDefaultSessionName);
                    localFileLogSession.LogFileGenerated += LogFileGeneratedHandler;

                    // Log all messages using Verbose filter
                    localFileLogSession.AddLoggingChannel(logChannel, LoggingLevel.Verbose);
                }
            }
            else
            {
                localFileLogSession = new FileLoggingSession(CommonConstants.LogDefaultSessionName);
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c> true </c> to release both managed and unmanaged resources; <c> false </c> to release
        /// only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (loggingSessionDisposed == false)
            {
                loggingSessionDisposed = true;

                if (disposing)
                {
                    if (logChannel != null)
                    {
                        logChannel.Dispose();
                        logChannel = null;
                    }

                    if (localFileLogSession != null)
                    {
                        localFileLogSession.Dispose();
                        localFileLogSession = null;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the time stamp.
        /// </summary>
        /// <returns>
        /// Time stamp.
        /// </returns>
        private static string GetTimeStamp()
        {
            DateTime now = DateTime.Now;
            return string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        "{0:D2}{1:D2}{2:D2}-{3:D2}{4:D2}{5:D2}{6:D3}",
                        now.Year - 2000,
                        now.Month,
                        now.Day,
                        now.Hour,
                        now.Minute,
                        now.Second,
                        now.Millisecond);
        }

        /// <summary>
        /// Helper function for other methods to call to check Dispose() state.
        /// </summary>
        private void CheckDisposed()
        {
            if (loggingSessionDisposed)
            {
                throw new ObjectDisposedException("CommonLogging");
            }
        }

        /// <summary>
        /// Closes the session save final log file.
        /// </summary>
        /// <returns>
        /// Final Log file name.
        /// </returns>
        private async Task<string> CloseSessionSaveFinalLogFile()
        {
            CheckDisposed();

            try
            {
                // Save the final log file before closing the this.localFileLogSession.
                StorageFile finalFileBeforeSuspend = await localFileLogSession.CloseAndSaveToFileAsync();

                if (finalFileBeforeSuspend != null)
                {
                    LogFileGeneratedCount++;

                    // Get the the app-defined log file folder.
                    StorageFolder sampleAppDefinedLogFolder =
                    await ApplicationData.Current.LocalFolder.CreateFolderAsync(
                       CommonConstants.LogAppLogFolderName,
                       CreationCollisionOption.OpenIfExists);

                    // Create a new log file name based on a date/time stamp.
                    string newLogFileName = string.Format("{0}-{1}.etl", CommonConstants.LogAppLogFileBaseName, GetTimeStamp());

                    // Move the final log into the app-defined log file folder.
                    await finalFileBeforeSuspend.MoveAsync(sampleAppDefinedLogFolder, newLogFileName);

                    // Return the path to the log folder.
                    return System.IO.Path.Combine(sampleAppDefinedLogFolder.Path, newLogFileName);
                }
                else
                {
                    return null;
                }
            }
            catch (ObjectDisposedException ex)
            {
                // TODO Clarify this
                DataStore.CN.NotifyException("Exception. ", ex);

                // The object is already disposed
                return null;
            }
            catch (Exception ex)
            {
                DataStore.CN.NotifyException("CloseSessionSaveFinalLogFile", ex);

                throw;
            }
        }

        /// <summary>
        /// This handler is called by the FileLoggingSession instance when a log file reaches a size
        /// of 256MB. When FileLoggingSession calls this handler, it's effectively giving the
        /// developer a chance to own the log file.
        /// </summary>
        /// <param name="sender">
        /// The FileInfoLoggingSession to the this.localFileLogSession which has generated a new file.
        /// </param>
        /// <param name="args">
        /// The LogFileGeneratedEventArgs instance which contains a StorageFile field
        /// LogFileGeneratedEventArgs.File representing the new log file.
        /// </param>
        private async void LogFileGeneratedHandler(IFileLoggingSession sender, LogFileGeneratedEventArgs args)
        {
            LogFileGeneratedCount++;
            StorageFolder sampleAppDefinedLogFolder =
                await ApplicationData.Current.LocalFolder.CreateFolderAsync(
                CommonConstants.LogAppLogFolderName,
                CreationCollisionOption.OpenIfExists);

            string newLogFileName = string.Format("{0}-{1}.etl", CommonConstants.LogAppLogFileBaseName, GetTimeStamp());
            await args.File.MoveAsync(sampleAppDefinedLogFolder, newLogFileName);

            // TODO fu why this code exists if (IsPreparingForSuspend == false) { string
            // newLogFileFullPathName = System.IO.Path.Combine(sampleAppDefinedLogFolder.Path,
            // newLogFileName); }
        }

        /// <summary>
        /// Called when [application resuming].
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnAppResuming(object sender, object e)
        {
            // If logging was active at the last suspend, ResumeLoggingIfApplicable will re-activate logging.
            ResumeLoggingIfApplicable();
        }

        ///// <summary>
        ///// Called when [application suspending].
        ///// </summary>
        ///// <param name="sender"> The sender. </param>
        ///// <param name="e">
        ///// The <see cref="Windows.ApplicationViewModel.SuspendingEventArgs"/> instance containing the
        ///// event data.
        ///// </param>
        // private async void OnAppSuspending(object sender,
        // Windows.ApplicationViewModel.SuspendingEventArgs e) { // Get a deferral before performing
        // any async operations to avoid suspension prior to // LoggingScenario completing var
        // deferral = e.SuspendingOperation.GetDeferral();

        // //Prepare logging for suspension. await PrepareToSuspendAsync();

        // // From LoggingScenario's perspective, it's now okay to suspend, so release the deferral.
        // deferral.Complete(); }
    }
}