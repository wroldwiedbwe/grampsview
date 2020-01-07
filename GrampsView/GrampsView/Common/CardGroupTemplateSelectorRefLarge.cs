//-----------------------------------------------------------------------
//
// Common routines for the CommonRoutines
//
// <copyright file="CardGroupTemplateSelectorRefLarge.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Common
{
    using GrampsView.Data.Model;

    using Xamarin.Forms;

    /// <summary>
    /// Object Collection Template Selector.
    /// </summary>
    public class CardGroupTemplateSelectorRefLarge : DataTemplateSelector
    {
        public DataTemplate RepositoryRefTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Selects the template core.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <returns>
        /// A data template.
        /// </returns>
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is HLinkRepositoryModel)
            {
                return RepositoryRefTemplate;
            }

            return null;
        }
    }
}