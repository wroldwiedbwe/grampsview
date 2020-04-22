namespace GrampsView.UserControls
{
    using GrampsView.Common;

    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;

    using Xamarin.Forms;
    using Xamarin.Forms.Internals;

    public partial class FlexBase : Frame
    {
        public static int startItemGet = 0;

        public static int virtualItemGet = 5;

        public FlexBase()
        {
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

        public virtual CardGroup FsctSource { get; set; }

        public virtual DataTemplate FsctTemplate { get; set; }

        public int IndexLength { get; set; }  // Gota start with enough so scrollbar is visible on the desktop

        public int IndexStart { get; set; } = 0;

        public static void OnItemsSourceChanged(BindableObject obj, object oldValue, object newValue)
        {
            var layout = obj as FlexBase;

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

        public static void OnItemTemplateChanged(BindableObject obj, object oldValue, object newValue)
        {
            var layout = obj as FlexBase;

            if (layout?.FsctSource != null && layout?.FsctTemplate != null)

                layout.BuildLayout();
        }

        public void AddToDisplay()
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

        public virtual void AddToLayout(int argStartIndex)
        {
        }

        public virtual void BuildLayout()
        {
        }

        public void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
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
        public void Scroller_Scrolled(object sender, ScrolledEventArgs e)
        {
            var t = sender as ScrollView;

            if (t.ContentSize.Height - t.Height <= (e.ScrollY + 10))
            {
                int currentEnd = IndexStart;
                AddToDisplay();
                AddToLayout(currentEnd);
            }
        }
    }
}