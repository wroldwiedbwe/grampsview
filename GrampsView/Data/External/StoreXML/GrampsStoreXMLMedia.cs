//-----------------------------------------------------------------------
//
// Storage routines for the GrampsStoreXML
//
// <copyright file="GrampsStoreXMLMedia.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.ExternalStorageNS
{
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    using Xamarin.Forms;

    /// <summary>
    /// Private Storage Routines.
    /// </summary>
    public partial class GrampsStoreXML : IGrampsStoreXML
    {
        /// <summary>
        /// load media objects from external storage.
        /// </summary>
        /// <param name="mediaRepository">
        /// The media repository.
        /// </param>
        /// <returns>
        /// Flag showing of loaded successfully.
        /// </returns>
        public async Task<bool> LoadMediaObjectsAsync()
        {
            localGrampsCommonLogging.LogRoutineEntry("loadMediaObjects");

            await DataStore.CN.MajorStatusAdd("Loading Media Objects").ConfigureAwait(false);
            {
                // start file load
                await DataStore.CN.MajorStatusAdd("Loading Media").ConfigureAwait(false);

                // Get colour
                Application.Current.Resources.TryGetValue("CardBackGroundMedia", out var varCardColour);
                Color cardColour = (Color)varCardColour;

                // Load notes Run query
                var de =
                    from el in localGrampsXMLdoc.Descendants(ns + "object")
                    select el;

                try
                {
                    foreach (XElement pname in de)
                    {
                        // <code> < define name = "object-content" > <ref name=
                        // "SecondaryColor-object" /> < element name = "file" > < attribute name =
                        // "src" > < text /> </ attribute > < attribute name = "mime" >

                        // < text />

                        // </ attribute >

                        // < optional >

                        // < attribute name = "checksum" >

                        // < text />

                        // </ attribute >

                        // </ optional > </code>

                        // < optional >

                        // < attribute name = "description" >

                        // < text />

                        // </ attribute >

                        // </ optional >

                        // </ element >

                        // < zeroOrMore >

                        // < element name = "attribute" >

                        // <ref name="attribute-content"/>

                        // </ element >

                        // </ zeroOrMore >

                        // < zeroOrMore >

                        // < element name = "noteref" >

                        // <ref name="noteref-content"/>

                        // </ element >

                        // </ zeroOrMore >

                        // < optional >

                        // <ref name="date-content"/>

                        // </ optional >

                        // < zeroOrMore >

                        // < element name = "citationref" >

                        // <ref name="citationref-content"/>

                        // </ element >

                        // </ zeroOrMore >

                        // </ element >

                        // </ zeroOrMore >

                        // </ define >
                        MediaModel loadObject = new MediaModel
                        {
                            // object details
                            Id = (string)pname.Attribute("id"),
                            Handle = (string)pname.Attribute("handle"),
                            Priv = SetPrivateObject((string)pname.Attribute("priv")),
                            Change = GetDateTime(GetAttribute(pname, "change")),
                        };

                        if (loadObject.Id == "O0220")
                        {
                        }

                        // file details
                        XElement filedetails = pname.Element(ns + "file");
                        if (filedetails != null)
                        {
                            loadObject.FileContentType = (string)filedetails.Attribute("mime");

                            string mediaFileName = (string)filedetails.Attribute("src");

                            if (mediaFileName.Length == 0)
                            {
                                DataStore.CN.NotifyError("Error trying to load a media file for object (" + loadObject.Id + ") listed in the GRAMPS file.  FileName is null");
                                loadObject.MediaStorageFile = null;
                            }
                            else
                            {
                                try
                                {
                                    string temp = StoreFileUtility.CleanFilePath(mediaFileName);
                                    await DataStore.CN.MajorStatusAdd("Loading media file: " + temp).ConfigureAwait(false);
                                    loadObject.OriginalFilePath = temp;

                                    //loadObject.MediaStorageFile = await localStoreFile.GetStorageFileAsync(temp);

                                    //if (loadObject.IsMediaStorageFileValid)
                                    //{
                                    //    await DataStore.CN.MajorStatusDelete();

                                    // Check for null length
                                    //    BasicProperties pro = await loadObject.MediaStorageFile.GetBasicPropertiesAsync();
                                    //    if (pro.Size == 0)
                                    //    {
                                    //        DataStore.CN.NotifyError("Error trying to load a media file (" + loadObject.OriginalFilePath + ") listed in the GRAMPS file.  File is zero length");
                                    //        loadObject.MediaStorageFile = null;
                                    //    }
                                    //}

                                    loadObject.GDescription = (string)filedetails.Attribute("description");

                                    //}
                                }
                                catch (Exception ex)
                                {
                                    DataStore.CN.NotifyException("Error trying to load a media file (" + loadObject.OriginalFilePath + ") listed in the GRAMPS file", ex);
                                    throw;
                                }
                            }
                        }

                        // date details
                        XElement dateval = pname.Element(ns + "dateval");
                        if (dateval != null)
                        {
                            loadObject.GDateValue = SetDate(pname);
                        }

                        // Load NoteRefs
                        loadObject.GNoteRefCollection = GetNoteCollection(pname);

                        // var localNoteElement = from ElementEl in pname.Descendants(ns +
                        // "noteref") select ElementEl;

                        // if (localNoteElement.Count() != 0) { // load note references foreach
                        // (XElement loadNoteElement in localNoteElement) { HLinkNoteModel noteHLink
                        // = new HLinkNoteModel { // object details HLinkKey =
                        // (string)loadNoteElement.Attribute("hlink"), }; // localUnityContainer.Resolve<HLinkNoteModel>();

                        // // save the object loadObject.NoteReferenceCollection.Add(noteHLink); }

                        // // Don't sort here as the objects pointed to may not have been loaded. //
                        // Sort in Post Load cleanup }

                        // citationref details TODO Event References
                        loadObject.GCitationRefCollection = GetCitationCollection(pname);

                        // < zeroOrMore > < element name = "tagref" > <ref name="tagref-content" />
                        loadObject.GTagRefCollection = GetTagCollection(pname);

                        loadObject.HomeImageHLink.HomeSymbolColour = cardColour;

                        // save the object
                        DV.MediaDV.MediaData.Add(loadObject);

                        localGrampsCommonLogging.LogVariable("LoadMedia", loadObject.GDescription);
                    }

                    // sort the collection mediaRepository.Items.Sort(MediaModel => MediaModel);
                }
                catch (Exception e)
                {
                    // TODO handle this
                    DataStore.CN.NotifyException("Loading Media Objects", e);

                    throw;
                }
            }

            await DataStore.CN.MajorStatusDelete().ConfigureAwait(false);

            localGrampsCommonLogging.LogRoutineExit(nameof(LoadMediaObjectsAsync));
            return true;
        }
    }
}