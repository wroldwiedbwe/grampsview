// <copyright file="EventCardLarge.xaml.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

/// <summary>
/// </summary>
namespace GrampsView.UserControls
{
    using Xamarin.Forms;

    /// <summary>
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.UserControl" />
    /// /// /// /// ///
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// /// /// /// ///
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public partial class EventCardLarge : Frame
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventCardLarge" /> class.
        /// </summary>
        public EventCardLarge()
        {
            InitializeComponent();

            //DataContextChanged += (s, e) => Bindings.Update();
        }

        //public CardListLineCollection EventDetailsList
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
        //            new CardListLine("Date:", ViewModel.GEventDate.GetShortDateAsString),
        //            new CardListLine("Type:", ViewModel.GType),
        //            new CardListLine("Description:", ViewModel.GDescription),
        //            new CardListLine("Years Ago:", ViewModel.GEventDate.GetAge),
        //        };
        //        }
        //    }
        //}

        ///// <summary>
        ///// Gets. </summary>
        ///// <value>
        ///// The ViewModel.
        ///// </value>
        //public EventModel ViewModel
        //{
        //    get
        //    {
        //        return (EventModel)DataContext;
        //    }
        //}
    }
}