namespace GrampsView.UserControls
{
    using GrampsView.Common;

    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;

    using Xamarin.Forms;

    public partial class FlexSingleCardGroup : Frame
    {
        public const int virtualItemGet = 10;

        public static readonly BindableProperty FSCGSourceProperty
                 = BindableProperty.Create(returnType: typeof(CardGroup), declaringType: typeof(FlexSingleCardGroup), propertyChanged: OnItemsSourceChanged, propertyName: nameof(FSCGSource));

        public static readonly BindableProperty FSCGTemplateProperty
            = BindableProperty.Create(nameof(FSCGTemplate), typeof(DataTemplate), typeof(FlexSingleCardGroup), propertyChanged: OnItemTemplateChanged);

        private static int startItemGet = 0;

        public FlexSingleCardGroup()
        {
            InitializeComponent();

            // Get start length
            if (Xamarin.Forms.Device.Idiom == TargetIdiom.Phone)
            {
                startItemGet = 20;
            }
            else
            {
                startItemGet = 100;
            }

            IndexLength = startItemGet;
        }

        public ObservableCollection<object> DisplayList { get; set; } = new ObservableCollection<object>();

        public CardGroup FSCGSource
        {
            get { return (CardGroup)GetValue(FSCGSourceProperty); }
            set { SetValue(FSCGSourceProperty, value); }
        }

        public DataTemplate FSCGTemplate
        {
            get { return (DataTemplate)GetValue(FSCGTemplateProperty); }
            set { SetValue(FSCGTemplateProperty, value); }
        }

        public int IndexLength { get; set; }  // Gota start with enough so scrollbar is visible on the desktop

        public int IndexStart { get; set; } = 0;

        private static void OnItemsSourceChanged(BindableObject obj, object oldValue, object newValue)
        {
            var layout = obj as FlexSingleCardGroup;

            // Register for items changed
            if (newValue is INotifyCollectionChanged observableCollection)
            {
                observableCollection.CollectionChanged += layout.OnItemsSourceCollectionChanged;
            }

            layout.DisplayList.CollectionChanged += layout.OnDisplayListCollectionChanged;

            // Layout out children
            if (layout?.FSCGSource != null && layout?.FSCGTemplate != null)
            {
                layout.IndexStart = 0;
                layout.IndexLength = startItemGet;

                layout.AddToDisplay();

                layout.BuildLayout();
            }
        }

        private static void OnItemTemplateChanged(BindableObject obj, object oldValue, object newValue)
        {
            var layout = obj as FlexSingleCardGroup;

            if (layout?.FSCGSource != null && layout?.FSCGTemplate != null)

                layout.BuildLayout();
        }

        private void AddToDisplay()
        {
            //Debug.WriteLine("GetIt");

            if (DisplayList.Count == FSCGSource.Cards.Count)
            {
                return;
            }

            foreach (var item in FSCGSource.Cards.Skip(IndexStart).Take(IndexLength).ToList())
            {
                DisplayList.Add(item);
            }

            IndexStart = IndexStart + IndexLength;
            IndexLength = virtualItemGet;
        }

        private void BuildLayout()
        {
            this.flextitle.Text = FSCGSource.Title;

            this.flexer.Children.Clear();

            foreach (var item in DisplayList)
            {
                View view = null;

                view = (View)FSCGTemplate.CreateContent();

                view.BindingContext = item;
                this.flexer.Children.Add(view);
            }
        }

        private View CreateChildView(object item)
        {
            if (FSCGTemplate is DataTemplateSelector)
            {
                var dts = FSCGTemplate as DataTemplateSelector;
                var itemTemplate = dts.SelectTemplate(item, null);
                itemTemplate.SetValue(BindableObject.BindingContextProperty, item);
                return (View)itemTemplate.CreateContent();
            }
            else
            {
                FSCGTemplate.SetValue(BindableObject.BindingContextProperty, item);
                return (View)FSCGTemplate.CreateContent();
            }
        }

        private void OnDisplayListCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
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
                // Item(s) added.
                for (int i = 0; i < e.NewItems.Count; i++)
                {
                    var item = e.NewItems[i];
                    var view = CreateChildView(item);
                    this.flexer.Children.Insert(e.NewStartingIndex + i, view);
                }
            }
        }

        private void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                // Items cleared
                this.DisplayList.Clear();
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

                    this.DisplayList.Add(item);
                }
            }
        }

        private void Scroller_Scrolled(object sender, ScrolledEventArgs e)
        {
            var t = sender as ScrollView;

            //Debug.WriteLine(t.ScrollY);
            //Debug.WriteLine(t.ContentSize.Height);

            if (t.ContentSize.Height - t.Height <= (e.ScrollY + 10))
            {
                AddToDisplay();
            }
        }
    }
}