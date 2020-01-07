// <copyright file="HLinkHeaderModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Data.Model
{
    using System.Runtime.Serialization;

    using GrampsView.Data.DataView;

    /// <summary>
    /// GRAMPS $$(hlink)$$ element class.
    /// </summary>
    [DataContract]
    public class HLinkHeaderModel : HLinkBase, IHLinkHeaderModel
    {
        /// <summary>
        /// Gets the de reference.
        /// </summary>
        /// <value>
        /// The de reference.
        /// </value>
        public HeaderModel DeRef
        {
            get
            {
                if (Valid)
                {
                    return new HeaderDataView().GetModel(HLinkKey);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}