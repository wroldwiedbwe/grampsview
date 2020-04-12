namespace GrampsView.UserControls
{
    using GrampsView.Common;

    using System.Collections.Specialized;
    using System.Linq;

    using Xamarin.Forms;

    public partial class FlexSingleGroupMultiCard : Frame
    {
        public static readonly BindableProperty FMultiSourceProperty = BindableProperty.Create(returnType: typeof(CardGroup), declaringType: typeof(FlexSingleGroupMultiCard), propertyChanged: OnItemsSourceChanged, propertyName: nameof(FMultiSource));

        public static readonly BindableProperty FsctTemplateProperty = BindableProperty.Create(nameof(FsctTemplate), typeof(DataTemplateSelector), typeof(FlexSingleGroupMultiCard), propertyChanged: OnItemTemplateChanged);

        private static int startItemGet = 0;
        private static int virtualItemGet = 5;

        public FlexSingleGroupMultiCard()
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

        public CardGroup DisplayList { get; set; } = new CardGroup();

        public bool FlexSingleCardVisible
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

        public DataTemplateSelector FsctTemplate
        {
            get { return (DataTemplateSelector)GetValue(FsctTemplateProperty); }
            set { SetValue(FsctTemplateProperty, value); }
        }

        public int IndexLength { get; set; }

        public int IndexStart { get; set; } = 0;

        private CardGroup ActualDisplayList { get; set; } = new CardGroup();

        // Gota start with enough so scrollbar is visible on the desktop
        private static void OnItemsSourceChanged(BindableObject obj, object oldValue, object newValue)
        {
            var layout = obj as FlexSingleGroupMultiCard;

            // Register for items changed
            if (newValue is INotifyCollectionChanged observableCollection)
            {
                observableCollection.CollectionChanged += layout.OnItemsSourceCollectionChanged;
            }

            // layout.ActualDisplayList.CollectionChanged += layout.OnActualDisplayListCollectionChanged;

            // Layout out children
            if (layout?.FMultiSource != null)
            {
                layout.IndexStart = 0;
                layout.IndexLength = startItemGet;
            }
        }

        private static void OnItemTemplateChanged(BindableObject obj, object oldValue, object newValue)
        {
            var layout = obj as FlexSingleGroupMultiCard;

            if (layout?.FMultiSource != null && layout?.FsctTemplate != null)

                layout.BuildLayout();
        }

        private void AddToDisplay()
        {
            //Debug.WriteLine("GetIt");

            if (DisplayList.Cards.Count == FMultiSource.Cards.Count)
            {
                return;
            }

            foreach (var item in ActualDisplayList.Cards.Skip(IndexStart).Take(IndexLength).ToList())
            {
                DisplayList.Add(item);
                this.flexer.Children.Add(CreateChildView(item));
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

            this.flexer.Children.Clear();

            foreach (var item in DisplayList.Cards)
            {
                this.flexer.Children.Add(CreateChildView(item));
            }
        }

        private View CreateChildView(object item)
        {
            var dts = FsctTemplate as DataTemplateSelector;
            var itemTemplate = dts.SelectTemplate(item, null);
            itemTemplate.SetValue(BindableObject.BindingContextProperty, item);
            return (View)itemTemplate.CreateContent();
        }

        private void OnActualDisplayListCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                // Items cleared
                this.flexer.Children.Clear();
            }

            if (e.OldItems != null)
            {
                // Items removed
                this.flexer.Children.RemoveAt(e.OldStartingIndex);
            }

            if (e.NewItems != null)
            {
                foreach (CardGroup argCardGroup in e.NewItems)
                {
                    // Item(s) added.
                    foreach (var item in argCardGroup.Cards)
                    {
                        ActualDisplayList.Add(item);
                    }
                }

                AddToDisplay();
            }
        }

        private void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                // Items cleared
                this.DisplayList.Clear();
                this.ActualDisplayList.Clear();
            }

            if (e.OldItems != null)
            {
                // Items removed this.DisplayList..Children.RemoveAt(e.OldStartingIndex);
            }

            if (e.NewItems != null)
            {
                foreach (CardGroup argCardGroup in e.NewItems)
                {
                    // Item(s) added.
                    foreach (var item in argCardGroup.Cards)
                    {
                        ActualDisplayList.Add(item);
                    }
                }

                AddToDisplay();

                BuildLayout();
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