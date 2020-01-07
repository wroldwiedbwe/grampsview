// <copyright file="SourceCardLarge.xaml.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

namespace GrampsView.UserControls
{
    using Xamarin.Forms;

    public partial class SourceCardLarge : Frame
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SourceCardLarge" /> class.
        /// </summary>
        public SourceCardLarge()
        {
            InitializeComponent();

            // DataContextChanged += (s, e) => Bindings.Update();
        }

        ///// <summary>
        ///// Gets the local person name details list.
        ///// </summary>
        //public CardListLineCollection PersonDetailsList
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
        //            new CardListLine("Title:", ViewModel.GSTitle),
        //            new CardListLine("Author:", ViewModel.GSAuthor),
        //            new CardListLine("Pub Info:", ViewModel.GSPubInfo),
        //            new CardListLine("Abbrev:", ViewModel.GSAbbrev),
        //        };
        //        }
        //    }
        //}

        ///// <summary>
        ///// Gets. </summary>
        ///// <value>
        ///// The ViewModel.
        ///// </value>
        //public SourceModel ViewModel
        //{
        //    get
        //    {
        //        if ((DataContext != null) && (DataContext.GetType() == typeof(SourceModel)))
        //        {
        //            return (SourceModel)DataContext;
        //        }
        //        else
        //        {
        //            return new SourceModel();
        //        }
        //    }
        //}
    }
}