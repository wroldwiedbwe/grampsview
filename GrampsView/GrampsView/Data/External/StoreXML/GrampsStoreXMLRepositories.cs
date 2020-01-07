//-----------------------------------------------------------------------
//
// Storage routines for GrampsStoreXML
//
// <copyright file="GrampsStoreXMLRepositories.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.ExternalStorageNS
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    using GrampsView.Common;
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
        public async Task LoadRepositoriesAsync()
        {
            await DataStore.CN.MajorStatusAdd("Loading Respository data");

            try
            {
                // Run query
                var de =
                    from el in localGrampsXMLdoc.Descendants(ns + "repository")
                    select el;

                foreach (XElement prepository in de)
                {
                    RepositoryModel loadRepository = DV.RepositoryDV.NewModel();

                    // Primary attributes
                    loadRepository.Id = (string)prepository.Attribute("id");
                    loadRepository.Change = (string)prepository.Attribute("change");
                    loadRepository.Priv = SetPrivateObject((string)prepository.Attribute("priv"));
                    loadRepository.Handle = (string)prepository.Attribute("handle");

                    // Repository fields TODO finish loading fields

                    // < element name = "rname" > < text /> </ element >
                    loadRepository.GRName = GetElement(prepository, "rname");

                    // < element name = "type" > < text /> </ element >
                    loadRepository.GType = GetElement(prepository, "type");

                    // < element name = "address" > <ref name= "address-content" /> </ element >
                    loadRepository.GAddress = GetAddressCollection(prepository);

                    // < element name = "url" > <ref name= "url-content" /> </ element >
                    loadRepository.GURL = GetURLCollection(prepository);

                    // < element name = "noteref" > <ref name= "noteref-content" /> </ element >
                    loadRepository.GNoteRefCollection = GetNoteCollection(prepository);

                    // < element name = "tagref" > <ref name= "tagref-content" /> </ element >
                    loadRepository.GTagRefCollection = GetTagCollection(prepository);

                    // set the Home image or symbol
                    loadRepository.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
                    loadRepository.HomeImageHLink.HomeSymbol = CommonConstants.IconRepository;

                    // save the event
                    DV.RepositoryDV.RepositoryData.Add(loadRepository);
                }

                // sort the collection eventRepository.Items.Sort(EventModel => EventModel);

                // let everybody know
            }
            catch (Exception e)
            {
                // TODO handle this
                await DataStore.CN.MajorStatusAdd(e.Message);

                throw;
            }

            await DataStore.CN.MajorStatusDelete();
            return;
        }
    }
}