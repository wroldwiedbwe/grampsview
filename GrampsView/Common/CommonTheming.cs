//-----------------------------------------------------------------------
//
// Common routines for the CommonRoutines
//
// <copyright file="CommonRoutines.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Common
{
    using GrampsView.Assets.Styles;
    using GrampsView.Data.Model;
    using System.Text.RegularExpressions;

    using Xamarin.Essentials;
    using Xamarin.Forms;

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
                                    SetThemeDark();
                                    break;
                                }
                            case AppTheme.Light:
                                {
                                    SetThemeLight();
                                    break;
                                }
                            default:
                                {
                                    SetThemeSystem();
                                    break;
                                }
                        }
                        break;
                    }

                case AppTheme.Light:
                    {
                        SetThemeLight();
                        break;
                    }
                case AppTheme.Dark:
                    {
                        SetThemeDark();
                        break;
                    }
                default:
                    {
                        SetThemeSystem();
                        break;
                    }
            }
        }

        public static void SetThemeDark()
        {
            Prism.PrismApplicationBase.Current.Resources.Add(new DarkTheme());

            CommonLocalSettings.ApplicationTheme = AppTheme.Dark;
        }

        public static void SetThemeLight()
        {
            Prism.PrismApplicationBase.Current.Resources.Add(new LightTheme());

            CommonLocalSettings.ApplicationTheme = AppTheme.Light;
        }

        public static void SetThemeSystem()
        {
            Prism.PrismApplicationBase.Current.Resources.Add(new SystemTheme());

            CommonLocalSettings.ApplicationTheme = AppTheme.Unspecified;
        }
    }
}