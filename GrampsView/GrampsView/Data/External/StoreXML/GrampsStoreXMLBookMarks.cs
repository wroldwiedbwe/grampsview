// <copyright file="GrampsStoreXMLBookMarks.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Data.ExternalStorageNS
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    using GrampsView.Data.DataView;

    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    /// <summary>
    /// Loads BookMark XML.
    /// </summary>
    /// <seealso cref="GrampsView.Common.CommonBindableBase" />
    /// /// /// /// ///
    /// <seealso cref="GrampsView.Data.ExternalStorageNS.IGrampsStoreXML" />
    ///
    public partial class GrampsStoreXML : IGrampsStoreXML
    {
        /// <summary>
        /// Loads the BookMark data asynchronous.
        /// </summary>
        /// <returns>
        /// True if loaded ok.
        /// </returns>
        public async Task LoadBookMarksAsync()
        {
            await DataStore.CN.MajorStatusAdd("Loading BookMark data").ConfigureAwait(false);
            {
                try
                {
                    // Run query
                    var de =
                        from el in localGrampsXMLdoc.Descendants(ns + "bookmark")
                        select el;

                    // set BookMark count field
                    int bookMarkCount = 0;

                    // Loop through results to get the Citation Uri _baseUri = new Uri("ms-appx:///");
                    foreach (XElement argBookMark in de)
                    {
                        BookMarkModel loadBookMark = DV.BookMarkDV.NewModel();

                        // BookMark Handle
                        bookMarkCount++;
                        loadBookMark.Handle = "BookMark" + Convert.ToString(bookMarkCount).Trim();

                        // BookMark fields
                        loadBookMark.GTarget = GetAttribute(argBookMark.Attribute("target"));
                        loadBookMark.BookMarkHLink = GetAttribute(argBookMark.Attribute("hlink"));

                        // save the event
                        DV.BookMarkDV.BookMarkData.Add(loadBookMark);
                    }

                    // sort the collection eventRepository.Items.Sort(EventModel => EventModel);

                    // let everybody know
                }
                catch (Exception e)
                {
                    // TODO handle this
                    await DataStore.CN.MajorStatusAdd(e.Message).ConfigureAwait(false);

                    throw;
                }
            }

            await DataStore.CN.MajorStatusDelete().ConfigureAwait(false);
            return;
        }
    }
}