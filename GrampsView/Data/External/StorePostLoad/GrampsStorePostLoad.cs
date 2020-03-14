// <copyright file="GrampsStorePostLoad.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Data.ExternalStorageNS
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;
    using GrampsView.Events;

    using Prism.Events;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// Creates a collection of entities with content read from a GRAMPS XML file.
    /// </summary>
    public partial class GrampsStorePostLoad : CommonBindableBase, IStorePostLoad
    {
        /// <summary>
        /// Gets or sets injected Event Aggregator.
        /// </summary>
        private readonly IEventAggregator _EventAggregator;

        /// <summary>
        /// The local common logging.
        /// </summary>
        private ICommonLogging _CL;

        /// <summary>
        /// Initializes a new instance of the <see cref="GrampsStorePostLoad"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The ioc common logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        public GrampsStorePostLoad(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator)
        {
            _EventAggregator = iocEventAggregator;
            _CL = iocCommonLogging;
            //localStoreFile = iocStoreFile;

            _EventAggregator.GetEvent<DataLoadXMLEvent>().Subscribe(LoadXMLUIItems, ThreadOption.UIThread);
        }

        public async static Task<bool> fixMediaFile(MediaModel argMediaModel)
        {
            try
            {
                if ((argMediaModel.HomeImageHLink.HomeUseImage == true) && argMediaModel.IsOriginalFilePathValid)
                {
                    //_CL.LogVariable("tt.OriginalFilePath", argMediaModel.OriginalFilePath); //
                    //_CL.LogVariable("localMediaFolder.path", DataStore.DS.CurrentDataFolder.FullName); //
                    //_CL.LogVariable("path", DataStore.DS.CurrentDataFolder.FullName + "\\" + argMediaModel.OriginalFilePath);

                    DataStore.DS.MediaData[argMediaModel.HLinkKey].MediaStorageFile = await StoreFolder.FolderGetFileAsync(DataStore.DS.CurrentDataFolder, argMediaModel.OriginalFilePath).ConfigureAwait(false);
                }
            }
            catch (FileNotFoundException ex)
            {
                DataStore.CN.NotifyError("File (" + argMediaModel.OriginalFilePath + ") not found while   loading media. Has the GRAMPS database been verified ? " + ex.ToString());

                DataStore.CN.NotifyException("Trying to  add media file pointer", ex);
            }
            catch (Exception ex)
            {
                CommonLocalSettings.DataSerialised = false;
                DataStore.CN.NotifyException("Trying to add media file pointer", ex);

                await DataStore.CN.MajorStatusDelete().ConfigureAwait(false);
                throw;
            }

            return false;
        }
    }
}