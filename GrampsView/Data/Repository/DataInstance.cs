// <copyright file="DataInstance.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

/// <summary>
/// </summary>
namespace GrampsView.Data.Repository
{
    using GrampsView.Common;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repositories;
    using Plugin.FilePicker.Abstractions;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    /// Static Data Store.
    /// </summary>
    [DataContract]
    public class DataInstance : CommonBindableBase
    {
        /// <summary>
        /// The local citation data.
        /// </summary>
        [DataMember]
        public RepositoryModelType<CitationModel, HLinkCitationModel> LocalCitationData = new RepositoryModelType<CitationModel, HLinkCitationModel>();

        /// <summary>
        /// The local event data.
        /// </summary>
        [DataMember]
        public RepositoryModelType<EventModel, HLinkEventModel> localEventData = new RepositoryModelType<EventModel, HLinkEventModel>();

        /// <summary>
        /// The local family data.
        /// </summary>
        [DataMember]
        public RepositoryModelType<FamilyModel, HLinkFamilyModel> localFamilyData = new RepositoryModelType<FamilyModel, HLinkFamilyModel>();

        /// <summary>
        /// The local header data.
        /// </summary>
        [DataMember]
        public RepositoryModelType<HeaderModel, HLinkHeaderModel> localHeaderData = new RepositoryModelType<HeaderModel, HLinkHeaderModel>();

        /// <summary>
        /// The local media data.
        /// </summary>
        [DataMember]
        public RepositoryModelType<MediaModel, HLinkMediaModel> localMediaData = new RepositoryModelType<MediaModel, HLinkMediaModel>();

        /// <summary>
        /// The local name map data.
        /// </summary>
        [DataMember]
        public RepositoryModelType<NameMapModel, HLinkNameMapModel> localNameMapData = new RepositoryModelType<NameMapModel, HLinkNameMapModel>();

        /// <summary>
        /// The local note data.
        /// </summary>
        [DataMember]
        public RepositoryModelType<NoteModel, HLinkNoteModel> localNoteData = new RepositoryModelType<NoteModel, HLinkNoteModel>();

        /// <summary>
        /// The local person data.
        /// </summary>
        [DataMember]
        public RepositoryModelType<PersonModel, HLinkPersonModel> localPersonData = new RepositoryModelType<PersonModel, HLinkPersonModel>();

        /// <summary>
        /// The local place data.
        /// </summary>
        [DataMember]
        public RepositoryModelType<PlaceModel, HLinkPlaceModel> localPlaceData = new RepositoryModelType<PlaceModel, HLinkPlaceModel>();

        /// <summary>
        /// The local repository data.
        /// </summary>
        [DataMember]
        public RepositoryModelType<RepositoryModel, HLinkRepositoryModel> localRepositoryData = new RepositoryModelType<RepositoryModel, HLinkRepositoryModel>();

        /// <summary>
        /// The local tag data.
        /// </summary>
        [DataMember]
        public RepositoryModelType<TagModel, HLinkTagModel> localTagData = new RepositoryModelType<TagModel, HLinkTagModel>();

        /// <summary>
        /// The local book mark data.
        /// </summary>
        [DataMember]
        private RepositoryModelType<BookMarkModel, HLinkBookMarkModel> _BookMarkData = new RepositoryModelType<BookMarkModel, HLinkBookMarkModel>();

        private DirectoryInfo _CurrentDataFolder;

        private FileData _CurrentInputFile = null;

        private DirectoryInfo _CurrentInputFolder;

        /// <summary>
        /// The local source data.
        /// </summary>
        [DataMember]
        private RepositoryModelType<SourceModel, HLinkSourceModel> _SourceData = new RepositoryModelType<SourceModel, HLinkSourceModel>();

        /// <summary>
        /// Initializes a new instance of the <see cref="DataInstance" /> class.
        /// </summary>
        public DataInstance()
        {
        }

        /// <summary>
        /// Gets or sets the book mark data.
        /// </summary>
        /// <value>
        /// The book mark data.
        /// </value>
        public RepositoryModelType<BookMarkModel, HLinkBookMarkModel> BookMarkData
        {
            get
            {
                return _BookMarkData;
            }

            set
            {
                SetProperty(ref _BookMarkData, value);
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
        /// Gets or sets a value indicating whether this instance is data loaded.
        /// </summary>
        /// <value>
        /// <c> true </c> if this instance is data loaded; otherwise, <c> false </c>.
        /// </value>
        [DataMember]
        public bool IsDataLoaded
        {
            get;
            set;
        }

        = false;

        /// <summary>
        /// Gets or sets source Data repository.
        /// </summary>
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
        /// Loads the data store from existign known details
        /// </summary>
        public async Task LoadDataStore()
        {
            //await StoreFileNames.DataFolderSetToExistingAsync().ConfigureAwait(false);

            CurrentDataFolder = new DirectoryInfo(Xamarin.Essentials.FileSystem.CacheDirectory);
        }
    }
}