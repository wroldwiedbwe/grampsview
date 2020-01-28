//-----------------------------------------------------------------------
//
// Storage routines for the FamilyModel
//
// <copyright file="FamilyModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
///
///  -- Completed
///  - primary-object
///  - rel
///  - father
///  - mother
///  - eventref
///  - objref
///  -
///  - attribute
///  -
///  - noteref
///  - citationref
///  - tagref
///
/// </summary>
///
/// // TODO Finish adding these
///// <code>
////
////    <zeroOrMore>
////      <element name = "childref" >
////        < attribute name="hlink">
////          <data type = "IDREF" />
////        </ attribute >
////        < optional >
////          < attribute name="priv">
////            <ref name="priv-content" />
////          </attribute>
////        </optional>
////        <optional>
////          <attribute name = "mrel" >
////            <ref name="child-rel" />
////          </attribute>
////        </optional>
////        <optional>
////          <attribute name = "frel" >
////            <ref name="child-rel" />
////          </attribute>
////        </optional>
////        <zeroOrMore>
////          <element name = "citationref" >
////            <ref name="citationref-content" />
////          </element>
////        </zeroOrMore>
////        <zeroOrMore>
////          <element name = "noteref" >
////            <ref name="noteref-content" />
////          </element>
////        </zeroOrMore>
////      </element>
////    </zeroOrMore>
////
////    <optional>
////      <ref name="date-content" />
////    </optional>
///// </code>

namespace GrampsView.Data.Model
{
    using System;
    using System.Collections;
    using System.Runtime.Serialization;
    using System.Text;

    using GrampsView.Common;
    using GrampsView.Data.Collections;

    /// <summary>
    /// Data model for a family.
    /// </summary>
    /// <seealso cref="GrampsView.Data.ViewModel.ModelBase" />
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// <seealso cref="GrampsView.Data.ViewModel.IFamilyModel" />
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// <seealso cref="System.IComparable" />
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// <seealso cref="System.Collections.IComparer" />
    [DataContract]
    public sealed class FamilyModel : ModelBase, IFamilyModel, IComparable, IComparer
    {
        /// <summary>
        /// Collection of Child References $$(childref)$$.
        /// </summary>
        private HLinkPersonModelCollection childRefCollection = new HLinkPersonModelCollection();

        /// <summary>
        /// relationship $$(rel)$$.
        /// </summary>
        private string familyRelationshipField = string.Empty;

        /// <summary>
        /// Family Father Handle father?.
        /// </summary>
        private HLinkPersonModel fatherHLink = new HLinkPersonModel();

        /// <summary>
        /// The local attribute reference.
        /// </summary>
        private OCAttributeModelCollection localAttributeReference = new OCAttributeModelCollection();

        // citationref*
        private HLinkCitationModelCollection localCitationReferenceCollection = new HLinkCitationModelCollection();

        /// <summary>
        /// The local event collection.
        /// </summary>
        private HLinkEventModelCollection localEventReferenceCollection = new HLinkEventModelCollection();

        /// <summary>
        /// Backing store for the HLink Note collection.
        /// </summary>
        private HLinkNoteModelCollection localGNoteRefCollection = new HLinkNoteModelCollection();

        /// <summary>
        /// The local media collection.
        /// </summary>
        private HLinkMediaModelCollection localMediaReferenceCollection = new HLinkMediaModelCollection();

        /// <summary>
        /// Family Father Handle mother?.
        /// </summary>
        private HLinkPersonModel motherHLink = new HLinkPersonModel();

        //// TODO lds_ord*

        /// <summary>
        /// Initializes a new instance of the <see cref="FamilyModel" /> class.
        /// </summary>
        public FamilyModel()
        {
            HomeImageHLink.HomeSymbol = CommonConstants.IconFamilies;
        }

        /// <summary>
        /// Gets the Mother and Father display name.
        /// </summary>
        /// <value>
        /// The display name of the family.
        /// </value>
        public string FamilyDisplayName
        {
            get
            {
                string familyName;

                // set family display name
                if (GFather.Valid)
                {
                    familyName = GFather.DeRef.GBirthName.FullName;
                }
                else
                {
                    familyName = "Unknown";
                }

                familyName += " - ";

                if (motherHLink.Valid)
                {
                    StringBuilder t = new StringBuilder();
                    t.Append(familyName);
                    t.Append(GMother.DeRef.GBirthName.FullName);
                    familyName = t.ToString();
                }
                else
                {
                    familyName += "Unknown";
                }

                return familyName;
            }
        }

        /// <summary>
        /// Gets the family display name sort.
        /// </summary>
        /// <value>
        /// The family display name sort.
        /// </value>
        public string FamilyDisplayNameSort
        {
            get
            {
                string familyName;

                // set family display name
                if (GFather.Valid)
                {
                    familyName = GFather.DeRef.GBirthName.GSurName.GetPrimarySurname;
                }
                else
                {
                    familyName = "Unknown";
                }

                if (motherHLink.Valid)
                {
                    StringBuilder t = new StringBuilder();
                    t.Append(familyName);
                    t.Append(GMother.DeRef.GBirthName.GSurName);
                    familyName = t.ToString();
                }
                else
                {
                    familyName += "Unknown";
                }

                return familyName;
            }
        }

        /// <summary>
        /// Gets or sets the g attribute collection. This is the [attribute*] attribute.
        /// </summary>
        /// <value>
        /// The g attribute collection.
        /// </value>
        [DataMember]
        public OCAttributeModelCollection GAttributeCollection
        {
            get
            {
                return localAttributeReference;
            }

            set
            {
                SetProperty(ref localAttributeReference, value);
            }
        }

        /// <summary>
        /// Gets or sets Child Reference collection.
        /// </summary>
        /// <value>
        /// The g child reference collection.
        /// </value>
        [DataMember]
        public HLinkPersonModelCollection GChildRefCollection
        {
            get
            {
                return childRefCollection;
            }

            set
            {
                SetProperty(ref childRefCollection, value);
            }
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
            get
            {
                return localCitationReferenceCollection;
            }

            set
            {
                SetProperty(ref localCitationReferenceCollection, value);
            }
        }

        /// <summary>
        /// Gets the get h link.
        /// </summary>
        /// <value>
        /// The get h link.
        /// </value>
        public HLinkFamilyModel HLink
        {
            get
            {
                HLinkFamilyModel t = new HLinkFamilyModel
                {
                    HLinkKey = HLinkKey,
                };
                return t;
            }
        }

        /// <summary>
        /// Gets or sets the event collection. This is the [eventref*] attribute.
        /// </summary>
        /// <value>
        /// The event collection.
        /// </value>
        [DataMember]
        public HLinkEventModelCollection GEventRefCollection
        {
            get
            {
                return localEventReferenceCollection;
            }

            set
            {
                SetProperty(ref localEventReferenceCollection, value);
            }
        }

        /// <summary>
        /// Gets or sets Family Relationship.
        /// </summary>
        /// <value>
        /// The family relationship.
        /// </value>
        [DataMember]
        public string GFamilyRelationship
        {
            get
            {
                return familyRelationshipField;
            }

            set
            {
                SetProperty(ref familyRelationshipField, value);
            }
        }

        /// <summary>
        /// Gets or sets Fathers $$(hLink)$$.
        /// </summary>
        /// <value>
        /// The fathers h link.
        /// </value>
        [DataMember]
        public HLinkPersonModel GFather
        {
            get
            {
                return fatherHLink;
            }

            set
            {
                SetProperty(ref fatherHLink, value);
            }
        }

        /// <summary>
        /// Gets or sets the g media reference collection. This is the [objref*] attribute.
        /// </summary>
        /// <value>
        /// The g media reference collection.
        /// </value>
        [DataMember]
        public HLinkMediaModelCollection GMediaRefCollection
        {
            get
            {
                return localMediaReferenceCollection;
            }

            set
            {
                SetProperty(ref localMediaReferenceCollection, value);
            }
        }

        /// <summary>
        /// Gets or sets Mothers $$(hLink)$$.
        /// </summary>
        /// <value>
        /// The mothers h link.
        /// </value>
        [DataMember]
        public HLinkPersonModel GMother
        {
            get
            {
                return motherHLink;
            }

            set
            {
                SetProperty(ref motherHLink, value);
            }
        }

        /// <summary>
        /// Gets or sets the HLink Note reference collection.
        /// </summary>
        /// <value>
        /// The g note reference collection.
        /// </value>
        [DataMember]
        public HLinkNoteModelCollection GNoteRefCollection
        {
            get
            {
                return localGNoteRefCollection;
            }

            set
            {
                SetProperty(ref localGNoteRefCollection, value);
            }
        }

        /// <summary>
        /// Gets or sets the g tag reference collection. This is the [tagref*] attribute.
        /// </summary>
        /// <value>
        /// The g tag reference collection.
        /// </value>
        [DataMember]
        public HLinkTagModelCollection GTagRefCollection
        {
            get; set;
        }

        /// <summary>
        /// Compare two FamilyModels.
        /// </summary>
        /// <param name="x">
        /// first object.
        /// </param>
        /// <param name="y">
        /// second object.
        /// </param>
        /// <returns>
        /// returns 1, 2, or 3.
        /// </returns>
        public new int Compare(object x, object y)
        {
            FamilyModel c1 = (FamilyModel)x;
            FamilyModel c2 = (FamilyModel)y;

            // compare on surnname first
            int testFlag = string.Compare(c1.GFather.DeRef.GBirthName.SortName, c2.GFather.DeRef.GBirthName.SortName, StringComparison.CurrentCulture);

            if (testFlag.Equals(0))
            {
                // equal so check firstname
                testFlag = string.Compare(c1.GMother.DeRef.GBirthName.SortName, c2.GMother.DeRef.GBirthName.SortName, StringComparison.CurrentCulture);
            }

            return testFlag;
        }

        /// <summary>
        /// Implement IComparable CompareTo method.
        /// </summary>
        /// <param name="obj">
        /// compare to object.
        /// </param>
        /// <returns>
        /// returns 1, 2 or 3.
        /// </returns>
        public int CompareTo(object obj)
        {
            FamilyModel secondFamilyModel = (FamilyModel)obj;

            // compare on fathers name first TODO use culture related sort
            int testFlag = string.Compare(GFather.DeRef.GBirthName.SortName, secondFamilyModel.GFather.DeRef.GBirthName.SortName, StringComparison.CurrentCulture);

            if (testFlag.Equals(0))
            {
                // equal so check firstname
                testFlag = string.Compare(GMother.DeRef.GBirthName.SortName, secondFamilyModel.GMother.DeRef.GBirthName.SortName, StringComparison.CurrentCulture);
            }

            return testFlag;
        }
    }
}