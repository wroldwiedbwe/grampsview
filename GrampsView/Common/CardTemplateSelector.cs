// <copyright file="CardTemplateSelector.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Common
{
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;
    using GrampsView.UserControls;

    using System.Diagnostics.Contracts;

    using Xamarin.Forms;

    /// <summary>
    /// Object Collection Template Selector.
    /// </summary>
    public class CardTemplateSelector : DataTemplateSelector
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

        /// <summary>
        /// Gets or sets the Card Group template.
        /// </summary>
        /// <value>
        /// The Card Group template.
        /// </value>
        public DataTemplate CardGroupTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the List Line template.
        /// </summary>
        /// <value>
        /// The List Line template.
        /// </value>
        public DataTemplate CardListLineTemplate
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

        public DataTemplate HeaderTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the book mark template.
        /// </summary>
        /// <value>
        /// The book mark template.
        /// </value>
        public DataTemplate HLinkBookMarkTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the List Line template.
        /// </summary>
        /// <value>
        /// The List Line template.
        /// </value>
        public DataTemplate HLinkCardListLineTemplate
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
        public DataTemplate HLinkCitationTemplate
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
        public DataTemplate HLinkEventTemplate
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
        public DataTemplate HLinkFamilyTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the media reference template.
        /// </summary>
        /// <value>
        /// The media reference template.
        /// </value>
        public DataTemplate HLinkMediaTemplate { get; set; }

        /// <summary>
        /// Gets or sets the name map template.
        /// </summary>
        /// <value>
        /// The name map template.
        /// </value>
        public DataTemplate HLinkNameMapTemplate
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
        public DataTemplate HLinkNoteTemplate
        {
            get;
            set;
        }

        public DataTemplate HLinkParentLinkTemplate
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
        public DataTemplate HLinkPersonTemplate
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
        public DataTemplate HLinkPlaceTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the repository template.
        /// </summary>
        /// <value>
        /// The repository template.
        /// </value>
        public DataTemplate HLinkRepositoryTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the source template.
        /// </summary>
        /// <value>
        /// The source template.
        /// </value>
        public DataTemplate HLinkSourceTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the tag template.
        /// </summary>
        /// <value>
        /// The tag template.
        /// </value>
        public DataTemplate HLinkTagTemplate
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
        public DataTemplate HLinkURLTemplate
        {
            get;
            set;
        }

        public DataTemplate MediaCardLargeTemplate
        {
            get;
            set;
        }

        public DataTemplate MediaImageFullTemplate
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

        /// <summary>
        /// Gets or sets the name map template.
        /// </summary>
        /// <value>
        /// The name map template.
        /// </value>
        public DataTemplate NameMapTemplate
        {
            get;
            set;
        }

        public DataTemplate NoteCardFullTemplate
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

        ///// <summary>
        ///// Gets or sets the note template.
        ///// </summary>
        ///// <value>
        ///// The note template.
        ///// </value>
        // public DataTemplate NoteRefTemplate { get; set; }

        public DataTemplate ParentLinkTemplate
        {
            get;
            set;
        }

        public DataTemplate PersonNameTemplate
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

        /// <summary>
        /// Gets or sets the repository template.
        /// </summary>
        /// <value>
        /// The repository template.
        /// </value>
        public DataTemplate RepositoryTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the source template.
        /// </summary>
        /// <value>
        /// The source template.
        /// </value>
        public DataTemplate SourceTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the tag template.
        /// </summary>
        /// <value>
        /// The tag template.
        /// </value>
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

            if (item is CardGroup)
            {
                return CardGroupTemplate;
            }

            if (item is CardGroupBase<AddressModel>)
            {
                return CardGroupTemplate;
            }

            if (item is CardGroupBase<AttributeModel>)
            {
                return CardGroupTemplate;
            }

            if (item is CardGroupBase<LdsOrdModel>)
            {
                return CardGroupTemplate;
            }

            if (item is CardGroupBase<SrcAttributeModel>)
            {
                return CardGroupTemplate;
            }

            if (item is CardGroupBase<PersonNameModel>)
            {
                return CardGroupTemplate;
            }

            if (item is CardGroupBase<PersonRefModel>)
            {
                return CardGroupTemplate;
            }

            if (item is CardGroupBase<SurnameModel>)
            {
                return CardGroupTemplate;
            }

            if (item is CardGroupBase<URLModel>)
            {
                return CardGroupTemplate;
            }

            if (item is ICitationModel)
            {
                return CitationTemplate;
            }

            if (item is FamilyModel)
            {
                return FamilyTemplate;
            }

            if (item is CardListLineCollection)
            {
                return CardListLineTemplate;
            }

            if (item is HLinkCitationModel)
            {
                return CitationTemplate;
            }

            if (item is HLinkEventModel)
            {
                return EventTemplate;
            }

            if (item is HLinkFamilyModel)
            {
                return FamilyTemplate;
            }

            if (item is HLinkHeaderModel)
            {
                return HeaderTemplate;
            }

            if (item is HLinkMediaModel)
            {
                switch ((item as HLinkMediaModel).CardType)
                {
                    case DisplayFormat.MediaCardLarge:
                        return MediaCardLargeTemplate;

                    case DisplayFormat.MediaImageFullCard:
                        return MediaImageFullTemplate;

                    case DisplayFormat.Default:
                        return MediaTemplate;
                }
            }

            if (item is HLinkNameMapModel)
            {
                return NameMapTemplate;
            }

            if (item is HLinkNoteModel)
            {
                switch ((item as HLinkNoteModel).CardType)
                {
                    case DisplayFormat.Default:
                        return NoteTemplate;

                    case DisplayFormat.NoteCardFull:
                        return NoteCardFullTemplate;

                    default:
                        return NoteTemplate;
                }
            }

            if (item is HLinkPersonModel)
            {
                return PersonTemplate;
            }

            if (item is HLinkPlaceModel)
            {
                return PlaceTemplate;
            }

            if (item is HLinkRepositoryModel)
            {
                return RepositoryTemplate;
            }

            if (item is HLinkSourceModel)
            {
                return SourceTemplate;
            }

            if (item is HLinkTagModel)
            {
                return TagTemplate;
            }

            if (item is MediaModel)
            {
                return MediaTemplate;
            }

            if (item is HLinkHomeImageModel)
            {
                switch ((item as HLinkHomeImageModel).CardType)
                {
                    case DisplayFormat.MediaCardLarge:
                        return MediaCardLargeTemplate;

                    case DisplayFormat.MediaImageFullCard:
                        return MediaImageFullTemplate;

                    case DisplayFormat.Default:
                        return MediaTemplate;
                }
            }

            if (item is NameMapModel)
            {
                return NameMapTemplate;
            }

            if (item is NoteCardFull)
            {
                return NoteCardFullTemplate;
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

            if (item is PersonNameModel)
            {
                return PersonNameTemplate;
            }

            if (item is PlaceModel)
            {
                return PlaceTemplate;
            }

            if (item is RepositoryModel)
            {
                return RepositoryTemplate;
            }

            if (item is SourceCardSmall)
            {
                return SourceTemplate;
            }

            if (item is SourceModel)
            {
                return SourceTemplate;
            }

            if (item is TagModel)
            {
                return TagTemplate;
            }

            if (item is URLModel)
            {
                return URLTemplate;
            }

            // Error
            Contract.Assert(false, "Bad Data Template: " + item.GetType().ToString());
            return null;
        }
    }
}