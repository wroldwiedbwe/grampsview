using GrampsView.Common;
using GrampsView.UWP.Data.External.StoreFile;

using Xamarin.Forms;

[assembly: Dependency(typeof(StoreFileDirectory))]

namespace GrampsView.UWP.Data.External.StoreFile
{
    using System;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;

    using GrampsView.Data.External.StoreFile;

    using Windows.Storage;
    using Windows.Storage.AccessCache;
    using Windows.Storage.Pickers;
    using Windows.Storage.Search;

    internal class StoreFileDirectory : IStoreFileDataFolderDirectory
    {
        private StorageFolder CurrentThumbNailFolder;
        private StorageFolder DataStorageFolder;

        public SafeHandle DataFolderDirectoryFileOpen(string relativeFilePath)
        {
            //SafeFileHandle handle = DataStorageFolder.CreateSafeFileHandle(FileAccess.Read);

            //System.IO.FileStream t = new System.IO.FileStream(handle, System.IO.FileAccess.Read);

            return null;
        }

        public string DataFolderDirectoryGet()
        {
            if (!(DataStorageFolder is null))
            {
                return DataStorageFolder.Path;
            }

            return string.Empty;
        }

        public async Task<bool> DataFolderDirectoryLoad()
        {
            if (ApplicationData.Current.LocalSettings.Values[CommonConstants.SettingsDataStorageFolder] is string t)
            {
                try
                {
                    DataStorageFolder = await StorageApplicationPermissions.MostRecentlyUsedList.GetFolderAsync(t);
                }
                catch (Exception ex)
                {
                    // TODO Handle this DataStore.CN.NotifyException("DataFolderSettingsReadFrom", ex);
                    DataStorageFolder = null;
                    return false;
                }
            }

            return true;
        }

        public async Task<bool> DataFolderDirectoryPick()
        {
            try
            {
                DataStorageFolder = await InputFolderPicker(CommonConstants.StorageGPKGFileExtension).ConfigureAwait(false);

                if (DataStorageFolder != null)
                {
                    // TODO StoreFileNames.DataFolderSetToNew(inputFolder);

                    // Get the folder. Recreate it if it has been accidently or otherwise deleted.
                    CurrentThumbNailFolder = await DataStorageFolder.CreateFolderAsync(CommonConstants.StorageThumbNailFolder, CreationCollisionOpt.OpenIfExists);
                }
            }

            // TODO fix this
            catch (Exception ex)
            {
                return false;
            }

            // TODO Handle this better
            return true;
        }

        public async Task<bool> DataFolderDirectorySave()
        {
            string listToken = StorageApplicationPermissions.MostRecentlyUsedList.Add(DataStorageFolder);

            ApplicationData.Current.LocalSettings.Values[CommonConstants.SettingsDataStorageFolder] = listToken;

            return true;
        }

        public void DataFolderDirectorySet(string dataStoreSetting)
        {
            // Not needed. Covered by Get
        }

        /// <summary>
        /// Datas the folder file exists.
        /// </summary>
        /// <param name="fileToFind">
        /// The file to find.
        /// </param>
        /// <returns>
        /// The filename if it exists or else string.empty.
        /// </returns>
        public async Task<string> DataFolderFileExists(string fileToFind)
        {
            StorageItemQueryResult queryResult = DataStorageFolder.CreateItemQuery();

            // TODO output how many files that match the query were found
            foreach (IStorageItem item in await queryResult.GetItemsAsync())
            {
                if (item.IsOfType(StorageItemTypes.File))
                {
                    // string t = item.Name.Substring(item.Name.IndexOf('.'));
                    if ((item as StorageFile).FileType == fileToFind)
                    {
                        return item.Name;
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Allow the user to Pick a File.
        /// </summary>
        /// <param name="fileType">
        /// null if cancelled for a Storage File.
        /// </param>
        /// <returns>
        /// Storage File.
        /// </returns>
        private static async Task<StorageFolder> InputFolderPicker(string fileType)
        {
            FolderPicker openPicker = new FolderPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.ComputerFolder,
            };
            openPicker.FileTypeFilter.Add(CommonConstants.StorageGPKGFileExtension);
            openPicker.FileTypeFilter.Add(".gramps");

            StorageFolder file = await openPicker.PickSingleFolderAsync();

            return file;
        }
    }
}