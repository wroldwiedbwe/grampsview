namespace GrampsView.UserControls
{
    using GrampsView.Common;

    using System.Collections;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;

    using Xamarin.Forms;
    using Xamarin.Forms.Internals;

    public partial class FlexMultiCardEnum : Frame
    {
        public static readonly BindableProperty UconSourceProperty
                 = BindableProperty.Create(returnType: typeof(IEnumerator), declaringType: typeof(FlexMultiCardEnum), propertyChanged: OnItemsSourceChanged, propertyName: nameof(UconSource));

        private bool UconSourceAtEnd;

        public FlexMultiCardEnum()
        {
            InitializeComponent();

            UconSourceAtEnd = false;
        }

        public ObservableCollection<CardGroupCollection> DisplayMultiList { get; } = new ObservableCollection<CardGroupCollection>();

        public IEnumerator UconSource
        {
            get { return (IEnumerator)GetValue(UconSourceProperty); }
            set { SetValue(UconSourceProperty, value); }
        }

        public bool UconVisible
        {
            get
            {
                if (UconSource is null && UconSourceAtEnd)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        private static View CreateChildView(CardGroupCollection item)
        {
            Application.Current.Resources.TryGetValue("CardGroupTemplate", out var cardGroupTemplate);

            DataTemplate t = cardGroupTemplate as DataTemplate;

            View view = (View)t.CreateContent(item, null);

            view.BindingContext = item;

            return view;
        }

        private static void OnItemsSourceChanged(BindableObject obj, object oldValue, object newValue)
        {
            var layout = obj as FlexMultiCardEnum;

            // Register for items changed
            if (newValue is INotifyCollectionChanged observableCollection)
            {
                observableCollection.CollectionChanged += layout.OnItemsSourceCollectionChanged;
            }

            layout.DisplayMultiList.CollectionChanged += layout.OnDisplayListCollectionChanged;

            // Layout out children
            if (layout?.UconSource != null)
            {
                layout.AddToDisplay();

                layout.BuildLayout();
            }
        }

        private void AddToDisplay()
        {
            //Debug.WriteLine("GetIt");

            //foreach (CardGroupCollection item in UconSource)
            //{
            //    DisplayMultiList.Add(item);
            //}
        }

        private void BuildLayout()
        {
            this.multiflexer.Children.Clear();

            foreach (CardGroupCollection item in DisplayMultiList)
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
                    CardGroupCollection item = e.NewItems[i] as CardGroupCollection;
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
                    CardGroupCollection item = e.NewItems[i] as CardGroupCollection;

                    DisplayMultiList.Add(item);
                }
            }
        }
    }
}