namespace GrampsView.Common
{
    using Xamarin.Essentials;
    using Xamarin.Forms;

    internal static class CardWidths
    {
        private static DisplayOrientation thisDeviceOrientation = DisplayOrientation.Portrait;

        public static double CardLargeWidth
        {
            get
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

                        switch (thisDeviceOrientation)
                        {
                            case DisplayOrientation.Portrait:
                                {
                                    outVal = DeviceDisplay.MainDisplayInfo.Width;
                                    break;
                                }
                            case DisplayOrientation.Landscape:
                                {
                                    outVal = 400;
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
                        outVal = 400;
                        break;
                };

                return outVal;
            }
        }

        public static double CardMediumWidth
        {
            get
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
                        switch (thisDeviceOrientation)
                        {
                            case DisplayOrientation.Portrait:
                                {
                                    outVal = DeviceDisplay.MainDisplayInfo.Width;
                                    break;
                                }
                            case DisplayOrientation.Landscape:
                                {
                                    outVal = 110;
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
                        outVal = 110;
                        break;
                };

                return outVal;
            }
        }

        public static double CardSmallWidth
        {
            get
            {
                double outVal;

                switch (Device.Idiom)
                {
                    case TargetIdiom.Unsupported:

                    case TargetIdiom.Desktop:
                    case TargetIdiom.Tablet:
                        outVal = 270;
                        break;

                    case TargetIdiom.Phone:
                        switch (thisDeviceOrientation)
                        {
                            case DisplayOrientation.Portrait:
                                {
                                    outVal = DeviceDisplay.MainDisplayInfo.Width;
                                    break;
                                }
                            case DisplayOrientation.Landscape:
                                {
                                    outVal = 200;
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
                        outVal = 200;
                        break;
                };

                return outVal;
            }
        }

        public static void OnMainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
        {
            switch (e.DisplayInfo.Orientation)
            {
                case DisplayOrientation.Portrait:
                    {
                        thisDeviceOrientation = DisplayOrientation.Portrait;
                        break;
                    }
                case DisplayOrientation.Landscape:
                    {
                        thisDeviceOrientation = DisplayOrientation.Landscape;
                        break;
                    }
                default:
                    {
                        thisDeviceOrientation = DisplayOrientation.Portrait;
                        break;
                    }
            }
        }
    }
}