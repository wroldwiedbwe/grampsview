// <copyright file="GrampsStorePostLoad.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Data.ExternalStorageNS
{
    using GrampsView.Common;
    using GrampsView.Events;

    using Prism.Events;

    /// <summary>
    /// Creates a collection of entities with content read from a GRAMPS XML file.
    /// </summary>
    public partial class GrampsStorePostLoad : CommonBindableBase, IStorePostLoad
    {
        /// <summary>
        /// The local common logging.
        /// </summary>
        private ICommonLogging _CL;

        /// <summary>
        /// Gets or sets injected Event Aggregator.
        /// </summary>
        private readonly IEventAggregator _EventAggregator;

        /// <summary>Initializes a new instance of the <see cref="GrampsStorePostLoad"/> class.</summary>
        /// <param name="iocCommonLogging">The ioc common logging.</param>
        /// <param name="iocEventAggregator">The ioc event aggregator.</param>
        public GrampsStorePostLoad(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator)
        {
            _EventAggregator = iocEventAggregator;
            _CL = iocCommonLogging;
            //localStoreFile = iocStoreFile;

            _EventAggregator.GetEvent<DataLoadXMLEvent>().Subscribe(LoadXMLUIItems, ThreadOption.UIThread);
        }

     
        //}
    }
}