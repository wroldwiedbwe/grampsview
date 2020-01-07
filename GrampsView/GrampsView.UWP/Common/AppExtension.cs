// <copyright file="AppExtension.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
namespace GrampsView.UWP
{
    using System;
    using Microsoft.AppCenter;
    using Microsoft.AppCenter.Analytics;
    using Microsoft.AppCenter.Crashes;
    using Microsoft.AppCenter.Distribute;
    using Windows.UI.Xaml;

    /// <summary>
    /// Extends the Prism Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Hockeys the application initialize.
        /// </summary>
        private static void AppCenterInit()
        {
            // App Center init
            const string AndroidSecret = "712d14b3ffae4255a04b510c9aac9cb1";
            const string UWPSecret = "a768ead9-953b-4f35-8438-b510bd0e9ba3";
            const string IOSSecret = "e2d81aa4-8898-494c-8373-1a7dc349651e";

            AppCenter.Start("android=" + AndroidSecret + ";" +
                            "uwp=" + UWPSecret + ";" +
                            "ios=" + IOSSecret + ";",
                            typeof(Analytics), typeof(Crashes), typeof(Distribute));
        }
    }
}