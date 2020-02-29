// <copyright file="SurnameModelCollection.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

/// <summary>
/// </summary>
namespace GrampsView.Data.Collections
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.Serialization;

    using GrampsView.Common;
    using GrampsView.Data.Model;

    /// <summary>
    /// Attribute model collection.
    /// </summary>
    /// <seealso cref="System.Collections.ObjectViewModel.ObservableCollection{GrampsView.Data.ViewModel.AttributeModel}"/>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<PersonNameModel>))]
    public class PersonNameModelCollection : ModelBaseCollection<PersonNameModel>
    {
        public PersonNameModel GetPrimaryName
        {
            get
            {
                // Should always have a name but just in case
                if (Items.Count == 0)
                {
                    return new PersonNameModel();
                }

                // Return the primary name if it exists
                if (Items.Count > 0)
                {
                    return Items[0];
                }

                return new PersonNameModel();
            }
        }

        public override CardGroup GetCardGroup()
        {
            CardGroup t = new CardGroup
            {
                Title = "Person Names",
            };

            foreach (PersonNameModel item in Items)
            {
                t.Cards.Add(item);

                if (item.GCitationRefCollection.Count > 0)
                {
                    foreach (HLinkCitationModel citem in item.GCitationRefCollection)
                    {
                        t.Cards.Add(citem);
                    }
                }

                if (item.GNoteReferenceCollection.Count > 0)
                {
                    foreach (HLinkNoteModel nitem in item.GNoteReferenceCollection)
                    {
                        t.Cards.Add(nitem);
                    }
                }
            }

            return t;
        }
    }
}