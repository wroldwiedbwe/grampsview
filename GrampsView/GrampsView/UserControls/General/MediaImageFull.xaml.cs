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
        public static readonly BindableProperty UCMediaModelProperty = BindableProperty.Create(
                                                        propertyName: nameof(UCMediaModel),
                                                        returnType: typeof(MediaModel),
                                                        declaringType: typeof(MediaImageFull),
                                                        defaultValue: new MediaModel(),
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: HandleVMPropertyChanged
                                                        );

        public MediaImageFull()
        {
            InitializeComponent();

            //this.daImage.CacheKeyFactory = new CustomCacheKeyFactory();
        }

        public MediaModel UCMediaModel
        {
            get
            {
                return GetValue(UCMediaModelProperty) as MediaModel;
            }

            set
            {
                if (UCMediaModel != value)
                {
                    SetValue(UCMediaModelProperty, value);
                }
            }
        }

        private static void HandleVMPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            MediaImageFull mifModel = (bindable as MediaImageFull);

            if (newValue is MediaModel imageMediaModel)
            {
                if ((imageMediaModel.IsMediaStorageFileValid) && (imageMediaModel.IsMediaFile))
                {
                    try
                    {
                        mifModel.daImage.Source = imageMediaModel.MediaStorageFilePath;
                        var t = mifModel.daImage.Source;

                        mifModel.IsVisible = true;

                        // TODO cleanup code so does nto use bindignContext if possible
                        mifModel.BindingContext = imageMediaModel;

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