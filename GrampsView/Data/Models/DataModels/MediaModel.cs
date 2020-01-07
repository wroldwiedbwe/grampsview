//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="MediaModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;

    using System;
    using System.Collections;
    using System.Runtime.Serialization;

    /// <summary>
    /// Data model for a person.
    /// </summary>
    [DataContract]
    public sealed class MediaModel : ModelBase, IMediaModel, IComparable, IComparer
    {
        /// <summary>
        /// The local date value.
        /// </summary>
        private DateObjectModel localDateValue = new DateObjectModel();

        /// <summary>
        /// The local event collection.
        /// </summary>
        private HLinkEventModelCollection localEventCollection = new HLinkEventModelCollection();

        /// <summary>
        /// The local family collection.
        /// </summary>
        private HLinkFamilyModelCollection localFamilyCollection = new HLinkFamilyModelCollection();

        private string localFileContentType;

        /// <summary>
        /// My file description.
        /// </summary>
        private string localFileDescription = string.Empty;

        /// <summary>
        /// Local Storage File for media object.
        /// </summary>
        private FileInfoEx localMediaStorageFile = null;

        /// <summary>
        /// The local note reference collection.
        /// </summary>
        private HLinkNoteModelCollection localNoteReferenceCollection = new HLinkNoteModelCollection();

        /// <summary>
        /// The local original file path.
        /// </summary>
        private string localOriginalFilePath = string.Empty;

        /// <summary>
        /// The local person collection.
        /// </summary>
        private HLinkPersonModelCollection localPersonCollection = new HLinkPersonModelCollection();

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaModel" /> class.
        /// </summary>
        public MediaModel()
        {
            HomeImageHLink.HomeSymbol = CommonConstants.IconMedia;
        }

        ///// <summary>
        ///// Gets or sets the clip rect.
        ///// </summary>
        ///// <value>
        ///// The clip rect.
        ///// </value>
        //[DataMember]
        //public Rectangle ClipRect
        //{
        //    get; set;
        //}

        // = new Rectangle(0, 0, 100, 100);

        public string FileContentType
        {
            get
            {
                return localFileContentType;
            }

            set
            {
                if (value != null)
                {
                    if (value != "image/jpeg")
                    {
                    }

                    SetProperty(ref localFileContentType, value);

                    // get the first part
                    string[] t = value.Split('/');

                    if (t.Length > 0)
                    {
                        FileMimeType = t[0].ToLower();
                    }

                    if (t.Length > 1)
                    {
                        FileMimeSubType = t[1].ToLower();
                    }
                }
            }
        }

        [DataMember]
        public string FileMimeSubType
        {
            get; set;
        }

        /// <summary> Gets or sets the file MIME. </summary> <value> The file MIME.
        [DataMember]
        public string FileMimeType
        {
            get
           ;

            set
           ;
        }

        /// <summary>
        /// Gets or sets the g citation reference collection.
        /// </summary>
        /// <value>
        /// The g citation reference collection.
        /// </value>
        [DataMember]
        public HLinkCitationModelCollection GCitationRefCollection
        {
            get;
            set;
        }

        = new HLinkCitationModelCollection();

        /// <summary>
        /// Gets or sets the date value.
        /// </summary>
        /// <value>
        /// The date value.
        /// </value>
        [DataMember]
        public DateObjectModel GDateValue
        {
            get
            {
                return localDateValue;
            }

            set
            {
                SetProperty(ref localDateValue, value);
            }
        }

        /// <summary>
        /// Gets or sets the file description.
        /// </summary>
        /// <value>
        /// The file description.
        /// </value>
        [DataMember]
        public string GDescription
        {
            get
            {
                return localFileDescription;
            }

            set
            {
                SetProperty(ref localFileDescription, value);
            }
        }

        /// <summary>
        /// Gets the default text for notes which is the first twenty characters.
        /// </summary>
        /// <value>
        /// The get default text.
        /// </value>
        public new string GetDefaultText
        {
            get
            {
                return GDescription.Substring(0, Math.Min(40, GDescription.Length));
            }
        }

        /// <summary>
        /// Gets the get h link.
        /// </summary>
        /// <value>
        /// The get h link.
        /// </value>
        public HLinkMediaModel GetHLink
        {
            get
            {
                HLinkMediaModel t = new HLinkMediaModel
                {
                    HLinkKey = HLinkKey,
                };
                return t;
            }
        }

        /// <summary>
        /// Gets or sets the event reference collection, i.e. person models that reference this media
        /// object. These are not part of the normal GRAMPS XML file and are added after the media
        /// models are loaded.
        /// </summary>
        /// <value>
        /// The person reference collection.
        /// </value>
        [DataMember]
        public HLinkEventModelCollection GEventRefCollection
        {
            get
            {
                return localEventCollection;
            }

            set
            {
                SetProperty(ref localEventCollection, value);
            }
        }

        /// <summary>
        /// Gets or sets the event reference collection, i.e. person models that reference this media
        /// object. These are not part of the normal GRAMPS XML file and are added after the media
        /// models are loaded.
        /// </summary>
        /// <value>
        /// The person reference collection.
        /// </value>
        [DataMember]
        public HLinkFamilyModelCollection GFamilyRefCollection
        {
            get
            {
                return localFamilyCollection;
            }

            set
            {
                SetProperty(ref localFamilyCollection, value);
            }
        }

        /// <summary>
        /// Gets or sets the note reference collection.
        /// </summary>
        /// <value>
        /// The note reference collection.
        /// </value>
        [DataMember]
        public HLinkNoteModelCollection GNoteRefCollection
        {
            get
            {
                return localNoteReferenceCollection;
            }

            set
            {
                SetProperty(ref localNoteReferenceCollection, value);
            }
        }

        /// <summary>
        /// Gets or sets the person reference collection, i.e. person models that reference this
        /// media object. These are not part of the normal GRAMPS XML file and are added after the
        /// media models are loaded.
        /// </summary>
        /// <value>
        /// The person reference collection.
        /// </value>
        [DataMember]
        public HLinkPersonModelCollection GPersonRefCollection
        {
            get
            {
                return localPersonCollection;
            }

            set
            {
                SetProperty(ref localPersonCollection, value);
            }
        }

        /// <summary>
        /// Gets or sets the g tag reference collection.
        /// </summary>
        /// <value>
        /// The g tag reference collection.
        /// </value>
        [DataMember]
        public HLinkTagModelCollection GTagRefCollection { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is media file.
        /// </summary>
        /// <value>
        /// <c> true </c> if this instance is media file; otherwise, <c> false </c>.
        /// </value>
        public bool IsMediaFile
        {
            get
            {
                if (FileMimeType == "image")
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether [media storage file valid]. Runs various checks on the mediafile.
        /// </summary>
        /// <value>
        /// <c> true </c> if [media storage file valid]; otherwise, <c> false </c>.
        /// </value>
        public bool IsMediaStorageFileValid
        {
            get
            {
                // TODO Enhance to check for zero length files
                return ((MediaStorageFile == null) || (!MediaStorageFile.Valid)) ? false : true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether [original file path] is valid.
        /// </summary>
        /// <value>
        /// <c> true </c> if [original file path valid]; otherwise, <c> false </c>.
        /// </value>
        public bool IsOriginalFilePathValid
        {
            get
            {
                if (string.IsNullOrEmpty(OriginalFilePath))
                {
                    return false;
                }

                return StoreFileUtility.IsRelativeFilePathValid(OriginalFilePath);
            }
        }

        /// <summary>
        /// Gets or sets the media storage file. Serialised separately.
        /// </summary>
        /// <value>
        /// The media storage file.
        /// </value>
        public FileInfoEx MediaStorageFile
        {
            get
            {
                return localMediaStorageFile;
            }

            set
            {
                SetProperty(ref localMediaStorageFile, value);
            }
        }

        /// <summary>
        /// Gets the media storage file path or string.empty if null.
        /// </summary>
        /// <value>
        /// The media storage file.
        /// </value>
        public string MediaStorageFilePath
        {
            get
            {
                if (IsMediaStorageFileValid)
                {
                    return localMediaStorageFile.FInfo.FullName;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets the media storage file URI.
        /// </summary>
        /// <value>
        /// The media storage file URI.
        /// </value>
        public Uri MediaStorageFileUri
        {
            get
            {
                return new Uri(MediaStorageFilePath);
            }
        }

        [DataMember]
        public double MetaDataHeight { get; set; }

        [DataMember]
        public double MetaDataWidth { get; set; }

        /// <summary>
        /// Gets or sets the original file path.
        /// </summary>
        /// <value>
        /// The original file path.
        /// </value>
        [DataMember]
        public string OriginalFilePath
        {
            get
            {
                return localOriginalFilePath;
            }

            set
            {
                if (value != null)
                {
                    SetProperty(ref localOriginalFilePath, value);
                }
            }
        }

        /// <summary>
        /// Gets the MIME media.
        /// </summary>
        /// <value>
        /// The MIME media.
        /// </value>
        /// <summary>
        /// Gets the title decoded.
        /// </summary>
        /// <value>
        /// The title decoded.
        /// </value>
        public string TitleDecoded
        {
            get
            {
                return GDateValue.GetShortDateAsString + " - " + GDescription;
            }
        }

        // set { SetProperty(ref localFileMime, value); } }
        /// <summary>
        /// Compares the specified a.
        /// </summary>
        /// <param name="a">
        /// a.
        /// </param>
        /// <param name="b">
        /// The b.
        /// </param>
        /// <returns>
        /// </returns>
        public new int Compare(object a, object b)
        {
            MediaModel firstMediaModel = (MediaModel)a;
            MediaModel secondMediaModel = (MediaModel)b;

            // compare on surnname first
            int testFlag = DateTime.Compare(firstMediaModel.GDateValue.SortDate, secondMediaModel.GDateValue.SortDate);

            // If the same then on Description. Usual if there is no Date
            if (testFlag.Equals(0))
            {
                testFlag = string.Compare(firstMediaModel.GDescription, secondMediaModel.GDescription, StringComparison.CurrentCulture);
            }

            return testFlag;
        }

        ///// <summary>
        ///// Gets or Sets the MIME media.
        ///// </summary>
        ///// <value>
        ///// The MIME media.
        ///// </value>
        // [DataMember] public string MimeMedia { get { // get the first part string[] t =
        // localFileMime.Split('/'); if (localFileMime.Length > 0) { return t[0]; } else { return
        // "Unknown"; } }
        /// <summary>
        /// Implement IComparable CompareTo method.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        /// <returns>
        /// </returns>
        public int CompareTo(object obj)
        {
            MediaModel secondMediaModel = (MediaModel)obj;

            // compare on Date first
            int testFlag = DateTime.Compare(GDateValue.SortDate, secondMediaModel.GDateValue.SortDate);

            // If the same then on Description. Usual if there is no date
            if (testFlag.Equals(0))
            {
                testFlag = string.Compare(GDescription, secondMediaModel.GDescription, StringComparison.CurrentCulture);
            }

            return testFlag;
        }

        /// <summary>
        /// Cleans this instance.
        /// </summary>
        public void FullImageClean()
        {
            // TODO fix cache
            //IsFullImageLoaded = false;
            //ImageFullBitmap = null;
        }

        public MediaModel GetImageModel() => throw new NotImplementedException();

        /// <summary>
        /// Sets the clip rect.
        /// </summary>
        public void SetClipRect()
        {
            //double x1 = 0;
            //double y1 = 0;
            //double axisWidth = 0;
            //double axisHeight = 0;

            //// Load here due to async
            //ClipRect = new Rectangle(0, 0, 0, 0);

            //if (ImageThumbNail is null)
            //{
            //    return;
            //}

            //axisWidth = ImageThumbNail.PixelWidth;
            //axisHeight = ImageThumbNail.PixelHeight;

            //if (axisHeight == 0 || axisWidth == 0)
            //{
            //    return;
            //}

            //ClipRect = new Rectangle(x1, y1, axisWidth, axisHeight);

            //if (IsFullImageLoaded)
            //{
            //    axisWidth = ImageFullBitmap.PixelWidth;
            //    axisHeight = ImageFullBitmap.PixelHeight;

            //    ClipRect = new Rectangle(x1, y1, axisWidth, axisHeight);
            //}
        }
    }
}