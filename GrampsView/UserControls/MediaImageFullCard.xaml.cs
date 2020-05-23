// <copyright file="MediaCardLarge.xaml.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
namespace GrampsView.UserControls
{
    using GrampsView.Data.Model;

    using Xamarin.Forms;

    /// <summary>
    /// Media Control large code behind.
    /// </summary>
    public partial class MediaImageFullCard : Frame
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaImageFullCard"/> class.
        /// </summary>
        public MediaImageFullCard()
        {
            InitializeComponent();

            this.IsVisible = false;
        }

        public void ReloadImage()
        {
            image.ReloadImage();

            image.LoadingPlaceholder = null;
        }

        private void MediaImageFullCardRoot_BindingContextChanged(object sender, System.EventArgs e)
        {
            MediaImageFullCard mifModel = (sender as MediaImageFullCard);

            HLinkHomeImageModel imageMediaModel = new HLinkHomeImageModel();

            if (BindingContext is HLinkHomeImageModel)
            {
                imageMediaModel = this.BindingContext as HLinkHomeImageModel;
            }

            if (BindingContext is MediaImageFullCard)
            {
                imageMediaModel = ((this.BindingContext as MediaImageFullCard).BindingContext) as HLinkHomeImageModel;
            }

            if (!(imageMediaModel is null) && (imageMediaModel.Valid))
            {
                mifModel.image.Source = imageMediaModel.DeRef.MediaStorageFilePath;
            }

            // Check if anything to display
            if (mifModel.image.IsVisible)
            {
                mifModel.IsVisible = true;
            }
        }
    }
}