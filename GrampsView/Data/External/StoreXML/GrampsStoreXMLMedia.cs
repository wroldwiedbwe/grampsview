﻿//-----------------------------------------------------------------------
//
// Storage routines for the GrampsStoreXML
//
// <copyright file="GrampsStoreXMLMedia.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.ExternalStorageNS
{
    using GrampsView.Common;
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
        public static MediaModel SetHomeImage(MediaModel argModel)
        {
            // Setup HomeImage
            argModel.HomeImageHLink.HLinkKey = argModel.HLinkKey;

            switch (argModel.FileMimeType)
            {
                case "application":
                    {
                        switch (argModel.FileMimeSubType)
                        {
                            case "pdf":
                                {
                                    argModel.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
                                    argModel.HomeImageHLink.HomeSymbol = IconFont.FilePdf;
                                    break;
                                }

                            case "x-zip-compressed":
                                {
                                    argModel.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
                                    argModel.HomeImageHLink.HomeSymbol = IconFont.ZipBox;
                                    break;
                                }

                            case "zip":
                                {
                                    argModel.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
                                    argModel.HomeImageHLink.HomeSymbol = IconFont.ZipBox;
                                    break;
                                }

                            default:
                                {
                                    argModel.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
                                    argModel.HomeImageHLink.HomeSymbol = IconFont.FileDocument;
                                    break;
                                }
                        }

                        break;
                    }

                case "image":
                    {
                        argModel.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeThumbNail;
                        argModel.HomeImageHLink.HomeSymbol = IconFont.Image;
                        break;
                    }

                case "video":
                    {
                        argModel.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
                        argModel.HomeImageHLink.HomeSymbol = IconFont.Video;
                        break;
                    }

                default:
                    {
                        argModel.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeThumbNail;
                        argModel.HomeImageHLink.HomeSymbol = CommonConstants.IconMedia;
                        break;
                    }
            }

            //HLinkHomeImageModel hlink = argModel.GMediaRefCollection.FirstHLinkHomeImage;
            //if (!hlink.Valid)
            //{
            //    argModel.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
            //    argModel.HomeImageHLink.HomeSymbol = CommonConstants.IconFamilies;
            //}
            //else
            //{
            //    argModel.HomeImageHLink = SetHomeHLink(argModel.HomeImageHLink, hlink);
            //}

            // Get colour
            Application.Current.Resources.TryGetValue("CardBackGroundMedia", out var varCardColour);
            argModel.HomeImageHLink.HomeSymbolColour = (Color)varCardColour;

            return argModel;
        }

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

                //// Get colour
                //Application.Current.Resources.TryGetValue("CardBackGroundMedia", out var varCardColour);
                //Color cardColour = (Color)varCardColour;

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

                        if (loadObject.Id == "O0200")
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

                                    // Load FileInfoEx and metadata
                                    loadObject.MediaStorageFile = await StoreFile.GetStorageFileAsync(loadObject.OriginalFilePath).ConfigureAwait(false);

                                    var imageSize = DependencyService.Get<IImageResource>().GetSize(loadObject.MediaStorageFilePath);

                                    loadObject.MetaDataHeight = imageSize.Height;
                                    loadObject.MetaDataWidth = imageSize.Width;
                                }
                                catch (Exception ex)
                                {
                                    DataStore.CN.NotifyException("Error trying to load a media file (" + loadObject.OriginalFilePath + ") listed in the GRAMPS file", ex);
                                    throw;
                                }
                            }
                        }
                        // Get description
                        loadObject.GDescription = (string)filedetails.Attribute("description");

                        // date details
                        XElement dateval = pname.Element(ns + "dateval");
                        if (dateval != null)
                        {
                            loadObject.GDateValue = SetDate(pname);
                        }

                        // Load NoteRefs
                        loadObject.GNoteRefCollection = GetNoteCollection(pname);

                        // citationref details TODO Event References
                        loadObject.GCitationRefCollection = GetCitationCollection(pname);

                        loadObject.GTagRefCollection = GetTagCollection(pname);

                        loadObject = SetHomeImage(loadObject);

                        // save the object
                        DataStore.DS.MediaData.Add(loadObject);

                        localGrampsCommonLogging.LogVariable("LoadMedia", loadObject.GDescription);
                    }
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