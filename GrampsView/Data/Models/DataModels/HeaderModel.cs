//-----------------------------------------------------------------------
//
// datamodel for the external storage metadata
//
// <copyright file="HeaderModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

////<code>
////  <element name = "header" >
////    < element name="created">
////      <attribute name = "date" >
////        < data type="date" />
////      </attribute>
////      <attribute name = "version" >
////        < text />
////      </ attribute >
////    </ element >
////    < element name="researcher">
////      <optional>
////        <ref name="researcher-content" />
////      </optional>
////    </element>
////    <optional>
////      <element name = "mediapath" >
////        < text />
////      </ element >
////    </ optional >
////  </ element >

////      <define name = "researcher-content" >
////  < element name="resname">
////    <text />
////  </element>
////  <optional>
////    <element name = "resaddr" >
////      < text />
////    </ element >
////  </ optional >
////  < optional >
////    < element name="reslocality">
////      <text />
////    </element>
////  </optional>
////  <optional>
////    <element name = "rescity" >
////      < text />
////    </ element >
////  </ optional >
////  < optional >
////    < element name="resstate">
////      <text />
////    </element>
////  </optional>
////  <optional>
////    <element name = "rescountry" >
////      < text />
////    </ element >
////  </ optional >
////  < optional >
////    < element name="respostal">
////      <text />
////    </element>
////  </optional>
////  <optional>
////    <element name = "resphone" >
////      < text />
////    </ element >
////  </ optional >
////  < optional >
////    < element name="resemail">
////      <text />
////    </element>
////  </optional>
////</define>

namespace GrampsView.Data.Model
{
    using System.Runtime.Serialization;

    //// </code>
    [DataContract]
    public class HeaderModel : ModelBase, IHeaderModel
    {
        /// <summary>
        /// The local database version.
        /// </summary>
        private int localDatabaseVersion = 0;

        /// <summary>
        /// created date.
        /// </summary>
        private string localGCreatedDate = string.Empty;

        /// <summary>
        /// crated version.
        /// </summary>
        private string localGCreatedVersion = string.Empty;

        /// <summary>
        /// Media Path.
        /// </summary>
        private string localGMediaPath = string.Empty;

        /// <summary>
        /// Researcher Address.
        /// </summary>
        private string localGResearcherAddress = string.Empty;

        /// <summary>
        /// Researcher City.
        /// </summary>
        private string localGResearcherCity = string.Empty;

        /// <summary>
        /// Researcher Country.
        /// </summary>
        private string localGResearcherCountry = string.Empty;

        /// <summary>
        /// Researcher Email.
        /// </summary>
        private string localGResearcherEmail = string.Empty;

        /// <summary>
        /// Researcher Locality.
        /// </summary>
        private string localGResearcherLocality = string.Empty;

        /// <summary>
        /// Researcher Name.
        /// </summary>
        private string localGResearcherName = string.Empty;

        /// <summary>
        /// Researcher Phone.
        /// </summary>
        private string localGResearcherPhone = string.Empty;

        /// <summary>
        /// Researcher Postal Address.
        /// </summary>
        private string localGResearcherPostal = string.Empty;

        /// <summary>
        /// Researcher State.
        /// </summary>
        private string localGResearcherState = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderModel" /> class.
        /// </summary>
        public HeaderModel()
        {
        }

        /// <summary>
        /// Gets or sets the database version.
        /// </summary>
        /// <value>
        /// The database version.
        /// </value>
        [DataMember]
        public int DatabaseVersion
        {
            get
            {
                return localDatabaseVersion;
            }

            set
            {
                SetProperty(ref localDatabaseVersion, value);
            }
        }

        /// <summary>
        /// Gets or sets date the metadata was created.
        /// </summary>
        [DataMember]
        public string GCreatedDate
        {
            get
            {
                return localGCreatedDate;
            }

            set
            {
                SetProperty(ref localGCreatedDate, value);
            }
        }

        /// <summary>
        /// Gets or sets the data version.
        /// </summary>
        [DataMember]
        public string GCreatedVersion
        {
            get
            {
                return localGCreatedVersion;
            }

            set
            {
                SetProperty(ref localGCreatedVersion, value);
            }
        }

        /// <summary>
        /// Gets or sets the absolute path to the start of the media file file structure.
        /// </summary>
        [DataMember]
        public string GMediaPath
        {
            get
            {
                return localGMediaPath;
            }

            set
            {
                SetProperty(ref localGMediaPath, value);
            }
        }

        /// <summary>
        /// Gets or sets the researchers address.
        /// </summary>
        [DataMember]
        public string GResearcherAddress
        {
            get
            {
                return localGResearcherAddress;
            }

            set
            {
                SetProperty(ref localGResearcherAddress, value);
            }
        }

        /// <summary>
        /// Gets or sets the researchers city.
        /// </summary>
        [DataMember]
        public string GResearcherCity
        {
            get
            {
                return localGResearcherCity;
            }

            set
            {
                SetProperty(ref localGResearcherCity, value);
            }
        }

        /// <summary>
        /// Gets or sets the researchers country.
        /// </summary>
        [DataMember]
        public string GResearcherCountry
        {
            get
            {
                return localGResearcherCountry;
            }

            set
            {
                SetProperty(ref localGResearcherCountry, value);
            }
        }

        /// <summary>
        /// Gets or sets the researchers email address.
        /// </summary>
        [DataMember]
        public string GResearcherEmail
        {
            get
            {
                return localGResearcherEmail;
            }

            set
            {
                SetProperty(ref localGResearcherEmail, value);
            }
        }

        /// <summary>
        /// Gets or sets the researchers locality.
        /// </summary>
        [DataMember]
        public string GResearcherLocality
        {
            get
            {
                return localGResearcherLocality;
            }

            set
            {
                SetProperty(ref localGResearcherLocality, value);
            }
        }

        /// <summary>
        /// Gets or sets the researchers name.
        /// </summary>
        [DataMember]
        public string GResearcherName
        {
            get
            {
                return localGResearcherName;
            }

            set
            {
                SetProperty(ref localGResearcherName, value);
            }
        }

        /// <summary>
        /// Gets or sets the researchers phone.
        /// </summary>
        [DataMember]
        public string GResearcherPhone
        {
            get
            {
                return localGResearcherPhone;
            }

            set
            {
                SetProperty(ref localGResearcherPhone, value);
            }
        }

        /// <summary>
        /// Gets or sets the researchers postal address.
        /// </summary>
        [DataMember]
        public string GResearcherPostal
        {
            get
            {
                return localGResearcherPostal;
            }

            set
            {
                SetProperty(ref localGResearcherPostal, value);
            }
        }

        /// <summary>
        /// Gets or sets the researchers state.
        /// </summary>
        [DataMember]
        public string GResearcherState
        {
            get
            {
                return localGResearcherState;
            }

            set
            {
                SetProperty(ref localGResearcherState, value);
            }
        }
    }
}