namespace GrampsView.UserControls
{
    using GrampsView.Common;

    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Diagnostics.Contracts;

    using Xamarin.Forms;

    public partial class CollectionSingleCard : Frame, INotifyPropertyChanged
    {
        public static readonly BindableProperty FsctSourceProperty
              = BindableProperty.Create(returnType: typeof(IEnumerable), declaringType: typeof(CollectionSingleCard), propertyName: nameof(FsctSource), propertyChanged: OnItemsSourceChanged);

        public static readonly BindableProperty FsctTemplateProperty
                    = BindableProperty.Create(nameof(FsctTemplate), returnType: typeof(DataTemplate), declaringType: typeof(CollectionSingleCard), propertyChanged: OnItemTemplateChanged);

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
            Contract.Requires(argSource != null);
            Contract.Requires(newValue != null);

            CollectionSingleCard layout = argSource as CollectionSingleCard;

            IEnumerable iSource = newValue as IEnumerable;

            layout.theCollectionView.ItemsSource = iSource;
        }

        public static void OnItemTemplateChanged(BindableObject argSource, object oldValue, object newValue)
        {
            Contract.Requires(argSource != null);
            Contract.Requires(newValue != null);

            CollectionSingleCard layout = argSource as CollectionSingleCard;

            DataTemplate iTemplate = newValue as DataTemplate;

            layout.theCollectionView.ItemTemplate = iTemplate;
        }

        protected new void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CollectionSingleCardRoot_SizeChanged(object sender, EventArgs e)
        {
            Contract.Requires(sender != null);

            CollectionSingleCard t = sender as CollectionSingleCard;

            NumColumns = (Int32)(t.Width / CardSizes.Current.CardSmallWidth + 1);  // +1 for padding
        }
    }
}