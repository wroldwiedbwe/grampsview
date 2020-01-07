//-----------------------------------------------------------------------
//
// Repository Serialisation/Deserialisation code
//
// <copyright file="GrampsStoreSerial.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.External.StoreSerial
{
    using System;
    using System.IO;
    using System.IO.IsolatedStorage;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;
    using System.Xml;

    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    /// <summary>
    /// Creates a collection of entities with content read from a GRAMPS XML file.
    /// </summary>
    public class GrampsStoreSerial : IGrampsStoreSerial
    {
        /// <summary>
        /// The local common logging.
        /// </summary>
        private readonly ICommonLogging localGVLogging;

        /// <summary>
        /// Initializes a new instance of the <see cref="GrampsStoreSerial" /> class.
        /// </summary>
        /// <param name="iocGVProgress">
        /// The ioc gv progress.
        /// </param>
        /// <param name="iocGVLogging">
        /// The ioc gv logging.
        /// </param>
        public GrampsStoreSerial(ICommonLogging iocGVLogging)
        {
            // save injected references for later
            localGVLogging = iocGVLogging;
        }

        ///// <summary>
        ///// Gets or sets the local data repository.
        ///// </summary>
        ///// <value>
        ///// The local data repository.
        ///// </value>
        // private IDataRepository LocalDataRepository { get; set; }

        /// <summary>
        /// Deserialise the previously serialised repository. Perform as a single step so that it
        /// goes faster at the cost of providing less feedbak to the user.
        /// </summary>
        public void DeSerializeRepository()
        {
            localGVLogging.LogRoutineEntry(nameof(DeSerializeRepository));

            try
            {
                DataContractSerializer ser = new DataContractSerializer(typeof(DataInstance));

                using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    var stream = new IsolatedStorageFileStream(typeof(DataInstance).Name.Trim() + ".xml", FileMode.Open, isoStore);

                    XmlDictionaryReaderQuotas xmlQuot = new XmlDictionaryReaderQuotas
                    {
                        MaxStringContentLength = 32767
                    };

                    using (XmlDictionaryReader sw = XmlDictionaryReader.CreateTextReader(stream, xmlQuot))
                    {
                        DataInstance t = (DataInstance)ser.ReadObject(sw, true);

                        // Check for nulls
                        if (t.BookMarkData != null)
                        {
                            DataStore.DS.BookMarkData = t.BookMarkData;
                        }
                        else
                        {
                            CommonLocalSettings.DataSerialised = false;
                            DataStore.CN.NotifyError("Bad BookMark deserialisation error.  Data loading cancelled. Restart the program and reload the data.");
                        }

                        if (t.LocalCitationData != null)
                        {
                            DataStore.DS.LocalCitationData = t.LocalCitationData;
                        }
                        else
                        {
                            CommonLocalSettings.DataSerialised = false;
                            DataStore.CN.NotifyError("Bad Citation deserialisation error.  Data loading cancelled. Restart the program and reload the data.");
                        }

                        if (t.localEventData != null)
                        {
                            DataStore.DS.localEventData = t.localEventData;
                        }
                        else
                        {
                            CommonLocalSettings.DataSerialised = false;
                            DataStore.CN.NotifyError("Bad Event deserialisation error.  Data loading cancelled. Restart the program and reload the data.");
                        }

                        if (t.localFamilyData != null)
                        {
                            DV.FamilyDV.FamilyData = t.localFamilyData;
                        }
                        else
                        {
                            CommonLocalSettings.DataSerialised = false;
                            DataStore.CN.NotifyError("Bad Media deserialisation error.  Data loading cancelled. Restart the program and reload the data.");
                        }

                        if (t.localMediaData != null)
                        {
                            DV.MediaDV.MediaData = t.localMediaData;
                        }
                        else
                        {
                            CommonLocalSettings.DataSerialised = false;
                            DataStore.CN.NotifyError("Bad Media deserialisation error.  Data loading cancelled. Restart the program and reload the data.");
                        }

                        if (t.localPersonData != null)
                        {
                            DV.PersonDV.PersonData = t.localPersonData;
                        }
                        else
                        {
                            CommonLocalSettings.DataSerialised = false;
                            DataStore.CN.NotifyError("Bad Person deserialisation error.  Data loading cancelled. Restart the program and reload the data.");
                        }

                        // Check for nulls
                        if (t.SourceData != null)
                        {
                            DataStore.DS.SourceData = t.SourceData;
                        }
                        else
                        {
                            CommonLocalSettings.DataSerialised = false;
                            DataStore.CN.NotifyError("Bad Source data deserialisation error.  Data loading cancelled. Restart the program and reload the data.");
                        }

                        // Check for nulls
                        if (t.localTagData != null)
                        {
                            // Hack TODO Work out why xamarin not serilising color property
                            foreach (TagModel item in t.localTagData)
                            {
                                item.HomeImageHLink.HomeSymbolColour = item.GColor;
                            }

                            DataStore.DS.localTagData = t.localTagData;
                        }
                        else
                        {
                            CommonLocalSettings.DataSerialised = false;
                            DataStore.CN.NotifyError("Bad Tag data deserialisation error.  Data loading cancelled. Restart the program and reload the data.");
                        }

                        // TODO Finish setting the checks up on these
                        DataStore.DS.localFamilyData = t.localFamilyData;
                        DataStore.DS.localHeaderData = t.localHeaderData;
                        DataStore.DS.localNameMapData = t.localNameMapData;
                        DataStore.DS.localNoteData = t.localNoteData;

                        DataStore.DS.localPlaceData = t.localPlaceData;
                        DataStore.DS.localRepositoryData = t.localRepositoryData;
                    }
                }

                localGVLogging.LogRoutineExit(nameof(DeSerializeRepository));
            }
            catch (Exception ex)
            {
                localGVLogging.LogProgress("DeSerializeRepository - Exception ");
                CommonLocalSettings.DataSerialised = false;
                DataStore.CN.NotifyException("Old data deserialisation error.  Data loading cancelled", ex);
                throw;
            }

            return;
        }

        /// <summary>
        /// Serializes the object.
        /// </summary>
        /// <param name="theObject">
        /// The object.
        /// </param>
        /// <returns>
        /// </returns>
        public async Task<bool> SerializeObject(object theObject)
        {
            try
            {
                XmlWriterSettings ws = new XmlWriterSettings
                {
                    Async = true,
                };

                using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(typeof(DataInstance).Name.Trim() + ".xml", FileMode.Create, isoStore))

                    using (XmlWriter sw = XmlWriter.Create(stream, ws))
                    {
                        DataContractSerializer ser = new DataContractSerializer(theObject.GetType());
                        ser.WriteObject(sw, theObject);
                        await sw.FlushAsync().ConfigureAwait(false);
                    }
                }

                CommonLocalSettings.DataSerialised = true;
                return true;
            }
            catch (Exception ex)
            {
                DataStore.CN.NotifyException("Trying to serialise object ", ex);
                CommonLocalSettings.DataSerialised = false;
                throw;
            }
        }
    }
}