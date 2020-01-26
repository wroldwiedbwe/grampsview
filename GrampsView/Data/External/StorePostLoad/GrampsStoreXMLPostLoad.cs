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
    using Xamarin.Forms;

    /// <summary>
    /// Creates a collection of entities with content read from a GRAMPS XML file.
    /// </summary>
    public partial class GrampsStorePostLoad : IStorePostLoad
    {
        /// <summary>
        /// Organises the book mark repository.
        /// </summary>
        private static void OrganiseBookMarkRepository()
        {
            DataStore.CN.MajorStatusAdd("Organising BookMark data");
        }

        /// <summary>
        /// Organises the citation repository.
        /// </summary>
        private static void OrganiseCitationRepository()
        {
            DataStore.CN.MajorStatusAdd("Organising Citation data");

            foreach (CitationModel citationModel in DV.CitationDV.CitationData)
            {
                if (citationModel.Id == "C0575")
                {
                }

                HLinkCitationModel t = citationModel.GetHLink;

                // -- Organsie BackLinks
                // ---------------------

                // Media Collection - Create backlinks in media models to citation models
                foreach (HLinkMediaModel mediaRef in citationModel.GMediaRefCollection)
                {
                    DV.MediaDV.MediaData[mediaRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Back Reference Note HLinks
                foreach (HLinkNoteModel noteRef in citationModel.GNoteRef)
                {
                    DV.NoteDV.NoteData[noteRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Back Reference Source HLink
                DV.SourceDV.SourceData[citationModel.GSourceRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));

                // Back Reference Tag HLinks
                foreach (HLinkTagModel tagRef in citationModel.GTagRef)
                {
                    DV.TagDV.TagData[tagRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // -- Organsie Default FirstLinks
                // ------------------------------

                // Sort media collection and get first link images
                DV.CitationDV.CitationData[citationModel.HLinkKey].GMediaRefCollection.SortAndSetFirst();

                // Sort note collection and get first link images
                DV.CitationDV.CitationData[citationModel.HLinkKey].GNoteRef.SortAndSetFirst();

                // -- Organise Home Images
                // -----------------------

                // Try media reference collection first
                HLinkMediaModel hlink = citationModel.GMediaRefCollection.FirstHLink;

                // Check Source for Image
                if (hlink == null)
                {
                    if (citationModel.GSourceRef.DeRef.HomeImageHLink.HomeUseImage)
                    {
                        hlink = citationModel.GSourceRef.DeRef.HomeImageHLink;
                    }
                }

                // Handle the link if we can
                if (hlink == null)
                {
                    citationModel.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
                }
                else
                {
                    citationModel.HomeImageHLink = SetHomeHLink(citationModel.HomeImageHLink, hlink);
                }
            }
        }

        /// <summary>
        /// Organises the event repository.
        /// </summary>
        private static void OrganiseEventRepository()
        {
            DataStore.CN.MajorStatusAdd("Organising Event data");

            foreach (EventModel eventModel in DV.EventDV.EventData)
            {
                HLinkEventModel t = eventModel.GetHLink;

                if (eventModel.Id == "E0059")
                {
                }

                // Citation Collection
                foreach (HLinkCitationModel citationRef in eventModel.GCitationRefCollection)
                {
                    DV.CitationDV.CitationData[citationRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Media Collection
                foreach (HLinkMediaModel mediaRef in eventModel.GMediaRefCollection)
                {
                    DV.MediaDV.MediaData[mediaRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Sort media collection and get first link images
                DV.EventDV.EventData[eventModel.HLinkKey].GMediaRefCollection.SortAndSetFirst();

                // eventModel.GMediaRefCollection = DV.MediaDV.HLinkCollectionSort(eventModel.GMediaRefCollection);

                // DV.EventDV.EventData[eventModel.HLinkKey].GMediaRefCollection.FirstHLink = DV.MediaDV.GetFirstImageFromCollection(DV.EventDV.EventData[eventModel.HLinkKey].GMediaRefCollection);

                // Place Reference
                DV.PlaceDV.PlaceData[eventModel.GPlace.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));

                // Back Reference Note HLinks
                foreach (HLinkNoteModel noteRef in eventModel.GNoteRefCollection)
                {
                    DV.EventDV.GetModel(noteRef.HLinkKey).BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }
            }
        }

        /// <summary>
        /// Organises the family repository.
        /// </summary>
        private static void OrganiseFamilyRepository()
        {
            DataStore.CN.MajorStatusAdd("Organising Family data ");

            foreach (FamilyModel familyModel in DV.FamilyDV.FamilyData)
            {
                HLinkFamilyModel t = familyModel.GetHLink;

                // -- Organse Back Links
                // ---------------------

                // Child Collection
                foreach (HLinkPersonModel personRef in familyModel.GChildRefCollection)
                {
                    DV.PersonDV.PersonData[personRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Citation Collection
                foreach (HLinkCitationModel citationRef in familyModel.GCitationRefCollection)
                {
                    DV.CitationDV.CitationData[citationRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Back Reference Event HLinks
                foreach (HLinkEventModel eventRef in familyModel.GEventRefCollection)
                {
                    DV.EventDV.EventData[eventRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Media Collection
                foreach (HLinkMediaModel mediaRef in familyModel.GMediaRefCollection)
                {
                    DV.MediaDV.MediaData[mediaRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Note Collection
                foreach (HLinkNoteModel noteRef in familyModel.GNoteRefCollection)
                {
                    DV.NoteDV.NoteData[noteRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Tag Collection
                foreach (HLinkTagModel tagRef in familyModel.GTagRefCollection)
                {
                    DV.TagDV.TagData[tagRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // -- Organse First and Sorts
                // --------------------------

                DV.FamilyDV.FamilyData[familyModel.HLinkKey].GCitationRefCollection.SortAndSetFirst();

                DV.FamilyDV.FamilyData[familyModel.HLinkKey].GEventRefCollection.SortAndSetFirst();

                DV.FamilyDV.FamilyData[familyModel.HLinkKey].GMediaRefCollection.SortAndSetFirst();

                DV.FamilyDV.FamilyData[familyModel.HLinkKey].GNoteRefCollection.SortAndSetFirst();

                // -- Organse Home Image
                // ---------------------

                // Try media reference collection first
                HLinkMediaModel hlink = familyModel.GMediaRefCollection.FirstHLink;

                if (hlink == null)
                {
                    hlink = familyModel.GCitationRefCollection.FirstHLink;
                }

                if (hlink == null)
                {
                    hlink = familyModel.GEventRefCollection.FirstHLink;
                }

                if (hlink == null)
                {
                    hlink = familyModel.GNoteRefCollection.FirstHLink;
                }

                // Set the image if available
                if (hlink != null)
                {
                    familyModel.HomeImageHLink = SetHomeHLink(familyModel.HomeImageHLink, hlink);
                }
                else
                {
                    // Set to default
                    familyModel.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
                }
            }
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

            foreach (MediaModel mediaObject in DV.MediaDV.MediaData)
            {
                HLinkMediaModel t = mediaObject.GetHLink;

                // Event Collection
                mediaObject.GEventRefCollection = DV.EventDV.HLinkCollectionSort(mediaObject.GEventRefCollection);

                // TODO Change to SortAndSetFirst

                // Family Collection
                mediaObject.GFamilyRefCollection = DV.FamilyDV.HLinkCollectionSort(mediaObject.GFamilyRefCollection);

                // TODO Change to SortAndSetFirst

                // Back Reference Citation HLinks
                foreach (HLinkCitationModel citationRef in mediaObject.GCitationRefCollection)
                {
                    DV.CitationDV.CitationData[citationRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Back Reference Note HLinks
                foreach (HLinkNoteModel noteRef in mediaObject.GNoteRefCollection)
                {
                    DV.NoteDV.NoteData[noteRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Back Reference Tag HLinks
                foreach (HLinkTagModel tagRef in mediaObject.GTagRefCollection)
                {
                    DV.TagDV.TagData[tagRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Person Collection
                mediaObject.GPersonRefCollection = DV.PersonDV.HLinkCollectionSort(mediaObject.GPersonRefCollection);

                // TODO Change to SortAndSetFirst
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

            foreach (NoteModel note in DV.NoteDV.NoteData)
            {
                HLinkNoteModel t = note.GetHLink;

                // -- Organse Back Links
                // ---------------------

                // Citation Collection

                foreach (HLinkTagModel tagnRef in note.GTagRefCollection)
                {
                    DV.TagDV.TagData[tagnRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }
            }
        }

        /// <summary>
        /// Organises the person repository.
        /// </summary>
        private static void OrganisePersonRepository()
        {
            DataStore.CN.MajorStatusAdd("Organising Person data");

            foreach (PersonModel person in DV.PersonDV.PersonData)
            {
                HLinkPersonModel t = person.GetHLink;

                if (person.Id == "I0568")
                {
                }
                // -- Organse Back Links
                // ---------------------

                // Citation Collection

                foreach (HLinkCitationModel citationRef in person.GCitationRefCollection)
                {
                    DV.CitationDV.CitationData[citationRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Event Collection

                foreach (HLinkEventModel eventRef in person.GEventRefCollection)
                {
                    DV.EventDV.GetModel(eventRef.HLinkKey).BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                foreach (HLinkMediaModel mediaRef in person.GMediaRefCollection)
                {
                    DV.MediaDV.MediaData[mediaRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Note Collection
                foreach (HLinkNoteModel noteRef in person.GNoteRefCollection)
                {
                    DV.NoteDV.NoteData[noteRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Parent RelationShip

                foreach (HLinkFamilyModel familyRef in person.GParentInRefCollection)
                {
                    DV.FamilyDV.FamilyData[familyRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Sibling Collection

                foreach (HLinkPersonModel personRef in person.SiblingRefCollection)
                {
                    DV.PersonDV.PersonData[personRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Back Reference Tag HLinks
                for (int i = 0; i < person.GTagRefCollection.Count; i++)
                {
                    HLinkTagModel tagRef = person.GTagRefCollection[i];

                    // Update the tag ref with colours and symbols
                    person.GTagRefCollection[i].HomeImageHLink = GetTagRefHomeLink(tagRef.DeRef, tagRef.HomeImageHLink);

                    // Set the backlinks
                    DV.TagDV.GetModel(tagRef.HLinkKey).BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // -- Organise First Image and Sorts
                // ------------------------------

                DV.PersonDV.PersonData[person.HLinkKey].GCitationRefCollection.SortAndSetFirst();

                DV.PersonDV.PersonData[person.HLinkKey].GEventRefCollection.SortAndSetFirst();

                // Sort media collection and get first link images
                DV.PersonDV.PersonData[person.HLinkKey].GMediaRefCollection.SortAndSetFirst();

                DV.PersonDV.PersonData[person.HLinkKey].GNoteRefCollection.SortAndSetFirst();

                DV.PersonDV.PersonData[person.HLinkKey].GParentInRefCollection.SortAndSetFirst();

                DV.PersonDV.PersonData[person.HLinkKey].SiblingRefCollection.SortAndSetFirst();

                // -- Organsie Home Image
                // ------------------------------

                foreach (PersonModel argModel in DV.PersonDV.PersonData)
                {
                    if (argModel.Id == "I0568")
                    {
                    }

                    // Get default image if available
                    HLinkMediaModel hlink = DV.PersonDV.GetDefaultImageFromCollection(argModel);

                    // Check Media for Images
                    if (hlink is null)
                    {
                        hlink = argModel.GMediaRefCollection.FirstHLink;
                    }

                    // Check Citation for Images
                    if (hlink is null)
                    {
                        hlink = argModel.GCitationRefCollection.FirstHLink;
                    }

                    // Action any Link
                    if (hlink is null)
                    {
                        argModel.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
                    }
                    else
                    {
                        argModel.HomeImageHLink = SetHomeHLink(argModel.HomeImageHLink, hlink);
                    }
                }

                // -- Setup some extra values
                // ------------------------------

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
                person.SiblingRefCollection = DV.FamilyDV.FamilyData[person.GChildOf.HLinkKey].GChildRefCollection;
            }
        }

        /// <summary>
        /// Organises the place repository.
        /// </summary>
        private static void OrganisePlaceRepository()
        {
            DataStore.CN.MajorStatusAdd("Organising Place data");

            foreach (PlaceModel placeObject in DV.PlaceDV.PlaceData)
            {
                HLinkPlaceModel t = placeObject.GetHLink;

                // TODO fill this

                // Back Reference Citation HLinks
                foreach (HLinkCitationModel citationRef in placeObject.GCitationRefCollection)
                {
                    DV.CitationDV.CitationData[citationRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Back Reference Note HLinks
                foreach (HLinkNoteModel noteRef in placeObject.GNoteRefCollection)
                {
                    DV.NoteDV.NoteData[noteRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Back Reference Media HLinks
                foreach (HLinkMediaModel mediaRef in placeObject.GMediaRefCollection)
                {
                    DV.MediaDV.MediaData[mediaRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Back Reference Place HLinks
                foreach (HLinkPlaceModel placeRef in placeObject.GPlaceRefCollection)
                {
                    DV.PlaceDV.PlaceData[placeRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Back Reference Tag HLinks
                foreach (HLinkTagModel tagRef in placeObject.GTagRefCollection)
                {
                    DV.TagDV.TagData[tagRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }
            }
        }

        /// <summary>
        /// Organises the repository repository.
        /// </summary>
        private static void OrganiseRepositoryRepository()
        {
            DataStore.CN.MajorStatusAdd("Organising Repository data");

            foreach (RepositoryModel repositoryObject in DV.RepositoryDV.RepositoryData)
            {
                HLinkRepositoryModel t = repositoryObject.GetHLink;

                // Back Reference Note HLinks
                foreach (HLinkNoteModel noteRef in repositoryObject.GNoteRefCollection)
                {
                    DV.NoteDV.NoteData[noteRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Back Reference Tag HLinks
                foreach (HLinkTagModel tagRef in repositoryObject.GTagRefCollection)
                {
                    DV.TagDV.TagData[tagRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }
            }
        }

        private static void OrganiseSourceRepository()
        {
            DataStore.CN.MajorStatusAdd("Organising Source data");

            foreach (SourceModel sourceObject in DV.SourceDV.SourceData)
            {
                HLinkSourceModel t = sourceObject.GetHLink;

                // -- Organse Back Links
                // ---------------------

                // Source Attribute Collection is model so no backlink

                //// Media Collection

                foreach (HLinkMediaModel mediaRef in sourceObject.GMediaRefCollection)
                {
                    DV.MediaDV.MediaData[mediaRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Note Collection
                foreach (IHLinkNoteModel noteRef in sourceObject.GNoteRefCollection)
                {
                    DV.NoteDV.NoteData[noteRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Repository Collection
                foreach (HLinkRepositoryModel repositoryRef in sourceObject.GRepositoryRefCollection)
                {
                    DV.RepositoryDV.RepositoryData[repositoryRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Tag Collection
                foreach (HLinkTagModel tagRef in sourceObject.GTagRefCollection)
                {
                    DV.TagDV.TagData[tagRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // -- Organse First and Sorts
                // ---------------------

                // Sort media collection and get first link images
                DV.SourceDV.SourceData[sourceObject.HLinkKey].GMediaRefCollection.SortAndSetFirst();

                // TODO First and Sort for Notes, Repositories and Tags

                // Get default image if available
                HLinkMediaModel hlink = sourceObject.GMediaRefCollection.FirstHLink;

                // Action default media image
                if (hlink is null)
                {
                    // Check for icon
                    hlink = DV.MediaDV.GetFirstImageFromCollection(sourceObject.GMediaRefCollection);
                }

                // Set default
                if (hlink is null)
                {
                    sourceObject.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
                }
                else
                {
                    sourceObject.HomeImageHLink = SetHomeHLink(sourceObject.HomeImageHLink, hlink);
                }
            }
        }

        /// <summary>
        /// Organises the tag repository.
        /// </summary>
        private static void OrganiseTagRepository()
        {
            DataStore.CN.MajorStatusAdd("Organising Tag data");

            foreach (TagModel argModel in DV.TagDV.TagData)
            {
                HLinkMediaModel hlink = null;

                // Set default
                if (hlink is null)
                {
                    argModel.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
                    argModel.HomeImageHLink.HomeSymbolColour = argModel.GColor;
                }
                else
                {
                    argModel.HomeImageHLink = SetHomeHLink(argModel.HomeImageHLink, hlink);
                }
            }
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
                    // Called in order of media linkages from Media outwards
                    OrganiseMediaRepository();

                    OrganiseSourceRepository();

                    OrganiseCitationRepository();

                    OrganiseEventRepository();

                    OrganiseFamilyRepository();

                    OrganiseHeaderRepository();

                    OrganiseBookMarkRepository();

                    OrganiseNameMapRepository();

                    OrganiseNoteRepository();

                    OrganisePlaceRepository();

                    OrganiseRepositoryRepository();

                    OrganiseTagRepository();

                    // People last as they depend on everything
                    OrganisePersonRepository();

                    // Preload image cache
                    StoreFile ttt = new StoreFile();

                    foreach (MediaModel item in DV.MediaDV.MediaData)
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

                        var imageSize = DependencyService.Get<IImageResource>().GetSize(item.MediaStorageFilePath);
                        System.Diagnostics.Debug.WriteLine(imageSize);
                        item.MetaDataHeight = imageSize.Height;
                        item.MetaDataWidth = imageSize.Width;
                    }

                    GetHomeImages();
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