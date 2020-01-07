// <copyright file="CitationRefCardSmall.xaml.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

namespace GrampsView.UserControls
{
    using Xamarin.Forms;

    /// <summary>
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.UserControl" />
    public partial class CitationRefCardSmall : Frame
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CitationRefCardSmall" /> class.
        /// </summary>
        public CitationRefCardSmall()
        {
            InitializeComponent();

            //DataContextChanged += (s, e) => Bindings.Update();
        }

        ///// <summary>
        ///// Gets the get text.
        ///// </summary>
        ///// <value>
        ///// The get text.
        ///// </value>
        //public string GetText
        //{
        //    get
        //    {
        //        if (ViewModel == null || !ViewModel.Valid)
        //        {
        //            return null;
        //        }

        // if (ViewModel.DeRef.GNoteRef.GetSummary.Length == 0) { return ViewModel.DeRef.GPage; }

        //        return ViewModel.DeRef.GNoteRef.GetSummary;
        //    }
        //}

        ///// <summary>
        ///// Gets the ViewModel.
        ///// </summary>
        ///// <value>
        ///// The ViewModel.
        ///// </value>
        //public HLinkCitationModel ViewModel
        //{
        //    get
        //    {
        //        if ((DataContext != null) && (DataContext.GetType() == typeof(HLinkCitationModel)))
        //        {
        //            return (HLinkCitationModel)DataContext;
        //        }
        //        else
        //        {
        //            return new HLinkCitationModel();
        //        }
        //    }
        //}
    }
}