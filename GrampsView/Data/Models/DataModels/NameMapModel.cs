﻿//-----------------------------------------------------------------------
//
// The data model defined by this file serves to hold Event data from the GRAMPS data file
//
// <copyright file="NameMapModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Model
{
    using System;
    using System.Collections;
    using System.Runtime.Serialization;

    using GrampsView.Common;

    /// <summary> data model for an event. <code> <
    // </code> </summary>
    [DataContract]
    public sealed class NameMapModel : ModelBase, INameMapModel, IComparable, IComparer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NameMapModel" /> class.
        /// </summary>
        public NameMapModel()
        {
            HomeImageHLink.HomeSymbol = CommonConstants.IconNameMaps;
        }

        /// <summary>
        /// Gets the get h link.
        /// </summary>
        /// <value>
        /// The get h link.
        /// </value>
        public HLinkNameMapModel GetHLink
        {
            get
            {
                HLinkNameMapModel t = new HLinkNameMapModel
                {
                    HLinkKey = HLinkKey,
                };
                return t;
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
        int IComparer.Compare(object a, object b)
        {
            // TagModel firstEvent = (TagModel)a; TagModel secondEvent = (TagModel)b;

            //// compare on Priority first
            // int testFlag = string.Compare(firstEvent.Name, secondEvent.Name, StringComparison.CurrentCulture);

            // return testFlag;
            return 0;
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
        int IComparable.CompareTo(object obj)
        {
            // TagModel secondEvent = (TagModel)obj;

            //// compare on Name first
            // int testFlag = string.Compare(Name, secondEvent.Name, StringComparison.CurrentCulture);

            // return testFlag;
            return 0;
        }
    }
}