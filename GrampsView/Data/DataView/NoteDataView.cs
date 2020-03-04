//-----------------------------------------------------------------------
//
// Various data modesl to small to be worth putting in their own file
// is first launched.
//
// <copyright file="NoteDataView.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.DataView
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repositories;
    using GrampsView.Data.Repository;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    // Event repository </summary>
    public class NoteDataView : DataViewBase<NoteModel, HLinkNoteModel, HLinkNoteModelCollection>, INoteDataView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoteDataView"/> class.
        /// </summary>
        public NoteDataView()
        {
        }

        public override IReadOnlyList<NoteModel> DataDefaultSort
        {
            get
            {
                return DataViewData.Items.OrderBy(NoteModel => NoteModel.GText).ToList();
            }
        }

        /// <summary>
        /// Gets the data view data.
        /// </summary>
        /// <value>
        /// The data view data.
        /// </value>
        public override RepositoryModelType<NoteModel, HLinkNoteModel> DataViewData
        {
            get
            {
                return NoteData;
            }
        }

        /// <summary>
        /// Gets or sets the note data.
        /// </summary>
        /// <value>
        /// The note data.
        /// </value>
        [DataMember]
        public RepositoryModelType<NoteModel, HLinkNoteModel> NoteData
        {
            get
            {
                return DataStore.DS.NoteData;
            }
        }

        /// <summary>
        /// Gets all as hlink.
        /// </summary>
        /// <returns>
        /// </returns>
        public HLinkNoteModelCollection GetAllAsHLink()
        {
            HLinkNoteModelCollection t = new HLinkNoteModelCollection();

            foreach (var item in DataDefaultSort)
            {
                t.Add(item.HLink);
            }

            return t;
        }

        /// <summary>
        /// Gets all t of aype.
        /// </summary>
        /// <param name="argType">
        /// Type of the argument.
        /// </param>
        /// <returns>
        /// </returns>
        public ObservableCollection<NoteModel> GetAllOfType(string argType)
        {
            IEnumerable<NoteModel> q = NoteData.Items.Where(NoteModel => NoteModel.GType == argType);

            return new ObservableCollection<NoteModel>(q);
        }

        public override CardGroup GetLatestChanges()
        {
            DateTime lastSixtyDays = DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0, 0));

            IEnumerable tt = DataViewData.Items.OrderByDescending(GetLatestChangest => GetLatestChangest.Change).Where(GetLatestChangestt => GetLatestChangestt.Change > lastSixtyDays).Take(3);

            CardGroup returnCardGroup = new CardGroup();

            foreach (NoteModel item in tt)
            {
                returnCardGroup.Cards.Add(item.HLink);
            }

            returnCardGroup.Title = "Latest Note Changes";

            return returnCardGroup;
        }

        /// <summary>
        /// hes the link collection sort.
        /// </summary>
        /// <param name="collectionArg">
        /// The collection argument.
        /// </param>
        /// <returns>
        /// Sorted hlink collection.
        /// </returns>
        public override HLinkNoteModelCollection HLinkCollectionSort(HLinkNoteModelCollection collectionArg)
        {
            if (collectionArg == null)
            {
                return null;
            }

            IOrderedEnumerable<HLinkNoteModel> t = collectionArg.OrderBy(HLinkNoteModel => HLinkNoteModel.DeRef.TextShort);

            HLinkNoteModelCollection tt = new HLinkNoteModelCollection();

            foreach (HLinkNoteModel item in t)
            {
                tt.Add(item);
            }

            return tt;
        }

        public override List<SearchItem> Search(string queryString)
        {
            List<SearchItem> itemsFound = new List<SearchItem>();

            var temp = NoteData.Items.Where(x => x.GText.ToLower(CultureInfo.CurrentCulture).Contains(queryString));

            if (temp.Any())
            {
                foreach (NoteModel tempMO in temp)
                {
                    itemsFound.Add(new SearchItem
                    {
                        HLink = tempMO.HLink,
                        Text = tempMO.GetDefaultText,
                    });
                }
            }

            return itemsFound;
        }

        public List<SearchItem> SearchTag(string queryString)
        {
            List<SearchItem> itemsFound = new List<SearchItem>();

            var temp = from gig in NoteData.Items
                       where gig.GTagRefCollection.Any(act => act.DeRef.GName == queryString)
                       select gig;

            if (temp.Any())
            {
                foreach (NoteModel tempMO in temp)
                {
                    itemsFound.Add(new SearchItem
                    {
                        HLink = tempMO.HLink,
                        Text = tempMO.GetDefaultText,
                    });
                }
            }

            return itemsFound;
        }
    }
}