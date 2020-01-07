﻿//-----------------------------------------------------------------------
//
// Handles GRAMPS Alt fields
//
// <copyright file="AttributeModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

////<define name = "attribute-content" >
////  < optional >
////    < attribute name="priv">
////      <ref name="priv-content" />
////    </attribute>
////  </optional>
////  <attribute name = "type" >
////    < text />
////  </ attribute >
////  < attribute name="value">
////    <text />
////  </attribute>
////  <zeroOrMore>
////    <element name = "citationref" >
////      <ref name="citationref-content" />
////    </element>
////  </zeroOrMore>
////  <zeroOrMore>
////    <element name = "noteref" >
////      <ref name="noteref-content" />
////    </element>
////  </zeroOrMore>
////</define>

namespace GrampsView.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using GrampsView.Common;
    using GrampsView.Data.Collections;

    /// <summary>
    /// GRAMPS Alt element class.
    /// </summary>
    [DataContract]
    public class AttributeModel : ModelBase, IAttributeModel, IComparable, IComparer<AttributeModel>
    {
        public AttributeModel()
        {
            HomeImageHLink.HomeSymbol = CommonConstants.IconAttribute;
        }

        /// <summary>
        /// Gets or sets the g citation reference collection.
        /// </summary>
        /// <value>
        /// The g citation reference collection.
        /// </value>
        [DataMember]
        public HLinkCitationModelCollection GCitationReferenceCollection
        {
            get;
            set;
        }

            = new HLinkCitationModelCollection();

        /// <summary>
        /// Gets or sets the g note model reference collection.
        /// </summary>
        /// <value>
        /// The g note model reference collection.
        /// </value>
        [DataMember]
        public HLinkNoteModelCollection GNoteModelReferenceCollection
        {
            get;
            set;
        }

            = new HLinkNoteModelCollection();

        ///// <summary>
        ///// Gets or sets a value indicating whether [g priv].
        ///// </summary>
        ///// <value>
        ///// <c> true </c> if [g priv]; otherwise, <c> false </c>.
        ///// </value>
        // [DataMember] public bool GPriv { get; set; }

        // = false;

        /// <summary>
        /// Gets or sets the g text.
        /// </summary>
        /// <value>
        /// The g text.
        /// </value>
        [DataMember]
        public string GType
        {
            get;
            set;
        }

            = null;

        /// <summary>
        /// Gets or sets the g value.
        /// </summary>
        /// <value>
        /// The g value.
        /// </value>
        [DataMember]
        public string GValue
        {
            get;
            set;
        }

            = null;

        /// <summary>
        /// Compares the specified x.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <param name="y">
        /// The y.
        /// </param>
        /// <returns>
        /// </returns>
        public int Compare(AttributeModel x, AttributeModel y)
        {
            return Compare(x.GType, y.GType);
        }

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        /// <returns>
        /// </returns>
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            AttributeModel secondSource = (AttributeModel)obj;

            // compare on Page first TODO compare on Page?
            int testFlag = GType.CompareTo(secondSource.GType);

            return testFlag;
        }
    }
}