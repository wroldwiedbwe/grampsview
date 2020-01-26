//-----------------------------------------------------------------------
//
// Storage routines for the GrampsStoreXML
//
// <copyright file="GrampsStoreXMLCitations.cs" company="PlaceholderCompany">
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
        public static CitationModel SetHomeImage(CitationModel argModel)
        {
            if (argModel is null)
            {
                throw new ArgumentNullException(nameof(argModel));
            }

            HLinkMediaModel hlink = argModel.GMediaRefCollection.FirstHLink;
            if (hlink is null)
            {
                argModel.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
                argModel.HomeImageHLink.HomeSymbol = CommonConstants.IconCitation;
            }
            else
            {
                argModel.HomeImageHLink = SetHomeHLink(argModel.HomeImageHLink, hlink);
            }

            return argModel;
        }

        /// <summary>
        /// load events from external storage.
        /// </summary>
        /// <param name="eventRepository">
        /// The event repository.
        /// </param>
        /// <returns>
        /// Flag of loaded successfully.
        /// </returns>
        public async Task LoadCitationsAsync()
        {
            await DataStore.CN.MajorStatusAdd("Loading Citation data").ConfigureAwait(false);
            {
                // XNamespace ns = grampsXMLNameSpace;
                try
                {
                    // Run query
                    var de =
                        from el in localGrampsXMLdoc.Descendants(ns + "citation")
                        select el;

                    // get Citation fields

                    // Loop through results to get the Citation Uri _baseUri = new Uri("ms-appx:///");
                    foreach (XElement pcitation in de)
                    {
                        CitationModel loadCitation = DV.CitationDV.NewModel();

                        // Citation attributes
                        loadCitation.Id = GetAttribute(pcitation, "id");
                        loadCitation.Change = GetDateTime(pcitation, "change");
                        loadCitation.Priv = SetPrivateObject(GetAttribute(pcitation, "priv"));
                        loadCitation.Handle = GetAttribute(pcitation, "handle");

                        // Citation fields

                        // < optional ><ref name = "date-content" /></ optional >
                        loadCitation.GDateContent = GetDate(pcitation);

                        // < optional >< element name = "page" >< text /></ element ></ optional >
                        loadCitation.GPage = GetElement(pcitation.Element(ns + "page"));

                        // < element name = "confidence" >< text /></ element >
                        loadCitation.GConfidence = GetElement(pcitation.Element(ns + "confidence"));

                        // < zeroOrMore >< element name = "noteref" > <ref name = "noteref-content"
                        // /> </ element ></ zeroOrMore >
                        loadCitation.GNoteRef = GetNoteCollection(pcitation);

                        // Don't sort here as the objects pointed to may not have been loaded. Sort
                        // in Post Load cleanup

                        // ObjectRef loading
                        loadCitation.GMediaRefCollection = GetObjectCollection(pcitation);

                        loadCitation.GSourceAttribute = GetSrcAttributeCollection(pcitation);

                        loadCitation.GSourceRef.HLinkKey = GetAttribute(pcitation.Element(ns + "sourceref"), "hlink");

                        loadCitation.GTagRef = GetTagCollection(pcitation);

                        // set the Home image or symbol now that everythign is laoded
                        loadCitation = SetHomeImage(loadCitation);

                        // save the event
                        DV.CitationDV.CitationData.Add(loadCitation);
                    }

                    // let everybody know
                }
                catch (Exception e)
                {
                    // TODO handle this
                    await DataStore.CN.MajorStatusAdd(e.Message).ConfigureAwait(false);

                    throw;
                }
            }

            await DataStore.CN.MajorStatusDelete().ConfigureAwait(false);
            return;
        }
    }
}