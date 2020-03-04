// <copyright file="MediaDetailViewModel.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

namespace GrampsView.ViewModels
{
    using System;
    using System.Collections.Generic;
    using FFImageLoading.Forms;
    using FFImageLoading.Transformations;
    using FFImageLoading.Work;

    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;
    using Prism.Commands;
    using Prism.Events;
    using Prism.Navigation;
    using Xamarin.Essentials;
    using Xamarin.Forms;

    /// <summary>
    /// Defines the EVent Detail Page View ViewModel.
    /// </summary>
    public class MediaDetailViewModel : ViewModelBase
    {
        private HLinkMediaModel _CurrentHLinkMedia = new HLinkMediaModel();

        /// <summary>
        /// The local media object.
        /// </summary>
        private MediaModel _MediaObject;

        private List<ITransformation> _Trans = new List<ITransformation>();

        /// <summary>
        /// My bit map image.
        /// </summary>
        private Image localBitMapImage = null;

        //private double mX = 0f;

        //private double mY = 0f;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaDetailViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The common logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The event aggregator.
        /// </param>
        /// <param name="iocNavigationService">
        /// The navigation service.
        /// </param>
        public MediaDetailViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
            : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
            BaseCL.LogProgress("MediaDetailViewModel created");

            OpenImageCommand = new DelegateCommand(OpenImage, CanOpenImage);
        }

        public HLinkMediaModel CurrentHLinkMedia
        {
            get
            {
                return _CurrentHLinkMedia;
            }

            set
            {
                SetProperty(ref _CurrentHLinkMedia, value);
            }
        }

        /// <summary>
        /// Gets or sets the current media object.
        /// </summary>
        /// <value>
        /// The current media object.
        /// </value>
        public MediaModel CurrentMediaObject
        {
            get
            {
                return _MediaObject;
            }

            set
            {
                SetProperty(ref _MediaObject, value);
            }
        }

        public double CurrentXOffset { get; set; }

        public double CurrentYOffset { get; set; }

        public double CurrentZoomFactor { get; set; }

        /// <summary>
        /// Gets or sets the media image bitmap.
        /// </summary>
        /// <value>
        /// The media image bitmap.
        /// </value>
        public Image ImageFullBitmap
        {
            get
            {
                return localBitMapImage;
            }

            set
            {
                SetProperty(ref localBitMapImage, value);
            }
        }

        public DelegateCommand OpenImageCommand { get; private set; }

        public List<ITransformation> Trans
        {
            get
            {
                return _Trans;
            }

            set
            {
                SetProperty(ref _Trans, value);
            }
        }

        /// <summary>
        /// Gets or sets the h link parameter.
        /// </summary>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        /// <value>
        /// The h link parameter.
        /// </value>
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);

            // Clear large Bitmap Image
            if (CurrentMediaObject != null)
            {
                CurrentMediaObject.FullImageClean();
            }

            ImageFullBitmap = null;
        }

        /// <summary>
        /// Handles navigation in wards and sets up the event model parameter.
        /// </summary>
        /// <returns>
        /// </returns>
        public override void PopulateViewModel()
        {
            BaseCL.LogRoutineEntry("MediaDetailViewModel OnNavigatedTo");

            CurrentHLinkMedia = BaseNavParamsHLink as HLinkMediaModel;

            if (!(CurrentHLinkMedia is null))
            {
                CurrentMediaObject = DV.MediaDV.GetModelFromHLink(CurrentHLinkMedia);

                if (!(CurrentMediaObject is null))
                {
                    BaseTitle = CurrentMediaObject.GetDefaultText;
                    BaseTitleIcon = CommonConstants.IconMedia;

                    // Get basic details
                    CardGroup t = new CardGroup { Title = "Header Details" };

                    t.Cards.Add(new CardListLineCollection
                    {
                        new CardListLine("Card Type:", "Media Detail"),
                        new CardListLine("Date:", CurrentMediaObject.GDateValue.GetLongDateAsString),
                        new CardListLine("File Description:", CurrentMediaObject.GDescription),
                        new CardListLine("File Mime Type:", CurrentMediaObject.FileMimeType),
                        new CardListLine("File Content Type:", CurrentMediaObject.FileContentType),
                        new CardListLine("File Mime SubType:", CurrentMediaObject.FileMimeSubType),
                        new CardListLine("OriginalFilePath:", CurrentMediaObject.OriginalFilePath),
                    });

                    // Set up note re opening in photo app
                    CardListLineCollection t1 = new CardListLineCollection
                {
                    new CardListLine(string.Empty, "Note: Double click the image to open it.")
                };

                    t.Cards.Add(t1);

                    // Add standard details
                    t.Cards.Add(DV.MediaDV.GetModelInfoFormatted(CurrentMediaObject));

                    BaseHeader.Add(t);

                    // Add HLink Details
                    BaseDetail.Add(CurrentHLinkMedia.GAttributeRefCollection.GetCardGroup("Hlink Attributes"));
                    BaseDetail.Add(CurrentHLinkMedia.GCitationRefCollection.GetCardGroup("HLink Citations"));
                    BaseDetail.Add(CurrentHLinkMedia.GNoteRefCollection.GetCardGroup("HLink Notes"));

                    // Add Model details
                    BaseDetail.Add(CurrentMediaObject.GCitationRefCollection.GetCardGroup());
                    BaseDetail.Add(CurrentMediaObject.GNoteRefCollection.GetCardGroup());
                    BaseDetail.Add(CurrentMediaObject.GTagRefCollection.GetCardGroup());

                    BaseBackLinks.Add(CurrentMediaObject.BackHLinkReferenceCollection.GetCardGroup());
                }

                BaseCL.LogRoutineExit("MediaDetailViewModel OnNavigatedTo");
            }
        }

        public void ReloadImage(CachedImage argmdp)
        {
            if (argmdp is null)
            {
                throw new ArgumentNullException(nameof(argmdp));
            }

            Trans = new List<ITransformation>() {
                new CropTransformation(CurrentZoomFactor, CurrentXOffset, CurrentYOffset, 1f, 1f)
            };

            try
            {
                argmdp.ReloadImage();

                argmdp.LoadingPlaceholder = null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool CanOpenImage()
        {
            return true;
        }

        private void OpenImage()
        {
            OpenFileRequest t = new OpenFileRequest(CurrentMediaObject.GDescription, new ReadOnlyFile(CurrentMediaObject.MediaStorageFilePath));

            Xamarin.Essentials.Launcher.OpenAsync(t);

            // TODO implement logic
        }
    }
}