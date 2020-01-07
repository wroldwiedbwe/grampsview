// <copyright file="CardListLineCollection.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

/// <summary>
/// Common routines
/// </summary>
namespace GrampsView.Data.Model
{
    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;

    /// <summary>
    /// </summary>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<CardListLine>))]
    public class CardListLineCollection : ObservableCollection<CardListLine>
    {
        public CardListLineCollection()
        {
            //Items = new ObservableCollectionEx<CardListLine>();

            //Items.CollectionChanged += (sender, e) =>
            //{
            //    //this.SomeOtheMember = Items.Where(c => c.Count_TB == true).Count();
            //    NotifyPropertyChanged("Items");
            //};
            //Items.ItemPropertyChanged += (sender, e) =>
            //{
            //    //this.SomeOtheMember = Items.Where(c => c.Count_TB == true).Count();
            //    NotifyPropertyChanged("Items");
            //};
        }

        //public event PropertyChangedEventHandler PropertyChanged;

        //public int Count
        //{
        //    get
        //    {
        //        return Items.Count;
        //    }
        //}

        //public ObservableCollectionEx<CardListLine> Items
        //{
        //    get;
        //    private set;
        //}

        //private void NotifyPropertyChanged(string p)
        //{
        //    if (PropertyChanged != null)
        //        PropertyChanged(this, new PropertyChangedEventArgs(p));
        //}
        public string Title { get; set; }

        public new void Add(CardListLine newLine)
        {
            if (!(newLine is null) & (!string.IsNullOrEmpty(newLine.Value)))
            {
                Items.Add(newLine);
            }
        }

        //public IEnumerator GetEnumerator() => ((IEnumerable)Items).GetEnumerator();

        /// <summary>
        /// Repalces the item list with the specified arguments.
        /// </summary>
        /// <param name="theArgs">
        /// The arguments.
        /// </param>
        public void Set(CardListLineCollection theArgs)
        {
            Items.Clear();

            foreach (CardListLine item in theArgs)
            {
                Items.Add(item);
            }
        }

        //private int someOtheMember;
        //public int SomeOtheMember
        //{
        //    get
        //    {
        //        return this.someOtheMember;
        //    }
        //    set
        //    {
        //        if ((this.someOtheMember != value))
        //        {
        //            this.someOtheMember = value;
        //            NotifyPropertyChanged("SomeOtheMember");
        //        }
        //    }
        //}
        //public void Refresh()
        //{
        //    base.Items = base.Items;
        //}
    }
}