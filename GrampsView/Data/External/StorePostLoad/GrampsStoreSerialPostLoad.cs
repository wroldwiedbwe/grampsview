// <copyright file="GrampsStoreSerialPostLoad.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Data.ExternalStorageNS
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// Creates a collection of entities with content read from a GRAMPS XML file.
    /// </summary>
    public partial class GrampsStorePostLoad
    {
        /// <summary>
        /// Loads the UI items.
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public async Task LoadSerialUiItems()
        {
            _CL.LogRoutineEntry("LoadSerialUiItems");

            await DataStore.CN.ChangeLoadingMessage("Organising data after load").ConfigureAwait(false);
            {
                _CL.LogRoutineExit(string.Empty);

                await FixMediaFiles().ConfigureAwait(false);
            }

            await DataStore.CN.MajorStatusDelete().ConfigureAwait(false);

            await DataStore.CN.MinorStatusAdd("Serial UI Load Complete. Data ready for display").ConfigureAwait(false);

            _CL.LogRoutineExit(nameof(LoadSerialUiItems));
        }

        /// <summary>
        /// Fixes the media files.
        /// </summary>
        /// <returns>
        /// true.
        /// </returns>
        private async Task<bool> FixMediaFiles()
        {
            _CL.LogRoutineEntry("FixMediaFiles");

            DirectoryInfo localMediaFolder = DataStore.DS.CurrentDataFolder;

            if (localMediaFolder != null)
            {
                await DataStore.CN.MinorStatusAdd("Loading media file pointers").ConfigureAwait(false);

                foreach (MediaModel item in DV.MediaDV.MediaData)
                { // if (item.Id == "O0259") { }
                    await DataStore.CN.MinorStatusAdd("Loading media file pointer: " + item.OriginalFilePath).ConfigureAwait(false);

                    try
                    {
                        if ((item.HomeImageHLink.HomeUseImage == true) && item.IsOriginalFilePathValid)
                        {
                            _CL.LogVariable("tt.OriginalFilePath", item.OriginalFilePath); //
                            _CL.LogVariable("localMediaFolder.path", localMediaFolder.FullName); //
                            _CL.LogVariable("path", localMediaFolder.FullName + "\\" + item.OriginalFilePath);

                            DV.MediaDV.MediaData[item.HLinkKey].MediaStorageFile = await StoreFolder.FolderGetFileAsync(localMediaFolder, item.OriginalFilePath).ConfigureAwait(false);
                        }
                    }
                    catch (FileNotFoundException ex)
                    {
                        DataStore.CN.NotifyError("File (" + item.OriginalFilePath + ") not found while   loading media. Has the GRAMPS database been verified ? " + ex.ToString());

                        DataStore.CN.NotifyException("Trying to  add media file pointer", ex);
                    }
                    catch (Exception ex)
                    {
                        CommonLocalSettings.DataSerialised = false;
                        DataStore.CN.NotifyException("Trying to add media file pointer", ex);

                        await DataStore.CN.MajorStatusDelete().ConfigureAwait(false);
                        throw;
                    }
                }
            }

            _CL.LogRoutineExit(string.Empty);

            return true;
        }
    }
}