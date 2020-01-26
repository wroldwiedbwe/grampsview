﻿//-----------------------------------------------------------------------
//
// The data model defined by this file serves to hold Event data from the GRAMPS data file
//
// <copyright file="ModelBase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;

    /// <summary>
    /// Base for Models.
    /// </summary>
    /// <seealso cref="GrampsView.Common.CommonBindableBase" />
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// /// ///
    /// <seealso cref="GrampsView.Data.ViewModel.IModelBase" />
    [DataContract]
    [KnownType(typeof(HLinkBookMarkModel))]
    [KnownType(typeof(HLinkCitationModel))]
    [KnownType(typeof(HLinkEventModel))]
    [KnownType(typeof(HLinkFamilyModel))]
    [KnownType(typeof(HLinkHeaderModel))]
    [KnownType(typeof(HLinkMediaModel))]
    [KnownType(typeof(HLinkNameMapModel))]
    [KnownType(typeof(HLinkPersonModel))]
    [KnownType(typeof(HLinkNoteModel))]
    [KnownType(typeof(HLinkPlaceModel))]
    [KnownType(typeof(HLinkRepositoryModel))]
    [KnownType(typeof(HLinkSourceModel))]
    [KnownType(typeof(HLinkTagModel))]
    public class ModelBase : CommonBindableBase, IModelBase
    {
        /// <summary>
        /// The local h link reference collection.
        /// </summary>
        private HLinkBackLinkModelCollection _BackHLinkReferenceCollection = new HLinkBackLinkModelCollection();

        /// <summary>
        /// The local change.
        /// </summary>
        private DateTime _Change = DateTime.MinValue;

        /// <summary>
        /// The local handle.
        /// </summary>
        private string _Handle = string.Empty;

        /// <summary>
        /// The local h link key.
        /// </summary>
        private string _HLinkKey = string.Empty;

        /// <summary>
        /// The local identifier.
        /// </summary>
        private string _Id = string.Empty;

        /// <summary>
        /// The local image h link.
        /// </summary>
        private HLinkMediaModel _ImageHLink = new HLinkMediaModel();

        /// <summary>
        /// The local priv.
        /// </summary>
        private bool _Priv = default(bool);

        /// <summary>
        /// Gets or sets the h link reference collection.
        /// </summary>
        /// <value>
        /// The h link reference collection.
        /// </value>
        [DataMember]
        public HLinkBackLinkModelCollection BackHLinkReferenceCollection
        {
            get
            {
                return _BackHLinkReferenceCollection;
            }

            set
            {
                SetProperty(ref _BackHLinkReferenceCollection, value);
            }
        }

        /// <summary>
        /// Gets or sets the change CDATA #REQUIRED.
        /// </summary>
        /// <value>
        /// The change.
        /// </value>
        [DataMember]
        public DateTime Change
        {
            get
            {
                return _Change;
            }

            set
            {
                SetProperty(ref _Change, value);
            }
        }

        /// <summary>
        /// Gets the de reference.
        /// </summary>
        /// <value>
        /// The de reference.
        /// </value>
        public virtual ModelBase DeRef
        {
            get
            {
                return new ModelBase();
            }
        }

        /// <summary>
        /// Gets the get default text for this Model.
        /// </summary>
        /// <value>
        /// The default text.
        /// </value>
        public virtual string GetDefaultText
        {
            get
            {
                return HLinkKey;
            }
        }

        /// <summary>
        /// Gets or sets the handle ID #REQUIRED.
        /// </summary>
        /// <value>
        /// The handle.
        /// </value>
        [DataMember]
        public string Handle
        {
            get
            {
                return _Handle;
            }

            set
            {
                SetProperty(ref _Handle, value);

                HLinkKey = value;
            }
        }

        public virtual HLinkBase HLink
        {
            get
            {
                return new HLinkBase();
            }
        }

        /// <summary>
        /// Gets or sets the h link key.
        /// </summary>
        /// <value>
        /// The h link key.
        /// </value>
        [DataMember]
        public string HLinkKey
        {
            get
            {
                return _HLinkKey;
            }

            set
            {
                SetProperty(ref _HLinkKey, value);
            }
        }

        /// <summary>
        /// Gets or sets the image h link key.
        /// </summary>
        /// <value>
        /// The image h link key.
        /// </value>
        [DataMember]
        public HLinkMediaModel HomeImageHLink
        {
            get
            {
                return _ImageHLink;
            }

            set
            {
                SetProperty(ref _ImageHLink, value);
            }
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [DataMember]
        public string Id
        {
            get
            {
                return _Id;
            }

            set
            {
                SetProperty(ref _Id, value);
            }
        }

        /// <summary>
        /// Gets or sets the model common logging.
        /// </summary>
        /// <value>
        /// The model common logging.
        /// </value>
        public ICommonLogging ModelCommonLogging { get; set; } = null;

        /// <summary>
        /// Gets a value indicating whether [h link key valid].
        /// </summary>
        /// <value>
        /// <c> true </c> if [h link key valid]; otherwise, <c> false </c>.
        /// </value>
        public bool ModelPopulated
        {
            get
            {
                return !string.IsNullOrEmpty(_HLinkKey);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ModelBase" /> is priv.
        /// </summary>
        /// <value>
        /// <c> true </c> if priv; otherwise, <c> false </c>.
        /// </value>
        [DataMember]
        public bool Priv
        {
            get
            {
                return _Priv;
            }

            set
            {
                SetProperty(ref _Priv, value);
            }
        }

        //    set
        //    {
        //        SetProperty(ref localModelUserActivity, value);
        //    }
        //}
        /// <summary>
        /// Gets the priv as string.
        /// </summary>
        /// <value>
        /// The priv as string.
        /// </value>
        public string PrivAsString
        {
            get
            {
                return _Priv.ToString(CultureInfo.CurrentCulture);
            }
        }

        /// <summary>
        /// Gets a value indicating whether returns true if the modelbase is valid.
        /// </summary>
        /// <value>
        /// <c> true </c> if this instance is valid; otherwise, <c> false </c>.
        /// </value>
        public bool Valid
        {
            get
            {
                return !string.IsNullOrEmpty(HLinkKey);
            }
        }

        public static bool operator !=(ModelBase left, ModelBase right)
        {
            return !(left == right);
        }

        public static bool operator <(ModelBase left, ModelBase right)
        {
            return ReferenceEquals(left, null) ? right is object : left.CompareTo(right) < 0;
        }

        public static bool operator <=(ModelBase left, ModelBase right)
        {
            return ReferenceEquals(left, null) || left.CompareTo(right) <= 0;
        }

        public static bool operator ==(ModelBase left, ModelBase right)
        {
            if (ReferenceEquals(left, null))
            {
                return ReferenceEquals(right, null);
            }

            return left.Equals(right);
        }

        public static bool operator >(ModelBase left, ModelBase right)
        {
            return !ReferenceEquals(left, null) && left.CompareTo(right) > 0;
        }

        public static bool operator >=(ModelBase left, ModelBase right)
        {
            return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Compares the specified a.
        /// </summary>
        /// <param name="argFirstModelBase">
        /// a.
        /// </param>
        /// <param name="argSecondModelBase">
        /// The b.
        /// </param>
        /// <returns>
        /// </returns>
        public int Compare(object argFirstModelBase, object argSecondModelBase)
        {
            if (argFirstModelBase is null)
            {
                throw new ArgumentNullException(nameof(argFirstModelBase));
            }

            if (argSecondModelBase is null)
            {
                throw new ArgumentNullException(nameof(argSecondModelBase));
            }

            ModelBase firstSource = (ModelBase)argFirstModelBase;
            ModelBase secondSource = (ModelBase)argSecondModelBase;

            // compare on Page first TODO compare on Page?
            int testFlag = Compare(firstSource.HLinkKey, secondSource.HLinkKey);

            return testFlag;
        }

        public int CompareTo(ModelBase other)
        {
            // compare on Page first TODO compare on Page?
            int testFlag = Compare(HLinkKey, other.HLinkKey);

            return testFlag;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is null)
            {
                return false;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            if (string.IsNullOrEmpty(this.Id))
            {
                return false;
            }

            if (string.IsNullOrEmpty((obj as ModelBase).Id))
            {
                return false;
            }

            if (this.Id == (obj as ModelBase).Id)
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            throw new System.NotImplementedException();
        }
    }
}