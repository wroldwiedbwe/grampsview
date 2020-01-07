// <copyright file="FamilyCardSmall.xaml.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

namespace GrampsView.UserControls
{
    using Xamarin.Forms;

    public partial class FamilyCardSmall : Frame
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FamilyCardSmall" /> class.
        /// </summary>
        public FamilyCardSmall()
        {
            InitializeComponent();

            //DataContextChanged += (s, e) => Bindings.Update();
        }

        ///// <summary>
        ///// Gets. </summary>
        ///// <value>
        ///// The ViewModel.
        ///// </value>
        //public FamilyModel ViewModel
        //{
        //    get
        //    {
        //        if ((DataContext != null) && (DataContext.GetType() == typeof(FamilyModel)))
        //        {
        //            return (FamilyModel)DataContext;
        //        }
        //        else
        //        {
        //            return new FamilyModel();
        //        }
        //    }
        //}
    }
}