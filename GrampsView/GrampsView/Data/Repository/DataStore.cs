// <copyright file="DataStore.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

/// <summary>
/// </summary>
namespace GrampsView.Data.Repository
{
    using System.Runtime.Serialization;

    using GrampsView.Common;

    /// <summary>
    /// Static Data Store.
    /// </summary>
    [DataContract]
    public sealed class DataStore : CommonBindableBase, IDataStore
    {
        private DataStore()
        {
        }

        /// <summary>
        /// Gets or sets the cn.
        /// </summary>
        /// <value>
        /// The cn.
        /// </value>
        public static ICommonNotifications CN { get; set; }

        /// <summary>
        /// Gets the Data Store.
        /// </summary>
        /// <value>
        /// The datastore.
        /// </value>
        public static DataInstance DS { get; } = new DataInstance();

        public static NavCmd NV { get; set; } = new NavCmd();

        //public static CommonLogging CL { get; set; } = new CommonLogging();
    }
}