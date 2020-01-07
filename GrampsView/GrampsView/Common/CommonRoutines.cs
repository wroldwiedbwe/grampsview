//-----------------------------------------------------------------------
//
// Common routines for the CommonRoutines
//
// <copyright file="CommonRoutines.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Common
{
    using GrampsView.Data.Model;
    using System;
    using System.Text.RegularExpressions;
    using Xamarin.Forms;

    /// <summary>
    /// Various common routines.
    /// </summary>
    public static class CommonRoutines
    {
        ///// <summary>
        ///// hes the link valid.
        ///// </summary>
        ///// <param name="arg">
        ///// The argument.
        ///// </param>
        ///// <returns>
        ///// </returns>
        //public static bool HLinkValid(IHLinkBase arg)
        //{
        //    if (arg == null)
        //    {
        //        return false;
        //    }

        //    if (arg.Valid == true)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        public static Color ResourceColourGet(string keyName)
        {
            var t = ResourceValueGet(keyName);

            if (t is null)
            {
                return Color.White;
            }

            return (Color)t;
        }

        public static object ResourceValueGet(string keyName)
        {
            // Search all dictionaries
            if (Application.Current.Resources.TryGetValue(keyName, out var retVal)) { }
            return retVal;
        }

        public static string ReplaceLineSeperators(string argString)
        {
            return Regex.Replace(argString, @"[\u000A\u000B\u000C\u000D\u2028\u2029\u0085]+", "");
        }

        /// <summary>
        /// The global resources.
        /// </summary>
        //public static ResourceLoader GlobalResources = ResourceLoader.GetForViewIndependentUse();

        ///// <summary>
        ///// Bytes the array to bitmap image.
        ///// </summary>
        ///// <param name="byteArray">
        ///// The byte array.
        ///// </param>
        ///// <returns>
        ///// </returns>
        //public static async Task<BitmapImage> ByteArrayToBitmapImage(byte byteArray)
        //{
        //    RenderTargetBitmap rendered = new RenderTargetBitmap();

        // //InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream(); //var buffer =
        // await rendered.GetPixelsAsync();

        //    //BitmapImage img = new BitmapImage();
        //    //var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream);
        //    //encoder.SetPixelData(
        //    //    BitmapPixelFormat.Bgra8,
        //    //    BitmapAlphaMode.Straight,
        //    //    (uint)rendered.PixelWidth,
        //    //    (uint)rendered.PixelHeight,
        //    //    DisplayInformation.GetForCurrentView().LogicalDpi,
        //    //    DisplayInformation.GetForCurrentView().LogicalDpi,
        //    //    buffer.ToArray());
        //    //await encoder.FlushAsync();
        //    //await img.SetSourceAsync(stream);
        //    //return img;
        //    return new BitmapImage();
        //}

        ///// <summary>
        ///// return a solid color brush from a color name.
        ///// </summary>
        ///// <param name="name">
        ///// Windows XAMl color Name.
        ///// </param>
        ///// <returns>
        ///// A Solid Color Brush.
        ///// </returns>
        //public static SolidColorBrush ColorStringToBrush(string name)
        //{
        //    var property = typeof(Colors).GetRuntimeProperty(name);
        //    if (property != null)
        //    {
        //        //return new SolidColorBrush((Color)property.GetValue(null));
        //        return new SolidColorBrush();
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        ///// <summary>
        ///// returns the correct BitMapDecode type based on the file type these are the only ones
        ///// allowed for notification tiles.
        ///// </summary>
        ///// <param name="stream">
        ///// input stream.
        ///// </param>
        ///// <param name="contentType">
        ///// Content type.
        ///// </param>
        ///// <returns>
        ///// Bitmap Decoder.
        ///// </returns>
        //public static async Task GetProperDecoder(IRandomAccessStream stream, string contentType)
        //{
        //    if (contentType != "image/jpeg")
        //    {
        //    }

        // // switch (contentType) { case "image/bmp": return await //
        // BitmapDecoder.CreateAsync(BitmapDecoder.BmpDecoderId, stream);

        // // case "image/gif": return await BitmapDecoder.CreateAsync(BitmapDecoder.GifDecoderId, stream);

        // // case "image/jpg": case "image/jpeg": return await //
        // BitmapDecoder.CreateAsync(BitmapDecoder.JpegDecoderId, stream);

        // // case "image/png": return await BitmapDecoder.CreateAsync(BitmapDecoder.PngDecoderId, stream);

        // // case "image/tif": case "image/tiff": return await //
        // BitmapDecoder.CreateAsync(BitmapDecoder.TiffDecoderId, stream); }

        //    // Return the default decoder
        //    //return await BitmapDecoder.CreateAsync(stream);
        //}
        //public static async Task NavigateMainPage(INavigationService navService, string page)
        //{
        //    navService.GetEvent<PageNavigateEvent>()..Send(PageNavigate, ThreadOption.UIThread);

        //    //  await navService.NavigateAsync("MainPage/NavigationPage/" + page.Trim());
        //}
    }
}