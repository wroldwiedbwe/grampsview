// <copyright file="MediaImage.xaml.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

namespace GrampsView.UserControls
{
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using SkiaSharp;
    using SkiaSharp.Views.Forms;

    using System;
    using System.Diagnostics;
    using System.IO;

    using Xamarin.Forms;

    public partial class MediaImageSkia : Frame, IDisposable
    {
        public static readonly BindableProperty UConHideSymbolProperty
               = BindableProperty.Create(returnType: typeof(bool), declaringType: typeof(MediaImage), propertyName: nameof(UConHideSymbol), defaultValue: false, propertyChanged: MediaImage_UConPropertyChanged);

        private bool disposedValue = false;

        private SKBitmap resourceBitmap = new SKBitmap();

        private MediaModel theMediaModel = new MediaModel();

        public MediaImageSkia()
        {
            InitializeComponent();
        }

        public bool UConHideSymbol
        {
            get { return (bool)GetValue(UConHideSymbolProperty); }
            set { SetValue(UConHideSymbolProperty, value); }
        }

        private HLinkHomeImageModel HLinkMedia { get; set; }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above. GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    resourceBitmap.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        private static void MediaImage_UConPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
        }

        private void DaImage_Error(object sender, FFImageLoading.Forms.CachedImageEvents.ErrorEventArgs e)
        {
            DataStore.CN.NotifyError("Error in MediaImage.  Error is " + e.Exception.Message);

            (sender as FFImageLoading.Forms.CachedImage).Cancel();
            (sender as FFImageLoading.Forms.CachedImage).Source = null;
        }

        private void MediaImageSkia_BindingContextChanged(object sender, EventArgs e)
        {
            HLinkMedia = this.BindingContext as HLinkHomeImageModel;

            if (HLinkMedia is null)
            {
                //DataStore.CN.NotifyError("Bad HlinkMediaModel (is null) passed to MediaImage");
                return;
            }

            if (!HLinkMedia.Valid)
            {
                //DataStore.CN.NotifyError("Invalid HlinkMediaModel (" + HLinkMedia.HLinkKey + ") passed to MediaImage");
                return;
            }

            theMediaModel = HLinkMedia.DeRef;

            if (theMediaModel.Id == "O0196")
            {
            }

            if (!HLinkMedia.Valid || !HLinkMedia.HomeUseImage || !theMediaModel.IsMediaFile)
            {
                this.daSymbol.IsVisible = true;

                // Set symbol
                FontImageSource tt = this.daSymbol.Source as FontImageSource;
                tt.Glyph = HLinkMedia.HomeSymbol;
                tt.Color = HLinkMedia.HomeSymbolColour;

                if (tt.Glyph == null)
                {
                    DataStore.CN.NotifyError("MediaImage (" + HLinkMedia.HLinkKey + ") Null Glyph");
                }

                if (tt.Color == null)
                {
                    DataStore.CN.NotifyError("MediaImage (" + HLinkMedia.HLinkKey + ") Null Colour");
                }

                this.daSymbol.Source = tt;
                //this.daImage.IsVisible = false;

                if (UConHideSymbol)
                {
                    this.daSymbol.IsVisible = true;
                }

                return;
            }

            // Have a media image to display
            Debug.WriteLine(HLinkMedia.DeRef.MediaStorageFilePath, "MediaImage");

            this.daSymbol.IsVisible = false;

            if (HLinkMedia.GCorner1X > 0 || HLinkMedia.GCorner1Y > 0 || HLinkMedia.GCorner2X > 0 || HLinkMedia.GCorner2Y > 0)
            {
                this.daImage.IsVisible = false;
                this.daSymbol.IsVisible = false;
                this.skiaBitMapImage.IsVisible = true;

                SKBitmapImageSource skiabmimage = new SKBitmapImageSource();

                using (StreamReader stream = new StreamReader(HLinkMedia.DeRef.MediaStorageFilePath))
                {
                    resourceBitmap = SKBitmap.Decode(stream.BaseStream);
                }

                // Check for too large a bitmap
                Debug.WriteLine("resourceBitmap size", resourceBitmap.ByteCount);
                if (resourceBitmap.ByteCount > int.MaxValue - 1000)
                {
                    // TODO Handle this better. Perhaps resize? Delete for now
                    resourceBitmap = new SKBitmap();
                }

                float crleft = (float)(HLinkMedia.GCorner1X / 100d * theMediaModel.MetaDataWidth);
                float crright = (float)(HLinkMedia.GCorner2X / 100d * theMediaModel.MetaDataWidth);
                float crtop = (float)(HLinkMedia.GCorner1Y / 100d * theMediaModel.MetaDataHeight);
                float crbottom = (float)(HLinkMedia.GCorner2Y / 100d * theMediaModel.MetaDataHeight);

                SKRect cropRect = new SKRect(crleft, crtop, crright, crbottom);

                SKBitmap croppedBitmap = new SKBitmap((int)cropRect.Width,
                                                  (int)cropRect.Height);
                SKRect dest = new SKRect(0, 0, cropRect.Width, cropRect.Height);
                SKRect source = new SKRect(cropRect.Left, cropRect.Top,
                                           cropRect.Right, cropRect.Bottom);

                using (SKCanvas canvas = new SKCanvas(croppedBitmap))
                {
                    canvas.DrawBitmap(resourceBitmap, source, dest);
                }

                resourceBitmap.Dispose();

                skiabmimage.Bitmap = croppedBitmap;

                skiaBitMapImage.Source = skiabmimage;
            }
            else
            {
                this.daImage.IsVisible = true;
                this.daSymbol.IsVisible = false;
                this.skiaBitMapImage.IsVisible = false;

                this.daImage.DownsampleToViewSize = true;

                this.daImage.Source = theMediaModel.MediaStorageFilePath;
            }
        }

        // To detect redundant calls
        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~MediaImageSkia() { // Do not change this code. Put cleanup code in Dispose(bool
        // disposing) above. Dispose(false); }
    }
}