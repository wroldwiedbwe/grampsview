namespace GrampsView.UserControls
{
    using GrampsView.Common;

    using System;
    using System.Collections;
    using System.ComponentModel;
    using Xamarin.Forms;

    public partial class CollectionSingleCard : Frame, INotifyPropertyChanged
    {
        public static readonly BindableProperty FsctSourceProperty
              = BindableProperty.Create(returnType: typeof(IEnumerable), declaringType: typeof(CollectionSingleCard), propertyName: nameof(FsctSource), propertyChanged: OnItemsSourceChanged);

        public static readonly BindableProperty FsctTemplateProperty
                    = BindableProperty.Create(nameof(FsctTemplate), typeof(DataTemplate), typeof(CollectionSingleCard), propertyChanged: OnItemTemplateChanged);

        private Int32 _NumColumns = 3;

        public CollectionSingleCard()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable FsctSource
        {
            get { return (IEnumerable)GetValue(FsctSourceProperty); }
            set { SetValue(FsctSourceProperty, value); }
        }

        public DataTemplate FsctTemplate
        {
            get { return (DataTemplate)GetValue(FsctTemplateProperty); }
            set { SetValue(FsctTemplateProperty, value); }
        }

        public Int32 NumColumns
        {
            get
            {
                return _NumColumns;
            }

            set
            {
                _NumColumns = value;
                OnPropertyChanged(nameof(NumColumns));
            }
        }

        public static void OnItemsSourceChanged(BindableObject argSource, object oldValue, object newValue)
        {
            var layout = argSource as CollectionSingleCard;

            layout.theCollectionView.ItemsSource = layout.FsctSource;
        }

        public static void OnItemTemplateChanged(BindableObject argSource, object oldValue, object newValue)
        {
            var layout = argSource as CollectionSingleCard;

            layout.theCollectionView.ItemTemplate = layout.FsctTemplate;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CollectionSingleCardRoot_SizeChanged(object sender, EventArgs e)
        {
            CollectionSingleCard t = sender as CollectionSingleCard;

            NumColumns = (Int32)(t.Width / CardWidths.Current.CardSmallWidth + 1);  // +1 for padding
        }
    }
}