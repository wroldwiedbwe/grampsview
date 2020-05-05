//-----------------------------------------------------------------------
//
// Common routines for the CommonTheming
//
// <copyright file="CommonTheming.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Common
{
    using GrampsView.Assets.Styles;

    using Xamarin.Essentials;

    /// <summary>
    /// Various common routines.
    /// </summary>
    public static class CommonTheming
    {
        public static void SetAppTheme()
        {
            // Set theme
            switch (CommonLocalSettings.ApplicationTheme)
            {
                case AppTheme.Unspecified:
                    {
                        // Honor the system request
                        switch (AppInfo.RequestedTheme)
                        {
                            case AppTheme.Dark:
                                {
                                    //ThemeDarkSet();
                                    break;
                                }
                            case AppTheme.Light:
                                {
                                    //ThemeLightSet();
                                    break;
                                }
                            default:
                                {
                                    //ThemeSystemSet();
                                    break;
                                }
                        }
                        break;
                    }

                case AppTheme.Light:
                    {
                        //ThemeLightSet();

                        break;
                    }
                case AppTheme.Dark:
                    {
                        //ThemeDarkSet();

                        break;
                    }
                default:
                    {
                        //ThemeSystemSet();

                        break;
                    }
            }
        }

        public static void ThemeDarkSave()
        {
            CommonLocalSettings.ApplicationTheme = AppTheme.Dark;
        }

        //public static void ThemeDarkSet()
        //{
        //    Prism.PrismApplicationBase.Current.Resources.Add(new DarkThemeDictionary());
        //}

        public static void ThemeLightSave()
        {
            CommonLocalSettings.ApplicationTheme = AppTheme.Light;
        }

        //public static void ThemeLightSet()
        //{
        //    Prism.PrismApplicationBase.Current.Resources.Add(new LightThemeDictionary());
        //}

        public static void ThemeSystemSave()
        {
            CommonLocalSettings.ApplicationTheme = AppTheme.Unspecified;
        }

        //public static void ThemeSystemSet()
        //{
        //    Prism.PrismApplicationBase.Current.Resources.Add(new SystemThemeDictionary());
        //}
    }
}