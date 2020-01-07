//-----------------------------------------------------------------------
//
// Various data modesl to small to be worth putting in their own file
// is first launched.
//
// <copyright file="MediaDetailPage.xaml.cs" company="MeMyselfAndI">
// Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

// The Item Detail Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234232
namespace GrampsView.Views
{
    using FFImageLoading.Forms;

    using GrampsView.ViewModels;

    using Xamarin.Forms;

    /// <summary>
    /// A page that displays details for a single item within a group while allowing gestures to flip
    /// through other items belonging to the same group.
    /// </summary>
    public partial class MediaDetailPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaDetailPage" /> class.
        /// </summary>
        public MediaDetailPage()
        {
            InitializeComponent();
        }

        public void ReloadImage()
        {
            image.ReloadImage();

            image.LoadingPlaceholder = null;
        }

        private void OnPanUpdated(object sender, PanUpdatedEventArgs args)
        {
            MediaDetailViewModel t = this.BindingContext as MediaDetailViewModel;

            CachedImage tt = sender as CachedImage;

            t.OnPanUpdated(tt, args);
        }

        private void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs args)
        {
            MediaDetailViewModel t = this.BindingContext as MediaDetailViewModel;

            CachedImage tt = sender as CachedImage;

            t.OnPinchUpdated(tt, args);
        }
    }
}