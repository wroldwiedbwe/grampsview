namespace GrampsView.UserControls
{
    using GrampsView.Common;

    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Diagnostics.Contracts;

    using Xamarin.Forms;

    public partial class CollectionCardGroup : Grid, INotifyPropertyChanged
    {
        private Int32 _NumColumns = 1;

        public CollectionCardGroup()
        {
            InitializeComponent();

            UpdateSpan();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public double daHeight
        {
            get
            {
                return 50;
            }
        }

        private void CardGroupTemplateFlexer_MeasureInvalidated(object sender, EventArgs e)
        {
        }

        private void CollectionCardGroup_SizeChanged(object sender, EventArgs e)
        {
            UpdateSpan();
        }

        private void theCollectionView_ChildAdded(object sender, ElementEventArgs e)
        {
            FlexLayout t = sender as FlexLayout;
        }

        private void UpdateSpan()
        {
            CollectionCardGroup t = this as CollectionCardGroup;
        }
    }
}