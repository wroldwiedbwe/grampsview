// <copyright file="MediaImageFull.xaml.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

namespace GrampsView.UserControls
{
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System;

    using Xamarin.Forms;

    public partial class MediaImageFull : Frame
    {
        public static readonly BindableProperty UCHLinkMediaModelProperty = BindableProperty.Create(
                                                        propertyName: nameof(UCHLinkMediaModel),
                                                        returnType: typeof(HLinkMediaModel),
                                                        declaringType: typeof(MediaImageFull),
                                                        defaultValue: new HLinkMediaModel(),
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: HandleVMPropertyChanged
                                                        );

        public MediaImageFull()
        {
            InitializeComponent();

            //this.daImage.CacheKeyFactory = new CustomCacheKeyFactory();
        }

        public HLinkMediaModel UCHLinkMediaModel
        {
            get
            {
                return GetValue(UCHLinkMediaModelProperty) as HLinkMediaModel;
            }

            set
            {
                if (UCHLinkMediaModel != value)
                {
                    SetValue(UCHLinkMediaModelProperty, value);
                }
            }
        }

        private static void HandleVMPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            MediaImageFull mifModel = (bindable as MediaImageFull);

            HLinkHomeImageModel imageHLinkMediaModel = newValue as HLinkHomeImageModel;

            if (!(imageHLinkMediaModel is null) && (imageHLinkMediaModel.Valid))
            {
                MediaModel t = imageHLinkMediaModel.DeRef;

                if ((t.IsMediaStorageFileValid) && (t.IsMediaFile))
                {
                    try
                    {
                        mifModel.daImage.Source = t.MediaStorageFilePath;
                        var tt = mifModel.daImage.Source;

                        mifModel.IsVisible = true;

                        // TODO cleanup code so does nto use bindignContext if possible
                        mifModel.BindingContext = imageHLinkMediaModel;

                        return;
                    }
                    catch (Exception ex)
                    {
                        DataStore.CN.NotifyException("Exception in MediaImageFull control", ex);
                        throw;
                    }
                }
            }

            // Nothing to display so hide
            mifModel.IsVisible = false;
        }

        private void daImage_Error(object sender, FFImageLoading.Forms.CachedImageEvents.ErrorEventArgs e)
        {
            DataStore.CN.NotifyError("Error in MediaImageFull.  Error is " + e.Exception.Message);

            (sender as FFImageLoading.Forms.CachedImage).Cancel();
            (sender as FFImageLoading.Forms.CachedImage).Source = null;
        }
    }
}