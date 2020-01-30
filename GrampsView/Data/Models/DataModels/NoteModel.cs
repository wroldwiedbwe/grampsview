//-----------------------------------------------------------------------
//
// The data model defined by this file serves to hold Event data from the GRAMPS data file
//
// <copyright file="NoteModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

////<define name = "note-content" >
////  <ref name="primary-object" />
////  <optional>
////    <attribute name = "format" >
////      < choice >
////        < value > 0 </ value >
////        < value > 1 </ value >
////      </ choice >
////    </ attribute >
////  </ optional >
////  < attribute name="type">
////    <text />
////  </attribute>
////  <ref name="styledtext" />
////  <zeroOrMore>
////    <element name = "tagref" >
////      <ref name="tagref-content" />
////    </element>
////  </zeroOrMore>
////</define>

////<define name = "styledtext" >
////   < element name="text">
////     <text />
////   </element>
////   <zeroOrMore>
////     <element name = "style" >
////       < attribute name="name">
////         <choice>
////           <value>bold</value>
////           <value>italic</value>
////           <value>underline</value>
////           <value>fontface</value>
////           <value>fontsize</value>
////           <value>fontcolor</value>
////           <value>highlight</value>
////           <value>superscript</value>
////           <value>link</value>
////         </choice>
////       </attribute>
////       <optional>
////         <attribute name = "value" >
////           < text />
////         </ attribute >
////       </ optional >
////       < oneOrMore >
////         < element name="range">
////           <attribute name = "start" >
////             < data type="int" />
////           </attribute>
////           <attribute name = "end" >
////             < data type="int" />
////           </attribute>
////         </element>
////       </oneOrMore>
////     </element>
////   </zeroOrMore>
//// </define>

namespace GrampsView.Data.Model
{
    using System;
    using System.Collections;
    using System.Runtime.Serialization;
    using GrampsView.Common;
    using GrampsView.Data.Collections;

    /// <summary>
    /// data model for an event.
    /// </summary>
    [DataContract]
    public sealed class NoteModel : ModelBase, INoteModel, IComparable, IComparer
    {
        /// <summary>
        /// The g type to do.
        /// </summary>
        public const string GTypeToDo = "To Do";

        /// <summary>
        /// The local format.
        /// </summary>
        private string localFormat = string.Empty;

        ///// <summary>
        ///// The local tagreference collection.
        ///// </summary>
        // private HLinkTagModelCollection localTagReference = new HLinkTagModelCollection();

        /// <summary>
        /// The local text.
        /// </summary>
        private string localText = string.Empty;

        /// <summary>
        /// The local type.
        /// </summary>
        private string localType = string.Empty;

        public NoteModel()
        {
            HomeImageHLink.HomeSymbol = CommonConstants.IconNotes;
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
                return TextShort;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="NoteModel" /> is format (0|1) #IMPLIED.
        /// </summary>
        /// <value>
        /// <c> true </c> if format; otherwise, <c> false </c>.
        /// </value>
        [DataMember]
        public string GFormat
        {
            get
            {
                return localFormat;
            }

            set
            {
                SetProperty(ref localFormat, value);
            }
        }

        /// <summary>
        /// Gets or sets the g tag reference collection.
        /// </summary>
        /// <value>
        /// The g tag reference collection.
        /// </value>
        [DataMember]
        public HLinkTagModelCollection GTagRefCollection
        {
            get; set;
        }

        = new HLinkTagModelCollection();

        // TODO add field style*
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        [DataMember]
        public string GText
        {
            get
            {
                return localText;
            }

            set
            {
                SetProperty(ref localText, value);
            }
        }

        /// <summary>
        /// Gets or sets the type CDATA #REQUIRED.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [DataMember]
        public string GType
        {
            get
            {
                return localType;
            }

            set
            {
                SetProperty(ref localType, value);
            }
        }

        /// <summary>
        /// Gets the get h link.
        /// </summary>
        /// <value>
        /// The get h link.
        /// </value>
        public HLinkNoteModel HLink
        {
            get
            {
                HLinkNoteModel t = new HLinkNoteModel
                {
                    HLinkKey = HLinkKey,
                };
                return t;
            }
        }

        /// <summary>
        /// Gets the shortened form of the text. Maximum length is 100.
        /// </summary>
        /// <value>
        /// The text short.
        /// </value>
        public string TextShort
        {
            get
            {
                return localText.Substring(0, Math.Min(localText.Length, 100));
            }
        }

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
            NoteModel firstEvent = (NoteModel)a;
            NoteModel secondEvent = (NoteModel)b;

            // compare on Date first
            int testFlag = string.Compare(firstEvent.GText, secondEvent.GText, StringComparison.CurrentCulture);

            return testFlag;
        }

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
            if (obj is null)
            {
                return 0;
            }

            NoteModel secondEvent = (NoteModel)obj;

            // compare on String first
            int testFlag = string.Compare(GText, secondEvent.GText, StringComparison.CurrentCulture);

            return testFlag;
        }
    }
}