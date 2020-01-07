// <copyright file="NoteCardLarge.xaml.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

namespace GrampsView.UserControls
{
    using Xamarin.Forms;

    /// <summary>
    /// Code behind for Note Card Large.
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.UserControl" />
    public partial class NoteCardLarge : Frame
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoteCardLarge" /> class. The local person
        /// data view.
        /// </summary>
        // private EventDataView localEventDataView = new EventDataView();

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteCardLarge" /> class.
        /// </summary>
        public NoteCardLarge()
        {
            InitializeComponent();

            // DataContextChanged += (s, e) => Bindings.Update();
        }

        ///// <summary>
        ///// Gets the ViewModel.
        ///// </summary>
        ///// <value>
        ///// The ViewModel.
        ///// </value>
        //public NoteModel ViewModel
        //{
        //    get
        //    {
        //        if ((DataContext != null) && (DataContext.GetType() == typeof(NoteModel)))
        //        {
        //            return (NoteModel)DataContext;
        //        }
        //        else
        //        {
        //            return new NoteModel();
        //        }
        //    }
        //}
    }
}