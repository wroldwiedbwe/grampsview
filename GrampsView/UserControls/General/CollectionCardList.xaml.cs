namespace GrampsView.UserControls
{
    using GrampsView.Common;

    using System;
    using System.Collections;
    using System.ComponentModel;

    using Xamarin.Forms;

    public partial class CollectionCardList : Frame, INotifyPropertyChanged
    {
        public static readonly BindableProperty FsctSourceProperty
              = BindableProperty.Create(returnType: typeof(IEnumerable), declaringType: typeof(CollectionCardList), propertyName: nameof(FsctSource), propertyChanged: OnItemsSourceChanged);

        private Int32 _NumColumns = 3;

        public CollectionCardList()
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
            var layout = argSource as CollectionCardList;

            layout.theCollectionView.ItemsSource = newValue as IEnumerable;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //private void CollectionCardListRoot_SizeChanged(object sender, EventArgs e)
        //{
        //    CollectionCardList t = sender as CollectionCardList;

        //    NumColumns = (Int32)(t.Width / CardSizes.Current.CardSmallWidth + 1);  // +1 for padding
        //}
    }
}