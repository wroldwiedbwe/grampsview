// <copyright file="MediaImageSkia.xaml.cs" company="MeMyselfAndI">
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
    using System.Threading;
    using Xamarin.Forms;

    public partial class MediaImageSkia : Frame, IDisposable
    {
        public static readonly BindableProperty UConHideSymbolProperty
               = BindableProperty.Create(returnType: typeof(bool), declaringType: typeof(MediaImageSkia), propertyName: nameof(UConHideSymbol), defaultValue: false, propertyChanged: MediaImage_UConPropertyChanged);

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
            DataStore.CN.NotifyError("Error in MediaImageSkia.  Error is " + e.Exception.Message);

            (sender as FFImageLoading.Forms.CachedImage).Cancel();
            (sender as FFImageLoading.Forms.CachedImage).Source = null;
        }

        private void MediaImageSkia_BindingContextChanged(object sender, EventArgs e)
        {
            HLinkHomeImageModel newHLinkMedia = this.BindingContext as HLinkHomeImageModel;

            if (newHLinkMedia is null)
            {
                //DataStore.CN.NotifyError("Bad HlinkMediaModel (is null) passed to MediaImage");
                return;
            }

            if (!newHLinkMedia.Valid)
            {
                //DataStore.CN.NotifyError("Invalid HlinkMediaModel (" + HLinkMedia.HLinkKey + ") passed to MediaImage");
                return;
            }

            if (newHLinkMedia == HLinkMedia)
            {
                return;
            }

            // Input valid so start work
            this.daSymbol.IsVisible = false;
            this.daImage.IsVisible = false;
            this.daImage.Source = null;

            HLinkMedia = newHLinkMedia;

            if (!HLinkMedia.Valid || !HLinkMedia.LinkToImage)
            {
                this.daSymbol.IsVisible = true;

                // Set symbol
                FontImageSource tt = this.daSymbol.Source as FontImageSource;
                tt.Glyph = HLinkMedia.HomeSymbol;
                tt.Color = HLinkMedia.HomeSymbolColour;

                if (tt.Glyph == null)
                {
                    DataStore.CN.NotifyError("MediaImageSkia (" + HLinkMedia.HLinkKey + ") Null Glyph");
                }

                if (tt.Color == null)
                {
                    DataStore.CN.NotifyError("MediaImageSkia (" + HLinkMedia.HLinkKey + ") Null Colour");
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

            theMediaModel = HLinkMedia.DeRef;

            if (theMediaModel.Id == "O0003")
            {
            }

            Debug.WriteLine(HLinkMedia.DeRef.MediaStorageFilePath, "MediaImageSkia");
            if (string.IsNullOrEmpty(HLinkMedia.DeRef.MediaStorageFilePath))
            {
                DataStore.CN.NotifyError("The media file path is null for Id:" + HLinkMedia.DeRef.Id);
                return;
            }

            this.daSymbol.IsVisible = false;

            this.daImage.IsVisible = true;
            this.daImage.DownsampleToViewSize = true;
            this.daImage.Source = theMediaModel.MediaStorageFilePath;
        }

        // To detect redundant calls
        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~MediaImageSkia() { // Do not change this code. Put cleanup code in Dispose(bool
        // disposing) above. Dispose(false); }
    }
}