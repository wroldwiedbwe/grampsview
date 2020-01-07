namespace GrampsView.UserControls
{
    using GrampsView.Data.Model;

    using System.Collections.Specialized;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListLineCardLarge : Frame
    {
        public static readonly BindableProperty UCCardListLineColProperty
             = BindableProperty.Create(returnType: typeof(CardListLineCollection), declaringType: typeof(ListLineCardLarge), propertyChanged: HandleVMPropertyChanged, propertyName: nameof(UCCardListLineCol));

        public ListLineCardLarge()
        {
            InitializeComponent();
        }

#pragma warning disable CA2227 // Collection properties should be read only

        public CardListLineCollection UCCardListLineCol
#pragma warning restore CA2227 // Collection properties should be read only
        {
            get
            {
                return (CardListLineCollection)base.GetValue(UCCardListLineColProperty);
            }

            set
            {
                if (this.UCCardListLineCol != value)
                {
                    base.SetValue(UCCardListLineColProperty, value);
                }
            }
        }

        private static void HandleVMPropertyChanged(
                          BindableObject bindable, object oldValue, object newValue)
        {
            // Get values as proper objects
            ListLineCardLarge thisLayout = bindable as ListLineCardLarge;

            if (thisLayout is null)
            {
                return;
            }

            CardListLineCollection newCardListLines = newValue as CardListLineCollection;

            if (newCardListLines is null)
            {
                return;
            }

            // Register for items changed
            if (newValue is CardListLineCollection observableCollection)
            {
                observableCollection.CollectionChanged += thisLayout.OnItemsSourceCollectionChanged;
            }

            thisLayout.largeDetailList.ItemsSource = newCardListLines;

            // Turn off Header if not set
            if (newCardListLines.Title is null)
            {
                thisLayout.LLCardLargeFrame.IsVisible = false;
            }
            else
            {
                thisLayout.LLCardLargeTitle.Text = newCardListLines.Title;

                thisLayout.LLCardLargeFrame.IsVisible = true;
            }
        }

        private void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                // Items cleared
                //this.DisplayList.Clear();
            }

            if (e.OldItems != null)
            {
                // Items removed this.DisplayList.RemoveAt(e.OldStartingIndex);
            }

            if (e.NewItems != null)
            {
                CardListLineCollection t = e.NewItems[0] as CardListLineCollection;

                //CardTitle = t.Title;

                //foreach (CardListLine item in t)
                //{
                //    DisplayList.Add(item);
                //}
            }
        }
    }
}