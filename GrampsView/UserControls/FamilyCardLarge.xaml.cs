// <copyright file="FamilyCardLarge.xaml.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

namespace GrampsView.UserControls
{
    using Xamarin.Forms;

    /// <summary>
    /// Code behind for the large Family Card.
    /// </summary>
    public partial class FamilyCardLarge : Frame
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FamilyCardLarge" /> class.
        /// </summary>
        public FamilyCardLarge()
        {
            InitializeComponent();

            //DataContextChanged += (s, e) => Bindings.Update();
        }

        ///// <summary>
        ///// Gets the family details list.
        ///// </summary>
        ///// <value>
        ///// The family details list.
        ///// </value>
        //public CardListLineCollection FamilyDetailsList
        //{
        //    get
        //    {
        //        if (ViewModel == null)
        //        {
        //            return new CardListLineCollection();
        //        }
        //        else
        //        {
        //            CardListLineCollection t =
        //             new CardListLineCollection
        //            {
        //            new CardListLine("Mother:", ViewModel.GMother.DeRef.GPersonNamesCollection.GetPrimaryName.FullName),
        //            new CardListLine("Father:", ViewModel.GFather.DeRef.GPersonNamesCollection.GetPrimaryName.FullName),
        //            new CardListLine("Relationship:", ViewModel.GFamilyRelationship),
        //            };

        // EventModel tt = DV.EventDV.GetEventType(ViewModel.GEventRefCollection, GrampsView.Common.CommonConstants.EventTypeMarriage);

        // if (!(tt is null)) { t.Add(new CardListLine("Years Ago:", tt.GEventDate.GetAge)); }

        //            return t;
        //        }
        //    }
        //}

        ///// <summary>
        ///// Gets the ViewModel.
        ///// </summary>
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