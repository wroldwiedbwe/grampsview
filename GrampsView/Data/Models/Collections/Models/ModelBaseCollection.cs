﻿//-----------------------------------------------------------------------
//
// Various data modesl to small to be worth putting in their own file
// is first launched.
//
// <copyright file="HLinkBaseCollection.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Model
{
    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;

    using GrampsView.Common;

    /// <summary>
    /// GRAMPS $$(Hlink)$$ element class.
    /// </summary>
    [CollectionDataContract]
    public class ModelBaseCollection<T> : CardGroup
         where T : ModelBase, new()
    {
    }
}