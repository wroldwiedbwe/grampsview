//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="CardGroup.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// </summary>
namespace GrampsView.Common
{
    using Newtonsoft.Json;

    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;

    /// <summary>
    /// </summary>
    public class CardGroup : INotifyPropertyChanged, INotifyCollectionChanged
    {
        private ObservableCollection<object> data = new ObservableCollection<object>();

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add
            {
                ((INotifyCollectionChanged)Cards).CollectionChanged += value;
            }

            remove
            {
                ((INotifyCollectionChanged)Cards).CollectionChanged -= value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                ((INotifyPropertyChanged)Cards).PropertyChanged += value;
            }

            remove
            {
                ((INotifyPropertyChanged)Cards).PropertyChanged -= value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether [control visible].
        /// </summary>
        /// <value>
        /// <c>true</c> if [control visible]; otherwise, <c>false</c>.
        /// </value>
        public bool CardGroupVisible
        {
            get
            {
                if (!(Cards is null) && (Cards.Count > 0))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Gets or sets the models.
        /// </summary>
        /// <value>
        /// The models.
        /// </value>
        [JsonIgnore]
        public ObservableCollection<object> Cards
        {
            get
            {
                // TODO Handle Hlinks properly so they have their own data

                return data;
            }
        }

        public string SerializedData
        {
            get
            {
                return JsonConvert.SerializeObject(data, Formatting.Indented);
            }
            set
            {
                data = JsonConvert.DeserializeObject<ObservableCollection<object>>(value);
            }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }
    }
}