using Xamarin.Forms;

namespace GrampsView.Common
{
    internal static class CardWidths
    {
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
                        outVal = Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Width;
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
                        outVal = Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Width;
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
                        outVal = Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Width;
                        break;

                    default:
                        outVal = 200;
                        break;
                };

                return outVal;
            }
        }
    }
}