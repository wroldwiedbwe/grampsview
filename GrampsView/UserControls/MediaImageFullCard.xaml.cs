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
                                                                                                returnType: typeof(HLinkHomeImageModel),
                                                                                                declaringType: typeof(MediaImageFullCard),
                                                                                                defaultValue: new HLinkHomeImageModel(),
                                                                                                defaultBindingMode: BindingMode.OneWay,
                                                                                                propertyChanged: HandleVMPropertyChanged
                                                                                                );

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaImageFullCard"/> class.
        /// </summary>
        public MediaImageFullCard()
        {
            InitializeComponent();

            this.IsVisible = false;
        }

        public HLinkHomeImageModel UCMedia
        {
            get
            {
                if (base.GetValue(UCMediaProperty) != null)
                {
                }

                return (HLinkHomeImageModel)base.GetValue(UCMediaProperty);
            }

            set
            {
                if (this.UCMedia != value)
                {
                    base.SetValue(UCMediaProperty, value);
                }
            }
        }

        public void ReloadImage()
        {
            image.ReloadImage();

            image.LoadingPlaceholder = null;
        }

        private static void HandleVMPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            MediaImageFullCard mifModel = (bindable as MediaImageFullCard);

            HLinkHomeImageModel imageMediaModel = newValue as HLinkHomeImageModel;

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