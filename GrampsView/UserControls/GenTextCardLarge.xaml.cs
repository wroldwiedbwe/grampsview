// <copyright file="GenTextCardLarge.xaml.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
/// <summary>
/// Code behind for the GenTextCardLarge UserControl.
/// </summary>
namespace GrampsView.UserControls
{
    using Xamarin.Forms;

    /// <summary>
    /// Code behind for the Gen Text Card Large User Control.
    /// </summary>
    public partial class GenTextCardLarge : Frame
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenTextCardLarge" /> class.
        /// </summary>
        public GenTextCardLarge()
        {
            InitializeComponent();

            // DataContextChanged += (s, e) => Bindings.Update();
        }

        ///// <summary>
        ///// Gets the View Model.
        ///// </summary>
        //public TextDetailCardModel ViewModel
        //{
        //    get
        //    {
        //        if ((DataContext != null) && (DataContext.GetType() == typeof(TextDetailCardModel)))
        //        {
        //            return (TextDetailCardModel)DataContext;
        //        }
        //        else
        //        {
        //            return new TextDetailCardModel();
        //        }
        //    }
        //}
    }
}