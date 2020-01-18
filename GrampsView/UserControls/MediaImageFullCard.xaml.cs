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
        public static readonly BindableProperty UCMediaProperty = BindableProperty.Create(propertyName: nameof(UCMedia),
                                                                                                returnType: typeof(MediaModel),
                                                                                                declaringType: typeof(MediaImageFullCard),
                                                                                                defaultValue: new MediaModel(),
                                                                                                defaultBindingMode: BindingMode.OneWay,
                                                                                                propertyChanged: HandleVMPropertyChanged
                                                                                                );

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaImageFullCard" /> class.
        /// </summary>
        public MediaImageFullCard()
        {
            InitializeComponent();
        }

        public MediaModel UCMedia
        {
            get
            {
                if (base.GetValue(UCMediaProperty) != null)
                {
                }

                return (MediaModel)base.GetValue(UCMediaProperty);
            }

            set
            {
                if (this.UCMedia != value)
                {
                    base.SetValue(UCMediaProperty, value);
                }
            }
        }

        private static void HandleVMPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            MediaImageFullCard mifModel = (bindable as MediaImageFullCard);

            if (newValue is HLinkMediaModel imageMediaModel)
            {
                mifModel.mediaFull.UCHLinkMediaModel = imageMediaModel;
            }

            // Check if anything to display
            if (mifModel.mediaFull.IsVisible)
            {
                mifModel.IsVisible = true;
            }
            else
            {
                // Nothing to display so hide
                mifModel.IsVisible = false;
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
#if WINDOWS_UWP

            MediaModel t = ((Image)sender).BindingContext as MediaModel;

            if (t.IsOriginalFilePathValid)
            {
                OpenFileRequest tt = new OpenFileRequest

                {
                    File = new ReadOnlyFile(t.MediaStorageFileUri.AbsoluteUri)
                };

                Launcher.OpenAsync(tt);
            }
#endif
        }
    }
}