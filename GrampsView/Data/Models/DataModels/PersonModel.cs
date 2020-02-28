//-----------------------------------------------------------------------
//
// Storage routines for the PersonModel
//
// <copyright file="PersonModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

////    -- Completed
////    - SecondaryColor-object
////    - name
////    - gender
////    - citationref
////    - eventref
////    - noteref
////    - tagref
////    - parentin
////    - lds_ord
////    - address
////    - attribute
////    - childof
////    - url
////
////   <zeroOrMore>
////     <element name = "personref" >
////       <ref name="personref-content" />
////     </element>
////   </zeroOrMore>
////   <zeroOrMore>

namespace GrampsView.Data.Model
{
    using System;
    using GrampsView.Common;
    using GrampsView.Data.Collections;

    using System.Collections;
    using System.Runtime.Serialization;

    /// <summary>
    /// data model for a person.
    /// </summary>
    [DataContract]
    [KnownType(typeof(HLinkFamilyModel))]
    public sealed class PersonModel : ModelBase, IPersonModel, IComparable, IComparer
    {
        /// <summary>
        /// The local attribute reference.
        /// </summary>
        private OCAttributeModelCollection _AttributeReference = new OCAttributeModelCollection();

        /// <summary>
        /// Reference to the parent family object.
        /// </summary>
        private HLinkFamilyModel _ChildOf = new HLinkFamilyModel();

        /// <summary>
        /// The local citation reference.
        /// </summary>
        private HLinkCitationModelCollection _CitationReference = new HLinkCitationModelCollection();

        /// <summary>
        /// Person Element - Event References.
        /// </summary>
        private HLinkEventModelCollection _EventReference = new HLinkEventModelCollection();

        /// <summary>
        /// Person Element - Gender.
        /// </summary>
        private string _Gender = string.Empty;

        /// <summary>
        /// The local LDS collection.
        /// </summary>
        private OCLdsOrdModelCollection _GLDSCollection = new OCLdsOrdModelCollection();

        /// <summary>
        /// Person Element - Name.
        /// </summary>
        private PersonNameModelCollection _GPersonNamesCollection = new PersonNameModelCollection();

        /// <summary>
        /// The local is living.
        /// </summary>
        private bool _IsLiving = false;

        /// <summary>
        /// Collection of Media References $$(mediaRef)$$.
        /// </summary>
        private HLinkMediaModelCollection _MediaCollection = new HLinkMediaModelCollection();

        /// <summary>
        /// Person Element - Note References.
        /// </summary>
        private HLinkNoteModelCollection _NoteReference = new HLinkNoteModelCollection();

        /// <summary>
        /// Collection of Family References $$(parentin)$$.
        /// </summary>
        private HLinkFamilyModelCollection _ParentInCollection = new HLinkFamilyModelCollection();

        /// <summary>
        /// The local sibling reference collection.
        /// </summary>
        private HLinkPersonModelCollection _SiblingRefCollection = new HLinkPersonModelCollection();

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonModel"/> class.
        /// </summary>
        public PersonModel()
        {
            HomeImageHLink.HomeSymbol = CommonConstants.IconPeople;
        }

        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        /// <value>
        /// The birth date.
        /// </value>
        [DataMember]
        public DateObjectModel BirthDate
        {
            get; set;
        }

            = new DateObjectModel();

        /// <summary>
        /// Gets or sets address collection.
        /// </summary>
        [DataMember]
        public OCAddressModelCollection GAddress { get; set; }

        = new OCAddressModelCollection();

        /// <summary>
        /// Gets or sets the g attribute collection.
        /// </summary>
        /// <value>
        /// The g attribute collection.
        /// </value>
        [DataMember]
        public OCAttributeModelCollection GAttributeCollection
        {
            get
            {
                return _AttributeReference;
            }

            set
            {
                SetProperty(ref _AttributeReference, value);
            }
        }

        /// <summary>
        /// Gets or sets the child of.
        /// </summary>
        /// <value>
        /// The child of.
        /// </value>
        [DataMember]
        public HLinkFamilyModel GChildOf
        {
            get
            {
                return _ChildOf;
            }

            set
            {
                SetProperty(ref _ChildOf, value);
            }
        }

        /// <summary>
        /// Gets or sets the citation reference collection.
        /// </summary>
        /// <value>
        /// The citation reference collection.
        /// </value>
        [DataMember]
        public HLinkCitationModelCollection GCitationRefCollection
        {
            get
            {
                return _CitationReference;
            }

            set
            {
                SetProperty(ref _CitationReference, value);
            }
        }

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
                return GPersonNamesCollection.GetPrimaryName.GetDefaultText;
            }
        }

        public string GetGenderGlyph

        {
            get
            {
                switch (GGender)
                {
                    case "F":
                        {
                            return IconFont.GenderFemale;
                        }

                    case "M":
                        {
                            return IconFont.GenderMale;
                        }

                    case "U":
                        {
                            return IconFont.BatteryUnknown;
                        }

                    default:
                        return IconFont.BatteryUnknown;
                }
            }
        }

        /// <summary> Gets or sets the Event Reference Collection.
        // </summary>
        [DataMember]
        public HLinkEventModelCollection GEventRefCollection
        {
            get
            {
                return _EventReference;
            }

            set
            {
                SetProperty(ref _EventReference, value);
            }
        }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        [DataMember]
        public string GGender
        {
            get
            {
                return _Gender;
            }

            set
            {
                SetProperty(ref _Gender, value);
            }
        }

        /// <summary>
        /// Gets the gender decode.
        /// </summary>
        /// <value>
        /// The gender decode.
        /// </value>
        public string GGenderAsString
        {
            get
            {
                switch (GGender)
                {
                    case "F":
                        {
                            return "Female";
                        }

                    case "M":
                        {
                            return "Male";
                        }

                    case "U":
                        {
                            return "Unknown";
                        }

                    default:
                        return "Unknown";
                }
            }
        }

        /// <summary>
        /// Gets or sets the LDS collection.
        /// </summary>
        /// <value>
        /// The GLDS collection.
        /// </value>
        [DataMember]
        public OCLdsOrdModelCollection GLDSCollection
        {
            get
            {
                return _GLDSCollection;
            }

            set
            {
                SetProperty(ref _GLDSCollection, value);
            }
        }

        /// <summary> Gets or sets Media In $$(hLink)$$.
        // </summary>
        [DataMember]
        public HLinkMediaModelCollection GMediaRefCollection
        {
            get
            {
                return _MediaCollection;
            }

            set
            {
                SetProperty(ref _MediaCollection, value);
            }
        }

        /// <summary>
        /// Gets or sets the Note reference collection.
        /// </summary>
        /// <value>
        /// The Note reference.
        /// </value>
        [DataMember]
        public HLinkNoteModelCollection GNoteRefCollection
        {
            get
            {
                return _NoteReference;
            }

            set
            {
                SetProperty(ref _NoteReference, value);
            }
        }

        /// <summary>
        /// Gets or sets the parent relationship collection.
        /// </summary>
        [DataMember]
        public HLinkFamilyModelCollection GParentInRefCollection
        {
            get
            {
                return _ParentInCollection;
            }

            set
            {
                SetProperty(ref _ParentInCollection, value);
            }
        }

        /// <summary>
        /// Gets or sets the Persons Birthname.
        /// </summary>
        [DataMember]
        public PersonNameModelCollection GPersonNamesCollection
        {
            get
            {
                return _GPersonNamesCollection;
            }

            set
            {
                SetProperty(ref _GPersonNamesCollection, value);
            }
        }

        /// <summary>
        /// Gets or sets the g person reference collection.
        /// </summary>
        /// <value>
        /// The g person reference collection.
        /// </value>
        [DataMember]
        public HLinkPersonModelCollection GPersonRefCollection { get; set; } = new HLinkPersonModelCollection();

        /// <summary>
        /// Gets or sets the g tag reference collection.
        /// </summary>
        /// <value>
        /// The g tag reference collection.
        /// </value>
        [DataMember]
        public HLinkTagModelCollection GTagRefCollection { get; set; } = new HLinkTagModelCollection();

        /// <summary>
        /// Gets or sets the URL collection.
        /// </summary>
        /// <value>
        /// The URL collection.
        /// </value>
        [DataMember]
        public OCURLModelCollection GURLCollection { get; set; } = new OCURLModelCollection();

        /// <summary>
        /// Gets the get h link.
        /// </summary>
        /// <value>
        /// The get h link.
        /// </value>
        public HLinkPersonModel HLink
        {
            get
            {
                HLinkPersonModel t = new HLinkPersonModel
                {
                    HLinkKey = HLinkKey,
                };

                return t;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is living.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is living; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool IsLiving
        {
            get
            {
                return _IsLiving;
            }

            set
            {
                SetProperty(ref _IsLiving, value);
            }
        }

        /// <summary>
        /// Gets the is living as string.
        /// </summary>
        /// <value>
        /// The is living as string.
        /// </value>
        public string IsLivingAsString
        {
            get
            {
                return _IsLiving.ToString();
            }
        }

        /// <summary>
        /// Gets or sets the sibling reference collection.
        /// </summary>
        /// <value>
        /// The sibling reference collection.
        /// </value>
        public HLinkPersonModelCollection SiblingRefCollection
        {
            get
            {
                return _SiblingRefCollection;
            }

            set
            {
                SetProperty(ref _SiblingRefCollection, value);
            }
        }

        /// <summary>
        /// Compare two PersonModels.
        /// </summary>
        /// <param name="a">
        /// first object.
        /// </param>
        /// <param name="b">
        /// second object.
        /// </param>
        /// <returns>
        /// returns 1, 2, or 3.
        /// </returns>
        public new int Compare(object a, object b)
        {
            PersonModel firstPersonModel = (PersonModel)a;
            PersonModel secondPersonModel = (PersonModel)b;

            // compare on surnname first
            int testFlag = string.Compare(firstPersonModel.GPersonNamesCollection.GetPrimaryName.GSurName.GetPrimarySurname, secondPersonModel.GPersonNamesCollection.GetPrimaryName.GSurName.GetPrimarySurname, StringComparison.CurrentCulture);

            if (testFlag.Equals(0))
            {
                // equal so check firstname
                testFlag = string.Compare(firstPersonModel.GPersonNamesCollection.GetPrimaryName.GFirstName, secondPersonModel.GPersonNamesCollection.GetPrimaryName.GFirstName, StringComparison.CurrentCulture);
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
            PersonModel secondPersonModel = (PersonModel)obj;

            // compare on surnname first
            int testFlag = string.Compare(GPersonNamesCollection.GetPrimaryName.GSurName.GetPrimarySurname, secondPersonModel.GPersonNamesCollection.GetPrimaryName.GSurName.GetPrimarySurname, StringComparison.CurrentCulture);

            if (testFlag.Equals(0))
            {
                // equal so check firstname
                testFlag = string.Compare(GPersonNamesCollection.GetPrimaryName.GFirstName, secondPersonModel.GPersonNamesCollection.GetPrimaryName.GFirstName, StringComparison.CurrentCulture);
            }

            return testFlag;
        }
    }
}