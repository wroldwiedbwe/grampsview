// <copyright file="MediaCardLarge.xaml.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
namespace GrampsView.UserControls
{
    using GrampsView.Data.Model;

    using System.Diagnostics.Contracts;

    using Xamarin.Forms;

    /// <summary>
    /// Media Control large code behind.
    /// </summary>
    public partial class MediaImageFullCard : Grid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaImageFullCard"/> class.
        /// </summary>
        public MediaImageFullCard()
        {
            InitializeComponent();

            this.IsVisible = false;
        }

        private void MediaImageFullCardRoot_BindingContextChanged(object sender, System.EventArgs e)
        {
            MediaImageFullCard mifModel = (sender as MediaImageFullCard);

            // Xamarin sets to null as the parent page is destroyed
            if (this.BindingContext is null)
            {
                return;
            }

            Contract.Assert(BindingContext is HLinkHomeImageModel);

            HLinkHomeImageModel imageMediaModel = this.BindingContext as HLinkHomeImageModel;

            Contract.Assert(imageMediaModel != null);

            if (imageMediaModel.Valid)
            {
                mifModel.image.BindingContext = imageMediaModel;
            }

            // Check if anything to display
            if (mifModel.image.IsVisible)
            {
                mifModel.IsVisible = true;
            }
        }
    }
}