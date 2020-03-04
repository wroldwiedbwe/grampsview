//-----------------------------------------------------------------------
// Storage routines for GrampsStoreXML
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
    using Xamarin.Forms;

    /// <summary>
    /// Load Citations from external storage routines.
    /// </summary>
    public partial class GrampsStoreXML : IGrampsStoreXML
    {
        /// <summary>
        /// Sets the Citation home image.
        /// </summary>
        /// <param name="argModel">
        /// The argument model.
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// argModel
        /// </exception>
        public static CitationModel SetHomeImage(CitationModel argModel)
        {
            if (argModel is null)
            {
                throw new ArgumentNullException(nameof(argModel));
            }

            HLinkHomeImageModel hlink = argModel.GMediaRefCollection.FirstHLink;
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
        /// Load Citations from external storage.
        /// </summary>
        /// <returns>
        /// Flag if loaded successfully.
        /// </returns>
        public async Task LoadCitationsAsync()
        {
            await DataStore.CN.MajorStatusAdd("Loading Citation data").ConfigureAwait(false);
            {
                // Get colour
                Application.Current.Resources.TryGetValue("CardBackGroundCitation", out var varCardColour);
                Color cardColour = (Color)varCardColour;

                // Load notes

                try
                {
                    // Run query
                    var de =
                        from el in localGrampsXMLdoc.Descendants(ns + "citation")
                        select el;

                    // Loop through results to get the Citation

                    foreach (XElement pcitation in de)
                    {
                        CitationModel loadCitation = DV.CitationDV.NewModel();

                        // Citation attributes
                        loadCitation.Id = GetAttribute(pcitation, "id");
                        loadCitation.Change = GetDateTime(pcitation, "change");
                        loadCitation.Priv = SetPrivateObject(GetAttribute(pcitation, "priv"));
                        loadCitation.Handle = GetAttribute(pcitation, "handle");

                        // Citation fields

                        loadCitation.GDateContent = GetDate(pcitation);

                        loadCitation.GPage = GetElement(pcitation.Element(ns + "page"));

                        loadCitation.GConfidence = GetElement(pcitation.Element(ns + "confidence"));

                        loadCitation.GNoteRefCollection = GetNoteCollection(pcitation);

                        // ObjectRef loading
                        loadCitation.GMediaRefCollection = GetObjectCollection(pcitation);

                        loadCitation.GSourceAttributeCollection = GetSrcAttributeCollection(pcitation);

                        loadCitation.GSourceRef.HLinkKey = GetAttribute(pcitation.Element(ns + "sourceref"), "hlink");

                        loadCitation.GTagRef = GetTagCollection(pcitation);

                        // set the Home image or symbol now that everything is laoded
                        loadCitation = SetHomeImage(loadCitation);
                        loadCitation.HomeImageHLink.HomeSymbolColour = cardColour;

                        // save the event
                        DV.CitationDV.CitationData.Add(loadCitation);
                    }
                }
                catch (Exception e)
                {
                    DataStore.CN.NotifyException("Exception loading Citations form XML", e);
                    throw;
                }
            }

            await DataStore.CN.MajorStatusDelete().ConfigureAwait(false);
            return;
        }
    }
}