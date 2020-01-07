// <copyright file="RepositoryRefCardLarge.xaml.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

namespace GrampsView.UserControls
{
    using Xamarin.Forms;

    public partial class RepositoryRefCardLarge : Frame
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryRefCardLarge" /> class.
        /// </summary>
        public RepositoryRefCardLarge()
        {
            InitializeComponent();

            // DataContextChanged += (s, e) => Bindings.Update();
        }

        ///// <summary>
        ///// Gets the rep reference details list.
        ///// </summary>
        ///// <value>
        ///// The rep reference details list.
        ///// </value>
        //public CardListLineCollection RepRefDetailsList
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
        //            new CardListLine("Name:", ViewModel.DeRef.GRName),
        //            new CardListLine("Type:", ViewModel.DeRef.GType),
        //            new CardListLine("Type:", ViewModel.GPriv),
        //            new CardListLine("Call No:", ViewModel.GCallNo),
        //            new CardListLine("Medium:", ViewModel.GMedium),
        //            new CardListLine("Note:", ViewModel.GNoteRef[0].DeRef.GText),
        //        };
        //        }
        //    }
        //}

        ///// <summary>
        ///// Gets the ViewModel.
        ///// </summary>
        ///// <value>
        ///// The ViewModel.
        ///// </value>
        //public HLinkRepositoryModel ViewModel
        //{
        //    get
        //    {
        //        if ((DataContext != null) && (DataContext.GetType() == typeof(HLinkRepositoryModel)))
        //        {
        //            return (HLinkRepositoryModel)DataContext;
        //        }
        //        else
        //        {
        //            return new HLinkRepositoryModel();
        //        }
        //    }
        //}
    }
}