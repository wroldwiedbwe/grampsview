namespace GrampsView.UserControls
{
    using GrampsView.Common;

    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using Xamarin.Forms;

    /// <summary>
    /// Base class for the Flex User Controls.
    /// </summary>
    public partial class FlexBase : Frame, INotifyPropertyChanged
    {
        internal static int startItemGet = 0;

        internal static int virtualItemGet = 5;

        private bool _Visible = false;

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

        public CardGroupBase<object> DisplayList { get; } = new CardGroupBase<object>();

        public virtual CardGroup FsctSource { get; set; }

        public virtual DataTemplate FsctTemplate { get; set; }

        public int IndexLength { get; set; }  // Gota start with enough so scrollbar is visible on the desktop

        public int IndexStart { get; set; } = 0;

        public bool Visible
        {
            get
            {
                return _Visible;
            }
            set
            {
                _Visible = value;
                OnPropertyChanged(nameof(Visible));
            }
        }

        //private void FlexOnPropertyChanged<T>([CallerMemberName]string caller = null)
        //{
        //    // make sure only to call this if the value actually changes

        //    FlexBasePropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        //}

        //public event PropertyChangedEventHandler FlexBasePropertyChanged;

        public static void OnItemsSourceChanged(BindableObject argSource, object oldValue, object newValue)
        {
            var layout = argSource as FlexBase;

            // Register for items changed
            if (newValue is INotifyCollectionChanged observableCollection)
            {
                observableCollection.CollectionChanged += layout.OnItemsSourceCollectionChanged;
            }

            // Layout out children
            if (layout?.FsctSource != null)
            {
                layout.IndexStart = 0;
                layout.IndexLength = startItemGet;

                layout.AddToDisplay();

                layout.BuildLayout();
            }
        }

        public static void OnItemTemplateChanged(BindableObject argSource, object oldValue, object newValue)
        {
            var layout = argSource as FlexBase;

            if (layout?.FsctSource != null && layout?.FsctTemplate != null)

                layout.BuildLayout();
        }

        public void AddToDisplay()
        {
            //Debug.WriteLine("GetIt");

            if (DisplayList.Count == FsctSource.Count)
            {
                return;
            }

            foreach (var item in FsctSource.Skip(IndexStart).Take(IndexLength).ToList())
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
                IndexStart = 0;

                BuildLayout();
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

            Visible = (!(FsctSource is null) && (FsctSource.Count > 0)) ? true : false;
        }

        /// <summary>
        /// Handles the Scrolled event of the Scroller control.
        /// </summary>
        /// <param name="argSender">
        /// The source of the event.
        /// </param>
        /// <param name="argEventArgs">
        /// The <see cref="ScrolledEventArgs"/> instance containing the event data.
        /// </param>
        public void Scroller_Scrolled(object argSender, ScrolledEventArgs argEventArgs)
        {
            if (argEventArgs is null)
            {
                return;
            }

            ScrollView t = argSender as ScrollView;

            if (!(t is null))
            {
                if (t.ContentSize.Height - t.Height <= (argEventArgs.ScrollY + 20))
                {
                    int currentEnd = IndexStart;
                    AddToDisplay();
                    AddToLayout(currentEnd);
                }
            }
        }
    }
}