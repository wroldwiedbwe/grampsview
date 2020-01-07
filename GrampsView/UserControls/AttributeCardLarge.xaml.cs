// <copyright file="AttributeCardLarge.xaml.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

namespace GrampsView.UserControls
{
    using Xamarin.Forms;

    public partial class AttributeCardLarge : Frame
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeCardLarge" /> class.
        /// </summary>
        public AttributeCardLarge()
        {
            InitializeComponent();

            //DataContextChanged += (s, e) => Bindings.Update();
        }

        //public CardListLineCollection AttributeList
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
        //            new CardListLine("Type:", ViewModel.GType),
        //            new CardListLine("Value:", ViewModel.GValue),
        //        };
        //        }
        //    }
        //}

        ///// <summary>
        ///// Gets. </summary>
        ///// <value>
        ///// The ViewModel.
        ///// </value>
        //public AttributeModel ViewModel
        //{
        //    get
        //    {
        //        if ((DataContext != null) && (DataContext.GetType() == typeof(AttributeModel)))
        //        {
        //            return (AttributeModel)DataContext;
        //        }
        //        else
        //        {
        //            return new AttributeModel();
        //        }
        //    }
        //}
    }
}