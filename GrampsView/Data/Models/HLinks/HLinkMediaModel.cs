//-----------------------------------------------------------------------
//
// Various data modesl to small to be worth putting in their own file
// is first launched.
//
// <copyright file="HLinkMediaModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

//// gramps XML 1.71 - Done
////
//// HLink
//// Priv
//// region
//// attribute
//// citationref
//// noteref

namespace GrampsView.Data.Model
{
    using GrampsView.Data.Collections;
    using GrampsView.Data.DataView;

    using System.Runtime.Serialization;

    /// <summary>
    /// GRAMPS $$(hlink)$$ element class.
    /// </summary>
    [DataContract]
    public class HLinkMediaModel : HLinkBase, IHLinkMediaModel
    {
        ///// <summary>
        ///// The local home use image.
        ///// </summary>
        //private int _HomeImageType = CommonConstants.HomeImageTypeUnknown;

        //private Color _HomeSymbolColour = Color.White;

        ///// <summary>
        ///// The local internal default character icon
        ///// </summary
        //[EnumMember]
        //private string _IDefaultSymbol = CommonConstants.IconDDefault;

        /// <summary>
        /// Initializes a new instance of the <see cref="HLinkMediaModel"/> class.
        /// </summary>
        public HLinkMediaModel()
        {
        }

        /// <summary>
        /// Gets the associated media model
        /// </summary>
        /// <value>
        /// The media model. <note type="caution">This can not hold a local copy of the media model
        /// as the Model Base has a hlinkmediamodel in it and this will cause a referene loop</note>
        /// </value>
        public MediaModel DeRef
        {
            get
            {
                if (Valid)
                {
                    return DV.MediaDV.GetModelFromHLinkString(HLinkKey);
                }
                else
                {
                    return new MediaModel();
                }
            }
        }

        /// <summary>
        /// Gets or sets the Attribute.
        /// </summary>
        /// <value>
        /// The Attribute.
        /// </value>
        [DataMember]
        public OCAttributeModelCollection GAttributeRefCollection { get; set; } = new OCAttributeModelCollection();

        /// <summary>
        /// Gets or sets the g citation model collection.
        /// </summary>
        /// <value>
        /// The g citation model collection.
        /// </value>
        [DataMember]
        public HLinkCitationModelCollection GCitationRefCollection { get; set; } = new HLinkCitationModelCollection();

        ///// <summary>
        ///// Gets or sets the g corner1 x.
        ///// </summary>
        ///// <value>
        ///// The g corner1 x.
        ///// </value>
        //[DataMember]
        //public int GCorner1X { get; set; } = 0;

        ///// <summary>
        ///// Gets or sets the g corner1 y.
        ///// </summary>
        ///// <value>
        ///// The g corner1 y.
        ///// </value>
        //[DataMember]
        //public int GCorner1Y { get; set; } = 0;

        ///// <summary>
        ///// Gets or sets the g corner2 x.
        ///// </summary>
        ///// <value>
        ///// The g corner2 x.
        ///// </value>
        //[DataMember]
        //public int GCorner2X { get; set; } = 0;

        ///// <summary>
        ///// Gets or sets the g corner2 y.
        ///// </summary>
        ///// <value>
        ///// The g corner2 y.
        ///// </value>
        //[DataMember]
        //public int GCorner2Y { get; set; } = 0;

        /// <summary>
        /// Gets or sets the g note model collection.
        /// </summary>
        /// <value>
        /// The g note model collection.
        /// </value>
        [DataMember]
        public HLinkNoteModelCollection GNoteRefCollection { get; set; } = new HLinkNoteModelCollection();

        /// <summary>
        /// Gets or sets the loading clip information.
        /// </summary>
        /// <value>
        /// Temporary field to store clipping information during the load process.
        /// </value>

        public HLinkHomeImageModel LoadingClipInfo { get; set; } = new HLinkHomeImageModel();

        ///// <summary>
        ///// Gets or sets the home image clipped bitmap.
        ///// </summary>
        ///// <value>
        ///// The home image clipped bitmap.
        ///// </value>
        //public Image HomeImageClippedBitmap
        //{
        //    get; set;
        //}

        ///// <summary>
        ///// Gets the home image display bit map.
        ///// </summary>
        ///// <value>
        ///// The home image display bit map.
        ///// </value>
        //public Image HomeImageDisplayBitMap
        //{
        //    get
        //    {
        //        switch (_HomeImageType)
        //        {
        //            //case CommonConstants.HomeImageTypeClippedBitmap:
        //            //    {
        //            //        return HomeImageClippedBitmap;
        //            //    }

        // case CommonConstants.HomeImageTypeThumbNail: { // TODO FIx this //return
        // DeRef.ImageThumbNail; return null; }

        //            default:
        //                {
        //                    return null;
        //                }
        //        }
        //    }
        //}

        ///// <summary>
        ///// Gets or sets a value indicating whether [home use image].
        ///// </summary>
        ///// <value>
        ///// <c>true</c> if [home use image]; otherwise, <c>false</c>.
        ///// </value>
        //[DataMember]
        //public int HomeImageType
        //{
        //    get
        //    {
        //        return _HomeImageType;
        //    }

        //    set
        //    {
        //        SetProperty(ref _HomeImageType, value);
        //    }
        //}

        //// TODO Change to use GV static styles
        ///// <summary>
        ///// Gets or sets the home symbol.
        ///// </summary>
        ///// <value>
        ///// The home symbol.
        ///// </value>
        //[DataMember]
        //public string HomeSymbol
        //{
        //    get
        //    {
        //        return _IDefaultSymbol;
        //    }

        //    set
        //    {
        //        SetProperty(ref _IDefaultSymbol, value);
        //    }
        //}

        ///// <summary>
        ///// Gets or sets the background colour.
        ///// </summary>
        ///// <value>
        ///// The background colour.
        ///// </value>
        //[DataMember]
        //public Color HomeSymbolColour
        //{
        //    get
        //    {
        //        return _HomeSymbolColour;
        //    }

        // set { if (value != Color.Default) { }

        //        SetProperty(ref _HomeSymbolColour, value);
        //    }
        //}

        ///// <summary>
        ///// Gets a value indicating whether [home use image].
        ///// </summary>
        ///// <value>
        ///// <c>true</c> if [home use image]; otherwise, <c>false</c>.
        ///// </value>
        //public bool HomeUseImage
        //{
        //    get
        //    {
        //        if (HomeImageType == CommonConstants.HomeImageTypeThumbNail || HomeImageType == CommonConstants.HomeImageTypeClippedBitmap)
        //        {
        //            return true;
        //        }

        //        return false;
        //    }
        //}

        /// <summary>
        /// Gets a value indicating whether gets boolean showing if the $$(HLink)$$ is valid. <note
        /// type="note">Can have a HLink or be a pointer to an image. <br/><br/> So, MUST be valid
        /// for both types and MUST be invalid for a default new instance. <br/></note>
        /// </summary>
        /// <value>
        /// Boolean showing if $$(HLink)$$ is valid.
        /// </value>
        public override bool Valid
        {
            get
            {
                return !string.IsNullOrEmpty(HLinkKey);
            }
        }
    }
}