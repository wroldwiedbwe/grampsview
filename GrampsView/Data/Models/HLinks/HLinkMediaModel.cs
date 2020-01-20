//-----------------------------------------------------------------------
//
// Various data modesl to small to be worth putting in their own file
// is first launched.
//
// <copyright file="HLinkMediaModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

////<define name = "object-content" >
////    <ref name="primary-object" />
////    <element name = "file" >
////      < attribute name="src">
////        <text />
////      </attribute>
////      <attribute name = "mime" >
////        < text />
////      </ attribute >
////      < optional >
////        < attribute name="checksum">
////          <text />
////        </attribute>
////      </optional>
////      <optional>
////        <attribute name = "description" >
////          < text />
////        </ attribute >
////      </ optional >
////    </ element >
////    < zeroOrMore >
////      < element name="attribute">
////        <ref name="attribute-content" />
////      </element>
////    </zeroOrMore>
////    <zeroOrMore>
////      <element name = "noteref" >
////        <ref name="noteref-content" />
////      </element>
////    </zeroOrMore>
////    <optional>
////      <ref name="date-content" />
////    </optional>
////    <zeroOrMore>
////      <element name = "citationref" >
////        <ref name="citationref-content" />
////      </element>
////    </zeroOrMore>
////    <zeroOrMore>
////      <element name = "tagref" >
////        <ref name="tagref-content" />
////      </element>
////    </zeroOrMore>
////  </define>

namespace GrampsView.Data.Model
{
    using System.Runtime.Serialization;

    using GrampsView.Common;

    using GrampsView.Data.Collections;
    using GrampsView.Data.DataView;

    using Xamarin.Forms;

    /// <summary>
    /// GRAMPS $$(hlink)$$ element class.
    /// </summary>
    [DataContract]
    public class HLinkMediaModel : HLinkBase, IHLinkMediaModel
    {
        private Color _HomeSymbolColour = Color.White;

        /// <summary>
        /// The local home use image.
        /// </summary>
        private int localHomeImageType = CommonConstants.HomeImageTypeSymbol;

        ///// <summary>
        ///// The local internal default character icon
        ///// </summary
        [EnumMember]
        private string localIDefaultSymbol = CommonConstants.IconDDefault;

        /// <summary>
        /// Initializes a new instance of the <see cref="HLinkMediaModel" /> class.
        /// </summary>
        public HLinkMediaModel()
        {
        }

        /// <summary>Gets the associated media model </summary>
        /// <value>The media model.<note type="caution">This can not hold a local copy of the media model as the Model Base has a hlinkmediamodel in it and this will cause a referene loop</note></value>
        public MediaModel DeRef
        {
            get
            {
                if (Valid)
                {
                    return DV.MediaDV.GetModel(HLinkKey);
                }
                else
                {
                    return new MediaModel();
                }
            }
        }

        /// <summary>
        /// Gets or sets the g attribute.
        /// </summary>
        /// <value>
        /// The g attribute.
        /// </value>
        [DataMember]
        public AttributeModel GAttribute { get; set; }

        /// <summary>
        /// Gets or sets the g citation model collection.
        /// </summary>
        /// <value>
        /// The g citation model collection.
        /// </value>
        [DataMember]
        public HLinkCitationModelCollection GCitationModelCollection { get; set; }

        /// <summary>
        /// Gets or sets the g corner1 x.
        /// </summary>
        /// <value>
        /// The g corner1 x.
        /// </value>
        [DataMember]
        public int GCorner1X { get; set; } = 0;

        /// <summary>
        /// Gets or sets the g corner1 y.
        /// </summary>
        /// <value>
        /// The g corner1 y.
        /// </value>
        [DataMember]
        public int GCorner1Y { get; set; } = 0;

        /// <summary>
        /// Gets or sets the g corner2 x.
        /// </summary>
        /// <value>
        /// The g corner2 x.
        /// </value>
        [DataMember]
        public int GCorner2X { get; set; } = 0;

        /// <summary>
        /// Gets or sets the g corner2 y.
        /// </summary>
        /// <value>
        /// The g corner2 y.
        /// </value>
        [DataMember]
        public int GCorner2Y { get; set; } = 0;

        /// <summary>
        /// Gets or sets the g note model collection.
        /// </summary>
        /// <value>
        /// The g note model collection.
        /// </value>
        [DataMember]
        public HLinkNoteModelCollection GNoteModelCollection { get; set; }

        /// <summary>
        /// Gets or sets the home image clipped bitmap.
        /// </summary>
        /// <value>
        /// The home image clipped bitmap.
        /// </value>
        public Image HomeImageClippedBitmap
        {
            get; set;
        }

        /// <summary>
        /// Gets the home image display bit map.
        /// </summary>
        /// <value>
        /// The home image display bit map.
        /// </value>
        public Image HomeImageDisplayBitMap
        {
            get
            {
                switch (localHomeImageType)
                {
                    case CommonConstants.HomeImageTypeClippedBitmap:
                        {
                            return HomeImageClippedBitmap;
                        }

                    case CommonConstants.HomeImageTypeThumbNail:
                        {
                            // TODO FIx this
                            //return DeRef.ImageThumbNail;
                            return null;
                        }

                    default:
                        {
                            return null;
                        }
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [home use image].
        /// </summary>
        /// <value>
        /// <c> true </c> if [home use image]; otherwise, <c> false </c>.
        /// </value>
        [DataMember]
        public int HomeImageType
        {
            get
            {
                return localHomeImageType;
            }

            set
            {
                SetProperty(ref localHomeImageType, value);
            }
        }

        // TODO Change to use GV static styles
        /// <summary>
        /// Gets or sets the home symbol.
        /// </summary>
        /// <value>
        /// The home symbol.
        /// </value>
        [DataMember]
        public string HomeSymbol
        {
            get
            {
                return localIDefaultSymbol;
            }

            set
            {
                SetProperty(ref localIDefaultSymbol, value);
            }
        }

        /// <summary>
        /// Gets or sets the background colour.
        /// </summary>
        /// <value>
        /// The background colour.
        /// </value>
        [DataMember]
        public Color HomeSymbolColour
        {
            get
            {
                return _HomeSymbolColour;
            }

            set
            {
                if (value != Color.Default)
                {
                }

                SetProperty(ref _HomeSymbolColour, value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether [home use image].
        /// </summary>
        /// <value>
        /// <c> true </c> if [home use image]; otherwise, <c> false </c>.
        /// </value>
        public bool HomeUseImage
        {
            get
            {
                if (HomeImageType == CommonConstants.HomeImageTypeSymbol)
                {
                    return false;
                }

                return true;
            }
        }

        /// <summary>Gets a value indicating whether gets boolean showing if the $$(HLink)$$ is valid.
        /// Can have a HLInk or be a pointer to an image.</summary>
        /// <value>Boolean showing if $$(HLink)$$ is valid.</value>
        public override bool Valid
        {
            get
            {
                return ((!string.IsNullOrEmpty(HLinkKey)) || (HomeImageType == CommonConstants.HomeImageTypeSymbol));
            }
        }
    }
}