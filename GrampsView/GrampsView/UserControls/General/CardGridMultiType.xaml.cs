// <copyright file="CardGridMultiType.xaml.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

namespace GrampsView.UserControls
{
    using System.Collections.Specialized;

    using Xamarin.Forms;

    public partial class CardGridMultiType : ContentPage
    {
        ///// <summary>
        ///// The control item template property.
        ///// </summary>
        //public static readonly DependencyProperty ControlItemSelectorProperty =
        //    DependencyProperty.Register("ControlItemSelector", typeof(DataTemplateSelector), typeof(CardGridMultiType), null);

        ///// <summary>
        ///// The control items source property.
        ///// </summary>
        //public static readonly DependencyProperty ControlItemSourceProperty =
        //    DependencyProperty.Register("ControlItemSource", typeof(CardGroupCollection), typeof(CardGridMultiType), new PropertyMetadata(null, new PropertyChangedCallback(DevStatesPropertyChangedCallBack)));

        /// <summary>
        /// Initializes a new instance of the <see cref="CardGridMultiType" /> class.
        /// </summary>
        public CardGridMultiType()
        {
            InitializeComponent();

            //DataContextChanged += (s, e) => Bindings.Update();

            //ControlItemSource = new CardGroupCollection();
        }

        ///// <summary>
        ///// Gets or sets the control item template.
        ///// </summary>
        ///// <value>
        ///// The control item template.
        ///// </value>
        //public DataTemplateSelector ControlItemSelectorTemplate
        //{
        //    get { return (DataTemplateSelector)GetValue(ControlItemSelectorProperty); }
        //    set { SetValue(ControlItemSelectorProperty, value); }
        //}

        ///// <summary>
        ///// Gets or sets the items source.
        ///// </summary>
        ///// <value>
        ///// The items source.
        ///// </value>
        //public CardGroupCollection ControlItemSource
        //{
        //    get { return (CardGroupCollection)GetValue(ControlItemSourceProperty); }
        //    set { SetValue(ControlItemSourceProperty, value); }
        //}

        ///// <summary>
        ///// Gets the control visible.
        ///// </summary>
        ///// <value>
        ///// The control visible.
        ///// </value>
        //public Visibility ControlVisible
        //{
        //    get
        //    {
        //        if (ControlItemSource == null)
        //        {
        //            return Visibility.Collapsed;
        //        }

        //        return ControlItemSource.ControlVisible;
        //    }
        //}

        private static void DevStates_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
        }

        // private static void DevStatesPropertyChangedCallBack(DependencyObject d,
        // DependencyPropertyChangedEventArgs value) { var instance = d as object; if (instance ==
        // null) { return; }

        // var newCollection = value.NewValue as CardGroupCollection; var oldCollection =
        // value.OldValue as CardGroupCollection;

        // if (newCollection != null) { newCollection.CollectionChanged += new
        // System.Collections.Specialized.NotifyCollectionChangedEventHandler(DevStates_CollectionChanged); }

        // if (oldCollection != null) { oldCollection.CollectionChanged -= new
        // System.Collections.Specialized.NotifyCollectionChangedEventHandler(DevStates_CollectionChanged);
        // } }
    }
}