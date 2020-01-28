//-----------------------------------------------------------------------
//
// Various data modesl to small to be worth putting in their own file
// is first launched.
//
// <copyright file="HLinkEventModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

////<define name = "eventref-content" >
////  < attribute name="hlink">
////    <data type = "IDREF" />
////  </ attribute >
////  < optional >
////    < attribute name="priv">
////      <ref name="priv-content" />
////    </attribute>
////  </optional>
////  <optional>
////    <attribute name = "role" >
////      < text />
////    </ attribute >
////  </ optional >
////  < zeroOrMore >
////    < element name="attribute">
////      <ref name="attribute-content" />
////    </element>
////  </zeroOrMore>
////  <zeroOrMore>
////    <element name = "noteref" >
////      <ref name="noteref-content" />
////    </element>
////  </zeroOrMore>
////</define>

namespace GrampsView.Data.Model
{
    using System.Runtime.Serialization;

    using GrampsView.Data.DataView;

    /// <summary>
    /// GRAMPS $$(hlink)$$ element class.
    /// </summary>
    [DataContract]
    public class HLinkEventModel : HLinkBase, IHLinkEventModel
    {
        private EventModel _Deref = new EventModel();

        /// <summary>
        /// Gets the de reference.
        /// </summary>
        /// <value>
        /// The de reference.
        /// </value>
        public EventModel DeRef
        {
            get
            {
                if (Valid)
                {
                    if ((_Deref is null) || (!_Deref.Valid))
                    {
                        _Deref = DV.EventDV.GetModelFromHLinkString(HLinkKey);
                    }

                    return _Deref;
                }
                else
                {
                    return new EventModel();
                }
            }
        }
    }
}