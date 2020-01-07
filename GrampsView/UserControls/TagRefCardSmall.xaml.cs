// <copyright file="TagRefCardSmall.xaml.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

namespace GrampsView.UserControls
{
    using Xamarin.Forms;

    public partial class TagRefCardSmall : Frame
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagRefCardSmall" /> class.
        /// </summary>
        public TagRefCardSmall()
        {
            InitializeComponent();

            // DataContextChanged += (s, e) => Bindings.Update();
        }

        ///// <summary>
        ///// Gets. </summary>
        ///// <value>
        ///// The ViewModel.
        ///// </value>
        //public HLinkTagModel ViewModel
        //{
        //    get
        //    {
        //        return (HLinkTagModel)DataContext;
        //    }
        //}
    }
}