//-----------------------------------------------------------------------
//
// Various note models
//
// <copyright file="HLinkNoteModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

////<define name = "noteref-content" >
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
    public class HLinkBackLink : HLinkBase
    {
        public HLinkBase DeRef
        {
            get
            {
                if (Valid)
                {
                    return null;
                    //return DV.NoteDV.GetHLink(HLinkKey);
                }
                else
                {
                    return null;
                }
            }
        }

        public int HLinkType { get; set; } // = (int)HLinkBase.HLinkTypeEnum.Unknown;
    }
}