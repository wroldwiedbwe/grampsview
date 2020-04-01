//-----------------------------------------------------------------------
//
// Common routines for the CommonRoutines
//
// <copyright file="CardGroupTemplateSelectorLarge.cs" company="PlaceholderCompany">
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
    public class CardGroupTemplateSelectorLarge : DataTemplateSelector
    {
        /// <summary>
        /// Gets or sets the address template.
        /// </summary>
        /// <value>
        /// The attribute template.
        /// </value>
        public DataTemplate AddressTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the attribute template.
        /// </summary>
        /// <value>
        /// The attribute template.
        /// </value>
        public DataTemplate AttributeTemplate
        {
            get;
            set;
        }

        ///// <summary>
        ///// Gets or sets the book mark template.
        ///// </summary>
        ///// <value>
        ///// The book mark template.
        ///// </value>
        //public DataTemplate BookMarkTemplate
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// Gets or sets the card list model template.
        /// </summary>
        /// <value>
        /// The card list model template.
        /// </value>
        public DataTemplate CardListLineCollectionTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the citation template.
        /// </summary>
        /// <value>
        /// The citation template.
        /// </value>
        public DataTemplate CitationTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the event template.
        /// </summary>
        /// <value>
        /// The event template.
        /// </value>
        public DataTemplate EventTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the family template.
        /// </summary>
        /// <value>
        /// The family template.
        /// </value>
        public DataTemplate FamilyTemplate
        {
            get;
            set;
        }

        public DataTemplate GenListCardTemplate
        {
            get;
            set;
        }

        public DataTemplate InstructTextTemplate
        {
            get;
            set;
        }

        public DataTemplate ListLineTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the media template.
        /// </summary>
        /// <value>
        /// The media template.
        /// </value>
        public DataTemplate MediaTemplate
        {
            get;
            set;
        }

        public DataTemplate NameMapTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the note template.
        /// </summary>
        /// <value>
        /// The note template.
        /// </value>
        public DataTemplate NoteTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the person template.
        /// </summary>
        /// <value>
        /// The person template.
        /// </value>
        public DataTemplate ParentLinkTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the person template.
        /// </summary>
        /// <value>
        /// The person template.
        /// </value>
        public DataTemplate PersonTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the place template.
        /// </summary>
        /// <value>
        /// The place template.
        /// </value>
        public DataTemplate PlaceTemplate
        {
            get;
            set;
        }

        public DataTemplate RepositoryTemplate
        {
            get;
            set;
        }

        public DataTemplate SourceTemplate
        {
            get;
            set;
        }

        public DataTemplate TagTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the URL template.
        /// </summary>
        /// <value>
        /// The attribute template.
        /// </value>
        public DataTemplate URLTemplate
        {
            get;
            set;
        }

        // public DataTemplate TagRefTemplate { get; set; }
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
            if (item is AddressModel)
            {
                return AddressTemplate;
            }

            if (item is AttributeModel)
            {
                return AttributeTemplate;
            }

            //if (item is BookMarkModel)
            //{
            //    return BookMarkTemplate;
            //}

            if (item is CardListLineCollection)
            {
                return CardListLineCollectionTemplate;
            }

            if (item is ICitationModel)
            {
                return CitationTemplate;
            }

            if (item is EventModel)
            {
                return EventTemplate;
            }

            if (item is FamilyModel)
            {
                return FamilyTemplate;
            }

            if (item is CardListLineCollection)
            {
                return ListLineTemplate;
            }

            // if (item is HLinkNoteModel) { return NoteRefTemplate; }

            // if (item is HLinkTagModel) { return TagRefTemplate; }

            // if (item is ListDetailCardModel) { return this.GenListCardTemplate; }
            if (item is MediaModel)
            {
                return MediaTemplate;
            }

            if (item is NameMapModel)
            {
                return NameMapTemplate;
            }

            if (item is NoteModel)
            {
                return NoteTemplate;
            }

            if (item is ParentLinkModel)
            {
                return ParentLinkTemplate;
            }

            if (item is PersonModel)
            {
                return PersonTemplate;
            }

            if (item is PlaceModel)
            {
                return PlaceTemplate;
            }

            if (item is RepositoryModel)
            {
                return RepositoryTemplate;
            }

            if (item is SourceModel)
            {
                return SourceTemplate;
            }

            if (item is TagModel)
            {
                return TagTemplate;
            }

            if (item is InstructCardModel)
            {
                return InstructTextTemplate;
            }

            if (item is URLModel)
            {
                return URLTemplate;
            }

            return null;
        }
    }
}