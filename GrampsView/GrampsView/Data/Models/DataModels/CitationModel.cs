//-----------------------------------------------------------------------
//
// The data model defined by this file serves to hold Event data from the GRAMPS data file
//
// <copyright file="CitationModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

////<define name = "citation-content" >
////   <ref name="primary-object" />
////   <optional>
////     <ref name="date-content" />
////   </optional>
////   <optional>
////     <element name = "page" >
////       < text />
////     </ element >
////   </ optional >
////   < element name="confidence">
////     <text />
////   </element>
////   <zeroOrMore>
////     <element name = "noteref" >
////       <ref name="noteref-content" />
////     </element>
////   </zeroOrMore>
////   <zeroOrMore>
////     <element name = "objref" >
////       <ref name="objref-content" />
////     </element>
////   </zeroOrMore>
////   <zeroOrMore>
////     <element name = "srcattribute" >
////       <ref name="srcattribute-content" />
////     </element>
////   </zeroOrMore>
////   <element name = "sourceref" >
////     <ref name="sourceref-content" />
////   </element>
////   <zeroOrMore>
////     <element name = "tagref" >
////       <ref name="tagref-content" />
////     </element>
////   </zeroOrMore>
//// </define>

/// <summary>
/// </summary>
namespace GrampsView.Data.Model
{
    using System;
    using System.Collections;
    using System.Runtime.Serialization;

    using GrampsView.Common;
    using GrampsView.Data.Collections;

    /// <summary> Data model for a Citation. <code> ************************************************************
    [DataContract]
    public sealed class CitationModel : ModelBase, ICitationModel, IComparable, IComparer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CitationModel" /> class.
        /// </summary>
        public CitationModel()
        {
            HomeImageHLink.HomeSymbol = CommonConstants.IconCitation;
        }

        /// <summary>
        /// Gets or sets the g confidence.
        /// </summary>
        /// <value>
        /// The g confidence.
        /// </value>
        [DataMember]
        public string GConfidence
        {
            get;
            set;
        }

            = string.Empty;

        /// <summary>
        /// Gets or sets the content of the g date.
        /// </summary>
        /// <value>
        /// The content of the g date.
        /// </value>
        [DataMember]
        public DateObjectModel GDateContent
        {
            get;
            set;
        }

            = new DateObjectModel();

        /// <summary>
        /// Gets the get default text for this ViewModel.
        /// </summary>
        /// <value>
        /// The get default text.
        /// </value>
        public override string GetDefaultText
        {
            get
            {
                return GSourceRef.DeRef.GSTitle;
            }
        }

        /// <summary>
        /// Gets the get h link.
        /// </summary>
        /// <value>
        /// The get h link.
        /// </value>
        public HLinkCitationModel GetHLink
        {
            get
            {
                HLinkCitationModel t = new HLinkCitationModel
                {
                    HLinkKey = HLinkKey,
                };
                return t;
            }
        }

        /// <summary>Gets or sets the media reference collection.</summary>
        /// <value>The media reference collection.</value>
        [DataMember]
        public HLinkMediaModelCollection GMediaRefCollection
        {
            get;
            set;
        }

            = new HLinkMediaModelCollection();

        /// <summary>
        /// Gets or sets the g note reference.
        /// </summary>
        /// <value>
        /// The g note reference.
        /// </value>
        [DataMember]
        public HLinkNoteModelCollection GNoteRef
        {
            get;
            set;
        }

            = new HLinkNoteModelCollection();

        /// <summary>
        /// Gets or sets the g page.
        /// </summary>
        /// <value>
        /// The g page.
        /// </value>
        [DataMember]
        public string GPage
        {
            get;
            set;
        }

            = string.Empty;

        /// <summary>
        /// Gets or sets the g source attribute.
        /// </summary>
        /// <value>
        /// The g source attribute.
        /// </value>
        [DataMember]
        public OCSrcAttributeModelCollection GSourceAttribute
        {
            get;
            set;
        }

            = new OCSrcAttributeModelCollection();

        /// <summary>
        /// Gets or sets the g source reference.
        /// </summary>
        /// <value>
        /// The g source reference.
        /// </value>
        [DataMember]
        public HLinkSourceModel GSourceRef
        {
            get;
            set;
        }

            = new HLinkSourceModel();

        /// <summary>
        /// Gets or sets the gramps tag reference.
        /// </summary>
        /// <value>
        /// The g tag reference.
        /// </value>
        [DataMember]
        public HLinkTagModelCollection GTagRef
        {
            get;
            set;
        }

            = new HLinkTagModelCollection();

        /// <summary>
        /// Compares two objects.
        /// </summary>
        /// <param name="a">
        /// object A.
        /// </param>
        /// <param name="b">
        /// object B.
        /// </param>
        /// <returns>
        /// One, two or three.
        /// </returns>
        public new int Compare(object a, object b)
        {
            CitationModel firstEvent = (CitationModel)a;
            CitationModel secondEvent = (CitationModel)b;

            // compare on Date first
            int testFlag = DateTime.Compare(firstEvent.GDateContent.SortDate, secondEvent.GDateContent.SortDate);

            return testFlag;
        }

        // TODO tagref*
        /// <summary>
        /// Implement IComparable CompareTo method.
        /// </summary>
        /// <param name="obj">
        /// The object to compare.
        /// </param>
        /// <returns>
        /// One, two or three.
        /// </returns>
        public int CompareTo(object obj)
        {
            CitationModel secondEvent = (CitationModel)obj;

            int testFlag = DateTime.Compare(GDateContent.SortDate, secondEvent.GDateContent.SortDate);

            return testFlag;
        }
    }
}