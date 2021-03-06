﻿// <copyright file="MediaImageSkia.xaml.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

namespace GrampsView.UserControls
{
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using Prism.Commands;

    using System;

    using Xamarin.Essentials;
    using Xamarin.Forms;

    public partial class MediaImageSkia : Frame // , IDisposable
    {
        public static readonly BindableProperty UConHideSymbolProperty
               = BindableProperty.Create(returnType: typeof(bool), declaringType: typeof(MediaImageSkia), propertyName: nameof(UConHideSymbol), defaultValue: false, propertyChanged: MediaImage_UConPropertyChanged);

        public MediaImageSkia()
        {
            InitializeComponent();

            // Handle IsEnabled on the control and pass to the image Tap event
            this.daImage.IsEnabled = true;
            if (!this.IsEnabled)
            {
                this.daImage.IsEnabled = false;
            }
        }

        public bool UConHideSymbol
        {
            get { return (bool)GetValue(UConHideSymbolProperty); }
            set { SetValue(UConHideSymbolProperty, value); }
        }

        private HLinkHomeImageModel ExistingHLinkMedia { get; set; }

        public void ReloadImage()
        {
            this.daImage.ReloadImage();

            this.daImage.LoadingPlaceholder = null;
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

        private void daImage_Finish(object sender, FFImageLoading.Forms.CachedImageEvents.FinishEventArgs e)
        {
        }

        private void MediaImageSkia_BindingContextChanged(object sender, EventArgs e)
        {
            try
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

                if (newHLinkMedia == ExistingHLinkMedia)
                {
                    return;
                }

                // Input valid so start work
                this.daSymbol.IsVisible = false;
                this.daImage.IsVisible = false;
                this.daImage.Source = null;

                // Save the HLink so can check for duplicate chanegs later
                ExistingHLinkMedia = newHLinkMedia;

                if (!ExistingHLinkMedia.Valid || !ExistingHLinkMedia.LinkToImage)
                {
                    ShowSymbol();
                    return;
                }
                else
                {
                    ShowImage();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void ShowImage()
        {
            MediaModel theMediaModel = ExistingHLinkMedia.DeRef;

            ////if (theMediaModel.Id == "O0003")
            ////{
            ////}

            //Debug.WriteLine(HLinkMedia.DeRef.MediaStorageFilePath, "MediaImageSkia");

            if (string.IsNullOrEmpty(theMediaModel.MediaStorageFilePath))
            {
                DataStore.CN.NotifyError("The media file path is null for Id:" + theMediaModel.Id);
                return;
            }

            this.daSymbol.IsVisible = false;

            this.daImage.IsVisible = true;
            this.daImage.DownsampleToViewSize = true;
            //this.daImage.HeightRequest = theMediaModel.MetaDataHeight;
            //this.daImage.WidthRequest = theMediaModel.MetaDataWidth;
            this.daImage.Source = theMediaModel.MediaStorageFilePath;
        }

        private void ShowSymbol()
        {
            this.daSymbol.IsVisible = true;

            // Set symbol
            FontImageSource tt = this.daSymbol.Source as FontImageSource;
            tt.Glyph = ExistingHLinkMedia.HomeSymbol;
            tt.Color = ExistingHLinkMedia.HomeSymbolColour;

            if (tt.Glyph == null)
            {
                DataStore.CN.NotifyError("MediaImageSkia (" + ExistingHLinkMedia.HLinkKey + ") Null Glyph");
            }

            if (tt.Color == null)
            {
                DataStore.CN.NotifyError("MediaImageSkia (" + ExistingHLinkMedia.HLinkKey + ") Null Colour");
            }

            this.daSymbol.Source = tt;

            if (UConHideSymbol)
            {
                this.daSymbol.IsVisible = true;
            }
        }
    }
}