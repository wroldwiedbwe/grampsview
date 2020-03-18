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

    using System;
    using System.Threading.Tasks;

    using Xamarin.Forms;

    /// <summary>
    /// Creates a collection of entities with content read from a GRAMPS XML file.
    /// </summary>
    public partial class StorePostLoad : IStorePostLoad
    {
        /// <summary>
        /// Organises the citation repository.
        /// </summary>
        private static async Task<bool> OrganiseCitationRepository()
        {
            DataStore.CN.MajorStatusAdd("Organising Citation data");

            foreach (CitationModel theCitationModel in DV.CitationDV.DataViewData)
            {
                if (theCitationModel.Id == "C0575")
                {
                }

                HLinkCitationModel t = theCitationModel.HLink;

                // -- Organise BackLinks ---------------------

                // Media Collection - Create backlinks in media models to citation models
                foreach (HLinkMediaModel mediaRef in theCitationModel.GMediaRefCollection)
                {
                    DataStore.DS.MediaData[mediaRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Back Reference Note HLinks
                foreach (HLinkNoteModel noteRef in theCitationModel.GNoteRefCollection)
                {
                    DataStore.DS.NoteData[noteRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Back Reference Source HLink
                DataStore.DS.SourceData[theCitationModel.GSourceRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));

                // Back Reference Tag HLinks
                foreach (HLinkTagModel tagRef in theCitationModel.GTagRef)
                {
                    DataStore.DS.TagData[tagRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // -- Organise Home Images -----------------------
                theCitationModel.HomeImageHLink = await OrganiseCitationImage(theCitationModel).ConfigureAwait(false);

                DataStore.DS.CitationData[theCitationModel.HLinkKey] = theCitationModel;
            }

            return true;
        }

        /// <summary>
        /// Organises the event repository.
        /// </summary>
        private static async Task<bool> OrganiseEventRepository()
        {
            DataStore.CN.MajorStatusAdd("Organising Event data");

            foreach (EventModel theEventModel in DV.EventDV.DataViewData)
            {
                HLinkEventModel t = theEventModel.HLink;

                //if (theEventModel.Id == "E0059")
                //{
                //}

                // Citation Collection
                foreach (HLinkCitationModel citationRef in theEventModel.GCitationRefCollection)
                {
                    DataStore.DS.CitationData[citationRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Media Collection
                foreach (HLinkMediaModel mediaRef in theEventModel.GMediaRefCollection)
                {
                    DataStore.DS.MediaData[mediaRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Sort media collection and get first link images
                DataStore.DS.EventData[theEventModel.HLinkKey].GMediaRefCollection.SortAndSetFirst();

                // eventModel.GMediaRefCollection = DV.MediaDV.HLinkCollectionSort(eventModel.GMediaRefCollection);

                // DV.EventDV.EventData[eventModel.HLinkKey].GMediaRefCollection.FirstHLink = DV.MediaDV.GetFirstImageFromCollection(DV.EventDV.EventData[eventModel.HLinkKey].GMediaRefCollection);

                // Place Reference
                if (theEventModel.GPlace.Valid)
                {
                    DataStore.DS.PlaceData[theEventModel.GPlace.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Back Reference Note HLinks
                foreach (HLinkNoteModel noteRef in theEventModel.GNoteRefCollection)
                {
                    DataStore.DS.EventData[noteRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Setup home images
                theEventModel.HomeImageHLink = await OrganiseEventImage(theEventModel).ConfigureAwait(false);

                DataStore.DS.EventData[theEventModel.HLinkKey] = theEventModel;
            }

            return true;
        }

        /// <summary>
        /// Organises the family repository.
        /// </summary>
        private static async Task<bool> OrganiseFamilyRepository()
        {
            await DataStore.CN.MajorStatusAdd("Organising Family data ").ConfigureAwait(false);

            foreach (FamilyModel theFamilyModel in DV.FamilyDV.DataViewData)
            {
                HLinkFamilyModel t = theFamilyModel.HLink;

                // -- Organse Back Links ---------------------

                // Child Collection
                foreach (HLinkPersonModel personRef in theFamilyModel.GChildRefCollection)
                {
                    DataStore.DS.PersonData[personRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Citation Collection
                foreach (HLinkCitationModel citationRef in theFamilyModel.GCitationRefCollection)
                {
                    DataStore.DS.CitationData[citationRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Back Reference Event HLinks
                foreach (HLinkEventModel eventRef in theFamilyModel.GEventRefCollection)
                {
                    DataStore.DS.EventData[eventRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Media Collection
                foreach (HLinkMediaModel mediaRef in theFamilyModel.GMediaRefCollection)
                {
                    DataStore.DS.MediaData[mediaRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Note Collection
                foreach (HLinkNoteModel noteRef in theFamilyModel.GNoteRefCollection)
                {
                    DataStore.DS.NoteData[noteRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Tag Collection
                foreach (HLinkTagModel tagRef in theFamilyModel.GTagRefCollection)
                {
                    DataStore.DS.TagData[tagRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // -- Organse First and Sorts --------------------------
                theFamilyModel.HomeImageHLink = await OrganiseFamilyImage(theFamilyModel).ConfigureAwait(false);

                DataStore.DS.FamilyData[theFamilyModel.HLinkKey] = theFamilyModel;
            }

            return true;
        }

        /// <summary>
        /// Organises the header repository.
        /// </summary>
        private static async Task<bool> OrganiseHeaderRepository()
        {
            DataStore.CN.MajorStatusAdd("Organising Header data");

            return true;
        }

        /// <summary>
        /// Organises the media repository.
        /// </summary>
        private static async Task<bool> OrganiseMediaRepository()
        {
            DataStore.CN.MajorStatusAdd("Organising Media data");

            foreach (MediaModel theMediaObject in DV.MediaDV.DataViewData)
            {
                HLinkMediaModel t = theMediaObject.HLink;

                if (theMediaObject.Id == "O0032")
                {
                }

                // TODO Change to SortAndSetFirst

                // Back Reference Citation HLinks
                foreach (HLinkCitationModel citationRef in theMediaObject.GCitationRefCollection)
                {
                    DataStore.DS.CitationData[citationRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Back Reference Note HLinks
                foreach (HLinkNoteModel noteRef in theMediaObject.GNoteRefCollection)
                {
                    DataStore.DS.NoteData[noteRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Back Reference Tag HLinks
                foreach (HLinkTagModel tagRef in theMediaObject.GTagRefCollection)
                {
                    DataStore.DS.TagData[tagRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Setup HomeImage
                theMediaObject.HomeImageHLink = await OrganiseMediaImage(theMediaObject).ConfigureAwait(false);

                DataStore.DS.MediaData[theMediaObject.HLinkKey] = theMediaObject;
            }

            return true;
        }

        /// <summary>
        /// Organises the namemap repository.
        /// </summary>
        private static async Task<bool> OrganiseNameMapRepository()
        {
            DataStore.CN.MajorStatusAdd("Organising NameMap data");

            //foreach (NameMapModel nnameMapObject in DV.NameMapDV.NameMapData)
            //{
            //}

            return true;
        }

        /// <summary>
        /// Organises the note repository.
        /// </summary>
        private static async Task<bool> OrganiseNoteRepository()
        {
            DataStore.CN.MajorStatusAdd("Organising Note data");

            foreach (NoteModel theNoteModel in DV.NoteDV.DataViewData)
            {
                HLinkNoteModel t = theNoteModel.HLink;

                // -- Organse Back Links ---------------------

                // Citation Collection

                foreach (HLinkTagModel tagnRef in theNoteModel.GTagRefCollection)
                {
                    DataStore.DS.TagData[tagnRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }
            }

            return true;
        }

        /// <summary>
        /// Organises the person repository.
        /// </summary>
        private static async Task<bool> OrganisePersonRepository()
        {
            DataStore.CN.MajorStatusAdd("Organising Person data");

            foreach (PersonModel thePersonModel in DV.PersonDV.DataViewData)
            {
                HLinkPersonModel t = thePersonModel.HLink;

                //if (person.Id == "I0568")
                //{
                //}
                // -- Organse Back Links ---------------------

                // Citation Collection
                foreach (HLinkCitationModel citationRef in thePersonModel.GCitationRefCollection)
                {
                    DataStore.DS.CitationData[citationRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Event Collection
                foreach (HLinkEventModel eventRef in thePersonModel.GEventRefCollection)
                {
                    DataStore.DS.EventData[eventRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                foreach (HLinkMediaModel mediaRef in thePersonModel.GMediaRefCollection)
                {
                    DataStore.DS.MediaData[mediaRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Note Collection
                foreach (HLinkNoteModel noteRef in thePersonModel.GNoteRefCollection)
                {
                    DataStore.DS.NoteData[noteRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Parent RelationShip
                foreach (HLinkFamilyModel familyRef in thePersonModel.GParentInRefCollection)
                {
                    DataStore.DS.FamilyData[familyRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Sibling Collection
                foreach (HLinkPersonModel personRef in thePersonModel.SiblingRefCollection)
                {
                    DataStore.DS.PersonData[personRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Back Reference Tag HLinks
                for (int i = 0; i < thePersonModel.GTagRefCollection.Count; i++)
                {
                    HLinkTagModel tagRef = thePersonModel.GTagRefCollection[i];

                    // Update the tag ref with colours and symbols
                    thePersonModel.GTagRefCollection[i].HomeImageHLink = GetTagRefHomeLink(tagRef.DeRef, tagRef.HomeImageHLink);

                    // Set the backlinks
                    DataStore.DS.TagData[tagRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // -- Organise Home Image ------------------------------
                thePersonModel.HomeImageHLink = await OrganisePersonImage(thePersonModel).ConfigureAwait(false);

                // -- Setup some extra values ------------------------------

                // set Birthdate
                EventModel birthDate = DV.EventDV.GetEventType(thePersonModel.GEventRefCollection, CommonConstants.EventTypeBirth);
                if (birthDate.Valid)
                {
                    thePersonModel.BirthDate = birthDate.GDate;
                }

                // set Is Living
                if (DV.EventDV.GetEventType(thePersonModel.GEventRefCollection, CommonConstants.EventTypeDeath).Valid)
                {
                    thePersonModel.IsLiving = false;
                }
                else
                {
                    thePersonModel.IsLiving = true;
                }

                // set Sibling Collection
                if (thePersonModel.GChildOf.Valid)
                {
                    thePersonModel.SiblingRefCollection = DV.FamilyDV.FamilyData[thePersonModel.GChildOf.HLinkKey].GChildRefCollection;
                }

                DataStore.DS.PersonData[thePersonModel.HLinkKey] = thePersonModel;
            }

            return true;
        }

        /// <summary>
        /// Organises the place repository.
        /// </summary>
        private static async Task<bool> OrganisePlaceRepository()
        {
            DataStore.CN.MajorStatusAdd("Organising Place data");

            foreach (PlaceModel thePlaceModel in DV.PlaceDV.DataViewData)
            {
                HLinkPlaceModel t = thePlaceModel.HLink;

                // TODO fill this

                // Back Reference Citation HLinks
                foreach (HLinkCitationModel citationRef in thePlaceModel.GCitationRefCollection)
                {
                    DataStore.DS.CitationData[citationRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Back Reference Note HLinks
                foreach (HLinkNoteModel noteRef in thePlaceModel.GNoteRefCollection)
                {
                    DataStore.DS.NoteData[noteRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Back Reference Media HLinks
                foreach (HLinkMediaModel mediaRef in thePlaceModel.GMediaRefCollection)
                {
                    DataStore.DS.MediaData[mediaRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Setup Child Place HLinks
                foreach (HLinkPlaceModel placeRef in thePlaceModel.GPlaceRefCollection)
                {
                    DataStore.DS.PlaceData[placeRef.HLinkKey].PlaceChildCollection.Add(t);
                }

                // Back Reference Tag HLinks
                foreach (HLinkTagModel tagRef in thePlaceModel.GTagRefCollection)
                {
                    DataStore.DS.TagData[tagRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }
            }

            return true;
        }

        /// <summary>
        /// Organises the repository repository.
        /// </summary>
        private static async Task<bool> OrganiseRepositoryRepository()
        {
            DataStore.CN.MajorStatusAdd("Organising Repository data");

            foreach (RepositoryModel theRepositoryModel in DV.RepositoryDV.DataViewData)
            {
                HLinkRepositoryModel t = theRepositoryModel.HLink;

                // Back Reference Note HLinks
                foreach (HLinkNoteModel noteRef in theRepositoryModel.GNoteRefCollection)
                {
                    DataStore.DS.NoteData[noteRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }

                // Back Reference Tag HLinks
                foreach (HLinkTagModel tagRef in theRepositoryModel.GTagRefCollection)
                {
                    DataStore.DS.TagData[tagRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                }
            }

            return true;
        }

        private static async Task<bool> OrganiseSourceRepository()
        {
            DataStore.CN.MajorStatusAdd("Organising Source data");

            foreach (SourceModel theSourceModel in DV.SourceDV.DataViewData)
            {
                HLinkSourceModel t = theSourceModel.HLink;

                //if (t.HLinkKey == "_c49238f73e868050e85")
                //{
                //}

                try
                {
                    // -- Organse Back Links ---------------------

                    // Source Attribute Collection is model so no backlink

                    //// Media Collection

                    foreach (HLinkMediaModel mediaRef in theSourceModel.GMediaRefCollection)
                    {
                        DataStore.DS.MediaData[mediaRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                    }

                    // Note Collection
                    foreach (IHLinkNoteModel noteRef in theSourceModel.GNoteRefCollection)
                    {
                        DataStore.DS.NoteData[noteRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                    }

                    // Repository Collection
                    foreach (HLinkRepositoryModel repositoryRef in theSourceModel.GRepositoryRefCollection)
                    {
                        DataStore.DS.RepositoryData[repositoryRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                    }

                    // Tag Collection
                    foreach (HLinkTagModel tagRef in theSourceModel.GTagRefCollection)
                    {
                        DataStore.DS.TagData[tagRef.HLinkKey].BackHLinkReferenceCollection.Add(new HLinkBackLink(t));
                    }

                    // Get default image if available
                    theSourceModel.HomeImageHLink = await OrganiseSourceImage(theSourceModel).ConfigureAwait(false);

                    DataStore.DS.SourceData[theSourceModel.HLinkKey] = theSourceModel;
                }
                catch (Exception ex)
                {
                    DataStore.CN.NotifyException("OrganiseSourceRepository", ex);
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
            await DataStore.CN.MajorStatusAdd("Organising Tag data").ConfigureAwait(false);

            foreach (TagModel theTagModel in DV.TagDV.DataViewData)
            {
                theTagModel.HomeImageHLink = await OrganiseTagImage(theTagModel).ConfigureAwait(false);

                DataStore.DS.TagData[theTagModel.HLinkKey] = theTagModel;
            }

            return true;
        }
    }
}