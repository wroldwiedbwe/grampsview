// <copyright file="GrampsStoreXMLTags.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

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
    /// Load Tags from Gramps XML file.
    /// </summary>
    public partial class GrampsStoreXML : IGrampsStoreXML
    {
        /// <summary>
        /// Loads the tags from Gramps XML file asynchronously.
        /// </summary>
        /// <returns>
        /// True if loaded successfully.
        /// </returns>
        public async Task LoadTagsAsync()
        {
            await DataStore.CN.MajorStatusAdd("Loading Tag data").ConfigureAwait(false);
            {
                // XNamespace ns = grampsXMLNameSpace;
                try
                {
                    // Run query
                    var de =
                        from el in localGrampsXMLdoc.Descendants(ns + "tag")
                        select el;

                    // get Tag fields

                    // Loop through results
                    foreach (XElement pcitation in de)
                    {
                        TagModel loadTag = DV.TagDV.NewModel();

                        // Citation attributes
                        loadTag.Id = GetAttribute(pcitation, "id");
                        loadTag.Change = GetDateTime(pcitation, "change");
                        loadTag.Priv = SetPrivateObject(GetAttribute(pcitation, "priv"));
                        loadTag.Handle = GetAttribute(pcitation, "handle");

                        // Tag fields
                        loadTag.GColor = GetColour(pcitation, "color");
                        loadTag.GName = GetAttribute(pcitation, "name");
                        loadTag.GPriority = int.Parse(GetAttribute(pcitation, "priority"));

                        // set the Home image or symbol
                        loadTag.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
                        loadTag.HomeImageHLink.HomeSymbolColour = loadTag.GColor;
                        loadTag.HomeImageHLink.HomeSymbol = CommonConstants.IconTag;

                        // save the event
                        DV.TagDV.TagData.Add(loadTag);
                    }
                }
                catch (Exception ex)
                {
                    DataStore.CN.NotifyException("Error in LoadTagsAsync", ex);
                    throw;
                }
            }

            await DataStore.CN.MajorStatusDelete().ConfigureAwait(false);

            return;
        }
    }
}