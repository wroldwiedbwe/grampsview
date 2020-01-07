// <copyright file="CommonLogCircular.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Common
{
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.Serialization;

    using GrampsView.Events;

    using Prism.Events;

    /// <summary>
    /// Various common routines.
    /// </summary>
    [DataContract]
    public class CommonLogCircular : ObservableCollection<DataLogEntry>, INotifyPropertyChanged, ICommonCircularLog, IEnumerable
    {
        /// <summary>
        /// The local common logging.
        /// </summary>
        private readonly ICommonLogging localCL;

        private int maxLines = 10;

        private int currentIndex = -1;

        /// <summary>
        /// Injected Event Aggregator.
        /// </summary>
        private readonly IEventAggregator localEventAggregator;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonLogCircular" /> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The ioc common logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        public CommonLogCircular(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator)
        {
            localCL = iocCommonLogging;
            localEventAggregator = iocEventAggregator;

            localEventAggregator.GetEvent<GVProgressMajorTextUpdate>().Subscribe(LogAdd, ThreadOption.UIThread);

            // LogList = new Queue<DataLogEntry>(3);
        }

        ///// <summary>
        ///// Gets or sets the log list.
        ///// </summary>
        ///// <value>
        ///// The log list.
        ///// </value>
        //[DataMember]
        //public ObservableCollection<DataLogEntry> LogList { get; } = new ObservableCollection<DataLogEntry>();

        //public void Add(DataLogEntry item)
        //{
        //    this.Add(item);
        //}

        //public IEnumerator<DataLogEntry> GetEnumerator()
        //{
        //    return this.GetEnumerator();
        //}

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return GetEnumerator();
        //}

        /// <summary>
        /// Datas the loaded log add.
        /// </summary>
        /// <param name="entry">
        /// The entry.
        /// </param>
        private void LogAdd(DataLogEntry entry)
        {
            currentIndex = currentIndex + 1;

            this[currentIndex] = entry;

            localCL.LogProgress(string.Format("{0} - {1}", entry.Label, entry.Text));
        }

        /// <summary>
        /// Add log datas.
        ///
        /// Only log entries that are not null.
        /// </summary>
        /// <param name="entry">
        /// The entry.
        /// </param>
        private void LogAdd(string entry)
        {
            DataLogEntry t = default(DataLogEntry);

            if (entry != null)
            {
                t.Label = string.Format("{0:HH: mm:ss}", System.DateTime.Now);
                t.Text = entry;

                LogAdd(t);
            }
        }

        //public class Que<T> : CommonBindableBase, IEnumerable<T>
        //{
        //    private readonly int _maxCount;
        //    private readonly Queue<T> _queue;

        // public Que(int maxCount) { _maxCount = maxCount; _queue = new Queue<T>(maxCount); }

        // public void Add(T item) { if (_queue.Count == _maxCount) _queue.Dequeue();
        // _queue.Enqueue(item); }

        // public IEnumerator<T> GetEnumerator() { return _queue.GetEnumerator(); }

        //    IEnumerator IEnumerable.GetEnumerator()
        //    {
        //        return GetEnumerator();
        //    }
        //}
    }
}