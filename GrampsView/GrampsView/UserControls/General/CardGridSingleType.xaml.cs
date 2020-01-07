// <copyright file="CardGridSingleType.xaml.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

/// <summary>
/// Code behind for Card grid containing a single type of card in a fixed layout
/// </summary>
/// <copyright file="CardGridSingleType.xaml.cs" company="MeMyselfAndI">
/// </copyright>

namespace GrampsView.UserControls
{
    using Xamarin.Forms;

    /// <summary>
    /// Card grid containing a single type of card in a fixed layout.
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.UserControl" />
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public partial class CardGridSingleType : ContentPage // , ISemanticZoomInformation
    {
        ///// <summary>
        ///// The control header text property.
        ///// </summary>
        //public static readonly DependencyProperty ControlHeaderTextProperty =
        //            DependencyProperty.Register("ControlHeaderText", typeof(string), typeof(CardGridSingleType), new PropertyMetadata(string.Empty));

        ///// <summary>
        ///// The control items source property.
        ///// </summary>
        //public static readonly DependencyProperty ControlItemSourceProperty =
        //    DependencyProperty.Register("ControlItemSource", typeof(object), typeof(CardGridSingleType), null);

        ///// <summary>
        ///// The control item template property.
        ///// </summary>
        //public static readonly DependencyProperty ControlItemTemplateProperty =
        //    DependencyProperty.Register("ControlItemTemplate", typeof(DataTemplate), typeof(CardGridSingleType), null);

        /// <summary>
        /// Initializes a new instance of the <see cref="CardGridSingleType" /> class.
        /// </summary>
        public CardGridSingleType()
        {
            InitializeComponent();

            //DataContextChanged += (s, e) => Bindings.Update();
        }

        /// <summary>
        /// Gets or sets the control header text.
        /// </summary>
        /// <value>
        /// The control header text.
        /// </value>
        //public string ControlHeaderText
        //{
        //    get { return (string)GetValue(ControlHeaderTextProperty); }
        //    set { SetValue(ControlHeaderTextProperty, value); }
        //}

        ///// <summary>
        ///// Gets the control header text visible.
        ///// </summary>
        ///// <value>
        ///// The control header text visible.
        ///// </value>
        //public Visibility ControlHeaderTextVisible
        //{
        //    get
        //    {
        //        if (!string.IsNullOrEmpty(ControlHeaderText))
        //        {
        //            return Visibility.Visible;
        //        }

        //        return Visibility.Collapsed;
        //    }
        //}

        ///// <summary>
        ///// Gets or sets the items source.
        ///// </summary>
        ///// <value>
        ///// The items source.
        ///// </value>
        //public object ControlItemSource
        //{
        //    get { return (object)GetValue(ControlItemSourceProperty); }
        //    set { SetValue(ControlItemSourceProperty, value); }
        //}

        ///// <summary>
        ///// Gets or sets the control item template.
        ///// </summary>
        ///// <value>
        ///// The control item template.
        ///// </value>
        //public DataTemplate ControlItemTemplate
        //{
        //    get { return (DataTemplate)GetValue(ControlItemTemplateProperty); }
        //    set { SetValue(ControlItemTemplateProperty, value); }
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
        //        else
        //        {
        //            if ((ControlItemSource as IEnumerable<object>).Count() > 0)
        //            {
        //                return Visibility.Visible;
        //            }
        //            else
        //            {
        //                return Visibility.Collapsed;
        //            }
        //        }
        //    }
        //}

        //public bool IsActiveView { get => ((ISemanticZoomInformation)ListV).IsActiveView; set => ((ISemanticZoomInformation)ListV).IsActiveView = value; }

        //public bool IsZoomedInView { get => ((ISemanticZoomInformation)ListV).IsZoomedInView; set => ((ISemanticZoomInformation)ListV).IsZoomedInView = value; }

        //public SemanticZoom SemanticZoomOwner { get => ((ISemanticZoomInformation)ListV).SemanticZoomOwner; set => ((ISemanticZoomInformation)ListV).SemanticZoomOwner = value; }

        //public void CompleteViewChange() => ((ISemanticZoomInformation)ListV).CompleteViewChange();

        //public void CompleteViewChangeFrom(SemanticZoomLocation source, SemanticZoomLocation destination) => ((ISemanticZoomInformation)ListV).CompleteViewChangeFrom(source, destination);

        //public void CompleteViewChangeTo(SemanticZoomLocation source, SemanticZoomLocation destination) => ((ISemanticZoomInformation)ListV).CompleteViewChangeTo(source, destination);

        //public void InitializeViewChange() => ((ISemanticZoomInformation)ListV).InitializeViewChange();

        //public void MakeVisible(SemanticZoomLocation item) => ((ISemanticZoomInformation)ListV).MakeVisible(item);

        //public void StartViewChangeFrom(SemanticZoomLocation source, SemanticZoomLocation destination) => ((ISemanticZoomInformation)ListV).StartViewChangeFrom(source, destination);

        //public void StartViewChangeTo(SemanticZoomLocation source, SemanticZoomLocation destination) => ((ISemanticZoomInformation)ListV).StartViewChangeTo(source, destination);
    }
}