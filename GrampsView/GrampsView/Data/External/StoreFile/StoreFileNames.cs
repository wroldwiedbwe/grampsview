// <copyright file="StoreFileNames.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Data
{
    using System;
    using System.Threading.Tasks;

    using GrampsView.Common;
    using GrampsView.Data.Repository;

    using Xamarin.Essentials;

    /// <summary>
    /// Common file handling routines.
    /// </summary>
    public static class StoreFileNames
    {
        ///// <summary>
        ///// Sets the Data Folder in settings for Local Storage and creates if required.
        ///// </summary>
        ///// <returns>
        ///// Nothign Task.
        ///// </returns>
        //public static async Task DataFolderSetLocalStorageAsync()
        //{
        //    //await ApplicationData.Current.LocalFolder.CreateFolderAsync(CommonConstants.StorageInternalFolder, CreationCollisionOption.OpenIfExists);

        // //DataStore.DS.CurrentDataFolder = await
        // ApplicationData.Current.LocalFolder.CreateFolderAsync(CommonConstants.StorageInternalFolder, CreationCollisionOption.OpenIfExists);

        //    // DataFolderSettingsSaveTo();
        //}

        ///// <summary>
        ///// </summary>
        ///// <returns>
        ///// A <see cref="Task{TResult}" /> representing the result of the asynchronous operation.
        ///// </returns>
        //public static async Task<bool> DataFolderSetToExistingAsync()
        //{
        //    try
        //    {
        //        // Define data folder TODO DataStore.DS.CurrentInputFolder.AuthLoad(CommonConstants.SettingsDataStorageFolder);

        // //DataStore.DS.CurrentThumbNailFolder.AuthLoad(CommonConstants.StorageThumbNailFolder);

        // //DataStore.DS.CurrentDataFolder = await DataFolderSettingsReadFrom().ConfigureAwait(false);

        // //DataStore.DS.CurrentThumbNailFolder = await
        // DataStore.DS.CurrentDataFolder.CreateFolderAsync(CommonConstants.StorageThumbNailFolder, CreationCollisionOption.OpenIfExists);

        // // DataFolderSettingsSaveTo(); } catch (Exception ex) { CommonLocalSettings.DataSerialised
        // = false;

        // if (!(DataStore.DS.CurrentInputFolder is null)) { // TODO
        // DataStore.DS.CurrentInputFolder.AuthClear(CommonConstants.SettingsDataStorageFolder); }

        // //if (!(DataStore.DS.CurrentThumbNailFolder is null)) //{ //
        // DataStore.DS.CurrentThumbNailFolder.AuthClear(CommonConstants.StorageThumbNailFolder); //}

        // DataStore.CN.NotifyException("Trying DataFolderSetToExisting", ex);

        // return false; }

        //    return true;
        //}

        ///// <summary>
        ///// Datas the folder set to new.
        ///// </summary>
        ///// <param name="t">
        ///// The t.
        ///// </param>
        //public static void DataFolderSetToNew(string t)
        //{
        //    // Define data folder
        //    //DataStore.DS.CurrentDataFolder = t;

        //    DataFolderSettingsSaveTo();
        //}

        ///// <summary>
        ///// Files the get first GPKG.
        ///// </summary>
        ///// <returns>
        ///// </returns>
        //public static async Task<FileInfoEx> FileGetFirstGPKG()
        //{
        //    FileInfo[] tt = DataStore.DS.CurrentInputFolder.GetFiles();

        // foreach (FileInfo item in tt) { if (Path.GetExtension(item.Extension) == ".gpkg") { return
        // item; } }

        //    return null;
        //}

        ///// <summary>
        ///// Files the get latest GPKG.
        ///// </summary>
        ///// <returns>
        ///// </returns>
        //public static async Task<string> FileGetLatestGPKG()
        //{
        //    string latestGPKG = null;

        // //var options = new QueryOptions(); //options.FileTypeFilter.Add(".gpkg");
        // //options.FolderDepth = FolderDepth.Shallow;

        // //StorageFileQueryResult query = DataStore.DS.CurrentDataFolder.CreateFileQueryWithOptions(options);

        // //// TODO output how many files that match the query were found

        //    // TODO finish this
        //    return latestGPKG;
        //}

        /// <summary>
        /// Was the file modified since the last datetime saved?
        /// </summary>
        /// <param name="settingsKey">
        /// The settings key.
        /// </param>
        /// <param name="filenameToCheck">
        /// The filename to check.
        /// </param>
        /// <returns>
        /// True if the file was modified since last time.
        /// </returns>
        public static bool FileModifiedSinceLastSaveAsync(string settingsKey, FileInfoEx fileToCheck)
        {
            if ( fileToCheck is null)
            {
                throw new ArgumentNullException(nameof(fileToCheck));
            }

            // Check for file exists
            if (!fileToCheck.Valid)
            {
                return false;
            }

            try
            {
                DateTime fileDateTime = FileGetDateTimeModified(fileToCheck);

                // Need to reparse it so the ticks ar ethe same
                fileDateTime = DateTime.Parse(fileDateTime.ToString());

                // Save a fresh copy if null so we can load next time
                string oldDateTime = Preferences.Get(settingsKey, string.Empty);

                if (string.IsNullOrEmpty(oldDateTime))
                {
                    Preferences.Set(settingsKey, fileDateTime.ToString());

                    // No previous settings entry so do the load (it might be the FirstRun)
                    return true;
                }
                else
                {
                    DateTime settingsStoredDateTime;
                    settingsStoredDateTime = DateTime.Parse(oldDateTime);

             
                    int t = fileDateTime.CompareTo(settingsStoredDateTime);
                    if (t > 0)
                    {
                        return true;
                    }

                

                    return false;
                }
            }
            catch (Exception ex)
            {
                Preferences.Remove(settingsKey);

                DataStore.CN.NotifyException("FileModifiedSinceLastSaveAsync", ex);
                throw;
            }
        }

        /// <summary>
        /// Saves the datetime the file was last modified in System Settings.
        /// </summary>
        /// <param name="settingsKey">
        /// The settings key.
        /// </param>
        /// <param name="filenameToCheck">
        /// The filename to check.
        /// </param>
        public static void SaveFileModifiedSinceLastSave(string settingsKey, FileInfoEx filename)
        {
            Preferences.Set(settingsKey, filename.FInfo.LastWriteTimeUtc.ToString());
        }

        /// <summary>
        /// Indexes the file get date time modified.
        /// </summary>
        /// <returns>
        /// </returns>
        private static  DateTime FileGetDateTimeModified(FileInfoEx fileToCheck)
        {
            try
            {
                if (fileToCheck.Valid)
                {
                    return fileToCheck.FInfo.LastWriteTimeUtc;
                }

                return new DateTime();
            }
            catch (Exception ex)
            {
                DataStore.CN.NotifyException("Exception while checking FileGetDateTimeModified for =" + fileToCheck.FInfo.FullName, ex);

                throw;
            }
        }
    }
}