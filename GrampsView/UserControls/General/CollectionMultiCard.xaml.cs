namespace GrampsView.UserControls
{
    using GrampsView.Common;

    using System;
    using System.Collections;
    using System.ComponentModel;
    using Xamarin.Forms;

    public partial class CollectionMultiCard : Frame, INotifyPropertyChanged
    {
        public static readonly BindableProperty FsctSourceProperty
              = BindableProperty.Create(returnType: typeof(IEnumerable), declaringType: typeof(CollectionMultiCard), propertyName: nameof(FsctSource), propertyChanged: OnItemsSourceChanged);

        private Int32 _NumColumns = 3;

        public CollectionMultiCard()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable FsctSource
        {
            get { return (IEnumerable)GetValue(FsctSourceProperty); }
            set { SetValue(FsctSourceProperty, value); }
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
            var layout = argSource as CollectionMultiCard;

            layout.theCollectionView.ItemsSource = newValue as IEnumerable;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CollectionMultiCardRoot_SizeChanged(object sender, EventArgs e)
        {
            CollectionMultiCard t = sender as CollectionMultiCard;

            NumColumns = (Int32)(t.Width / CardSizes.Current.CardSmallWidth + 1);  // +1 for padding
        }
    }
}