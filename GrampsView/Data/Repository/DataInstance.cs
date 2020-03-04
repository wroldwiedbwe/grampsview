// <copyright file="DataInstance.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

/// <summary>
/// </summary>
namespace GrampsView.Data.Repository
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repositories;

    using Plugin.FilePicker.Abstractions;

    using System.Collections.ObjectModel;
    using System.IO;
    using System.Runtime.Serialization;

    /// <summary>
    /// Static Data Store.
    /// </summary>
    [DataContract]
    [KnownType(typeof(ObservableCollection<HLinkBackLink>))]
    [KnownType(typeof(HLinkBackLink))]
    public class DataInstance : CommonBindableBase
    {
        private HLinkBackLinkModelCollection _BookMarkCollection = new HLinkBackLinkModelCollection();

        private RepositoryModelType<CitationModel, HLinkCitationModel> _CitationData = new RepositoryModelType<CitationModel, HLinkCitationModel>();

        /// <summary>
        /// The local book mark data.
        /// </summary>
        private DirectoryInfo _CurrentDataFolder;

        private FileData _CurrentInputFile = null;

        private DirectoryInfo _CurrentInputFolder;

        /// <summary>
        /// The local event data.
        /// </summary>
        private RepositoryModelType<EventModel, HLinkEventModel> _EventData = new RepositoryModelType<EventModel, HLinkEventModel>();

        /// <summary>
        /// The local family data.
        /// </summary>
        private RepositoryModelType<FamilyModel, HLinkFamilyModel> _FamilyData = new RepositoryModelType<FamilyModel, HLinkFamilyModel>();

        /// <summary>
        /// The local header data.
        /// </summary>
        private RepositoryModelType<HeaderModel, HLinkHeaderModel> _HeaderData = new RepositoryModelType<HeaderModel, HLinkHeaderModel>();

        /// <summary>
        /// The local media data.
        /// </summary>
        private RepositoryModelType<MediaModel, HLinkMediaModel> _MediaData = new RepositoryModelType<MediaModel, HLinkMediaModel>();

        /// <summary>
        /// The local name map data.
        /// </summary>
        private RepositoryModelType<NameMapModel, HLinkNameMapModel> _NameMapData = new RepositoryModelType<NameMapModel, HLinkNameMapModel>();

        /// <summary>
        /// The local note data.
        /// </summary>
        private RepositoryModelType<NoteModel, HLinkNoteModel> _NoteData = new RepositoryModelType<NoteModel, HLinkNoteModel>();

        /// <summary>
        /// The local person data.
        /// </summary>
        private RepositoryModelType<PersonModel, HLinkPersonModel> _PersonData = new RepositoryModelType<PersonModel, HLinkPersonModel>();

        /// <summary>
        /// The local place data.
        /// </summary>
        private RepositoryModelType<PlaceModel, HLinkPlaceModel> _PlaceData = new RepositoryModelType<PlaceModel, HLinkPlaceModel>();

        /// <summary>
        /// The local repository data.
        /// </summary>
        private RepositoryModelType<RepositoryModel, HLinkRepositoryModel> _RepositoryData = new RepositoryModelType<RepositoryModel, HLinkRepositoryModel>();

        /// <summary>
        /// The local source data.
        /// </summary>
        private RepositoryModelType<SourceModel, HLinkSourceModel> _SourceData = new RepositoryModelType<SourceModel, HLinkSourceModel>();

        /// <summary>
        /// The local tag data.
        /// </summary>
        private RepositoryModelType<TagModel, HLinkTagModel> _TagData = new RepositoryModelType<TagModel, HLinkTagModel>();

        /// <summary>
        /// Initializes a new instance of the <see cref="DataInstance"/> class.
        /// </summary>
        public DataInstance()
        {
        }

        [DataMember]
        public HLinkBackLinkModelCollection BookMarkCollection
        {
            get
            {
                return _BookMarkCollection;
            }

            set
            {
                SetProperty(ref _BookMarkCollection, value);
            }
        }

        /// <summary>
        /// The local citation data.
        /// </summary>
        [DataMember]
        public RepositoryModelType<CitationModel, HLinkCitationModel> CitationData
        {
            get
            {
                return _CitationData;
            }

            set
            {
                SetProperty(ref _CitationData, value);
            }
        }

        /// <summary>
        /// Gets or sets the get current data folder.
        /// </summary>
        /// <value>
        /// The get current data folder.
        /// </value>
        public DirectoryInfo CurrentDataFolder
        {
            get
            {
                return _CurrentDataFolder;
            }

            set
            {
                SetProperty(ref _CurrentDataFolder, value);
            }
        }

        public bool CurrentDataFolderValid
        {
            get
            {
                return (!(CurrentDataFolder == null) && (CurrentDataFolder.Exists));
            }
        }

        public FileData CurrentInputFile
        {
            get
            {
                return _CurrentInputFile;
            }

            set
            {
                SetProperty(ref _CurrentInputFile, value);
            }
        }

        public bool CurrentInputFileValid
        {
            get
            {
                return (!(CurrentInputFile == null));
            }
        }

        public DirectoryInfo CurrentInputFolder
        {
            get
            {
                return _CurrentInputFolder;
            }

            set
            {
                SetProperty(ref _CurrentInputFolder, value);
            }
        }

        /// <summary>
        /// The local Event data.
        /// </summary>
        [DataMember]
        public RepositoryModelType<EventModel, HLinkEventModel> EventData
        {
            get
            {
                return _EventData;
            }

            set
            {
                SetProperty(ref _EventData, value);
            }
        }

        /// <summary>
        /// The local Family data.
        /// </summary>
        [DataMember]
        public RepositoryModelType<FamilyModel, HLinkFamilyModel> FamilyData
        {
            get
            {
                return _FamilyData;
            }

            set
            {
                SetProperty(ref _FamilyData, value);
            }
        }

        /// <summary>
        /// The local Header data.
        /// </summary>
        [DataMember]
        public RepositoryModelType<HeaderModel, HLinkHeaderModel> HeaderData
        {
            get
            {
                return _HeaderData;
            }

            set
            {
                SetProperty(ref _HeaderData, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is data loaded.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is data loaded; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool IsDataLoaded
        {
            get;
            set;
        }

        = false;

        /// <summary>
        /// The local Media data.
        /// </summary>
        [DataMember]
        public RepositoryModelType<MediaModel, HLinkMediaModel> MediaData
        {
            get
            {
                return _MediaData;
            }

            set
            {
                SetProperty(ref _MediaData, value);
            }
        }

        /// <summary>
        /// The local NameMap data.
        /// </summary>
        [DataMember]
        public RepositoryModelType<NameMapModel, HLinkNameMapModel> NameMapData
        {
            get
            {
                return _NameMapData;
            }

            set
            {
                SetProperty(ref _NameMapData, value);
            }
        }

        /// <summary>
        /// The local Note data.
        /// </summary>
        [DataMember]
        public RepositoryModelType<NoteModel, HLinkNoteModel> NoteData
        {
            get
            {
                return _NoteData;
            }

            set
            {
                SetProperty(ref _NoteData, value);
            }
        }

        /// <summary>
        /// The local Person data.
        /// </summary>
        [DataMember]
        public RepositoryModelType<PersonModel, HLinkPersonModel> PersonData
        {
            get
            {
                return _PersonData;
            }

            set
            {
                SetProperty(ref _PersonData, value);
            }
        }

        /// <summary>
        /// The local Place data.
        /// </summary>
        [DataMember]
        public RepositoryModelType<PlaceModel, HLinkPlaceModel> PlaceData
        {
            get
            {
                return _PlaceData;
            }

            set
            {
                SetProperty(ref _PlaceData, value);
            }
        }

        /// <summary>
        /// The local Place data.
        /// </summary>
        [DataMember]
        public RepositoryModelType<RepositoryModel, HLinkRepositoryModel> RepositoryData
        {
            get
            {
                return _RepositoryData;
            }

            set
            {
                SetProperty(ref _RepositoryData, value);
            }
        }

        /// <summary>
        /// Gets or sets source Data repository.
        /// </summary>
        [DataMember]
        public RepositoryModelType<SourceModel, HLinkSourceModel> SourceData
        {
            get
            {
                return _SourceData;
            }

            set
            {
                SetProperty(ref _SourceData, value);
            }
        }

        /// <summary>
        /// The local tag data.
        /// </summary>
        [DataMember]
        public RepositoryModelType<TagModel, HLinkTagModel> TagData
        {
            get
            {
                return _TagData;
            }

            set
            {
                SetProperty(ref _TagData, value);
            }
        }

        /// <summary>
        /// Loads the data store from existign known details
        /// </summary>
        public void LoadDataStore()
        {
            CurrentDataFolder = new DirectoryInfo(Xamarin.Essentials.FileSystem.CacheDirectory);
        }
    }
}