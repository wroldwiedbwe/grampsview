// <copyright file="GrampsStorePostLoad.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Data.ExternalStorageNS
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using SkiaSharp;

    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// Creates a collection of entities with content read from a GRAMPS XML file.
    /// </summary>
    public partial class StorePostLoad : CommonBindableBase, IStorePostLoad
    {
        public static async Task<string> CreateClippedMediaModel(MediaModel argBaseMediaModel, HLinkHomeImageModel argStartHLink)
        {
            if (argBaseMediaModel is null)
            {
                throw new ArgumentNullException(nameof(argBaseMediaModel));
            }

            if (argStartHLink is null)
            {
                throw new ArgumentNullException(nameof(argStartHLink));
            }

            SKBitmap resourceBitmap = new SKBitmap();

            string newHLinkKey = argStartHLink.HLinkKey + "-" + argStartHLink.GCorner1X + argStartHLink.GCorner1Y + argStartHLink.GCorner2X + argStartHLink.GCorner2Y;
            string outFileName = Path.Combine("Cropped", newHLinkKey + ".png");

            string outFilePath = Path.Combine(DataStore.DS.CurrentDataFolder.FullName, outFileName);

            Debug.WriteLine(argBaseMediaModel.MediaStorageFilePath);

            // Check if already exists
            MediaModel fileExists = DV.MediaDV.GetModelFromHLinkString(newHLinkKey);

            if (!fileExists.Valid)
            {
                // Needs clipping
                using (StreamReader stream = new StreamReader(argBaseMediaModel.MediaStorageFilePath))
                {
                    resourceBitmap = SKBitmap.Decode(stream.BaseStream);
                }

                // Check for too large a bitmap
                Debug.WriteLine("Image ResourceBitmap size: " + resourceBitmap.ByteCount);
                if (resourceBitmap.ByteCount > int.MaxValue - 1000)
                {
                    // TODO Handle this better. Perhaps resize? Delete for now
                    resourceBitmap = new SKBitmap();
                }

                float crleft = (float)(argStartHLink.GCorner1X / 100d * argBaseMediaModel.MetaDataWidth);
                float crright = (float)(argStartHLink.GCorner2X / 100d * argBaseMediaModel.MetaDataWidth);
                float crtop = (float)(argStartHLink.GCorner1Y / 100d * argBaseMediaModel.MetaDataHeight);
                float crbottom = (float)(argStartHLink.GCorner2Y / 100d * argBaseMediaModel.MetaDataHeight);

                SKRect cropRect = new SKRect(crleft, crtop, crright, crbottom);

                SKBitmap croppedBitmap = new SKBitmap(
                                                    (int)cropRect.Width,
                                                    (int)cropRect.Height
                                                    );

                SKRect dest = new SKRect(
                                        0,
                                        0,
                                        cropRect.Width,
                                        cropRect.Height
                                        );

                SKRect source = new SKRect(
                                        cropRect.Left,
                                        cropRect.Top,
                                        cropRect.Right,
                                        cropRect.Bottom);

                using (SKCanvas canvas = new SKCanvas(croppedBitmap))
                {
                    canvas.DrawBitmap(resourceBitmap, source, dest);
                }

                // create an image COPY
                SKImage image = SKImage.FromBitmap(croppedBitmap);

                // encode the image (defaults to PNG)
                SKData encoded = image.Encode();

                // get a stream over the encoded data

                using (Stream stream = File.Open(outFilePath, FileMode.OpenOrCreate, System.IO.FileAccess.Write, FileShare.ReadWrite))
                {
                    encoded.SaveTo(stream);
                }

                croppedBitmap.Dispose();

                // ------------ Save new MediaObject
                MediaModel t = argBaseMediaModel.Clone();

                t.HLinkKey = newHLinkKey;
                t.OriginalFilePath = outFileName;
                //newMediaObject.HomeImageHLink.HLinkKey = newHLinkKey;
                t.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeThumbNail;

                DataStore.DS.MediaData.Add(t);
                await fixMediaFile(t).ConfigureAwait(false);
            }

            resourceBitmap.Dispose();

            return newHLinkKey;
        }

        public async static Task<bool> fixMediaFile(MediaModel argMediaModel)
        {
            try
            {
                if ((argMediaModel.HomeImageHLink.HomeUseImage == true) && argMediaModel.IsOriginalFilePathValid)
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

        /// <summary>
        /// Gets the tag reference home link.
        /// </summary>
        /// <param name="argHLink">
        /// The argument h link.
        /// </param>
        /// <returns>
        /// </returns>
        public static HLinkMediaModel GetTagRefHomeLink(TagModel argModel, HLinkMediaModel argHLink)
        {
            if (argModel is null)
            {
                throw new ArgumentNullException(nameof(argModel));
            }

            if (argHLink is null)
            {
                throw new ArgumentNullException(nameof(argHLink));
            }

            HLinkMediaModel returnHLink = argHLink;

            returnHLink.LoadingClipInfo.HomeImageType = CommonConstants.HomeImageTypeSymbol;

            // Set the colour of the tag ref to match the tag
            returnHLink.LoadingClipInfo.HomeSymbolColour = argModel.GColor;

            return returnHLink;
        }

        /// <summary>
        /// Sets the home h link.
        /// </summary>
        /// <param name="HomeImageHLink">
        /// The home image h link.
        /// </param>
        /// <param name="argHLink">
        /// The hlink.
        /// </param>
        /// <returns>
        /// </returns>
        public static async Task<HLinkHomeImageModel> SetHomeHLink(HLinkHomeImageModel argStartHLink, HLinkHomeImageModel argHLink)
        {
            //if (argHLink.HLinkKey == "_c492389ce47626fa5e7")
            //{
            //}

            //Debug.WriteLine("SetHomeHLink HlinkKey " + argHLink.HLinkKey);

            MediaModel theMediaModel;

            // --------- Validate
            if (argStartHLink is null)
            {
                throw new ArgumentNullException(nameof(argStartHLink));
            }

            if (argHLink is null)
            {
                throw new ArgumentNullException(nameof(argHLink));
            }

            // --------- Copy link
            argStartHLink.HLinkKey = argHLink.HLinkKey;
            argStartHLink.HomeImageType = argHLink.HomeImageType;
            argStartHLink.GCorner1X = argHLink.GCorner1X;
            argStartHLink.GCorner1Y = argHLink.GCorner1Y;
            argStartHLink.GCorner2X = argHLink.GCorner2X;
            argStartHLink.GCorner2Y = argHLink.GCorner2Y;

            // --------- Check if media or symbol
            if (argStartHLink.HomeImageType == CommonConstants.HomeImageTypeSymbol)
            {
                return argStartHLink;
            }

            // --------- Check if MediaObject already exists
            theMediaModel = argStartHLink.DeRef;

            if (!theMediaModel.Valid)
            {
                DataStore.CN.NotifyError("Invalid argStartHLink DeRef (" + argStartHLink.HLinkKey + ") passed to SetHomeHLink");
                return argStartHLink;
            }

            //if (theMediaModel.Id == "O0003")
            //{
            //}

            if (string.IsNullOrEmpty(theMediaModel.MediaStorageFilePath))
            {
                DataStore.CN.NotifyError("The media file path is null for Id:" + theMediaModel.Id);
                return argStartHLink;
            }

            // --------- Save Cropped Image
            if (argStartHLink.NeedsClipping)
            {
                string newHLinkKey = await CreateClippedMediaModel(theMediaModel, argStartHLink).ConfigureAwait(false);

                // ------------ Change HomeImageLink to point to new image
                argStartHLink.HomeImageType = CommonConstants.HomeImageTypeThumbNail;
                argStartHLink.HLinkKey = newHLinkKey;
                argStartHLink.GCorner1X = 0;
                argStartHLink.GCorner1Y = 0;
                argStartHLink.GCorner2X = 0;
                argStartHLink.GCorner2Y = 0;
            }

            return argStartHLink;
        }

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

                    await fixMediaFile(item).ConfigureAwait(false);
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