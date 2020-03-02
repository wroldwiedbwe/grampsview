//-----------------------------------------------------------------------
//
// Various data modesl to small to be worth putting in their own file
// is first launched.
//
// <copyright file="NoteDetailPage.xaml.cs" company="MeMySelfandI">
//     GPL Copyright
// </copyright>
//-----------------------------------------------------------------------

// The Item Detail Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234232

namespace GrampsView.Views
{
    using Xamarin.Forms;

    /// <summary>
    /// A page that displays details for a single item within a group while allowing gestures to flip
    /// through other items belonging to the same group.
    /// </summary>
    public partial class CitationRefDetailPage : ContentPage
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CitationDetailPage" /> class.
        /// </summary>
        public CitationRefDetailPage()
        {
            InitializeComponent();

            //this.DataContextChanged += (s, e) => Bindings.Update();
        }

        #endregion Constructors

        ///// <summary>
        ///// </summary>
        ///// <value>
        ///// The Model.
        ///// </value>
        //public CitationRefDetailPageViewModel Model
        //{
        //    get
        //    {
        //        if ((this.DataContext != null) && (this.DataContext.GetType() == typeof(CitationRefDetailPageViewModel)))
        //        {
        //            return (CitationRefDetailPageViewModel)DataContext;
        //        }
        //        else
        //        {
        //            return new CitationRefDetailPageViewModel();
        //        }
        //    }
        //}
    }
}