// <copyright file="RepositoryModelType.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.Serialization;

    using GrampsView.Common;
    using GrampsView.Data.Model;
    using GrampsView.Exceptions;

    /// <summary>
    /// Partially based on http://stackoverflow.com/questions/8157140/net-4-0-indexer-with-observablecollection.
    /// </summary>
    /// <typeparam name="T1">
    /// Model Base.
    /// </typeparam>
    /// <typeparam name="T2">
    /// HLink Base.
    /// </typeparam>
    /// <seealso cref="GrampsView.Common.CommonBindableBase" />
    /// /// /// /// /// /// /// ///
    /// <seealso cref="GrampsView.Data.Repositories.IRepositoryModelType{T, U}" />
    /// /// /// /// /// /// /// ///
    /// <seealso cref="System.ComponentViewModel.INotifyPropertyChanged" />
    [DataContract]
    public class RepositoryModelType<T1, T2> : CommonBindableBase, IRepositoryModelType<T1, T2>, INotifyPropertyChanged
        where T1 : ModelBase, new()
        where T2 : HLinkBase, new()
    {
        /// <summary>
        /// Initialize a simple random number generator.
        /// </summary>
        private readonly Random localRandomNumberGenerator = new Random();

        /// <summary>
        /// The indecies.
        /// </summary>
        private Dictionary<string, int> indecies = new Dictionary<string, int>();

        /// <summary>
        /// The local items.
        /// </summary>
        private ObservableCollection<T1> localItems = new ObservableCollection<T1>();

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryModelType{T, U}" /> class.
        /// </summary>
        public RepositoryModelType()
        {
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count
        {
            get
            {
                return localItems.Count;
            }
        }

        /// <summary>
        /// Gets or sets the indecies.
        /// </summary>
        /// <value>
        /// The indecies.
        /// </value>
        [DataMember]
        public Dictionary<string, int> Indexes
        {
            get
            {
                return indecies;
            }

            set
            {
                SetProperty(ref indecies, value);
            }
        }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        [DataMember]
        public ObservableCollection<T1> Items
        {
            get
            {
                return localItems;
            }

            set
            {
                SetProperty(ref localItems, value);
            }
        }

        /// <summary>
        /// Gets the <see cref="T1" /> with the specified key.
        /// </summary>
        /// <value>
        /// The <see cref="T1" />.
        /// </value>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// </returns>
        public T1 this[string key]
        {
            get
            {
                return Get(key);
            }
        }

        /// <summary>
        /// Gets the <see cref="T1" /> with the specified h link.
        /// </summary>
        /// <value>
        /// The <see cref="T1" />.
        /// </value>
        /// <param name="hLink">
        /// The h link.
        /// </param>
        /// <returns>
        /// </returns>
        public T1 this[T2 hLink]
        {
            get
            {
                return Get(hLink.HLinkKey);
            }
        }

        /// <summary>
        /// Adds the specified argument tot he end of the list and update sthe Indexes.
        /// </summary>
        /// <param name="arg">
        /// The argument.
        /// </param>
        public void Add(T1 arg)
        {
            Update(localItems.Count, arg);
        }

        /// <summary>
        /// Removes all items from the collection.
        /// </summary>
        public void Clear()
        {
            localItems.Clear();
            Indexes.Clear();
        }

        /// <summary>
        /// Determines whether the specified key contains key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// </returns>
        public virtual bool ContainsKey(string key)
        {
            if (key != null)
            {
                return Indexes.ContainsKey(key);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Gets or sets the element with the specified key. If setting a new value, new value must
        /// have same key.
        /// </summary>
        /// <param name="key">
        /// Key of element to replace.
        /// </param>
        /// <returns>
        /// </returns>
        public T1 Get(string key)
        {
            if (ContainsKey(key) == true)
            {
                return localItems[Indexes[key]];
            }
            else
            {
                return new T1();
            }
        }

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// </returns>
        public T1 Get(int key)
        {
            if (key <= localItems.Count)
            {
                return localItems[key];
            }
            else
            {
                return new T1();
            }
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>
        /// </returns>
        public IEnumerator<T1> GetEnumerator()
        {
            return localItems.GetEnumerator();
        }

        /// <summary>
        /// Gets the model from h link.
        /// </summary>
        /// <param name="argHLink">
        /// The argument h link.
        /// </param>
        /// <returns>
        /// </returns>
        public T1 GetModelFromHLink(T2 argHLink)
        {
            return GetModelFromHLink(argHLink.HLinkKey);
        }

        /// <summary>
        /// Gets the model from h link.
        /// </summary>
        /// <param name="argHLink">
        /// The argument h link.
        /// </param>
        /// <returns>
        /// </returns>
        public T1 GetModelFromHLink(string argHLink)
        {
            T1 tempMO = localItems.OfType<T1>().FirstOrDefault(x => x.HLinkKey == argHLink);

            if (tempMO == null)
            {
                return new T1();
            }

            return tempMO;
        }

        /// <summary>
        /// returns a random object from the objects recorded.
        /// </summary>
        public T1 GetRandomItem()
        {
            if (localItems.Count > 0)
            {
                int rndItem = localRandomNumberGenerator.Next(0, localItems.Count);

                return localItems[rndItem];
            }
            else
            {
                // return nothing
                return new T1();
            }
        }

        /// <summary>
        /// Removes the specified key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// </returns>
        public virtual bool Remove(string key)
        {
            if (!Indexes.ContainsKey(key))
            {
                return false;
            }

            localItems.RemoveAt(Indexes[key]);
            return true;
        }

        /// <summary>
        /// Items the remove.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        public void Remove(int index)
        {
            var item = localItems[index];
            var key = _keySelector(item);

            localItems.RemoveAt(index);

            Indexes.Remove(key);

            foreach (var k in Indexes.Keys.Where(k => Indexes[k] > index).ToList())
            {
                Indexes[k]--;
            }
        }

        /// <summary>
        /// Replaces element at given key with new value. New value must have same key.
        /// </summary>
        /// <param name="key">
        /// Key of element to replace.
        /// </param>
        /// <param name="value">
        /// New value.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        /// <returns>
        /// False if key not found.
        /// </returns>
        public virtual bool Replace(string key, T1 value)
        {
            if (!Indexes.ContainsKey(key))
            {
                return false;
            }

            // confirm key matches
            if (!_keySelector(value).Equals(key))
            {
                throw new InvalidOperationException("Key of new value does not match");
            }

            localItems[Indexes[key]] = value;
            return true;
        }

        /// <summary>
        /// Sorts this instance by the default sort of the contents.
        /// </summary>
        public void Sort()
        {
            localItems.Sort(T => T.GetDefaultText);
        }

        /// <summary>
        /// Inserts the item.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <exception cref="GrampsView.Exceptions.DuplicateKeyException">
        /// </exception>
        public void Update(int index, T1 item)
        {
            var key = _keySelector(item);
            if (Indexes.ContainsKey(key))
            {
                throw new DuplicateKeyException(key);
            }

            if (index != localItems.Count)
            {
                foreach (var k in Indexes.Keys.Where(k => Indexes[k] >= index).ToList())
                {
                    Indexes[k]++;
                }
            }

            localItems.Insert(index, item);
            Indexes[key] = index;
        }

        /// <summary>
        /// Keys the selector.
        /// </summary>
        /// <param name="arg">
        /// The argument.
        /// </param>
        /// <returns>
        /// </returns>
        private string _keySelector(T1 arg)
        {
            return arg.HLinkKey;
        }
    }
}