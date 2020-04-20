namespace GrampsView.UserControls
{
    using GrampsView.Common;

    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;

    using Xamarin.Forms;
    using Xamarin.Forms.Internals;

    public partial class FlexSingleGroupSingleCard : Frame
    {
        public static readonly BindableProperty FsctSourceProperty
                 = BindableProperty.Create(returnType: typeof(CardGroup), declaringType: typeof(FlexSingleGroupSingleCard), propertyChanged: OnItemsSourceChanged, propertyName: nameof(FsctSource));

        public static readonly BindableProperty FsctTemplateProperty
            = BindableProperty.Create(nameof(FsctTemplate), typeof(DataTemplate), typeof(FlexSingleGroupSingleCard), propertyChanged: OnItemTemplateChanged);

        private static int startItemGet = 0;

        private static int virtualItemGet = 5;

        public FlexSingleGroupSingleCard()
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

        public ObservableCollection<object> DisplayList { get; } = new ObservableCollection<object>();

        public bool FlexSingleCardVisible
        {
            get
            {
                if (!(FsctSource is null) && (FsctSource.Cards.Count > 0))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public CardGroup FsctSource
        {
            get { return (CardGroup)GetValue(FsctSourceProperty); }
            set { SetValue(FsctSourceProperty, value); }
        }

        public DataTemplate FsctTemplate
        {
            get { return (DataTemplate)GetValue(FsctTemplateProperty); }
            set { SetValue(FsctTemplateProperty, value); }
        }

        public int IndexLength { get; set; }  // Gota start with enough so scrollbar is visible on the desktop

        public int IndexStart { get; set; } = 0;

        private static void OnItemsSourceChanged(BindableObject obj, object oldValue, object newValue)
        {
            var layout = obj as FlexSingleGroupSingleCard;

            // Register for items changed
            if (newValue is INotifyCollectionChanged observableCollection)
            {
                observableCollection.CollectionChanged += layout.OnItemsSourceCollectionChanged;
            }

            // Layout out children
            if (layout?.FsctSource != null && layout?.FsctTemplate != null)
            {
                layout.IndexStart = 0;
                layout.IndexLength = startItemGet;

                layout.AddToDisplay();

                layout.BuildLayout();
            }
        }

        private static void OnItemTemplateChanged(BindableObject obj, object oldValue, object newValue)
        {
            var layout = obj as FlexSingleGroupSingleCard;

            if (layout?.FsctSource != null && layout?.FsctTemplate != null)

                layout.BuildLayout();
        }

        private void AddToDisplay()
        {
            //Debug.WriteLine("GetIt");

            if (DisplayList.Count == FsctSource.Cards.Count)
            {
                return;
            }

            foreach (var item in FsctSource.Cards.Skip(IndexStart).Take(IndexLength).ToList())
            {
                DisplayList.Add(item);
                IndexStart += 1;
            }

            IndexLength = virtualItemGet;
        }

        private void BuildLayout()
        {
            if (string.IsNullOrWhiteSpace(FsctSource.Title))
            {
                this.flextitle.IsVisible = false;
            }
            else
            {
                this.flextitle.Text = FsctSource.Title;
                this.flextitle.IsVisible = true;
            }

            this.flexer.Children.Clear();

            foreach (var item in DisplayList)
            {
                View view;

                if (FsctTemplate is DataTemplateSelector)
                {
                    view = (View)FsctTemplate.CreateContent(item, null);
                }
                else
                {
                    view = (View)FsctTemplate.CreateContent();
                }

                view.BindingContext = item;
                this.flexer.Children.Add(view);
            }
        }

        private void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                // Items cleared TODO Should this be FSource?
                this.DisplayList.Clear();
            }

            if (e.OldItems != null)
            {
                // TODO Handle this Items removed this.DisplayList..Children.RemoveAt(e.OldStartingIndex);
            }

            if (e.NewItems != null)
            {
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