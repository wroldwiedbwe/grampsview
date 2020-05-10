//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="CardGroupCollection.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// </summary>
namespace GrampsView.Common
{
    using System;

    /// <summary>
    /// </summary>
    public class CardGroup : CardGroupBase<object>
    {
        public CardGroup()
        {
        }

        public void Add(CardGroup argCardGroup)
        {
            if (argCardGroup is null)
            {
                throw new ArgumentNullException(nameof(argCardGroup));
            }

            if (argCardGroup.Count > 0)
            {
                base.Add(argCardGroup);
            }
        }

        public void Add(CardGroup argCardGroup, string argTitle)
        {
            if (argCardGroup is null)
            {
                throw new ArgumentNullException(nameof(argCardGroup));
            }

            if (argTitle is null)
            {
                throw new ArgumentNullException(nameof(argTitle));
            }

            if (argCardGroup.Count > 0)
            {
                base.Add(argCardGroup);
                this.Title = argTitle;
            }
        }
    }
}