// <copyright file="RepositoryCardLarge.xaml.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

namespace GrampsView.UserControls
{
    using Xamarin.Forms;

    public partial class RepositoryCardLarge : Frame
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryCardLarge" /> class.
        /// </summary>
        public RepositoryCardLarge()
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
        //            new CardListLine("Name:", ViewModel.GRName),
        //            new CardListLine("Type:", ViewModel.GType),
        //        };
        //        }
        //    }
        //}

        ///// <summary>
        ///// Gets. </summary>
        ///// <value>
        ///// The ViewModel.
        ///// </value>
        //public RepositoryModel ViewModel
        //{
        //    get
        //    {
        //        if ((DataContext != null) && (DataContext.GetType() == typeof(RepositoryModel)))
        //        {
        //            return (RepositoryModel)DataContext;
        //        }
        //        else
        //        {
        //            return new RepositoryModel();
        //        }
        //    }
        //}
    }
}