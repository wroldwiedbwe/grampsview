//-----------------------------------------------------------------------
//
// Storage routines for the IMediaObjectModel
//
// <copyright file="IMediaModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Model
{
    using GrampsView.Data.Collections;

    /// <summary>
    /// Interfaces for IMediaObjectViewModel.
    /// </summary>
    public interface IMediaModel : IModelBase
    {
        string FileContentType
        {
            get;
            set;
        }

        string FileMimeSubType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the file MIME.
        /// </summary>
        /// <value>
        /// The file MIME.
        /// </value>
        string FileMimeType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the date value.
        /// </summary>
        /// <value>
        /// The date value.
        /// </value>
        DateObjectModel GDateValue
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the file description.
        /// </summary>
        /// <value>
        /// The file description.
        /// </value>
        string GDescription
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the get h link Media Model that points to this ViewModel.
        /// </summary>
        /// <value>
        /// The get h link.
        /// </value>
        HLinkMediaModel HLink
        {
            get;
        }

        /// <summary>
        /// Gets or sets the g tag reference collection.
        /// </summary>
        /// <value>
        /// The g tag reference collection.
        /// </value>
        HLinkTagModelCollection GTagRefCollection { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is media file.
        /// </summary>
        /// <value>
        /// <c> true </c> if this instance is media file; otherwise, <c> false </c>.
        /// </value>
        bool IsMediaFile
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether [media storage file valid]. Runs various checks on the mediafile.
        /// </summary>
        /// <value>
        /// <c> true </c> if [media storage file valid]; otherwise, <c> false </c>.
        /// </value>
        bool IsMediaStorageFileValid
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether [original file path valid].
        /// </summary>
        /// <value>
        /// <c> true </c> if [original file path valid]; otherwise, <c> false </c>.
        /// </value>
        bool IsOriginalFilePathValid
        {
            get;
        }

        /// <summary>
        /// Gets the media storage file path.
        /// </summary>
        /// <value>
        /// The media storage file path.
        /// </value>
        string MediaStorageFilePath
        {
            get;
        }

        /// <summary>Gets or sets the height of the meta data.</summary>
        /// <value>The height of the meta data.</value>
        double MetaDataHeight { get; set; }

        /// <summary>Gets or sets the width of the meta data.</summary>
        /// <value>The width of the meta data.</value>
        double MetaDataWidth { get; set; }

        /// <summary>
        /// Cleans this instance.
        /// </summary>
        void FullImageClean();

        /// <summary>
        /// Gets the image ViewModel.
        /// </summary>
        /// <returns>
        /// </returns>
        MediaModel GetImageModel();
    }
}