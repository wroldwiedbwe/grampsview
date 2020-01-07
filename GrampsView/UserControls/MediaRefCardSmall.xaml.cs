// <copyright file="MediaRefCardSmall.xaml.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

namespace GrampsView.UserControls
{
    using Xamarin.Forms;

    public partial class MediaRefCardSmall : Frame
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaRefCardSmall" /> class.
        /// </summary>
        public MediaRefCardSmall()
        {
            InitializeComponent();

            // DataContextChanged += (s, e) => Bindings.Update();
        }

        ///// <summary>
        ///// Gets. </summary>
        ///// <value>
        ///// The ViewModel.
        ///// </value>
        //public HLinkMediaModel ViewModel
        //{
        //    get
        //    {
        //        if ((DataContext != null) && (DataContext.GetType() == typeof(HLinkMediaModel)))
        //        {
        //            return (HLinkMediaModel)DataContext;
        //        }
        //        else
        //        {
        //            return new HLinkMediaModel();
        //        }
        //    }
        //}
    }
}