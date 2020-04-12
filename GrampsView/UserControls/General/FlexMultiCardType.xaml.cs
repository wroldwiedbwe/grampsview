namespace GrampsView.UserControls
{
    using GrampsView.Common;

    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;

    using Xamarin.Forms;
    using Xamarin.Forms.Internals;

    public partial class FlexMultiCardType : Frame
    {
        public static readonly BindableProperty FMultiSourceProperty
                   = BindableProperty.Create(returnType: typeof(CardGroup), declaringType: typeof(FlexMultiCardType), propertyChanged: OnItemsSourceChanged, propertyName: nameof(FMultiSource));

        private static int startItemGet = 0;
        private static int virtualItemGet = 5;

        public FlexMultiCardType()
        {
            InitializeComponent();

            // Get start length
            if (Device.Idiom == TargetIdiom.Phone)
            {
                startItemGet = 20;
                virtualItemGet = 20;
            }
            else
            {
                startItemGet = 120;
                virtualItemGet = 60;
            }

            IndexLength = startItemGet;
        }

        public ObservableCollection<object> DisplayMultiList { get; set; } = new ObservableCollection<object>();

        public bool FlexMultiCardVisible
        {
            get
            {
                if (!(FMultiSource is null) && (FMultiSource.Cards.Count > 0))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public CardGroup FMultiSource
        {
            get { return (CardGroup)GetValue(FMultiSourceProperty); }
            set { SetValue(FMultiSourceProperty, value); }
        }

        public int IndexLength { get; set; }  // Gota start with enough so scrollbar is visible on the desktop

        public int IndexStart { get; set; } = 0;

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
                layout.IndexStart = 0;
                layout.IndexLength = startItemGet;

                layout.AddToDisplay();

                layout.BuildLayout();
            }
        }

        private void AddToDisplay()
        {
            //Debug.WriteLine("GetIt");

            if (DisplayMultiList.Count == FMultiSource.Cards.Count)
            {
                return;
            }

            foreach (var item in FMultiSource.Cards.Skip(IndexStart).Take(IndexLength).ToList())
            {
                DisplayMultiList.Add(item);
            }

            IndexStart = IndexStart + IndexLength;
            IndexLength = virtualItemGet;
        }

        private void BuildLayout()
        {
            if (string.IsNullOrWhiteSpace(FMultiSource.Title))
            {
                this.flextitle.IsVisible = false;
            }
            else
            {
                this.flextitle.Text = FMultiSource.Title;
                this.flextitle.IsVisible = true;
            }

            this.multiflexer.Children.Clear();

            foreach (CardGroup item in DisplayMultiList)
            {
                this.multiflexer.Children.Add(CreateChildView(item));
            }
        }

        private View CreateChildView(object item)
        {
            Application.Current.Resources.TryGetValue("CardGroupTemplate", out var cardGroupTemplate);

            DataTemplate t = cardGroupTemplate as DataTemplate;

            View view = (View)t.CreateContent(item, null);

            view.BindingContext = item;

            return view;
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
                    var item = e.NewItems[i];
                    var view = CreateChildView(item);
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
                // Items removed this.DisplayList..Children.RemoveAt(e.OldStartingIndex);
            }

            if (e.NewItems != null)
            {
                // Item(s) added.
                for (int i = 0; i < e.NewItems.Count; i++)
                {
                    var item = e.NewItems[i];

                    this.DisplayMultiList.Add(item);
                }
            }
        }

        /// <summary>
        /// Handles the Scrolled event of the Scroller control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="ScrolledEventArgs"/> instance containing the event data.
        /// </param>
        private void Scroller_Scrolled(object sender, ScrolledEventArgs e)
        {
            var t = sender as ScrollView;

            if (t.ContentSize.Height - t.Height <= (e.ScrollY + 10))
            {
                AddToDisplay();
            }
        }
    }
}