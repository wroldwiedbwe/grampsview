// <copyright file="MediaImage.xaml.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

namespace GrampsView.UserControls
{
    using FFImageLoading.Transformations;
    using FFImageLoading.Work;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    using Xamarin.Forms;

    public partial class MediaImage : Frame
    {
        public static readonly BindableProperty UConHideSymbolProperty
               = BindableProperty.Create(returnType: typeof(bool), declaringType: typeof(MediaImage), propertyName: nameof(UConHideSymbol), defaultValue: false, propertyChanged: MediaImage_UConPropertyChanged);

        private double CropHeightRatio;

        private double CropWidthRatio;

        // Set some other stuff
        private double CurrentXOffset;

        private double CurrentYOffset;

        private double CurrentZoomFactor;

        public MediaImage()
        {
            InitializeComponent();
        }

        public bool UConHideSymbol
        {
            get { return (bool)GetValue(UConHideSymbolProperty); }
            set { SetValue(UConHideSymbolProperty, value); }
        }

        private HLinkMediaModel HLinkMedia { get; set; }

        private static void MediaImage_UConPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
        }

        private void DaImage_Error(object sender, FFImageLoading.Forms.CachedImageEvents.ErrorEventArgs e)
        {
            DataStore.CN.NotifyError("Error in MediaImage.  Error is " + e.Exception.Message);

            (sender as FFImageLoading.Forms.CachedImage).Cancel();
            (sender as FFImageLoading.Forms.CachedImage).Source = null;
        }

        private void mediaImage_BindingContextChanged(object sender, EventArgs e)
        {
            //if (HLinkMedia != null)
            //{
            //    return;
            //}

            HLinkMediaModel qq = this.BindingContext as HLinkMediaModel;
            if ((qq is null) || (!qq.Valid))
            {
                return;
            }

            HLinkMedia = qq;

            if (HLinkMedia is null)
            {
                DataStore.CN.NotifyError("Bad HlinkMediaModel (is null) passed to MediaImage");
                return;
            }

            MediaModel t = HLinkMedia.DeRef;

            if (t.Id == "O0196")
            {
            }

            if (!HLinkMedia.Valid || !HLinkMedia.HomeUseImage || !t.IsMediaFile)
            {
                this.daImage.IsVisible = false;
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

            Debug.WriteLine(HLinkMedia.DeRef.MediaStorageFilePath, "MediaImage");

            this.daImage.IsVisible = true;
            this.daSymbol.IsVisible = false;

            this.daImage.DownsampleToViewSize = true;

            if (HLinkMedia.GCorner1X > 0 || HLinkMedia.GCorner1Y > 0 || HLinkMedia.GCorner2X > 0 || HLinkMedia.GCorner2Y > 0)
            {
                double CropWidth = ((HLinkMedia.GCorner2X - HLinkMedia.GCorner1X) / 100d) * t.MetaDataWidth;
                double CropHeight = ((HLinkMedia.GCorner2Y - HLinkMedia.GCorner1Y) / 100d) * t.MetaDataHeight;

                CropWidthRatio = CropWidth / CropHeight;
                CropHeightRatio = CropHeight / CropWidth;
                CurrentZoomFactor = 100 / ((HLinkMedia.GCorner2X - HLinkMedia.GCorner1X));

                CurrentXOffset = (CropWidth / 2);
                CurrentXOffset = CurrentXOffset * (CropHeight / CropWidth);

                CurrentYOffset = (CropHeight / 2);
                CurrentYOffset = CurrentYOffset * (CropWidth / CropHeight);

                this.daImage.DownsampleToViewSize = false;

                this.daImage.Transformations = new List<ITransformation> {
                         new CropTransformation(CurrentZoomFactor, CurrentXOffset, CurrentYOffset, CropWidthRatio, CropHeightRatio) };
            }

            this.daSymbol.IsVisible = false;
            this.daImage.Source = t.MediaStorageFilePath;
        }
    }
}