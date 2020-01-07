// <copyright file="HLinkCitationModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

//// <define name = "citationref-content" >
////  < attribute name="hlink">
////    <data type = "IDREF" />
////  </ attribute >
//// </ define >

namespace GrampsView.Data.Model
{
    using System.Runtime.Serialization;

    using GrampsView.Data.DataView;

    /// <summary>
    /// GRAMPS $$(hlink)$$ element class.
    /// </summary>
    [DataContract]
    public class HLinkCitationModel : HLinkBase, IHLinkCitationModel
    {
        /// <summary>
        /// Gets the de reference.
        /// </summary>
        /// <value>
        /// The de reference.
        /// </value>
        public CitationModel DeRef
        {
            get
            {
                if (Valid)
                {
                    return DV.CitationDV.GetModel(HLinkKey);
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Compares to. Bases it on the HLInkKey for want of anything else that makes sense.
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
            if (obj.GetType() != typeof(HLinkCitationModel))
            {
                return 0;
            }

            return DeRef.CompareTo((obj as HLinkCitationModel).DeRef);
        }
    }
}