//-----------------------------------------------------------------------
//
// Various note models
//
// <copyright file="HLinkBookMarkModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Model
{
    using System.Runtime.Serialization;

    using GrampsView.Data.DataView;

    /// <summary>
    /// GRAMPS $$(hlink)$$ element class.
    /// </summary>
    [DataContract]
    public class HLinkBookMarkModel : HLinkBase, IHLinkBookMarkModel
    {
        public BookMarkModel DeRef
        {
            get
            {
                if (Valid)
                {
                    return new BookMarkDataView().GetModelFromHLinkString(HLinkKey);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}