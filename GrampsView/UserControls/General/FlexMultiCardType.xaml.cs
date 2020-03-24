namespace GrampsView.UserControls
{
    using GrampsView.Common;
    using GrampsView.Data.Model;

    using System.Collections.ObjectModel;
    using System.Collections.Specialized;

    using Xamarin.Forms;
    using Xamarin.Forms.Internals;

    public partial class FlexMultiCardType : Frame
    {
        public static readonly BindableProperty FMultiSourceProperty
                 = BindableProperty.Create(returnType: typeof(CardGroupCollection), declaringType: typeof(FlexMultiCardType), propertyChanged: OnItemsSourceChanged, propertyName: nameof(FMultiSource));

        public FlexMultiCardType()
        {
            InitializeComponent();
        }

        public ObservableCollection<CardGroup> DisplayMultiList { get; } = new ObservableCollection<CardGroup>();

        public bool FlexMultiCardVisible
        {
            get
            {
                if (!(FMultiSource is null) && (FMultiSource.Count > 0))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public CardGroupCollection FMultiSource
        {
            get { return (CardGroupCollection)GetValue(FMultiSourceProperty); }
            set { SetValue(FMultiSourceProperty, value); }
        }

        private static View CreateChildView(CardGroup item)
        {
            Application.Current.Resources.TryGetValue("CardGroupTemplate", out var cardGroupTemplate);

            DataTemplate t = cardGroupTemplate as DataTemplate;

            View view = (View)t.CreateContent(item, null);

            view.BindingContext = item;

            return view;
        }

        private static void OnItemsSourceChanged(BindableObject obj, object oldValue, object newValue)
        {
            var layout = obj as FlexMultiCardType;

            // Register for items changed
            if (newValue is INotifyCollectionChanged observableCollection)
            {
                observableCollection.CollectionChanged += layout.OnItemsSourceCollectionChanged;
            }

            layout.DisplayMultiList.CollectionChanged += layout.OnDisplayListCollectionChanged;

            // Layout out children
            if (layout?.FMultiSource != null)
            {
                layout.AddToDisplay();

                layout.BuildLayout();
            }
        }

        private void AddToDisplay()
        {
            //Debug.WriteLine("GetIt");

            foreach (CardGroup item in FMultiSource)
            {
                DisplayMultiList.Add(item);
            }
        }

        private void BuildLayout()
        {
            this.multiflexer.Children.Clear();

            foreach (CardGroup item in DisplayMultiList)
            {
                this.multiflexer.Children.Add(CreateChildView(item));
            }
        }

        private void OnDisplayListCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                // Items cleared
                this.multiflexer.Children.Clear();
            }

            if (e.OldItems != null)
            {
                // Items removed
                this.multiflexer.Children.RemoveAt(e.OldStartingIndex);
            }

            if (e.NewItems != null)
            {
                // Item(s) added.
                for (int i = 0; i < e.NewItems.Count; i++)
                {
                    CardGroup item = e.NewItems[i] as CardGroup;
                    View view = CreateChildView(item);
                    this.multiflexer.Children.Insert(e.NewStartingIndex + i, view);
                }
            }
        }

        private void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                // Items cleared
                this.DisplayMultiList.Clear();
            }

            if (e.OldItems != null)
            {
                // Items removed
                this.DisplayMultiList.RemoveAt(e.OldStartingIndex);
            }

            if (e.NewItems != null)
            {
                // Item(s) added.
                for (int i = 0; i < e.NewItems.Count; i++)
                {
                    CardGroup item = e.NewItems[i] as CardGroup;

                    DisplayMultiList.Add(item);
                }
            }
        }
    }
}