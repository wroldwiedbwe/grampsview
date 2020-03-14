//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="GrampsStoreXMLPostLoad.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.ExternalStorageNS
{
    using GrampsView.Common;

    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;
    using GrampsView.Events;
    using SkiaSharp;
    using SkiaSharp.Views.Forms;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    /// <summary>
    /// Creates a collection of entities with content read from a GRAMPS XML file.
    /// </summary>
    public partial class GrampsStorePostLoad : IStorePostLoad
    {
        /// <summary>
        /// Gets the tag reference home link.
        /// </summary>
        /// <param name="argHLink">
        /// The argument h link.
        /// </param>
        /// <returns>
        /// </returns>
        public static HLinkMediaModel GetTagRefHomeLink(TagModel argModel, HLinkMediaModel argHLink)
        {
            if (argModel is null)
            {
                throw new ArgumentNullException(nameof(argModel));
            }

            if (argHLink is null)
            {
                throw new ArgumentNullException(nameof(argHLink));
            }

            HLinkMediaModel returnHLink = argHLink;

            returnHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;

            // Set the colour of the tag ref to match the tag
            returnHLink.HomeSymbolColour = argModel.GColor;

            return returnHLink;
        }

        /// <summary>
        /// Sets the home h link.
        /// </summary>
        /// <param name="HomeImageHLink">
        /// The home image h link.
        /// </param>
        /// <param name="argHLink">
        /// The hlink.
        /// </param>
        /// <returns>
        /// </returns>
        public static async Task<HLinkHomeImageModel> SetHomeHLink(HLinkHomeImageModel argStartHLink, HLinkHomeImageModel argHLink)
        {
            if (argHLink.HLinkKey == "_e5bfa72904e68ce059252b501df")
            {
            }

            MediaModel theMediaModel = new MediaModel();

            SKBitmap resourceBitmap = new SKBitmap();

            // --------- Validate
            if (argStartHLink is null)
            {
                throw new ArgumentNullException(nameof(argStartHLink));
            }

            if (argHLink is null)
            {
                throw new ArgumentNullException(nameof(argHLink));
            }

            // --------- Copy link
            argStartHLink = argHLink;

            // --------- Check if media or symbol
            if (argStartHLink.HomeImageType == CommonConstants.HomeImageTypeSymbol)
            {
                return argStartHLink;
            }

            // --------- Check if MediaObject already exists
            theMediaModel = argStartHLink.DeRef;

            if (!theMediaModel.Valid)
            {
                DataStore.CN.NotifyError("Invalid argStartHLink DeRef (" + argStartHLink.HLinkKey + ") passed to SetHomeHLink");
                return argStartHLink;
            }

            if (theMediaModel.Id == "O0003")
            {
            }

            Debug.WriteLine(theMediaModel.MediaStorageFilePath);

            if (string.IsNullOrEmpty(theMediaModel.MediaStorageFilePath))
            {
                DataStore.CN.NotifyError("The media file path is null for Id:" + theMediaModel.Id);
                return argStartHLink;
            }

            // --------- Save Cropped Image
            string newHLinkKey = argStartHLink.HLinkKey + "-" + argStartHLink.GCorner1X + argStartHLink.GCorner1Y + argStartHLink.GCorner2X + argStartHLink.GCorner2Y;
            string outFileName = Path.Combine("Cropped", newHLinkKey + ".png");
            //string outFileName = Path.Combine("Cropped", argStartHLink.HLinkKey + ".png");

            string outFilePath = Path.Combine(DataStore.DS.CurrentDataFolder.FullName, outFileName);

            // Have a media image to display
            if (argStartHLink.NeedsClipping)
            {
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
                    Debug.WriteLine("resourceBitmap size", resourceBitmap.ByteCount);
                    if (resourceBitmap.ByteCount > int.MaxValue - 1000)
                    {
                        // TODO Handle this better. Perhaps resize? Delete for now
                        resourceBitmap = new SKBitmap();
                    }

                    float crleft = (float)(argStartHLink.GCorner1X / 100d * theMediaModel.MetaDataWidth);
                    float crright = (float)(argStartHLink.GCorner2X / 100d * theMediaModel.MetaDataWidth);
                    float crtop = (float)(argStartHLink.GCorner1Y / 100d * theMediaModel.MetaDataHeight);
                    float crbottom = (float)(argStartHLink.GCorner2Y / 100d * theMediaModel.MetaDataHeight);

                    SKRect cropRect = new SKRect(crleft, crtop, crright, crbottom);

                    SKBitmap croppedBitmap = new SKBitmap((int)cropRect.Width,
                                                      (int)cropRect.Height);
                    SKRect dest = new SKRect(0, 0, cropRect.Width, cropRect.Height);
                    SKRect source = new SKRect(cropRect.Left, cropRect.Top,
                                               cropRect.Right, cropRect.Bottom);

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

                    resourceBitmap.Dispose();

                    // ------------ Save new MediaObject
                    MediaModel newMediaObject = theMediaModel.Clone();

                    newMediaObject.HLinkKey = newHLinkKey;
                    newMediaObject.OriginalFilePath = outFileName;
                    //newMediaObject.HomeImageHLink.HLinkKey = newHLinkKey;
                    newMediaObject.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeThumbNail;

                    DataStore.DS.MediaData.Add(newMediaObject);
                    await fixMediaFile(newMediaObject).ConfigureAwait(false);
                }

                // ------------ Change HomeImageLink to point to new image
                argStartHLink.HomeImageType = CommonConstants.HomeImageTypeThumbNail;
                argStartHLink.HLinkKey = newHLinkKey;
                argStartHLink.GCorner1X = 0;
                argStartHLink.GCorner1Y = 0;
                argStartHLink.GCorner2X = 0;
                argStartHLink.GCorner2Y = 0;
            }

            return argStartHLink;
        }

        /// <summary>
        /// Organises the book mark repository.
        /// </summary>
        private static void OrganiseBookMarkRepository()
        {
            DataStore.CN.MajorStatusAdd("Organising BookMark data");

            //foreach (BookMarkModel argModel in DV.BookMarkDV.BookMarkData)
            //{
            //    argModel.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
            //}
        }

        /// <summary>
        /// Organises the citation repository.
        /// </summary>
        private static async Task<bool> OrganiseCitationRepository()
        {
            DataStore.CN.MajorStatusAdd("Organising Citation data");

            foreach (CitationModel citationModel in DV.CitationDV.DataViewData)
            {
                if (citationModel.Id == "C0575")
                {
                }

                HLinkCitationModel t = citationModel.HLink;

                // -- Organise BackLinks ---------------------

                // Media Collection - Create backlinks in media models to citation models
                foreach (HLinkMediaModel mediaRef in citationModel.GMediaRefCollection)
                {
                    DataStore.DS.MediaData[mediaRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Back Reference Note HLinks
                foreach (HLinkNoteModel noteRef in citationModel.GNoteRefCollection)
                {
                    DataStore.DS.NoteData[noteRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Back Reference Source HLink
                DataStore.DS.SourceData[citationModel.GSourceRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));

                // Back Reference Tag HLinks
                foreach (HLinkTagModel tagRef in citationModel.GTagRef)
                {
                    DataStore.DS.TagData[tagRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // -- Organsie Default FirstLinks ------------------------------

                // Sort media collection and get first link images
                DataStore.DS.CitationData[citationModel.HLinkKey].GMediaRefCollection.SortAndSetFirst();

                // Sort note collection and get first link images
                DataStore.DS.CitationData[citationModel.HLinkKey].GNoteRefCollection.SortAndSetFirst();

                // -- Organise Home Images -----------------------

                // Try media reference collection first
                HLinkHomeImageModel hlink = citationModel.GMediaRefCollection.FirstHLinkHomeImage;

                // Check Source for Image
                if (!hlink.Valid)
                {
                    if (citationModel.GSourceRef.DeRef.HomeImageHLink.HomeUseImage)
                    {
                        hlink = citationModel.GSourceRef.DeRef.HomeImageHLink;
                    }
                }

                // Handle the link if we can
                if (!hlink.Valid)
                {
                    citationModel.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
                }
                else
                {
                    citationModel.HomeImageHLink = await SetHomeHLink(citationModel.HomeImageHLink, hlink);
                }

                DataStore.DS.CitationData[citationModel.HLinkKey] = citationModel;
            }

            return true;
        }

        /// <summary>
        /// Organises the event repository.
        /// </summary>
        private static async Task<bool> OrganiseEventRepository()
        {
            DataStore.CN.MajorStatusAdd("Organising Event data");

            foreach (EventModel eventModel in DV.EventDV.DataViewData)
            {
                HLinkEventModel t = eventModel.HLink;

                if (eventModel.Id == "E0059")
                {
                }

                // Citation Collection
                foreach (HLinkCitationModel citationRef in eventModel.GCitationRefCollection)
                {
                    DataStore.DS.CitationData[citationRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Media Collection
                foreach (HLinkMediaModel mediaRef in eventModel.GMediaRefCollection)
                {
                    DataStore.DS.MediaData[mediaRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Sort media collection and get first link images
                DataStore.DS.EventData[eventModel.HLinkKey].GMediaRefCollection.SortAndSetFirst();

                // eventModel.GMediaRefCollection = DV.MediaDV.HLinkCollectionSort(eventModel.GMediaRefCollection);

                // DV.EventDV.EventData[eventModel.HLinkKey].GMediaRefCollection.FirstHLink = DV.MediaDV.GetFirstImageFromCollection(DV.EventDV.EventData[eventModel.HLinkKey].GMediaRefCollection);

                // Place Reference
                if (eventModel.GPlace.Valid)
                {
                    DataStore.DS.PlaceData[eventModel.GPlace.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Back Reference Note HLinks
                foreach (HLinkNoteModel noteRef in eventModel.GNoteRefCollection)
                {
                    DataStore.DS.EventData[noteRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Setup home images

                // Try media reference collection first
                HLinkHomeImageModel hlink = eventModel.GMediaRefCollection.FirstHLinkHomeImage;

                // Check Media for Images
                if (!hlink.Valid)
                {
                    hlink = eventModel.GMediaRefCollection.FirstHLinkHomeImage;
                }

                // Check Citation for Images
                if (!hlink.Valid)
                {
                    hlink = eventModel.GCitationRefCollection.FirstHLinkHomeImage;

                    //hlink = DV.CitationDV.GetFirstImageFromCollection(argModel.GCitationRefCollection);
                }

                // Handle the link if we can
                if (!hlink.Valid)
                {
                    eventModel.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
                }
                else
                {
                    eventModel.HomeImageHLink = await SetHomeHLink(eventModel.HomeImageHLink, hlink);
                }

                DataStore.DS.EventData[eventModel.HLinkKey] = eventModel;
            }

            return true;
        }

        /// <summary>
        /// Organises the family repository.
        /// </summary>
        private static async Task<bool> OrganiseFamilyRepository()
        {
            DataStore.CN.MajorStatusAdd("Organising Family data ");

            foreach (FamilyModel familyModel in DV.FamilyDV.DataViewData)
            {
                HLinkFamilyModel t = familyModel.HLink;

                // -- Organse Back Links ---------------------

                // Child Collection
                foreach (HLinkPersonModel personRef in familyModel.GChildRefCollection)
                {
                    DataStore.DS.PersonData[personRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Citation Collection
                foreach (HLinkCitationModel citationRef in familyModel.GCitationRefCollection)
                {
                    DataStore.DS.CitationData[citationRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Back Reference Event HLinks
                foreach (HLinkEventModel eventRef in familyModel.GEventRefCollection)
                {
                    DataStore.DS.EventData[eventRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Media Collection
                foreach (HLinkMediaModel mediaRef in familyModel.GMediaRefCollection)
                {
                    DataStore.DS.MediaData[mediaRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Note Collection
                foreach (HLinkNoteModel noteRef in familyModel.GNoteRefCollection)
                {
                    DataStore.DS.NoteData[noteRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Tag Collection
                foreach (HLinkTagModel tagRef in familyModel.GTagRefCollection)
                {
                    DataStore.DS.TagData[tagRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // -- Organse First and Sorts --------------------------

                DataStore.DS.FamilyData[familyModel.HLinkKey].GCitationRefCollection.SortAndSetFirst();

                DataStore.DS.FamilyData[familyModel.HLinkKey].GEventRefCollection.SortAndSetFirst();

                DataStore.DS.FamilyData[familyModel.HLinkKey].GMediaRefCollection.SortAndSetFirst();

                DataStore.DS.FamilyData[familyModel.HLinkKey].GNoteRefCollection.SortAndSetFirst();

                // -- Organse Home Image ---------------------

                // Try media reference collection first
                HLinkHomeImageModel hlink = familyModel.GMediaRefCollection.FirstHLinkHomeImage;

                if (!hlink.Valid)
                {
                    hlink = familyModel.GCitationRefCollection.FirstHLinkHomeImage;
                }

                if (!hlink.Valid)
                {
                    hlink = familyModel.GEventRefCollection.FirstHLinkHomeImage;
                }

                if (!hlink.Valid)
                {
                    hlink = familyModel.GNoteRefCollection.FirstHLinkHomeImage;
                }

                // Set the image if available
                if (hlink.Valid)
                {
                    familyModel.HomeImageHLink = await SetHomeHLink(familyModel.HomeImageHLink, hlink);
                }
                else
                {
                    // Set to default
                    familyModel.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
                }

                DataStore.DS.FamilyData[familyModel.HLinkKey] = familyModel;
            }

            return true;
        }

        /// <summary>
        /// Organises the header repository.
        /// </summary>
        private static void OrganiseHeaderRepository()
        {
            DataStore.CN.MajorStatusAdd("Organising Header data");
        }

        /// <summary>
        /// Organises the media repository.
        /// </summary>
        private static void OrganiseMediaRepository()
        {
            DataStore.CN.MajorStatusAdd("Organising Media data");

            foreach (MediaModel mediaObject in DV.MediaDV.DataViewData)
            {
                HLinkMediaModel t = mediaObject.HLink;

                if (mediaObject.Id == "O0032")
                {
                }

                // TODO Change to SortAndSetFirst

                // Back Reference Citation HLinks
                foreach (HLinkCitationModel citationRef in mediaObject.GCitationRefCollection)
                {
                    DataStore.DS.CitationData[citationRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Back Reference Note HLinks
                foreach (HLinkNoteModel noteRef in mediaObject.GNoteRefCollection)
                {
                    DataStore.DS.NoteData[noteRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Back Reference Tag HLinks
                foreach (HLinkTagModel tagRef in mediaObject.GTagRefCollection)
                {
                    DataStore.DS.TagData[tagRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // TODO Change to SortAndSetFirst

                // Setup HomeImage
                mediaObject.HomeImageHLink.HLinkKey = mediaObject.HLink.HLinkKey;

                switch (mediaObject.FileMimeType)
                {
                    case "application":
                        {
                            switch (mediaObject.FileMimeSubType)
                            {
                                case "pdf":
                                    {
                                        mediaObject.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
                                        mediaObject.HomeImageHLink.HomeSymbol = IconFont.FilePdf;
                                        break;
                                    }

                                case "x-zip-compressed":
                                    {
                                        mediaObject.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
                                        mediaObject.HomeImageHLink.HomeSymbol = IconFont.ZipBox;
                                        break;
                                    }

                                case "zip":
                                    {
                                        mediaObject.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
                                        mediaObject.HomeImageHLink.HomeSymbol = IconFont.ZipBox;
                                        break;
                                    }

                                default:
                                    {
                                        mediaObject.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
                                        mediaObject.HomeImageHLink.HomeSymbol = IconFont.FileDocument;
                                        break;
                                    }
                            }

                            break;
                        }

                    case "image":
                        {
                            mediaObject.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeThumbNail;
                            mediaObject.HomeImageHLink.HomeSymbol = IconFont.Image;
                            break;
                        }

                    case "video":
                        {
                            mediaObject.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
                            mediaObject.HomeImageHLink.HomeSymbol = IconFont.Video;
                            break;
                        }

                    default:
                        {
                            mediaObject.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeThumbNail;
                            mediaObject.HomeImageHLink.HomeSymbol = CommonConstants.IconMedia;
                            break;
                        }
                }

                DataStore.DS.MediaData[mediaObject.HLinkKey] = mediaObject;
            }
        }

        /// <summary>
        /// Organises the namemap repository.
        /// </summary>
        private static void OrganiseNameMapRepository()
        {
            DataStore.CN.MajorStatusAdd("Organising NameMap data");

            //foreach (NameMapModel nnameMapObject in DV.NameMapDV.NameMapData)
            //{
            //}
        }

        /// <summary>
        /// Organises the note repository.
        /// </summary>
        private static void OrganiseNoteRepository()
        {
            DataStore.CN.MajorStatusAdd("Organising Note data");

            foreach (NoteModel note in DV.NoteDV.DataViewData)
            {
                HLinkNoteModel t = note.HLink;

                // -- Organse Back Links ---------------------

                // Citation Collection

                foreach (HLinkTagModel tagnRef in note.GTagRefCollection)
                {
                    DataStore.DS.TagData[tagnRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }
            }
        }

        /// <summary>
        /// Organises the person repository.
        /// </summary>
        private static async Task<bool> OrganisePersonRepository()
        {
            DataStore.CN.MajorStatusAdd("Organising Person data");

            foreach (PersonModel person in DV.PersonDV.DataViewData)
            {
                HLinkPersonModel t = person.HLink;

                if (person.Id == "I0568")
                {
                }
                // -- Organse Back Links ---------------------

                // Citation Collection

                foreach (HLinkCitationModel citationRef in person.GCitationRefCollection)
                {
                    DataStore.DS.CitationData[citationRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Event Collection

                foreach (HLinkEventModel eventRef in person.GEventRefCollection)
                {
                    DataStore.DS.EventData[eventRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                foreach (HLinkMediaModel mediaRef in person.GMediaRefCollection)
                {
                    DataStore.DS.MediaData[mediaRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Note Collection
                foreach (HLinkNoteModel noteRef in person.GNoteRefCollection)
                {
                    DataStore.DS.NoteData[noteRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Parent RelationShip

                foreach (HLinkFamilyModel familyRef in person.GParentInRefCollection)
                {
                    DataStore.DS.FamilyData[familyRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Sibling Collection

                foreach (HLinkPersonModel personRef in person.SiblingRefCollection)
                {
                    DataStore.DS.PersonData[personRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Back Reference Tag HLinks
                for (int i = 0; i < person.GTagRefCollection.Count; i++)
                {
                    HLinkTagModel tagRef = person.GTagRefCollection[i];

                    // Update the tag ref with colours and symbols
                    person.GTagRefCollection[i].HomeImageHLink = GetTagRefHomeLink(tagRef.DeRef, tagRef.HomeImageHLink);

                    // Set the backlinks
                    DataStore.DS.TagData[tagRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // -- Organise First Image and Sorts ------------------------------

                DataStore.DS.PersonData[person.HLinkKey].GCitationRefCollection.SortAndSetFirst();

                DataStore.DS.PersonData[person.HLinkKey].GEventRefCollection.SortAndSetFirst();

                // Sort media collection and get first link images
                DataStore.DS.PersonData[person.HLinkKey].GMediaRefCollection.SortAndSetFirst();

                DataStore.DS.PersonData[person.HLinkKey].GNoteRefCollection.SortAndSetFirst();

                DataStore.DS.PersonData[person.HLinkKey].GParentInRefCollection.SortAndSetFirst();

                DataStore.DS.PersonData[person.HLinkKey].SiblingRefCollection.SortAndSetFirst();

                // -- Organsie Home Image ------------------------------

                foreach (PersonModel argModel in DV.PersonDV.PersonData.GetList)
                {
                    //if (argModel.Id == "I0568")
                    //{
                    //}

                    // Get default image if available
                    HLinkHomeImageModel hlink = DV.PersonDV.GetDefaultImageFromCollection(argModel);

                    // Check Media for Images
                    if (!hlink.Valid)
                    {
                        hlink = argModel.GMediaRefCollection.FirstHLinkHomeImage;
                    }

                    // Check Citation for Images
                    if (!hlink.Valid)
                    {
                        hlink = argModel.GCitationRefCollection.FirstHLinkHomeImage;
                    }

                    // Action any Link
                    if (!hlink.Valid)
                    {
                        argModel.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
                    }
                    else
                    {
                        argModel.HomeImageHLink = await SetHomeHLink(argModel.HomeImageHLink, hlink);
                    }

                    DataStore.DS.PersonData[argModel.HLinkKey] = argModel;
                }

                // -- Setup some extra values ------------------------------

                // set Birthdate
                EventModel birthDate = DV.EventDV.GetEventType(person.GEventRefCollection, CommonConstants.EventTypeBirth);
                if (birthDate.Valid)
                {
                    person.BirthDate = birthDate.GDate;
                }

                // set Is Living
                if (DV.EventDV.GetEventType(person.GEventRefCollection, CommonConstants.EventTypeDeath).Valid)
                {
                    person.IsLiving = false;
                }
                else
                {
                    person.IsLiving = true;
                }

                // set Sibling Collection
                if (person.GChildOf.Valid)
                {
                    person.SiblingRefCollection = DV.FamilyDV.FamilyData[person.GChildOf.HLinkKey].GChildRefCollection;
                }

                DataStore.DS.PersonData[person.HLinkKey] = person;
            }

            return true;
        }

        /// <summary>
        /// Organises the place repository.
        /// </summary>
        private static void OrganisePlaceRepository()
        {
            DataStore.CN.MajorStatusAdd("Organising Place data");

            foreach (PlaceModel placeObject in DV.PlaceDV.DataViewData)
            {
                HLinkPlaceModel t = placeObject.HLink;

                // TODO fill this

                // Back Reference Citation HLinks
                foreach (HLinkCitationModel citationRef in placeObject.GCitationRefCollection)
                {
                    DataStore.DS.CitationData[citationRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Back Reference Note HLinks
                foreach (HLinkNoteModel noteRef in placeObject.GNoteRefCollection)
                {
                    DataStore.DS.NoteData[noteRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Back Reference Media HLinks
                foreach (HLinkMediaModel mediaRef in placeObject.GMediaRefCollection)
                {
                    DataStore.DS.MediaData[mediaRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Back Reference Place HLinks
                foreach (HLinkPlaceModel placeRef in placeObject.GPlaceRefCollection)
                {
                    DataStore.DS.PlaceData[placeRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Back Reference Tag HLinks
                foreach (HLinkTagModel tagRef in placeObject.GTagRefCollection)
                {
                    DataStore.DS.TagData[tagRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }
            }
        }

        /// <summary>
        /// Organises the repository repository.
        /// </summary>
        private static void OrganiseRepositoryRepository()
        {
            DataStore.CN.MajorStatusAdd("Organising Repository data");

            foreach (RepositoryModel repositoryObject in DV.RepositoryDV.DataViewData)
            {
                HLinkRepositoryModel t = repositoryObject.HLink;

                // Back Reference Note HLinks
                foreach (HLinkNoteModel noteRef in repositoryObject.GNoteRefCollection)
                {
                    DataStore.DS.NoteData[noteRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Back Reference Tag HLinks
                foreach (HLinkTagModel tagRef in repositoryObject.GTagRefCollection)
                {
                    DataStore.DS.TagData[tagRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }
            }
        }

        private static async Task<bool> OrganiseSourceRepository()
        {
            DataStore.CN.MajorStatusAdd("Organising Source data");

            foreach (SourceModel sourceObject in DV.SourceDV.DataViewData)
            {
                HLinkSourceModel t = sourceObject.HLink;

                if (t.HLinkKey == "_c49238f73e868050e85")
                {
                }

                try
                {
                    // -- Organse Back Links ---------------------

                    // Source Attribute Collection is model so no backlink

                    //// Media Collection

                    foreach (HLinkMediaModel mediaRef in sourceObject.GMediaRefCollection)
                    {
                        DataStore.DS.MediaData[mediaRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                    }

                    // Note Collection
                    foreach (IHLinkNoteModel noteRef in sourceObject.GNoteRefCollection)
                    {
                        DataStore.DS.NoteData[noteRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                    }

                    // Repository Collection
                    foreach (HLinkRepositoryModel repositoryRef in sourceObject.GRepositoryRefCollection)
                    {
                        DataStore.DS.RepositoryData[repositoryRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                    }

                    // Tag Collection
                    foreach (HLinkTagModel tagRef in sourceObject.GTagRefCollection)
                    {
                        DataStore.DS.TagData[tagRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                    }

                    // -- Organse First and Sorts ---------------------

                    // Sort media collection and get first link images
                    DataStore.DS.SourceData[sourceObject.HLinkKey].GMediaRefCollection.SortAndSetFirst();

                    // TODO First and Sort for Notes, Repositories and Tags

                    // Get default image if available
                    HLinkHomeImageModel hlink = sourceObject.GMediaRefCollection.FirstHLinkHomeImage;

                    // Action default media image
                    if (!hlink.Valid)
                    {
                        // Check for icon
                        hlink = DV.MediaDV.GetFirstImageFromCollection(sourceObject.GMediaRefCollection);
                    }

                    // Set default
                    if (!hlink.Valid)
                    {
                        sourceObject.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
                    }
                    else
                    {
                        sourceObject.HomeImageHLink = await SetHomeHLink(sourceObject.HomeImageHLink, hlink);
                    }

                    DataStore.DS.SourceData[sourceObject.HLinkKey] = sourceObject;
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return true;
        }

        /// <summary>
        /// Organises the tag repository.
        /// </summary>
        private static async Task<bool> OrganiseTagRepository()
        {
            DataStore.CN.MajorStatusAdd("Organising Tag data");

            foreach (TagModel argModel in DV.TagDV.DataViewData)
            {
                HLinkHomeImageModel hlink = null;

                // Set default
                if (hlink is null)
                {
                    argModel.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
                    argModel.HomeImageHLink.HomeSymbolColour = argModel.GColor;
                }
                else
                {
                    argModel.HomeImageHLink = await SetHomeHLink(argModel.HomeImageHLink, hlink);
                }

                DataStore.DS.TagData[argModel.HLinkKey] = argModel;
            }

            return true;
        }

        /// <summary>
        /// Loads the thumbnails etc on the UI thread due to limitations with BitMapImage in
        /// Background threads.
        /// </summary>
        /// <param name="notUsed">
        /// The not used.
        /// </param>
        private async void LoadXMLUIItems(object notUsed)
        {
            _CL.LogRoutineEntry("LoadXMLUIItems");

            await DataStore.CN.ChangeLoadingMessage("Organising data after load").ConfigureAwait(false);
            {
                await DataStore.CN.MajorStatusAdd("This will take a while...").ConfigureAwait(false);
                {
                    // Preload image cache
                    StoreFile ttt = new StoreFile();

                    foreach (MediaModel item in DataStore.DS.MediaData.GetList)
                    {
                        //ImageInformation t = new ImageInformation();

                        ////LoadingResult tt = new LoadingResult();

                        item.MediaStorageFile = await StoreFile.GetStorageFileAsync(item.OriginalFilePath).ConfigureAwait(false);

                        //if (item.IsMediaFile)
                        //{
                        //    TaskParameter imageTask;

                        // ImageService.Instance.LoadFile(item.MediaStorageFilePath).OnSuccess(ImageInformation
                        // info, LoadingResult result) =>

                        //    {
                        //        item.MetaDataHeight = info.OriginalHeight;
                        //        item.MetaDataWidth = info.OriginalWidth;
                        //    });
                        //}

                        if (item.Id == "O0196")
                        {
                        }

                        var imageSize = DependencyService.Get<IImageResource>().GetSize(item.MediaStorageFilePath);
                        System.Diagnostics.Debug.WriteLine(imageSize);
                        item.MetaDataHeight = imageSize.Height;
                        item.MetaDataWidth = imageSize.Width;
                    }

                    // Called in order of media linkages from Media outwards
                    OrganiseMediaRepository();

                    await OrganiseSourceRepository().ConfigureAwait(false);

                    await OrganiseCitationRepository().ConfigureAwait(false);

                    await OrganiseEventRepository().ConfigureAwait(false);

                    await OrganiseFamilyRepository().ConfigureAwait(false);

                    OrganiseHeaderRepository();

                    OrganiseBookMarkRepository();

                    OrganiseNameMapRepository();

                    OrganiseNoteRepository();

                    OrganisePlaceRepository();

                    OrganiseRepositoryRepository();

                    await OrganiseTagRepository().ConfigureAwait(false);

                    // People last as they depend on everything
                    await OrganisePersonRepository().ConfigureAwait(false);
                }

                await DataStore.CN.MajorStatusDelete().ConfigureAwait(false);
            }

            await DataStore.CN.ChangeLoadingMessage(null).ConfigureAwait(false);

            await DataStore.CN.MajorStatusAdd("Load XML UI Complete - Data ready for display").ConfigureAwait(false);

            //// save the data in a serial format for next time
            _EventAggregator.GetEvent<DataSaveSerialEvent>().Publish(null);

            // let everybody know we have finished loading data
            _EventAggregator.GetEvent<DataLoadCompleteEvent>().Publish(null);

            _CL.LogRoutineExit(nameof(LoadXMLUIItems));
        }
    }
}