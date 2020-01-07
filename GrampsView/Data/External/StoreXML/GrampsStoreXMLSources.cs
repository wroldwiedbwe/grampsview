//-----------------------------------------------------------------------
//
// Storage routines for the GrampsStoreXML
//
// <copyright file="GrampsStoreXMLSources.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.ExternalStorageNS
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    /// <summary>
    /// Private Storage Routines.
    /// </summary>
    public partial class GrampsStoreXML : IGrampsStoreXML
    {
        /// <summary>
        /// load events from external storage.
        /// </summary>
        /// <param name="eventRepository">
        /// The event repository.
        /// </param>
        /// <returns>
        /// Flag of loaded successfully.
        /// </returns>
        public async Task LoadSourcesAsync()
        {
            await DataStore.CN.MajorStatusAdd(strMessage: "Loading Source data").ConfigureAwait(false);
            {
                // XNamespace ns = grampsXMLNameSpace;
                try
                {
                    // Run query
                    var de =
                        from el in localGrampsXMLdoc.Descendants(ns + "source")
                        select el;

                    // get BookMark fields

                    // Loop through results to get the Citation Uri _baseUri = new Uri("ms-appx:///");
                    foreach (XElement pSource in de)
                    {
                        SourceModel loadSource = DV.SourceDV.NewModel();

                        // Citation attributes
                        loadSource.Id = (string)pSource.Attribute("id");
                        loadSource.Change = (string)pSource.Attribute("change");
                        loadSource.Priv = SetPrivateObject((string)pSource.Attribute("priv"));
                        loadSource.Handle = (string)pSource.Attribute("handle");

                        loadSource.GSourceAttributeCollection = GetAttributeCollection(pSource);

                        // Media refs
                        loadSource.GMediaRefCollection = GetObjectCollection(pSource);

                        // Note refs
                        loadSource.GNoteRefCollection = GetNoteCollection(pSource);

                        loadSource.GSAbbrev = GetElement(pSource, "sabbrev");

                        loadSource.GSAuthor = GetElement(pSource, "sauthor");

                        loadSource.GSPubInfo = GetElement(pSource, "spubinfo");

                        loadSource.GSTitle = GetElement(pSource, "stitle");

                        // Tag refs
                        loadSource.GTagRefCollection = GetTagCollection(pSource);

                        // Repository refs
                        loadSource.GRepositoryRefCollection = GetRepositoryCollection(pSource);

                        // save the event
                        DV.SourceDV.SourceData.Add(loadSource);
                    }

                    // sort the collection eventRepository.Items.Sort(EventModel => EventModel);

                    // let everybody know
                }
                catch (Exception e)
                {
                    // TODO handle this
                    await DataStore.CN.MajorStatusAdd(e.Message).ConfigureAwait(false);

                    throw;
                }
            }

            await DataStore.CN.MajorStatusDelete();
            return;
        }
    }
}