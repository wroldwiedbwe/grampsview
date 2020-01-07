// <copyright file="DateObjectModelVal.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Data.Model
{
    using System;
    using System.Runtime.Serialization;

    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    /// <summary>
    /// Create Val version of DateObjectModel.
    /// </summary>

    public partial class DateObjectModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateObjectModelVal" /> class.
        /// </summary>
        /// <param name="aType">
        /// a type.
        /// </param>
        /// <param name="aCFormat">
        /// a c format.
        /// </param>
        /// <param name="aDualDated">
        /// if set to <c> true </c> [a dual dated].
        /// </param>
        /// <param name="aNewYear">
        /// a new year.
        /// </param>
        /// <param name="aQuality">
        /// a quality.
        /// </param>
        /// <param name="aStart">
        /// a start.
        /// </param>
        /// <param name="aStop">
        /// a stop.
        /// </param>
        /// <param name="aVal">
        /// a value.
        /// </param>
        /// <param name="aValType">
        /// Type of a value.
        /// </param>

        private string localGValType;

        public void DateObjectModelVal(string aCFormat, bool aDualDated, string aNewYear, string aQuality, string aStart, string aStop, string aVal, string aValType)

        {
            try
            {
                //// cformat CDATA #REQUIRED
                // GCformat = aCFormat;

                //// dualdated value #REQUIRED
                // GDualdated = aDualDated;

                //// newyear CDATA #IMPLIED
                // GNewYear = aNewYear;

                //// type CDATA #REQUIRED
                // GQuality = aQuality;

                //// type CDATA #REQUIRED
                //// TODO fix this
                ////tempDate.GType = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(stringFound);

                //// val CDATA #REQUIRED
                // GVal = aVal;

                // type
                GValType = aValType;

                // Set NotionalDate
                NotionalDate = ConvertRFC1123StringToDateTime(GVal);
            }
            catch (Exception e)
            {
                // TODO
                DataStore.CN.NotifyException("Error in SetDate", e);
                throw;
            }
        }

        public string ValGetYear
        {
            get
            {
                if (DateValid)
                {
                    return NotionalDate.Year.ToString();
                }
                else
                {
                    return "Unknown";
                }
            }
        }

        public int ValGetAge
        {
            get
            {
                int outputAge;

                // calculate the age
                DateTime today = DateTime.Today;
                outputAge = today.Year - NotionalDate.Year;

                return outputAge;
            }
        }

        public string ValGetLongDateAsString
        {
            get
            {
                string dateString;

                dateString = GVal;

                if (!string.IsNullOrEmpty(GCformat))
                {
                    dateString += " Format: " + GCformat;
                }

                // Handle Type
                dateString = GValType + " " + dateString;

                if (!string.IsNullOrEmpty(GQuality))
                {
                    dateString += GQuality;
                }

                if (GDualdated)
                {
                    dateString += " Dual dated";
                }

                if (!string.IsNullOrEmpty(GNewYear))
                {
                    dateString += " New Year: " + GNewYear;
                }

                return dateString.Trim();
            }
        }

        /// <summary>
        /// Gets the string version of the date field.
        /// </summary>
        /// <returns>
        /// a string version of the date.
        /// </returns>
        public string ValGetShortDateAsString
        {
            get
            {
                string dateString;

                dateString = GVal;

                // Handle Type
                dateString = GValType + " " + dateString;

                return dateString.Trim();
            }
        }

        /// <summary>
        /// Gets the type of the g value.
        /// </summary>
        /// <value>
        /// The type of the g value.
        /// </value>
        [DataMember]
        public string ValGValType
        {
            get
            {
                return localGValType;
            }

            internal set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    SetProperty(ref localGValType, value);
                }
            }
        }
    }
}