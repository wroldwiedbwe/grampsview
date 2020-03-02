//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="GrampsStorePostLoadClippedBM.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.ExternalStorageNS
{
    using System;
    using System.Threading.Tasks;

    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;
    using GrampsView.Data.StoreCache;

    using Xamarin.Forms;

    /// <summary>
    /// Creates a collection of entities with content read from a GRAMPS XML file.
    /// </summary>
    public partial class GrampsStorePostLoad : CommonBindableBase, IStorePostLoad
    {
        /// <summary>
        /// Gets the clipped bit maps.
        /// </summary>
        /// <returns>
        /// Nothing.
        /// </returns>
        public async Task GetClippedBitMaps()
        {
            localCL.LogRoutineEntry("GetClippedBitMaps");

            await DataStore.CN.ChangeLoadingMessage("Getting Clipped Image Bitmaps").ConfigureAwait(false);

            // TODO do the rest of the clipped bitmaps
            await GetClippedCitationBitMaps().ConfigureAwait(false);

            await GetClippedPersonBitMaps().ConfigureAwait(false);

            await GetClippedSourceBitMaps().ConfigureAwait(false);

            await GetClippedEventBitMaps().ConfigureAwait(false);

            localCL.LogRoutineExit("GetClippedBitMaps");
        }

        /// <summary>
        /// Gets the clipped citation bit maps.
        /// </summary>
        /// <returns>
        /// Nothing.
        /// </returns>
        public async Task GetClippedCitationBitMaps()
        {
            // Create any required cropped media thumbnails
            foreach (CitationModel thisCitationModel in DV.CitationDV.CitationData)
            {
                if (thisCitationModel.Id == "C0144")
                {
                }

                try
                {
                    // Clip Home Image
                    if (thisCitationModel.HomeImageHLink.HomeImageType == CommonConstants.HomeImageTypeClippedBitmap)
                    {
                        MediaModel t = DV.MediaDV.MediaData[thisCitationModel.HomeImageHLink.HLinkKey];

                        thisCitationModel.HomeImageHLink.HomeImageClippedBitmap = await GetHomeImage(t, thisCitationModel.HomeImageHLink).ConfigureAwait(false);
                    }

                    // Clip MediaReferences
                    foreach (HLinkMediaModel item in thisCitationModel.GMediaRefCollection)
                    {
                        if (item.HomeImageType == CommonConstants.HomeImageTypeClippedBitmap)
                        {
                            MediaModel t = DV.MediaDV.MediaData[item.HLinkKey];

                            item.HomeImageClippedBitmap = await GetHomeImage(t, item).ConfigureAwait(false);
                        }
                    }
                }
                catch (Exception ex)
                {
                    DataStore.CN.NotifyException("Exception creating  Clipped Thumbnails for Citations.", ex);
                }
            }
        }

        /// <summary>
        /// Gets the clipped event bit maps.
        /// </summary>
        /// <returns>
        /// </returns>
        public async Task GetClippedEventBitMaps()
        {
            // Create any required cropped media thumbnails
            foreach (EventModel thisEventModel in DV.EventDV.EventData)
            {
                if (thisEventModel.Id == "E1196")
                {
                }

                try
                {
                    // Clip Home Image
                    if (thisEventModel.HomeImageHLink.HomeImageType == CommonConstants.HomeImageTypeClippedBitmap)
                    {
                        MediaModel t = DV.MediaDV.MediaData[thisEventModel.HomeImageHLink.HLinkKey];

                        thisEventModel.HomeImageHLink.HomeImageClippedBitmap = await GetHomeImage(t, thisEventModel.HomeImageHLink).ConfigureAwait(false);
                    }

                    // Clip MediaReferences
                    foreach (HLinkMediaModel item in thisEventModel.GMediaRefCollection)
                    {
                        if (item.HomeImageType == CommonConstants.HomeImageTypeClippedBitmap)
                        {
                            MediaModel t = DV.MediaDV.MediaData[item.HLinkKey];

                            item.HomeImageClippedBitmap = await GetHomeImage(t, item).ConfigureAwait(false);
                        }
                    }
                }
                catch (Exception ex)
                {
                    DataStore.CN.NotifyException("Exception creating  Clipped Thumbnails for Events.", ex);
                }
            }
        }

        /// <summary>
        /// Gets the clipped person bit maps.
        /// </summary>
        /// <returns>
        /// Nothing.
        /// </returns>
        public async Task GetClippedPersonBitMaps()
        {
            // Get MediaRef thumbnails (With cropping)
            foreach (PersonModel thisPersonModel in DV.PersonDV.PersonData)
            {
                if (thisPersonModel.Id == "I0004")
                {
                }

                try
                {
                    if (thisPersonModel.HomeImageHLink.HomeImageType == CommonConstants.HomeImageTypeClippedBitmap)
                    {
                        // Clip Home Image
                        MediaModel t1 = DV.MediaDV.MediaData[thisPersonModel.HomeImageHLink.HLinkKey];

                        thisPersonModel.HomeImageHLink.HomeImageClippedBitmap = await GetHomeImage(t1, thisPersonModel.HomeImageHLink).ConfigureAwait(false);
                    }

                    // Clip MediaReferences
                    foreach (HLinkMediaModel item in thisPersonModel.GMediaRefCollection)
                    {
                        if (item.HomeImageType == CommonConstants.HomeImageTypeClippedBitmap)
                        {
                            MediaModel t2 = DV.MediaDV.MediaData[item.HLinkKey];

                            item.HomeImageClippedBitmap = await GetHomeImage(t2, item).ConfigureAwait(false);
                        }
                    }
                }
                catch (Exception ex)
                {
                    DataStore.CN.NotifyException("Create Clipped Thumbnails for Media File Issue.", ex);
                }
            }
        }

        /// <summary>
        /// Gets the clipped source bit maps.
        /// </summary>
        /// <returns>
        /// </returns>
        public async Task GetClippedSourceBitMaps()
        {
            // Create any required cropped media thumbnails
            foreach (SourceModel thisSourceModel in DV.SourceDV.SourceData)
            {
                try
                {
                    // Clip Home Image
                    if (thisSourceModel.HomeImageHLink.HomeImageType == CommonConstants.HomeImageTypeClippedBitmap)
                    {
                        MediaModel t = DV.MediaDV.MediaData[thisSourceModel.HomeImageHLink.HLinkKey];

                        thisSourceModel.HomeImageHLink.HomeImageClippedBitmap = await GetHomeImage(t, thisSourceModel.HomeImageHLink).ConfigureAwait(false);
                    }

                    // Clip MediaReferences
                    foreach (HLinkMediaModel item in thisSourceModel.GMediaRefCollection)
                    {
                        if (item.HomeImageType == CommonConstants.HomeImageTypeClippedBitmap)
                        {
                            MediaModel t = DV.MediaDV.MediaData[item.HLinkKey];

                            item.HomeImageClippedBitmap = await GetHomeImage(t, item).ConfigureAwait(false);
                        }
                    }
                }
                catch (Exception ex)
                {
                    DataStore.CN.NotifyException("Exception creating  Clipped Thumbnails for Sources.", ex);
                }
            }
        }

        /// <summary>
        /// Sets the home image.
        /// </summary>
        /// <param name="argMM">
        /// The argument mm.
        /// </param>
        /// <param name="argHLMM">
        /// The argument HLMM.
        /// </param>
        /// <returns>
        /// </returns>
        public async Task<Image> GetHomeImage(MediaModel argMM, HLinkMediaModel argHLMM)
        {
            if (argMM.Id == "O0220")
            {
            }

            Image returnBitMap;

            if (argHLMM.HomeUseImage && argMM.IsMediaFile)
            {
                returnBitMap = await StoreCache.GetCacheItem(argHLMM).ConfigureAwait(false);
            }
            else
            {
                // TODO Fix this
                //returnBitMap = argMM.ImageThumbNail;
                returnBitMap = null;
            }

            return returnBitMap;
        }

        ///// <summary>
        ///// Sets the clipped bit map asynchronous.
        ///// </summary>
        ///// <param name="argMM">
        ///// The argument mm.
        ///// </param>
        ///// <param name="argHLMM">
        ///// The argument HLMM.
        ///// </param>
        ///// <returns>
        ///// </returns>
        // public async Task<Image> SetClippedBitMapAsync(MediaModel argMM, HLinkMediaModel
        // argHLMM) { if (argMM.Id == "O0220") { }

        // if (argMM.IsMediaStorageFileValid) { Image returnBmp = await
        // DispatcherHelper.ExecuteOnUIThreadAsync(async () => { // Calculate new scaled x,y,width
        // and height base dont he size of the actual // image and not the scaled version using
        // (IRandomAccessStream stream = await argMM.MediaStorageFile.OpenReadAsync()) { // Create a
        // decoder from the stream. With the decoder, we can get the // properties of the image.
        // BitmapDecoder decoder = await CommonRoutines.GetProperDecoder(stream, argMM.MediaStorageFile.ContentType);

        // if (decoder.DecoderInformation.FriendlyName != "JPEG Decoder") { }

        // // Convert start point and size to integer. uint startPointX =
        // (uint)Math.Floor((decoder.PixelWidth / 100.0) * argHLMM.GCorner1X); uint startPointY =
        // (uint)Math.Floor((decoder.PixelHeight / 100.0) * argHLMM.GCorner1Y); uint height =
        // (uint)Math.Floor((decoder.PixelHeight / 100.0) * (argHLMM.GCorner2Y - argHLMM.GCorner1Y));
        // uint width = (uint)Math.Floor((decoder.PixelWidth / 100.0) * (argHLMM.GCorner2X - argHLMM.GCorner1X));

        // // Get the cropped pixels. Get the cropped pixels. BitmapTransform transform = new
        // BitmapTransform(); BitmapBounds bounds = new BitmapBounds { X = startPointX, Y =
        // startPointY, Height = height, Width = width, }; transform.Bounds = bounds;

        // transform.ScaledWidth = decoder.PixelWidth; transform.ScaledHeight = decoder.PixelHeight;

        // // Get the cropped pixels within the bounds of transform. PixelDataProvider pix = await
        // decoder.GetPixelDataAsync( BitmapPixelFormat.Bgra8, BitmapAlphaMode.Straight, transform,
        // ExifOrientationMode.IgnoreExifOrientation, ColorManagementMode.ColorManageToSRgb); byte[]
        // pixels = pix.DetachPixelData();

        // // Stream the bytes into a Image Image cropBmp = new Image((int)width, (int)height);
        // Stream pixStream = cropBmp.PixelBuffer.AsStream(); pixStream.Write(pixels, 0, (int)(width
        // * height * 4));

        // if (cropBmp.PixelHeight == 0) { }

        // return cropBmp; } }).ConfigureAwait(true);

        // return returnBmp; }

        // // Not a valid file return null; }
    }
}