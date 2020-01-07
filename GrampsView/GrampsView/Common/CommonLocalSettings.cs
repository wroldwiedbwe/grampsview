//-----------------------------------------------------------------------
//
// Common routines for the CommonRoutines
//
// <copyright file="CommonLocalSettings.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Common
{
    using Xamarin.Essentials;

    /// <summary>
    /// Various common routines.
    /// </summary>
    public static class CommonLocalSettings
    {
        //private const string SettingsKey = "settings_key";

        //private static readonly string SettingsDefault = string.Empty;

        /// <summary>
        /// Gets a value indicating whether needs the database reload.
        /// </summary>
        /// <returns>
        /// </returns>
        public static bool DatabaseReloadNeeded
        {
            get
            {
                if (DatabaseVersion < CommonConstants.GrampsViewDatabaseVersion)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //////////////////////////////////
        /// OLD
        /// //////////////////////////////
        /// <summary>
        /// Gets or sets the database version.
        /// </summary>
        /// <value>
        /// The database version.
        /// </value>
        public static int DatabaseVersion
        {
            get
            {
                int localGrampsViewDatabaseVersion = Preferences.Get(CommonConstants.SettingsGrampsViewDatabaseVersion, int.MinValue);

                if (localGrampsViewDatabaseVersion == int.MinValue)
                {
                    // If the Setting is not defined then assume the database has not been loaded so
                    // the version number is set to MinValue to force load
                    return int.MinValue;
                }

                // bool validDatabaseReloadFlag = int.TryParse(localGrampsViewDatabaseVersion, out
                // int grampsViewDatabaseVersion);

                return localGrampsViewDatabaseVersion;
            }

            set
            {
                SetInt("GrampsViewDatabaseVersion", value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [data serialised].
        /// </summary>
        /// <value>
        /// <c> true </c> if [data serialised]; otherwise, <c> false </c>.
        /// </value>
        public static bool DataSerialised
        {
            get
            {
                return GetBool("SerialisedData");
            }

            set
            {
                SetBool("SerialisedData", value);
            }
        }

        ///// <summary>
        ///// Gets or sets the file data input.
        ///// </summary>
        ///// <value>
        ///// The file data input.
        ///// </value>
        //public static string FileDataInput
        //{
        //    get
        //    {
        //        return Task.Run<string>(() => GetStorageFile("FileGrampsDataInput")).Result;
        //    }

        //    set
        //    {
        //        SetStorageFile("FileGrampsDataInput", value);
        //    }
        //}

        /// <summary>
        /// Gets or sets a value indicating whether [first run].
        /// </summary>
        /// <value>
        /// <c> true </c> if [first run]; otherwise, <c> false </c>.
        /// </value>
        public static bool FirstRun
        {
            get
            {
                return VersionTracking.IsFirstLaunchEver;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [logging enabled].
        /// </summary>
        /// <value>
        /// <c> true </c> if [logging enabled]; otherwise, <c> false </c>.
        /// </value>
        public static bool LoggingEnabled
        {
            get
            {
                return GetBool("LoggingEnabled");
            }

            set
            {
                SetBool("LoggingEnabled", value);
            }
        }

        public static void SetReloadDatabase()
        {
            // Remove the old dateTime stamps so the files get reloaded even if they have been seen before
            Preferences.Remove(Common.CommonConstants.SettingsGPKGFileLastDateTimeModified);
            Preferences.Remove(Common.CommonConstants.SettingsGPRAMPSFileLastDateTimeModified);
            Preferences.Remove(Common.CommonConstants.SettingsXMLFileLastDateTimeModified);

            DataSerialised = false;
        }

        //public static string GeneralSettings
        //{
        //    get
        //    {
        //        return Preferences.Get(SettingsKey, SettingsDefault);
        //    }
        //    set
        //    {
        //        Preferences.Set(SettingsKey, value);
        //    }
        //}
        //private static ISettings AppSettings
        //{
        //    get
        //    {
        //        return CrossSettings.Current;
        //    }
        //}

        /// <summary>
        /// Gets the bool local setting data.
        /// </summary>
        /// <param name="setting">
        /// The setting.
        /// </param>
        /// <returns>
        /// true if the value is Y.
        /// </returns>
        private static bool GetBool(string setting)
        {
            string settingFlag = Preferences.Get(setting, string.Empty);

            if (string.IsNullOrEmpty(settingFlag))
            {
                SetBool(setting, false);
                return false;
            }

            if (settingFlag == "Y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        ///// <summary>
        ///// Gets the storage file.
        ///// </summary>
        ///// <param name="setting">
        ///// The setting.
        ///// </param>
        ///// <returns>
        ///// </returns>
        //private static async Task<string> GetStorageFile(string setting)
        //{
        //    string fileGrampsDataInput = null;

        // //if (Preferences.Get(setting, string.Empty) == string.Empty) //{ // try // { //
        // fileGrampsDataInput = await
        // StorageApplicationPermissions.MostRecentlyUsedList.GetFileAsync(t); // } // catch
        // (Exception) // { // fileGrampsDataInput = null; // } //}
        //    return fileGrampsDataInput;
        //}

        /// <summary>
        /// Sets the bool.
        /// </summary>
        /// <param name="setting">
        /// The setting.
        /// </param>
        /// <param name="value">
        /// if set to <c> true </c> [value].
        /// </param>
        private static void SetBool(string setting, bool value)
        {
            if (value)
            {
                Preferences.Set(setting, "Y");
            }
            else
            {
                Preferences.Set(setting, "N");
            }
        }

        /// <summary>
        /// Sets the int.
        /// </summary>
        /// <param name="setting">
        /// The setting.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        private static void SetInt(string setting, int value)
        {
            Preferences.Set(setting, value);
        }

        ///// <summary>
        ///// Sets the storage file by saving a list token that incorporates security permissions .
        ///// Clears the value if null supplied.
        ///// </summary>
        ///// <param name="setting">
        ///// The setting.
        ///// </param>
        ///// <param name="value">
        ///// The value.
        ///// </param>
        //private static void SetStorageFile(string setting, string value)
        //{
        //    if (value != null)
        //    {
        //        //string listToken = StorageApplicationPermissions.MostRecentlyUsedList.Add(value);
        //        Preferences.Set(setting, value);
        //    }
        //    else
        //    {
        //        //string listToken = ApplicationData.Current.LocalSettings.Values[setting] as string;
        //        //ApplicationData.Current.LocalSettings.Values[setting] = null;

        //        //StorageApplicationPermissions.MostRecentlyUsedList.Remove(listToken);
        //    }
        //}
    }
}