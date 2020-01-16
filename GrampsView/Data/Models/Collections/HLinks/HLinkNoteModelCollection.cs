// <copyright file="HLinkNoteModelCollection.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

/// <summary>
/// </summary>
namespace GrampsView.Data.Collections
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.Serialization;

    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    /// <summary>
    /// Collection of HLinks to Notes.
    /// </summary>
    /// <seealso cref="GrampsView.Data.ViewModel.HLinkBaseCollection{GrampsView.Data.ViewModel.HLinkNoteModel}" />
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<HLinkNoteModel>))]
    public class HLinkNoteModelCollection : HLinkBaseCollection<HLinkNoteModel>
    {
        public NoteModel GetBio
        {
            get
            {
                var temp = this.Where(x => x.DeRef.GType == "Person Note");

                if (temp.Any())
                {
                    HLinkNoteModel tt = temp.FirstOrDefault();
                    return tt.DeRef;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets the get summary.
        /// </summary>
        /// <value>
        /// The get summary.
        /// </value>
        public string GetSummary
        {
            get
            {
                if (Count == 0)
                {
                    return string.Empty;
                }
                else
                {
                    return this[0].DeRef.GText;
                }
            }
        }

        public CardGroup GetCardGroup()
        {
            return base.GetCardGroup("Note Collection");
        }

        /// <summary>Helper method to sort and set the firt image link.</summary>
        public void SortAndSetFirst()
        {
            // Return if null
            if (this == null)
            {
                return;
            }

            // Set the first image link. Assumes main image is manually set to the first image in
            // Gramps if we need it to be, e.g. Citations.

            if (Count > 0)
            {
                // For Note collections just grab the first one
                FirstHLink = this[0].DeRef.HomeImageHLink;

                // Sort the collection
                List<HLinkNoteModel> t = this.OrderBy(hlinkNoteModel => hlinkNoteModel.DeRef.GText).ToList();

                Items.Clear();

                foreach (HLinkNoteModel item in t)
                {
                    Items.Add(item);
                }
            }
        }
    }
}