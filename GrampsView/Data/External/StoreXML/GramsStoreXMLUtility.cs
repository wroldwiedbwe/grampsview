// <summary>
// Utility routines for GramspStore XML readers
// </summary>
// <remarks>
// Can not load and sort as we go as we then lose the ability to choose the first image link for
// references. This can only be done when everything is fully loaded.
// </remarks>
// <copyright file="GramsStoreXMLUtility.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Data.ExternalStorageNS
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;
    using SkiaSharp;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    using Xamarin.Forms;

    /// <summary>
    /// Various utility and loading routines for XML data.
    /// </summary>
    /// <seealso cref="GrampsView.Common.CommonBindableBase"/>
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// /// ///
    /// <seealso cref="GrampsView.Data.ExternalStorageNS.IGrampsStoreXML"/>
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// /// ///
    /// <seealso cref="IGrampsStoreXML"/>
    public partial class GrampsStoreXML : IGrampsStoreXML
    {
        public static async Task<HLinkMediaModel> CreateClippedMediaModel(HLinkLoadImageModel argHLinkLoadImageModel)
        {
            if (argHLinkLoadImageModel is null)
            {
                throw new ArgumentNullException(nameof(argHLinkLoadImageModel));
            }

            if (!argHLinkLoadImageModel.DeRef.Valid)
            {
                throw new ArgumentException("CreateClippedMediaModel argument is invalid", nameof(argHLinkLoadImageModel));
            }

            // TODO cleanup code. Multiple copies of things in use

            MediaModel theMediaModel = argHLinkLoadImageModel.DeRef;

            SKBitmap resourceBitmap = new SKBitmap();

            MediaModel newMediaModel = new MediaModel();

            string newHLinkKey = argHLinkLoadImageModel.HLinkKey + "-" + argHLinkLoadImageModel.GCorner1X + argHLinkLoadImageModel.GCorner1Y + argHLinkLoadImageModel.GCorner2X + argHLinkLoadImageModel.GCorner2Y;
            string outFileName = Path.Combine("Cropped", newHLinkKey + ".png");

            string outFilePath = Path.Combine(DataStore.AD.CurrentDataFolder.FullName, outFileName);

            Debug.WriteLine(argHLinkLoadImageModel.DeRef.MediaStorageFilePath);

            // Check if already exists
            MediaModel fileExists = DV.MediaDV.GetModelFromHLinkString(newHLinkKey);

            if (!fileExists.Valid)
            {
                // Needs clipping
                using (StreamReader stream = new StreamReader(theMediaModel.MediaStorageFilePath))
                {
                    resourceBitmap = SKBitmap.Decode(stream.BaseStream);
                }

                // Check for too large a bitmap
                Debug.WriteLine("Image ResourceBitmap size: " + resourceBitmap.ByteCount);
                if (resourceBitmap.ByteCount > int.MaxValue - 1000)
                {
                    // TODO Handle this better. Perhaps resize? Delete for now
                    resourceBitmap = new SKBitmap();
                }

                float crleft = (float)(argHLinkLoadImageModel.GCorner1X / 100d * theMediaModel.MetaDataWidth);
                float crright = (float)(argHLinkLoadImageModel.GCorner2X / 100d * theMediaModel.MetaDataWidth);
                float crtop = (float)(argHLinkLoadImageModel.GCorner1Y / 100d * theMediaModel.MetaDataHeight);
                float crbottom = (float)(argHLinkLoadImageModel.GCorner2Y / 100d * theMediaModel.MetaDataHeight);

                SKRect cropRect = new SKRect(crleft, crtop, crright, crbottom);

                SKBitmap croppedBitmap = new SKBitmap(
                                                    (int)cropRect.Width,
                                                    (int)cropRect.Height
                                                    );

                SKRect dest = new SKRect(
                                        0,
                                        0,
                                        cropRect.Width,
                                        cropRect.Height
                                        );

                SKRect source = new SKRect(
                                        cropRect.Left,
                                        cropRect.Top,
                                        cropRect.Right,
                                        cropRect.Bottom);

                using (SKCanvas canvas = new SKCanvas(croppedBitmap))
                {
                    canvas.DrawBitmap(resourceBitmap, source, dest);
                }

                // create an image COPY
                SKImage image = SKImage.FromBitmap(croppedBitmap);

                // encode the image (defaults to PNG)
                SKData encoded = image.Encode();

                // get a stream over the encoded data

                using (Stream stream = File.Open(outFilePath, FileMode.OpenOrCreate, System.IO.FileAccess.Write, FileShare.ReadWrite))
                {
                    encoded.SaveTo(stream);
                }

                croppedBitmap.Dispose();

                // ------------ Save new MediaObject
                newMediaModel = theMediaModel.Copy();
                newMediaModel.HLinkKey = newHLinkKey;

                newMediaModel.HomeImageHLink.HLinkKey = newHLinkKey;
                newMediaModel.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeThumbNail;
                newMediaModel.HomeImageHLink.HomeSymbol = CommonConstants.IconMedia;

                newMediaModel.OriginalFilePath = outFileName;
                newMediaModel.MediaStorageFile = await StoreFolder.FolderGetFileAsync(DataStore.AD.CurrentDataFolder, outFileName).ConfigureAwait(false);
                newMediaModel.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeThumbNail;
                newMediaModel.IsClippedFile = true;

                newMediaModel.MetaDataHeight = cropRect.Height;
                newMediaModel.MetaDataWidth = cropRect.Width;

                DataStore.DS.MediaData.Add(newMediaModel);
                //await StorePostLoad.fixMediaFile(newMediaModel).ConfigureAwait(false);
            }

            resourceBitmap.Dispose();

            return newMediaModel.HLink;
        }

        /// <summary>
        /// Gets the bool.
        /// </summary>
        /// <param name="xmlData">
        /// The XML data.
        /// </param>
        /// <param name="argName">
        /// Name of the argument.
        /// </param>
        /// <returns>
        /// </returns>
        private static bool GetBool(XElement xmlData, string argName)
        {
            string boolString = GetAttribute(xmlData.Attribute(argName));

            if (boolString == null)
            {
                return false;
            }

            switch (boolString)
            {
                case "0":
                    {
                        return true;
                    }

                case "1":
                    {
                        return false;
                    }

                default:
                    {
                        return false;
                    }
            }
        }

        /// <summary>
        /// Converts a string into a uri (if it can).
        /// </summary>
        /// <param name="xmlData">
        /// string from XML.
        /// </param>
        private static Uri GetUri(string xmlData)
        {
            try
            {
                xmlData = xmlData.Trim();

                if (!Uri.IsWellFormedUriString(xmlData, UriKind.Absolute))
                {
                    // Handle sites with no leading http or https ( TODO Assumes they are all http...)
                    if (!xmlData.StartsWith("http://") && !xmlData.StartsWith("https://"))
                    {
                        xmlData = "http://" + xmlData;
                    }
                }

                if (Uri.IsWellFormedUriString(xmlData, UriKind.Absolute))
                {
                    Uri uri = new Uri(xmlData);

                    return uri;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                DataStore.CN.NotifyException("Exception in GetUri", ex);

                return null;
            }
        }

        ///// <summary>
        ///// Sets the home h link.
        ///// </summary>
        ///// <param name="argBaseHLink">
        ///// The home image h link.
        ///// </param>
        ///// <param name="argNewHLink">
        ///// The hlink.
        ///// </param>
        ///// <returns>
        ///// Updatded HomeImageLink
        ///// </returns>
        //private static HLinkHomeImageModel SetHomeHLink(HLinkHomeImageModel argBaseHLink, HLinkHomeImageModel argNewHLink)
        //{
        //    HLinkHomeImageModel returnHLink = argBaseHLink;

        // returnHLink.HLinkKey = argNewHLink.HLinkKey; returnHLink.HomeImageType =
        // argNewHLink.HomeImageType; returnHLink.HomeSymbol = argNewHLink.HomeSymbol;

        //    return returnHLink;
        //}

        private OCAddressModelCollection GetAddressCollection(XElement xmlData)
        {
            OCAddressModelCollection t = new OCAddressModelCollection();

            // Get colour
            Application.Current.Resources.TryGetValue("CardBackGroundAddress", out var varCardColour);
            Color cardColour = (Color)varCardColour;

            // Run query
            var theERElement =
                    from orElementEl
                    in xmlData.Elements(ns + "address")
                    select orElementEl;

            if (theERElement.Any())
            {
                // Load address object references
                foreach (XElement theLoadORElement in theERElement)
                {
                    AddressModel newAddressModel = new AddressModel
                    {
                        Handle = "AddressCollection",

                        GCitationRefCollection = GetCitationCollection(theLoadORElement),

                        GCity = GetElement(theLoadORElement, "city"),

                        GCountry = GetElement(theLoadORElement, "country"),

                        GCounty = GetElement(theLoadORElement, "county"),

                        GDate = GetDate(theLoadORElement),

                        GLocality = GetElement(theLoadORElement, "locality"),

                        GPhone = GetElement(theLoadORElement, "phone"),

                        GPostal = GetElement(theLoadORElement, "postal"),

                        GState = GetElement(theLoadORElement, "state"),

                        GStreet = GetElement(theLoadORElement, "street"),

                        Priv = SetPrivateObject(GetAttribute(theLoadORElement.Attribute("priv"))),

                        GNoteRefCollection = GetNoteCollection(theLoadORElement),
                    };

                    // set the Home image or symbol
                    newAddressModel.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
                    newAddressModel.HomeImageHLink.HomeSymbol = CommonConstants.IconAddress;
                    newAddressModel.HomeImageHLink.HomeSymbolColour = cardColour;

                    t.Add(newAddressModel);
                }

                t.Sort(x => x.GDate.SortDate);
            }

            return t;
        }

        /// <summary>
        /// Gets the attribute collection.
        /// </summary>
        /// <param name="xmlData">
        /// The XML data.
        /// </param>
        /// <returns>
        /// </returns>
        private OCAttributeModelCollection GetAttributeCollection(XElement xmlData)
        {
            OCAttributeModelCollection t = new OCAttributeModelCollection();

            // Get colour
            Application.Current.Resources.TryGetValue("CardBackGroundAttribute", out var varCardColour);
            Color cardColour = (Color)varCardColour;

            // Run query
            var theERElement =
                    from orElementEl
                    in xmlData.Elements(ns + "attribute")
                    select orElementEl;

            if (theERElement.Any())
            {
                // Load attribute object references
                foreach (XElement theLoadORElement in theERElement)
                {
                    AttributeModel newAttributeModel = new AttributeModel
                    {
                        Handle = "AttributeCollection",

                        GCitationReferenceCollection = GetCitationCollection(theLoadORElement),

                        GNoteModelReferenceCollection = GetNoteCollection(theLoadORElement),

                        Priv = SetPrivateObject(GetAttribute(theLoadORElement.Attribute("priv"))),

                        GType = GetAttribute(theLoadORElement.Attribute("type")),

                        GValue = GetAttribute(theLoadORElement.Attribute("value")),
                    };

                    // set the Home image or symbol
                    newAttributeModel.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
                    newAttributeModel.HomeImageHLink.HomeSymbol = CommonConstants.IconAttribute;
                    newAttributeModel.HomeImageHLink.HomeSymbolColour = cardColour;

                    t.Add(newAttributeModel);
                }
            }

            // Return sorted by the default text
            t.Sort(T => T.DeRef.GetDefaultText);

            return t;
        }

        /// <summary>
        /// Gets the citation collection.
        /// </summary>
        /// <remarks>
        /// Can not sort as we go as we then lose the ability to choose the first image link for
        /// references. This can only be done when everything is fully loaded.
        /// </remarks>
        /// <param name="xmlData">
        /// The XML data.
        /// </param>
        /// <returns>
        /// </returns>
        private HLinkCitationModelCollection GetCitationCollection(XElement xmlData)
        {
            HLinkCitationModelCollection t = new HLinkCitationModelCollection();

            IEnumerable<XElement> theERElement =
                    from ORElementEl in xmlData.Elements(ns + "citationref")
                    select ORElementEl;

            if (theERElement.Any())
            {
                // load citation object references
                foreach (XElement theLoadORElement in theERElement)
                {
                    HLinkCitationModel t2 = new HLinkCitationModel
                    {
                        HLinkKey = GetAttribute(theLoadORElement.Attribute("hlink")),
                    };
                    t.Add(t2);

                    if (t2.DeRef.Id == "C0525")
                    {
                    }
                }
            }

            // Return sorted by the default text
            t.SortAndSetFirst();

            return t;
        }

        /// <summary>
        /// Gets the colour.
        /// </summary>
        /// <param name="a">
        /// a.
        /// </param>
        /// <param name="b">
        /// The b.
        /// </param>
        /// <returns>
        /// </returns>
        private Color GetColour(XElement a, string b)
        {
            try
            {
                const string ColorNotSet = "#000000000000";

                var regexColorCode = new Regex("^#[a-fA-F0-9]{6}$");

                string hexColour = GetAttribute(a.Attribute(b));

                // Validate
                if ((!regexColorCode.IsMatch(hexColour.Trim()) && hexColour != ColorNotSet))
                {
                    Dictionary<string, string> argErrorDetail = new Dictionary<string, string>
                    {
                        { "Color element is", a.ToString() },
                        { "Attribute is", b }
                    };

                    DataStore.CN.NotifyError("Bad colour in GetColour", argErrorDetail);

                    hexColour = "#000000";
                }

                if (hexColour == ColorNotSet)
                {
                    hexColour = "#000000";
                }

                ColorTypeConverter colConverter = new ColorTypeConverter();

                return (Color)(colConverter.ConvertFromInvariantString(hexColour));
            }
            catch (Exception ex)
            {
                DataStore.CN.NotifyException("Error in XML Utils GetColour", ex);
                throw;
            }
        }

        /// <summary>
        /// Gets the date.
        /// </summary>
        /// <param name="xmlData">
        /// The XML data.
        /// </param>
        /// <returns>
        /// </returns>
        private DateObjectModel GetDate(XElement xmlData)
        {
            return SetDate(xmlData);
        }

        private DateTime GetDateTime(XElement a, string b)
        {
            string argUnixSecs = GetAttribute(a.Attribute(b));

            return GetDateTime(argUnixSecs);
        }

        private DateTime GetDateTime(string argUnixSecs)
        {
            long ls = new long();

            long.TryParse(argUnixSecs, out ls);

            DateTimeOffset t = DateTimeOffset.FromUnixTimeSeconds(ls);

            // TODO This is in UTC and need to convert to local time

            return t.DateTime;
        }

        /// <summary>
        /// Gets the event collection.
        /// </summary>
        /// <param name="xmlData">
        /// The XML data.
        /// </param>
        /// <returns>
        /// </returns>
        private HLinkEventModelCollection GetEventCollection(XElement xmlData)
        {
            HLinkEventModelCollection t = new HLinkEventModelCollection();

            var theERElement =
                    from _ORElementEl in xmlData.Elements(ns + "eventref")
                    select _ORElementEl;

            if (theERElement.Any())
            {
                // load event object references
                foreach (XElement theLoadORElement in theERElement)
                {
                    HLinkEventModel t2 = new HLinkEventModel
                    {
                        HLinkKey = GetAttribute(theLoadORElement.Attribute("hlink")),
                    };
                    t.Add(t2);
                }
            }

            // Return sorted by the default text
            t.SortAndSetFirst();

            return t;
        }

        /// <summary>
        /// Gets the note collection.
        /// </summary>
        /// <param name="xmlData">
        /// The XML data.
        /// </param>
        /// <returns>
        /// </returns>
        private HLinkNoteModelCollection GetNoteCollection(XElement xmlData)
        {
            HLinkNoteModelCollection t = new HLinkNoteModelCollection();

            // Load NoteRefs
            var localNoteElement =
                             from ElementEl in xmlData.Elements(ns + "noteref")
                             select ElementEl;

            if (localNoteElement.Any())
            {
                // load note references
                foreach (XElement loadNoteElement in localNoteElement)
                {
                    HLinkNoteModel noteHLink = new HLinkNoteModel
                    {
                        // object details
                        HLinkKey = GetAttribute(loadNoteElement.Attribute("hlink")),
                    };

                    // save the object
                    t.Add(noteHLink);
                }
            }

            // Return sorted by the default text
            t.SortAndSetFirst();

            return t;
        }

        /// <summary>
        /// Gets the object collection.
        /// </summary>
        /// <param name="xmlData">
        /// The XML data.
        /// </param>
        /// <returns>
        /// </returns>
        private async Task<HLinkMediaModelCollection> GetObjectCollection(XElement xmlData)
        {
            HLinkMediaModelCollection t = new HLinkMediaModelCollection();

            var theORElement = from _ORElementEl in xmlData.Elements(ns + "objref")
                               select _ORElementEl;

            if (theORElement.Any())
            {
                // load media object references
                foreach (XElement theLoadORElement in theORElement)
                {
                    // save the MediaObject reference
                    HLinkMediaModel t2 = new HLinkMediaModel
                    {
                        HLinkKey = GetAttribute(theLoadORElement.Attribute("hlink")),
                    };

                    // Get region
                    XElement regionDetails = theLoadORElement.Element(ns + "region");
                    if (regionDetails != null)
                    {
                        HLinkLoadImageModel t3 = new HLinkLoadImageModel
                        {
                            HLinkKey = t2.HLinkKey,
                            HomeImageType = CommonConstants.HomeImageTypeThumbNail,

                            GCorner1X = (int)regionDetails.Attribute("corner1_x"),
                            GCorner1Y = (int)regionDetails.Attribute("corner1_y"),
                            GCorner2X = (int)regionDetails.Attribute("corner2_x"),
                            GCorner2Y = (int)regionDetails.Attribute("corner2_y"),
                        };

                        t2 = await CreateClippedMediaModel(t3).ConfigureAwait(false);
                    }

                    // Get remaining fields
                    t2.GAttributeRefCollection = GetAttributeCollection(theLoadORElement);
                    t2.GCitationRefCollection = GetCitationCollection(theLoadORElement);
                    t2.GNoteRefCollection = GetNoteCollection(theLoadORElement);

                    // TODO !ELEMENT objref (region?, attribute*, citationref*, noteref*)&gt;
                    // !ATTLIST objref hlink IDREF #REQUIRED priv (0|1) #IMPLIED
                    t.Add(t2);
                }
            }

            // Return sorted by the default text
            t.SortAndSetFirst();

            return t;
        }

        private PersonNameModelCollection GetPersonNameCollection(XElement xmlData)
        {
            PersonNameModelCollection t = new PersonNameModelCollection();

            var theERElement =
                    from orElementEl
                    in xmlData.Elements(ns + "name")
                    select orElementEl;

            if (theERElement.Any())
            {
                // Load attribute object references
                foreach (XElement theLoadORElement in theERElement)
                {
                    // TODO is date handling correct
                    PersonNameModel newPersonNameModel = new PersonNameModel
                    {
                        Handle = "PersonNameCollection",

                        GCitationRefCollection = GetCitationCollection(theLoadORElement),

                        GDate = SetDate(theLoadORElement),

                        GDisplay = GetElement(theLoadORElement, "display"),

                        GFamilyNick = GetElement(theLoadORElement, "familynick"),

                        GFirstName = GetElement(theLoadORElement, "first"),

                        GGroup = GetElement(theLoadORElement, "group"),

                        GNick = GetElement(theLoadORElement, "nick"),

                        GPriv = SetPrivateObject(GetAttribute(theLoadORElement.Attribute("priv"))),

                        GSort = GetElement(theLoadORElement, "sort"),

                        GSuffix = GetElement(theLoadORElement, "suffix"),

                        GSurName = GetSurnameCollection(theLoadORElement),

                        GTitle = GetElement(theLoadORElement, "title"),

                        GType = GetAttribute(theLoadORElement.Attribute("type")),

                        GNoteReferenceCollection = GetNoteCollection(theLoadORElement),
                    };

                    newPersonNameModel.GAlt.SetAlt(GetAttribute(theLoadORElement, "alt"));

                    // set the Home image or symbol
                    newPersonNameModel.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
                    newPersonNameModel.HomeImageHLink.HomeSymbol = CommonConstants.IconAttribute;

                    t.Add(newPersonNameModel);
                }
            }

            // Return sorted by the default text
            t.Sort(T => T.DeRef.GetDefaultText);

            return t;
        }

        private PersonRefModelCollection GetPersonRefCollection(XElement xmlData)
        {
            // TODO < define name = "personref-content" > < data type = "IDREF" /> < attribute name
            // = "priv" > < attribute name = "rel" > < element name = "citationref" > < element name
            // = "noteref" > < ref name = "noteref-content" />

            PersonRefModelCollection t = new PersonRefModelCollection();

            IEnumerable<XElement> theERElement =
                    from ORElementEl in xmlData.Elements(ns + "personref")
                    select ORElementEl;

            if (theERElement.Any())
            {
                // load person object references
                foreach (XElement theLoadORElement in theERElement)
                {
                    PersonRefModel t2 = new PersonRefModel
                    {
                        HLinkKey = GetAttribute(theLoadORElement.Attribute("hlink")),
                        Id = GetAttribute(theLoadORElement.Attribute("id")),
                        Change = GetDateTime(theLoadORElement, "change"),
                        Priv = SetPrivateObject(GetAttribute(theLoadORElement.Attribute("priv"))),
                        Handle = GetAttribute(theLoadORElement, "handle"),
                    };
                    t.Add(t2);
                }
            }

            return t;
        }

        private HLinkPlaceModelCollection GetPlaceRefCollection(XElement xmlData)
        {
            HLinkPlaceModelCollection t = new HLinkPlaceModelCollection();

            // Load NoteRefs
            var localPlaceElement =
                             from ElementEl in xmlData.Elements(ns + "placeref")
                             select ElementEl;

            if (localPlaceElement.Any())
            {
                // load note references
                foreach (XElement loadPlaceElement in localPlaceElement)
                {
                    HLinkPlaceModel noteHLink = new HLinkPlaceModel
                    {
                        // object details
                        HLinkKey = GetAttribute(loadPlaceElement.Attribute("hlink")),
                    };

                    // save the object
                    t.Add(noteHLink);
                }
            }

            // Return sorted by the default text
            t.Sort(T => T.DeRef.GetDefaultText);

            return t;
        }

        /// <summary>
        /// Gets the repository collection.
        /// </summary>
        /// <param name="xmlData">
        /// The XML data.
        /// </param>
        /// <returns>
        /// HLink Repository Model Collection.
        /// </returns>
        private HLinkRepositoryModelCollection GetRepositoryCollection(XElement xmlData)
        {
            HLinkRepositoryModelCollection t = new HLinkRepositoryModelCollection();

            var theERElement = from _ORElementEl in xmlData.Elements(ns + "reporef")
                               select _ORElementEl;

            if (theERElement.Any())
            {
                // load repository references
                foreach (XElement theLoadORElement in theERElement)
                {
                    HLinkRepositoryModel t2 = new HLinkRepositoryModel
                    {
                        // "callno" Done "medium" Done; "noteref" Done
                        HLinkKey = GetAttribute(theLoadORElement.Attribute("hlink")),

                        GPriv = SetPrivateObject(GetAttribute(theLoadORElement.Attribute("priv"))),

                        GCallNo = GetAttribute(theLoadORElement.Attribute("callno")),

                        GMedium = GetAttribute(theLoadORElement.Attribute("medium")),

                        GNoteRef = GetNoteCollection(theLoadORElement),
                    };
                    t.Add(t2);
                }
            }

            // Return sorted by the default text
            t.Sort(T => T.DeRef.GetDefaultText);

            return t;
        }

        /// <summary>
        /// Gets the source attribute collection.
        /// </summary>
        /// <param name="xmlData">
        /// The XML data.
        /// </param>
        /// <returns>
        /// </returns>
        private OCSrcAttributeModelCollection GetSrcAttributeCollection(XElement xmlData)
        {
            OCSrcAttributeModelCollection t = new OCSrcAttributeModelCollection();

            var theERElement =
                    from oRElementEl in xmlData.Elements(ns + "srcattribute")
                    select oRElementEl;

            if (theERElement.Any())
            {
                // load event object references
                foreach (XElement theLoadORElement in theERElement)
                {
                    SrcAttributeModel tt = new SrcAttributeModel
                    {
                        GPriv = SetPrivateObject(GetAttribute(theLoadORElement.Attribute("priv"))),

                        GType = GetAttribute(theLoadORElement.Attribute("type")),
                        GValue = GetAttribute(theLoadORElement.Attribute("value")),
                    };

                    t.Add(tt);
                }
            }

            // Return sorted by the default text
            t.Sort(T => T.DeRef.GetDefaultText);

            return t;
        }

        private SurnameModelCollection GetSurnameCollection(XElement xmlData)
        {
            SurnameModelCollection t = new SurnameModelCollection();

            var theERElement = from _ORElementEl in xmlData.Elements(ns + "surname")
                               select _ORElementEl;

            if (theERElement.Any())
            {
                // load repository references
                foreach (XElement theLoadORElement in theERElement)
                {
                    SurnameModel t2 = new SurnameModel
                    {
                        GText = GetElement(theLoadORElement),
                    };
                    t.Add(t2);
                }
            }

            // Return sorted by the default text
            t.Sort(T => T.DeRef.GetDefaultText);

            return t;
        }

        /// <summary>
        /// Gets the tag collection.
        /// </summary>
        /// <param name="xmlData">
        /// The XML data.
        /// </param>
        /// <returns>
        /// </returns>
        private HLinkTagModelCollection GetTagCollection(XElement xmlData)
        {
            HLinkTagModelCollection t = new HLinkTagModelCollection();

            var theERElement =
                    from _ORElementEl in xmlData.Elements(ns + "tagref")
                    select _ORElementEl;

            if (theERElement.Any())
            {
                // load tag references
                foreach (XElement theLoadORElement in theERElement)
                {
                    HLinkTagModel t2 = new HLinkTagModel
                    {
                        HLinkKey = GetAttribute(theLoadORElement.Attribute("hlink")),
                    };

                    t.Add(t2);
                }
            }

            // Return sorted by the default text
            t.Sort(T => T.DeRef.GetDefaultText);

            return t;
        }

        /// <summary>
        /// Load zero or more url content xml elements into alist of url models.
        /// </summary>
        /// <param name="xmlData">
        /// the xElement containing the url references.
        /// </param>
        private OCURLModelCollection GetURLCollection(XElement xmlData)
        {
            OCURLModelCollection t = new OCURLModelCollection();

            // Get colour
            Application.Current.Resources.TryGetValue("CardBackGroundUtility", out var varCardColour);
            Color cardColour = (Color)varCardColour;

            // Run query
            var theERElement =
                    from orElementEl
                    in xmlData.Elements(ns + "url")
                    select orElementEl;

            if (theERElement.Any())
            {
                HLinkHomeImageModel newHomeLink = new HLinkHomeImageModel
                {
                    HomeImageType = CommonConstants.HomeImageTypeSymbol,
                    HomeSymbol = CommonConstants.IconBookMark // TODO  Windows.UI.Xaml.Controls.Symbol.World,
                };

                // load event object references
                foreach (XElement theLoadORElement in theERElement)
                {
                    URLModel tt = new URLModel
                    {
                        Handle = "URL Collection",

                        HomeImageHLink = newHomeLink,

                        Priv = SetPrivateObject(GetAttribute(theLoadORElement.Attribute("priv"))),

                        GType = GetAttribute(theLoadORElement.Attribute("type")),

                        GHRef = GetUri(GetAttribute(theLoadORElement.Attribute("href"))),

                        GDescription = GetAttribute(theLoadORElement.Attribute("description")),
                    };

                    // set the Home image or symbol
                    tt.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
                    tt.HomeImageHLink.HomeSymbol = IconFont.Link;
                    tt.HomeImageHLink.HomeSymbolColour = cardColour;

                    t.Add(tt);
                }
            }

            // Return sorted by the default text
            t.Sort(T => T.DeRef.GetDefaultText);

            return t;
        }

        /// <summary>
        /// Gets the h link.
        /// </summary>
        /// <param name="xmlData">
        /// The XML data.
        /// </param>
        /// <returns>
        /// </returns>
        private HLinkBase HLink(XElement xmlData)
        {
            HLinkBase t = new HLinkBase();

            if (xmlData != null)
            {
                t.HLinkKey = GetAttribute(xmlData.Attribute("hlink"));
            }

            return t;
        }
    }
}