﻿//-----------------------------------------------------------------------
//
// Various data modesl to small to be worth putting in their own file
// is first launched.
//
// <copyright file="PersonNameModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Model
{
    using System;
    using System.Collections;
    using System.Runtime.Serialization;

    using GrampsView.Data.Collections;

    /// <summary>Class for holding a person's name.</summary>
    /// <seealso cref="GrampsView.Data.Model.ModelBase" />
    /// <seealso cref="GrampsView.Data.Model.IPersonNameModel" />
    /// <seealso cref="System.IComparable" />
    /// <seealso cref="System.Collections.IComparer" />
    [DataContract]
    public class PersonNameModel : ModelBase, IPersonNameModel, IComparable, IComparer
    {
        /// <summary>Gets or sets the citation reference collection.</summary>
        /// <value>The citation reference collection.</value>
        [DataMember]
        public HLinkCitationModelCollection CitationRefCollection { get; set; }

        /// <summary>
        /// Gets the Persons FullName. Returns 'unknown' if no firstname or surname.
        /// </summary>
        public string FullName
        {
            get
            {
                string fullName = GFirstName + " " + GSurName.GetPrimarySurname;
                if (fullName.Trim().Length == 0)
                {
                    return "Unknown";
                }
                else
                {
                    return fullName;
                }
            }
        }

        /// <summary>Gets or sets the alt. details.</summary>
        /// <value>The g alt.</value>
        [DataMember]
        public AltModel GAlt { get; set; } = new AltModel();

        /// <summary>Gets or sets the  call details.</summary>
        /// <value>The g call.</value>
        [DataMember]
        public string GCall
        {
            get; set;
        }

        = string.Empty;

        /// <summary>Gets or sets the date.</summary>
        /// <value>The date.</value>
        [DataMember]
        public DateObjectModel GDate { get; set; } = new DateObjectModel();

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The g display.
        /// </value>
        [DataMember]
        public string GDisplay
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the default text for this ViewModel.
        /// </summary>
        /// <value>
        /// The get default text.
        /// </value>
        public new string GetDefaultText
        {
            get
            {
                return FullName;
            }
        }

        /// <summary>Gets or sets the  family nick.</summary>
        /// <value>The g family nick.</value>
        [DataMember]
        public string GFamilyNick
        {
            get; set;
        }

        /// <summary>Gets or sets the first name.</summary>
        /// <value>The first name of the g.</value>
        [DataMember]
        public string GFirstName
        {
            get;

            set;
        }

        /// <summary>Gets or sets the group.</summary>
        /// <value>The g group.</value>
        [DataMember]
        public string GGroup
        {
            get; set;
        }

        /// <summary>Gets or sets the nick.</summary>
        /// <value>The g nick.</value>
        [DataMember]
        public string GNick
        {
            get; set;
        }

        /// <summary>Gets or sets a value indicating whether [priv] set.</summary>
        /// <value>
        ///   <c>true</c> if [g priv]; otherwise, <c>false</c>.</value>
        public bool GPriv
        {
            get;
            set;
        }

            = false;

        /// <summary>Gets or sets the sort.</summary>
        /// <value>The g sort.</value>
        [DataMember]
        public string GSort
        {
            get;

            set;
        }

        /// <summary>Gets or sets the suffix.</summary>
        /// <value>The g suffix.</value>
        [DataMember]
        public string GSuffix
        {
            get; set;
        }

        /// <summary>Gets or sets the surname.</summary>
        /// <value>The name of the g sur.</value>
        [DataMember]
        public SurnameModelCollection GSurName
        {
            get;

            set;
        }

        /// <summary>Gets or sets the title.</summary>
        /// <value>The g title.</value>
        [DataMember]
        public string GTitle
        {
            get; set;
        }

        /// <summary>Gets or sets the type.</summary>
        /// <value>The type of the g.</value>
        [DataMember]
        public string GType
        {
            get;

            set;
        }

        /// <summary>Gets or sets the note reference collection.</summary>
        /// <value>The note reference collection.</value>
        [DataMember]
        public HLinkNoteModelCollection NoteReferenceCollection { get; set; }

        /// <summary>
        /// Gets the sort name o.
        /// </summary>
        /// <value>
        /// The name of the sort.
        /// </value>
        public string SortName
        {
            get
            {
                return GSurName.GetPrimarySurname + GFirstName;
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
            PersonNameModel firstEvent = (PersonNameModel)a;
            PersonNameModel secondEvent = (PersonNameModel)b;

            // compare on Date first
            int testFlag = string.Compare(firstEvent.SortName, secondEvent.SortName, StringComparison.CurrentCulture);

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
            PersonNameModel secondEvent = (PersonNameModel)obj;

            // compare on String first
            int testFlag = string.Compare(SortName, secondEvent.SortName, StringComparison.CurrentCulture);

            return testFlag;
        }
    }
}