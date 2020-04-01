// <copyright file="GrampsStorePostLoad.cs" company="PlaceholderCompany">
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
    public partial class StorePostLoad : CommonBindableBase, IStorePostLoad
    {
        public async static Task<bool> FixSingleMediaFile(MediaModel argMediaModel)
        {
            try
            {
                if ((argMediaModel.HomeImageHLink.LinkToImage == true) && argMediaModel.IsOriginalFilePathValid)
                {
                    //_CL.LogVariable("tt.OriginalFilePath", argMediaModel.OriginalFilePath); //
                    //_CL.LogVariable("localMediaFolder.path", DataStore.DS.CurrentDataFolder.FullName); //
                    //_CL.LogVariable("path", DataStore.DS.CurrentDataFolder.FullName + "\\" + argMediaModel.OriginalFilePath);

                    DataStore.DS.MediaData[argMediaModel.HLinkKey].MediaStorageFile = await StoreFolder.FolderGetFileAsync(DataStore.DS.CurrentDataFolder, argMediaModel.OriginalFilePath).ConfigureAwait(false);
                }
            }
            catch (FileNotFoundException ex)
            {
                DataStore.CN.NotifyError("File (" + argMediaModel.OriginalFilePath + ") not found while   loading media. Has the GRAMPS database been verified ? " + ex.ToString());

                DataStore.CN.NotifyException("Trying to  add media file pointer", ex);
            }
            catch (Exception ex)
            {
                CommonLocalSettings.DataSerialised = false;
                DataStore.CN.NotifyException("Trying to add media file pointer", ex);

                await DataStore.CN.MajorStatusDelete().ConfigureAwait(false);
                throw;
            }

            return false;
        }

        ///// <summary>
        ///// Gets the tag reference home link.
        ///// </summary>
        ///// <param name="argHLink">
        ///// The argument h link.
        ///// </param>
        ///// <returns>
        ///// </returns>
        //public static HLinkMediaModel GetTagRefHomeLink(TagModel argModel, HLinkMediaModel argHLink)
        //{
        //    if (argModel is null)
        //    {
        //        throw new ArgumentNullException(nameof(argModel));
        //    }

        // if (argHLink is null) { throw new ArgumentNullException(nameof(argHLink)); }

        // HLinkMediaModel returnHLink = argHLink;

        // returnHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;

        // // Set the colour of the tag ref to match the tag
        // returnHLink.LoadingClipInfo.HomeSymbolColour = argModel.GColor;

        //    return returnHLink;
        //}

        /// <summary>
        /// Fixes the media files.
        /// </summary>
        /// <returns>
        /// true.
        /// </returns>
        public async Task<bool> FixMediaFiles()
        {
            _CommonLogging.LogRoutineEntry("FixMediaFiles");

            DirectoryInfo localMediaFolder = DataStore.DS.CurrentDataFolder;

            if (localMediaFolder != null)
            {
                await DataStore.CN.MinorStatusAdd("Loading media file pointers").ConfigureAwait(false);

                foreach (MediaModel item in DV.MediaDV.DataViewData)
                {
                    await DataStore.CN.MinorStatusAdd("Loading media file pointer: " + item.OriginalFilePath).ConfigureAwait(false);

                    //if (item.Id == "O0004")
                    //{
                    //}

                    await FixSingleMediaFile(item).ConfigureAwait(false);
                }
            }

            //foreach (MediaModel item in DV.MediaDV.DataViewData)
            //{
            //    if (item.IsMediaFile && !item.IsMediaStorageFileValid)
            //    {
            //    }
            //}

            _CommonLogging.LogRoutineExit(string.Empty);

            return true;
        }
    }
}