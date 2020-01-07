//-----------------------------------------------------------------------
//
// Handles GRAMPS Alt fields
//
// <copyright file="AddressModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Model
{
    using System;
    using GrampsView.Data.Collections;

    using System.Runtime.Serialization;
    using GrampsView.Common;

    /// <summary>
    /// GRAMPS Alt element class.
    ///
    /// -- Completed
    /// - priv
    /// - date-content
    /// - street
    /// - locality.
    /// </summary>
    public class AddressModel : ModelBase, IAddressModel, IComparable<AddressModel>, IEquatable<AddressModel>
    {
        // <optional> <element name = "city" > < text /> </ element > </ optional > < optional > <
        // element name="county"> <text /> </element> </optional> <optional> <element name = "state"
        // > < text /> </ element > </ optional > < optional > < element name="country"> <text />
        // </element> </optional> <optional> <element name = "postal" > < text /> </ element > </
        // optional > < optional > < element name="phone"> <text /> </element> </optional>
        // <zeroOrMore> <element name = "noteref" > <ref name="noteref-content" /> </element>
        // </zeroOrMore> <zeroOrMore> <element name = "citationref" > <ref
        // name="citationref-content" /> </element> </zeroOrMore> </define>

        public AddressModel()
        {
            HomeImageHLink.HomeSymbol = CommonConstants.IconAddress;
        }

        /// <summary>
        /// Gets the formatted.
        /// </summary>
        /// <value>
        /// The formatted address.
        /// </value>
        public string Formatted
        {
            get
            {
                string formattedAddress = string.Empty;

                if (!string.IsNullOrEmpty(GStreet))
                {
                    formattedAddress = formattedAddress + GStreet + ",";
                }

                if (!string.IsNullOrEmpty(GLocality))
                {
                    formattedAddress = formattedAddress + GLocality + ",";
                }

                if (!string.IsNullOrEmpty(GCity))
                {
                    formattedAddress = formattedAddress + GCity + ",";
                }

                if (!string.IsNullOrEmpty(GCounty))
                {
                    formattedAddress = formattedAddress + GCounty + ",";
                }

                if (!string.IsNullOrEmpty(GState))
                {
                    formattedAddress = formattedAddress + GState + ",";
                }

                if (!string.IsNullOrEmpty(GCountry))
                {
                    formattedAddress = formattedAddress + GCountry + ",";
                }

                return formattedAddress;
            }
        }

        /// <summary>
        /// Gets or sets the g citation reference collection.
        /// </summary>
        /// <value>
        /// The g citation reference collection.
        /// </value>
        [DataMember]
        public HLinkCitationModelCollection GCitationRefCollection { get; set; }

        [DataMember]
        public string GCity { get; set; } = string.Empty;

        [DataMember]
        public string GCountry { get; set; } = string.Empty;

        [DataMember]
        public string GCounty { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Date recorded for the address.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        [DataMember]
        public DateObjectModel GDate { get; set; } = new DateObjectModel();

        /// <summary>
        /// Gets or sets the locality.
        /// </summary>
        /// <value>
        /// The locality.
        /// </value>
        [DataMember]
        public string GLocality { get; set; } = string.Empty;

        [DataMember]
        public HLinkNoteModelCollection GNoteRefCollection { get; set; }

        [DataMember]
        public string GPhone { get; set; } = string.Empty;

        [DataMember]
        public string GPostal { get; set; } = string.Empty;

        [DataMember]
        public string GState { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Street name and number.
        /// </summary>
        [DataMember]
        public string GStreet { get; set; } = string.Empty;

        public int CompareTo(AddressModel other)
        {
            return GDate.CompareTo(other.GDate);
        }

        public bool Equals(AddressModel other)
        {
            if (GDate == other.GDate)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}