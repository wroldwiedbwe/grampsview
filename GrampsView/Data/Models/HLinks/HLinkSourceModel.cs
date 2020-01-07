//-----------------------------------------------------------------------
//
// Various note models
//
// <copyright file="HLinkSourceModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

////<define name = "sourceref-content" >
////  < attribute name="hlink">
////    <data type = "IDREF" />
////  </ attribute >
////</ define >

namespace GrampsView.Data.Model
{
    using System.Runtime.Serialization;

    using GrampsView.Data.DataView;

    /// <summary>
    /// GRAMPS $$(hlink)$$ element class.
    /// </summary>
    [DataContract]
    public class HLinkSourceModel : HLinkBase, IHLinkSourceModel
    {
        ///// <summary>
        ///// The local image h link.
        ///// </summary>
        //private HLinkMediaModel localImageHLink = new HLinkMediaModel { HomeSymbol = Common.CommonConstants.IconSource };

        public SourceModel DeRef
        {
            get
            {
                if (Valid)
                {
                    return DV.SourceDV.GetModel(HLinkKey);
                }
                else
                {
                    return null;
                }
            }
        }

        ///// <summary>
        ///// Gets or sets the image h link key.
        ///// </summary>
        ///// <value>
        ///// The image h link key.
        ///// </value>
        //[DataMember]
        //public HLinkMediaModel HomeImageHLink
        //{
        //    get
        //    {
        //        return localImageHLink;
        //    }

        //    set
        //    {
        //        SetProperty(ref localImageHLink, value);
        //    }
        //}

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        /// <returns>
        /// </returns>
        public int CompareTo(HLinkSourceModel obj) => DeRef.CompareTo(obj);

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        /// <returns>
        /// </returns>
        public new int CompareTo(object obj)
        {
            // Null objects go first
            if (obj is null) { return 1; }

            // Can only comapre if they are the same type so assume equal
            if (obj.GetType() != typeof(HLinkSourceModel))
            {
                return 0;
            }

            return DeRef.CompareTo((obj as HLinkSourceModel).DeRef);
        }
    }
}