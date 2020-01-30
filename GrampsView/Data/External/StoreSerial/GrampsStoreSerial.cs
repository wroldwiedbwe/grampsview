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
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Repository;

    using Newtonsoft.Json;

    using System;
    using System.IO;
    using System.IO.IsolatedStorage;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;

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
                    // Check of the file exists
                    string DataInstanceFileName = typeof(DataInstance).Name.Trim() + ".json";

                    if (!isoStore.FileExists(DataInstanceFileName))
                    {
                        DataStore.CN.NotifyError("DeSerializeRepository - File: " + DataInstanceFileName + " does not exist.  Reload the GPKG file");
                        CommonLocalSettings.DataSerialised = false;
                        return;
                    }

                    var stream = new IsolatedStorageFileStream(DataInstanceFileName, FileMode.Open, isoStore);

                    using (StreamReader file = new StreamReader(stream))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Converters.Add(new GrampsView.Converters.NewtonSoftColorConverter());

                        DataInstance t = (DataInstance)serializer.Deserialize(file, typeof(DataInstance));

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

                        if (t.CitationData != null)
                        {
                            DataStore.DS.CitationData = t.CitationData;
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
                            //// Hack TODO Work out why xamarin not serilising color property
                            //foreach (TagModel item in t.localTagData)
                            //{
                            //    item.HomeImageHLink.HomeSymbolColour = item.GColor;
                            //}

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
                JsonSerializer serializer = new JsonSerializer();

                serializer.Converters.Add(new GrampsView.Converters.NewtonSoftColorConverter());

                using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(typeof(DataInstance).Name.Trim() + ".json", FileMode.Create, isoStore))
                    {
                        StreamWriter sw = new StreamWriter(stream);

                        using (JsonWriter writer = new JsonTextWriter(sw))
                        {
                            serializer.Serialize(writer, theObject);
                        }
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