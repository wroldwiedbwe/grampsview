namespace GrampsView.Common
{
    using GrampsView.Data.Repository;
    using System.ComponentModel;
    using System.Diagnostics;
    using Xamarin.Essentials;
    using Xamarin.Forms;

    public class CardWidths : INotifyPropertyChanged
    {
        private const double CardLargeWidthDefault = 400;

        private const double CardMediumWidthDefault = 300;

        private const double CardSmallWidthDefault = 270;

        private static double _CardLargeWidth = CardLargeWidthDefault;

        private static double _CardMediumWidth = CardMediumWidthDefault;

        private static double _CardSmallWidth = CardSmallWidthDefault;

        // Singleton
        private static CardWidths _current;

        public event PropertyChangedEventHandler PropertyChanged;

        public static CardWidths Current => _current ?? (_current = new CardWidths());

        public double CardLargeWidth
        {
            get
            {
                return _CardLargeWidth;
            }

            set
            {
                double outVal;

                switch (Device.Idiom)
                {
                    case TargetIdiom.Unsupported:

                    case TargetIdiom.Desktop:
                        outVal = 540;
                        break;

                    case TargetIdiom.Tablet:
                        outVal = 540;
                        break;

                    case TargetIdiom.Phone:

                        switch (DataStore.AD.CurrentOrientation)
                        {
                            case DisplayOrientation.Portrait:
                                {
                                    outVal = DeviceDisplay.MainDisplayInfo.Width;
                                    break;
                                }
                            case DisplayOrientation.Landscape:
                                {
                                    outVal = CardLargeWidthDefault;
                                    break;
                                }
                            default:
                                {
                                    outVal = DeviceDisplay.MainDisplayInfo.Width;
                                    break;
                                }
                        }

                        break;

                    default:
                        outVal = CardLargeWidthDefault;
                        break;
                };

                _CardSmallWidth = outVal;

                OnPropertyChanged("CardLargeWidth");
            }
        }

        public double CardMediumWidth
        {
            get
            {
                return _CardMediumWidth;
            }

            set
            {
                double outVal;

                switch (Device.Idiom)
                {
                    case TargetIdiom.Unsupported:

                    case TargetIdiom.Desktop:
                        outVal = 360;
                        break;

                    case TargetIdiom.Tablet:
                        outVal = 360;
                        break;

                    case TargetIdiom.Phone:
                        switch (DataStore.AD.CurrentOrientation)
                        {
                            case DisplayOrientation.Portrait:
                                {
                                    outVal = DeviceDisplay.MainDisplayInfo.Width;
                                    break;
                                }
                            case DisplayOrientation.Landscape:
                                {
                                    outVal = CardMediumWidthDefault;
                                    break;
                                }
                            default:
                                {
                                    outVal = DeviceDisplay.MainDisplayInfo.Width;
                                    break;
                                }
                        }

                        break;

                    default:
                        outVal = CardMediumWidthDefault;
                        break;
                };

                _CardMediumWidth = outVal;

                OnPropertyChanged("CardMediumWidth");
            }
        }

        public double CardSmallWidth
        {
            get
            {
                return _CardSmallWidth;
            }

            set
            {
                double outVal;

                switch (Device.Idiom)
                {
                    case TargetIdiom.Unsupported:

                    case TargetIdiom.Desktop:
                    case TargetIdiom.Tablet:
                        outVal = CardSmallWidthDefault;
                        break;

                    case TargetIdiom.Phone:
                        switch (DataStore.AD.CurrentOrientation)
                        {
                            case DisplayOrientation.Portrait:
                                {
                                    outVal = DeviceDisplay.MainDisplayInfo.Width;
                                    break;
                                }
                            case DisplayOrientation.Landscape:
                                {
                                    outVal = CardSmallWidthDefault;
                                    break;
                                }
                            default:
                                {
                                    outVal = CardSmallWidthDefault;
                                    break;
                                }
                        }
                        break;

                    default:
                        outVal = CardSmallWidthDefault;
                        break;
                };

                Debug.WriteLine("Card Width changed to " + outVal.ToString());
                _CardSmallWidth = outVal;

                OnPropertyChanged("CardSmallWidth");
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}