using GrampsView.Common;
using GrampsView.Data.Model;

using Nito.AsyncEx.Synchronous;

using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;

using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GrampsView.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible, IInitialize
    {
        private bool _BaseIsLoading;

        private HLinkBase _BaseNavParamsHLink = null;

        private string _BaseTitle = string.Empty;

        private string _BaseTitleIcon = string.Empty;

        /// <summary>
        /// The local cl.
        /// </summary>
        private ICommonLogging _CL;

        /// <summary>
        /// The local event aggregator.
        /// </summary>
        private IEventAggregator _EventAggregator;

        private INavigationService _NavigationService;

        /// <summary>
        /// The local nav parameters.
        /// </summary>
        private INavigationParameters _NavParams;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseViewModel"/> class.
        /// </summary>
        public ViewModelBase()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseViewModel"/> class. Initializes the
        /// specified ioc common logging.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The ioc common logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        public ViewModelBase(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
        {
            BaseCL = iocCommonLogging;
            BaseEventAggregator = iocEventAggregator;
            BaseNavigationService = iocNavigationService;
        }

        /// <summary>
        /// Gets or sets the base common logger.
        /// </summary>
        /// <value>
        /// The base cl.
        /// </value>
        public ICommonLogging BaseCL
        {
            get
            {
                Debug.Assert(_CL != null, "BaseCL is null.  Was this set in the constructor for the derived class?");

                return _CL;
            }

            private set
            {
                SetProperty(ref _CL, value);
            }
        }

        /// <summary>
        /// Gets or sets the model grid view.
        /// </summary>
        /// <value>
        /// The model grid view.
        /// </value>
        public CardGroup BaseDetail
        {
            get;

            //set
            //{
            //    SetProperty(ref localBaseDetail, value);
            //}
        }

        = new CardGroup();

        /// <summary>
        /// Gets or sets the base event aggregator.
        /// </summary>
        /// <value>
        /// The base event aggregator.
        /// </value>
        public IEventAggregator BaseEventAggregator
        {
            get
            {
                Debug.Assert(_EventAggregator != null, "BaseEventAggregator is null.  Was this set in the constructor for the derived class?");

                return _EventAggregator;
            }

            private set
            {
                SetProperty(ref _EventAggregator, value);
            }
        }

        public bool BaseIsLoading
        {
            get
            {
                return this._BaseIsLoading;
            }

            set
            {
                this._BaseIsLoading = value;
                RaisePropertyChanged(nameof(BaseIsLoading));
            }
        }

        public INavigationService BaseNavigationService
        {
            get
            {
                return _NavigationService;
            }
            set
            {
                SetProperty(ref _NavigationService, value);
            }
        }

        /// <summary>
        /// Gets or sets the base nav parameters.
        /// </summary>
        /// <value>
        /// The base nav parameters.
        /// </value>
        public INavigationParameters BaseNavParams
        {
            get
            {
                return _NavParams;
            }

            set
            {
                SetProperty(ref _NavParams, value);
            }
        }

        public HLinkBase BaseNavParamsHLink
        {
            get
            {
                Debug.Assert(_BaseNavParamsHLink != null, "BaseNavParamsHLink is null.");

                return _BaseNavParamsHLink;
            }

            set
            {
                SetProperty(ref _BaseNavParamsHLink, value);
            }
        }

        public string BaseTitle
        {
            get
            {
                return _BaseTitle;
            }
            set
            {
                value = CommonRoutines.ReplaceLineSeperators(value);

                SetProperty(ref _BaseTitle, value);

                //var t = this;
            }
        }

        public string BaseTitleIcon
        {
            get
            {
                return _BaseTitleIcon;
            }

            set
            {
                SetProperty(ref _BaseTitleIcon, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [detail data loaded flag].
        /// </summary>
        /// <value>
        /// <c>true</c> if [detail data loaded flag]; otherwise, <c>false</c>.
        /// </value>
        private bool DetailDataLoadedFlag { get; set; } = false;

        public HLinkBase BaseNavParamsHLinkDefault(HLinkBase argDefault)
        {
            if (_BaseNavParamsHLink is null)
            {
                return argDefault;
            }

            return BaseNavParamsHLink;
        }

        public virtual void Destroy()
        {
        }

        /// <summary>
        /// Initializes the specified parameters.
        /// </summary>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        public void Initialize(INavigationParameters parameters)
        {
            // TODO See https://github.com/PrismLibrary/Prism/issues/1748

            BaseNavParams = parameters;

            parameters.TryGetValue("hlink", out _BaseNavParamsHLink);
        }

        /// <summary>
        /// Gets or sets the h link parameter.
        /// </summary>
        /// <value>
        /// The h link parameter.
        /// </value>
        /// <summary>
        /// Called when [navigating from].
        /// </summary>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
            //TryFromJson(parameters, out localNavParams);

            //BaseCL.LogRoutineExit("Navigated from " + BaseNavParams.TargetView);
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
            if (!DetailDataLoadedFlag)
            {
                BaseIsLoading = true;

                DetailDataLoadedFlag = true;

                //PopulateViewModel().SafeFireAndForget(onException: ex => DataStore.CN.NotifyException("Trouble calling PopulateViewModel for " + BaseNavParams.TargetView, ex));

                PopulateViewModel();

                //var task = PopulateViewModelAsync();
                //task.WaitAndUnwrapException();

                PopulateViewModelAsync();

                BaseIsLoading = false;
            }
        }

        public virtual void OnNavigatingTo(INavigationParameters parameters)
        {
        }

        /// <summary>
        /// Populates the view ViewModel.
        /// </summary>
        /// <returns>
        /// Nothibg.
        /// </returns>
        public virtual void PopulateViewModel()
        {
            return;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

        public virtual async Task<bool> PopulateViewModelAsync()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return true;
        }
    }
}