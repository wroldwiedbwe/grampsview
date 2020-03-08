﻿//-----------------------------------------------------------------------
//
// Storage routines for the GrampsStoreXML
//
// <copyright file="GrampsStoreXMLNotes.cs" company="PlaceholderCompany">
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
    using Xamarin.Essentials;
    using Xamarin.Forms;

    /// <summary>
    /// Private Storage Routines.
    /// </summary>
    public partial class GrampsStoreXML : IGrampsStoreXML
    {
        /// <summary>
        /// load Notes from external storage.
        /// </summary>
        /// <param name="noteRepository">
        /// The event repository.
        /// </param>
        /// <returns>
        /// Flag of loaded successfully.
        /// </returns>
        public async Task LoadNotesAsync()
        {
            await DataStore.CN.MajorStatusAdd("Loading Note data").ConfigureAwait(false);
            {
                // Get colour
                Application.Current.Resources.TryGetValue("CardBackGroundNote", out var varCardColour);
                Color cardColour = (Color)varCardColour;

                // Load notes
                try
                {
                    // Run query
                    var de =
                        from el in localGrampsXMLdoc.Descendants(ns + "note")
                        select el;

                    // get event fields TODO

                    // Loop through results to get the Notes Uri _baseUri = new Uri("ms-appx:///");
                    foreach (XElement pname in de)
                    {
                        NoteModel loadNote = DV.NoteDV.NewModel();

                        // Note attributes
                        loadNote.Id = (string)pname.Attribute("id");
                        loadNote.Change = GetDateTime(pname, "change");
                        loadNote.Priv = SetPrivateObject((string)pname.Attribute("priv"));
                        loadNote.Handle = (string)pname.Attribute("handle");

                        loadNote.HLinkKey = loadNote.Handle;
                        loadNote.GFormat = (string)pname.Attribute("format");
                        loadNote.GType = (string)pname.Attribute("type");

                        // Note fields

                        // Load Styled Text
                        if (loadNote.Id == "N0170")
                        {
                        }

                        loadNote.GText = (string)pname.Element(ns + "text");

                        //loadString.Spans.Add(new Span { Text = theText, FontSize = 12 });

                        //loadNote = GetFormattedString(pname);
                        ;

                        //// TODO Style

                        // TagRef
                        loadNote.GTagRefCollection = GetTagCollection(pname);

                        // set the Home image or symbol
                        loadNote.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
                        loadNote.HomeImageHLink.HomeSymbol = CommonConstants.IconNotes;
                        loadNote.HomeImageHLink.HomeSymbolColour = cardColour;

                        DV.NoteDV.NoteData.Add(loadNote);
                    }
                }
                catch (Exception ex)
                {
                    // TODO handle this
                    DataStore.CN.NotifyException("Exception loading Notes form the Gramps file", ex);

                    throw;
                }
            }

            await DataStore.CN.MajorStatusDelete().ConfigureAwait(false);
            return;
        }

        private FormattedString GetFormattedString(XElement argStyledText)
        {
            FormattedString loadString = new FormattedString();

            string theText = (string)argStyledText.Element(ns + "text");

            loadString.Spans.Add(new Span { Text = theText, FontSize = 12 });

            return loadString;
        }
    }
}