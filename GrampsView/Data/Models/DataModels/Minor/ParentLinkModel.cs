//-----------------------------------------------------------------------
//
// Handles GRAMPS Alt fields
//
// <copyright file="ParentLinkModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Model
{
    using System.Runtime.Serialization;

    /// <summary>
    /// GRAMPS Alt element class.
    /// </summary>
    [DataContract]
    public class ParentLinkModel : ModelBase, IDetailViewText
    {
        public ParentLinkModel()
        {
        }

        public FamilyModel Parents { get; set; }

                    = new FamilyModel();
    }
}