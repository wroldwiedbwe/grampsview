// <copyright file="MediaCardLarge.xaml.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
namespace GrampsView.UserControls
{
    using Xamarin.Forms;

    /// <summary>
    /// Media Control large code behind.
    /// </summary>
    public partial class MediaCardLarge : Frame
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaCardLarge" /> class.
        /// </summary>
        public MediaCardLarge()
        {
            InitializeComponent();

            //DataContextChanged += (s, e) => Bindings.Update();
        }

        ///// <summary>
        ///// Gets the media details list.
        ///// </summary>
        ///// <value>
        ///// The media details list.
        ///// </value>
        //public CardListLineCollection MediaDetailsList
        //{
        //    get
        //    {
        //        if (ViewModel == null)
        //        {
        //            return new CardListLineCollection();
        //        }
        //        else
        //        {
        //            return new CardListLineCollection
        //        {
        //             new CardListLine("Title:", ViewModel.GDescription),
        //             new CardListLine("Date:", ViewModel.GDateValue.GetShortDateAsString),
        //             new CardListLine("Title Decoded:", ViewModel.TitleDecoded),
        //        };
        //        }
        //    }
        //}

        ///// <summary>
        ///// Gets the View Model.
        ///// </summary>
        ///// <value>
        ///// The ViewModel.
        ///// </value>
        //public MediaModel ViewModel
        //{
        //    get
        //    {
        //        if ((DataContext != null) && (DataContext.GetType() == typeof(MediaModel)))
        //        {
        //            return (MediaModel)DataContext;
        //        }
        //        else
        //        {
        //            return new MediaModel();
        //        }
        //    }
        //}
    }
}