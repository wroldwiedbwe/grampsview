namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.Repository;
    using GrampsView.Events;
    using GrampsView.Services;
    using GrampsView.Views;

    using Prism.Commands;
    using Prism.Events;
    using Prism.Navigation;
    using Prism.Services.Dialogs;

    using System;
    using System.Collections.Generic;

    using Xamarin.Forms;

    public class MainPageViewModel : ViewModelBase
    {
        private IDialogService _dialogService;
        private string CurrentPage = string.Empty;

        private IDatabaseReloadDisplayService localDatabaseReloadDisplayService = new DatabaseReloadDisplayService();

        private IFirstRunDisplayService localFirstRunDisplayService = new FirstRunDisplayService();

        private IWhatsNewDisplayService localWhatsNewDisplayService = new WhatsNewDisplayService();

        public MainPageViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService, FirstRunDisplayService iocFirstRunDisplayService, WhatsNewDisplayService iocWhatsNewDisplayService, DatabaseReloadDisplayService iocDatabaseReloadDisplayService, IDialogService dialogService)
                                                    : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
            localWhatsNewDisplayService = iocWhatsNewDisplayService;

            localFirstRunDisplayService = iocFirstRunDisplayService;

            localDatabaseReloadDisplayService = iocDatabaseReloadDisplayService;

            _dialogService = dialogService;

            BaseEventAggregator.GetEvent<PageNavigateEvent>().Subscribe(OnNavigateCommandExecuted, ThreadOption.UIThread);

            BaseEventAggregator.GetEvent<PageNavigateParmsEvent>().Subscribe(OnNavigateParmsCommandExecuted, ThreadOption.UIThread);

            BaseEventAggregator.GetEvent<DataLoadCompleteEvent>().Subscribe(LoadHubPage, ThreadOption.UIThread);

            BaseEventAggregator.GetEvent<GRAMPSDialogBoxEvent>().Subscribe(ActionDialog, ThreadOption.UIThread);

            BaseEventAggregator.GetEvent<AppStartWhatsNewEvent>().Subscribe(ServiceFirstRun, ThreadOption.UIThread);

            BaseEventAggregator.GetEvent<AppStartFirstRunEvent>().Subscribe(ServiceReloadDatabase, ThreadOption.UIThread);

            BaseEventAggregator.GetEvent<AppStartReloadDatabaseEvent>().Subscribe(ServiceLoadData, ThreadOption.UIThread);

            // Build the Menu
            NavigateCommand = new DelegateCommand<string>(OnNavigateCommandExecuted);

            MainMenuItems = new List<MainMenuItem>()

            {
                new MainMenuItem() { Title = "Hub", Icon = CommonConstants.IconHub, TargetType = nameof(HubPage) },

                new MainMenuItem() { Title = "Bookmarks", Icon = CommonConstants.IconBookMark, TargetType = nameof(BookMarkListPage) },

                new MainMenuItem() { Title = "Citations", Icon = CommonConstants.IconCitation, TargetType = nameof(CitationListPage) },

                new MainMenuItem() { Title = "Events", Icon = CommonConstants.IconEvents, TargetType = nameof(EventListPage) },

                new MainMenuItem() { Title = "Families", Icon = CommonConstants.IconFamilies, TargetType = nameof(FamilyListPage) },

                new MainMenuItem() { Title = "Media", Icon = CommonConstants.IconMedia, TargetType = nameof(MediaListPage) },

                new MainMenuItem() { Title = "Notes", Icon = CommonConstants.IconNotes, TargetType = nameof(NoteListPage) },

                new MainMenuItem() { Title = "People", Icon = CommonConstants.IconPeople, TargetType = nameof(PersonListPage) },

                new MainMenuItem() { Title = "Places", Icon = CommonConstants.IconPlace, TargetType = nameof(PlaceListPage) },

                new MainMenuItem() { Title = "Repositories", Icon = CommonConstants.IconRepository, TargetType = nameof(RepositoryListPage) },

                new MainMenuItem() { Title = "Sources", Icon = CommonConstants.IconSource, TargetType = nameof(SourceListPage) },

                new MainMenuItem() { Title = "Tags", Icon = CommonConstants.IconTag, TargetType = nameof(TagListPage) },

                            //<!--
                            //    new NavigationViewItemSeparator
                            //    {
                            //    },
                            //-->

                new MainMenuItem() { Title = "PeopleGraph", Icon = CommonConstants.IconPeopleGraph, TargetType = nameof(PeopleGraphPage) },

                            //<!--
                            //    new NavigationViewItemSeparator
                            //    {
                            //    },
                            //-->

                new MainMenuItem() { Title = "Search", Icon = CommonConstants.IconSearch, TargetType = nameof(SearchPage) },

                new MainMenuItem() { Title = "Choose data file", Icon = CommonConstants.IconChooseDataFile, TargetType = nameof(FileInputHandlerPage) },

                new MainMenuItem() { Title = "About", Icon = CommonConstants.IconSettings, TargetType = nameof(AboutPage) },
            };
        }

        public List<MainMenuItem> MainMenuItems { get; }

        public DelegateCommand<string> NavigateCommand { get; }

        public void ActionDialog(ActionDialogArgs argADA)
        {
            if (argADA is null)
            {
                throw new ArgumentNullException(nameof(argADA));
            }

            DialogParameters t = new DialogParameters
            {
                { "adaArgs", argADA }
            };

            //using the dialog service as-is
            _dialogService.ShowDialog("ErrorDialog", t);
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }

        /// <summary>
        /// Called when the implementer has been navigated to.
        /// </summary>
        /// <param name="parameters">
        /// The navigation parameters.
        /// </param>
        /// <returns>
        /// Null Task.
        /// </returns>
        public override void PopulateViewModel()
        {
            if (!localWhatsNewDisplayService.ShowIfAppropriate(BaseEventAggregator))
            {
                BaseEventAggregator.GetEvent<AppStartWhatsNewEvent>().Publish();
            }
        }

        public void ServiceFirstRun()
        {
            if (!localFirstRunDisplayService.ShowIfAppropriate(BaseEventAggregator))
            {
                BaseEventAggregator.GetEvent<AppStartFirstRunEvent>().Publish();
            }
        }

        public void ServiceLoadData()
        {
            if (CommonLocalSettings.DataSerialised)
            {
                BaseEventAggregator.GetEvent<AppStartEvent>().Publish(false);

                // Start data load
                BaseEventAggregator.GetEvent<PageNavigateEvent>().Publish(nameof(MessageLogPage));
                return;
            }

            // No Serialised Data and made it this far so some problem has occurred. Load everything
            // from the beginning.
            BaseEventAggregator.GetEvent<PageNavigateEvent>().Publish(nameof(FileInputHandlerPage));
        }

        public void ServiceReloadDatabase()
        {
            if (!localDatabaseReloadDisplayService.ShowIfAppropriate(BaseEventAggregator))
            {
                BaseEventAggregator.GetEvent<AppStartReloadDatabaseEvent>().Publish();
            }
        }

        private void LoadHubPage(object obj)
        {
            OnNavigateCommandExecuted(nameof(HubPage));
        }

        private async void OnNavigateCommandExecuted(string page)
        {
            // await CommonRoutines.NavigateMainPage(BaseNavigationService, path);

            if (page != CurrentPage)
            {
                CurrentPage = page;

                var result = await BaseNavigationService.NavigateAsync(nameof(NavigationPage) + "/" + page.Trim()).ConfigureAwait(false);

                if (!result.Success)
                {
                    DataStore.CN.NotifyException("OnNavigateCommandExecuted", result.Exception);
                }
            }

            //await BaseNavigationService.NavigateAsync("MainPage/NavigationPage/" + page.Trim());
        }

        private async void OnNavigateParmsCommandExecuted(INavigationParameters obj)
        {
            obj.TryGetValue("Target", out string target);

            if (target != CurrentPage)
            {
                CurrentPage = target;

                string t = nameof(NavigationPage) + "/" + target.Trim();

                var result = await BaseNavigationService.NavigateAsync(t, obj).ConfigureAwait(false);

                if (!result.Success)
                {
                    DataStore.CN.NotifyException("OnNavigateCommandExecuted", result.Exception);
                }
            }
        }
    }
}