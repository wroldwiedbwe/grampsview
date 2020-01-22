﻿// <copyright file="SearchViewModel.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

namespace GrampsView.ViewModels
{
    using System.Windows.Input;

    using GrampsView.Common;
    using GrampsView.Data.DataView;

    using Prism.Events;
    using Prism.Navigation;

    using Xamarin.Forms;

    /// <summary>
    /// Search ViewModel class.
    /// </summary>
    /// <seealso cref="GrampsView.ViewModels.ViewModelBase" />
    public class SearchViewModel : ViewModelBase
    {
        /// <summary>
        /// The search command backing store.
        /// </summary>
        private ICommand _searchCommand;

        /// <summary>
        /// The local search text.
        /// </summary>
        private string localSearchText;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchViewModel" /> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// Common Logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// Event Aggregator.
        /// </param>
        /// <param name="iocNavigationService">
        /// NavigationService
        /// </param>
        public SearchViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
            : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
            BaseTitle = "Search Page";

            SearchButtonCommand = new Command<string>(SearchProcessQuery);
        }

        /// <summary>
        /// Gets the search button command.
        /// </summary>
        /// <value>
        /// The search button command.
        /// </value>
        public ICommand SearchButtonCommand { get; private set; }

        /// <summary>
        /// Handles the search command.
        /// </summary>
        /// <value>
        /// The search command.
        /// </value>
        public ICommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new Command<string>((text) =>
                {
                    // TODO Fix search display as you go
                    //ProcessQuery(text, 10);
                }));
            }
        }

        /// <summary>
        /// Gets or sets the search text.
        /// </summary>
        /// <value>
        /// The search text.
        /// </value>
        public string SearchText
        {
            get
            {
                return localSearchText;
            }

            set
            {
                SetProperty(ref localSearchText, value);
            }
        }

        /// <summary>
        /// Processes the query.
        /// </summary>
        /// <param name="argSearch">
        /// Search Text.
        /// </param>
        /// <param name="argLimit">
        /// Search Limit for search terms found while typing.
        /// </param>
        public void ProcessQuery(string argSearch, int argLimit)
        {
            SearchText = argSearch;
            BaseDetail.Clear();

            CardGroup SearchCards;

            if (SearchText.Length > 0)
            {
                // Create Person Cards
                SearchCards = new CardGroup
                {
                    Title = "People"
                };

                foreach (SearchItem item in DV.PersonDV.Search(SearchText))
                {
                    if (SearchCards.Cards.Count < argLimit)
                    {
                        SearchCards.Cards.Add(item.HLink);
                    }
                }
                BaseDetail.Add(SearchCards);

                // Create Family Cards
                SearchCards = new CardGroup
                {
                    Title = "Families"
                };

                foreach (SearchItem item in DV.FamilyDV.Search(SearchText))
                {
                    if (SearchCards.Cards.Count < argLimit)
                    {
                        SearchCards.Cards.Add(item.HLink);
                    }
                }
                BaseDetail.Add(SearchCards);

                // Create Event Cards
                SearchCards = new CardGroup
                {
                    Title = "Events"
                };

                foreach (SearchItem item in DV.EventDV.Search(SearchText))
                {
                    if (SearchCards.Cards.Count < argLimit)
                    {
                        SearchCards.Cards.Add(item.HLink);
                    }
                }
                BaseDetail.Add(SearchCards);

                // Create Note cards
                SearchCards = new CardGroup
                {
                    Title = "Notes"
                };

                foreach (SearchItem item in DV.NoteDV.Search(SearchText))
                {
                    if (SearchCards.Cards.Count < argLimit)
                    {
                        SearchCards.Cards.Add(item.HLink);
                    }
                }
                BaseDetail.Add(SearchCards);

                // Create Citation cards
                SearchCards = new CardGroup
                {
                    Title = "Citations"
                };

                foreach (SearchItem item in DV.CitationDV.Search(SearchText))
                {
                    if (SearchCards.Cards.Count < argLimit)
                    {
                        SearchCards.Cards.Add(item.HLink);
                    }
                }
                BaseDetail.Add(SearchCards);

                // Create Media Cards
                SearchCards = new CardGroup
                {
                    Title = "Media"
                };

                foreach (SearchItem item in DV.MediaDV.Search(SearchText))
                {
                    if (SearchCards.Cards.Count < argLimit)
                    {
                        SearchCards.Cards.Add(item.HLink);
                    }
                }
                BaseDetail.Add(SearchCards);

                // Create Place Cards
                SearchCards = new CardGroup
                {
                    Title = "Places"
                };

                foreach (SearchItem item in DV.PlaceDV.Search(SearchText))
                {
                    if (SearchCards.Cards.Count < argLimit)
                    {
                        SearchCards.Cards.Add(item.HLink);
                    }
                }
                BaseDetail.Add(SearchCards);
            }
        }

        /// <summary>
        /// Processes the search query.
        /// </summary>
        /// <param name="argSearch">
        /// </param>
        public void SearchProcessQuery(string argSearch)
        {
            ProcessQuery(argSearch, int.MaxValue);
        }
    }
}