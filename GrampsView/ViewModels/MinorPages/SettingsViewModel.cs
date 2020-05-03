﻿//-----------------------------------------------------------------------
//
// View model for the fly-out page view
//
// <copyright file="AboutViewModel.cs" company="MeMyselfAndI">
// Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.Model;
    using GrampsView.UserControls;

    using Prism.Commands;
    using Prism.Events;
    using Prism.Navigation;

    using System;
    using System.Threading.Tasks;

    using Xamarin.Essentials;

    public class SettingsViewModel : ViewModelBase
    {
        private CardListLineCollection _ApplicationVersionList = new CardListLineCollection();

        //private RadioItems _daRadioItems = new RadioItems();

        private string _DaText = string.Empty;

        private CardGroup _HeaderDetailList = new CardGroup();

        private HeaderModel _HeaderModel = null;

        private bool _ThemeButtonDarkChecked = false;

        private bool _ThemeButtonLightChecked = false;

        private bool _ThemeButtonSystemChecked = false;

        public SettingsViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
                                                                    : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
            BaseTitle = "About";
            BaseTitleIcon = CommonConstants.IconSettings;

            ////Setup Theme radio buttons
            //RadioButtonToggledCommand = new DelegateCommand<RadioItemToggledEventArgs>(HandleRadioToggle);

            //DaRadioItems.Add(new RadioItem
            //{
            //    Text = "Light Theme",
            //    Toggled = false
            //});

            //DaRadioItems.Add(new RadioItem
            //{
            //    Text = "Dark Theme",
            //    Toggled = false
            //});

            //DaRadioItems.Add(new RadioItem
            //{
            //    Text = "System Theme",
            //    Toggled = false
            //});

            switch (CommonLocalSettings.ApplicationTheme)
            {
                case AppTheme.Light:
                    {
                        ThemeButtonLightChecked = true;
                        break;
                    }

                case AppTheme.Dark:
                    {
                        ThemeButtonDarkChecked = true;
                        break;
                    }

                default:
                    {
                        ThemeButtonSystemChecked = true;
                        break;
                    }
            }
        }

        public string DaText
        {
            get
            {
                return _DaText;
            }

            set
            {
                SetProperty(ref _DaText, value);
            }
        }

        public bool ThemeButtonDarkChecked
        {
            get
            {
                return _ThemeButtonDarkChecked;
            }

            set
            {
                if (_ThemeButtonDarkChecked != value)
                {
                    _ThemeButtonLightChecked = false;
                    _ThemeButtonDarkChecked = true;
                    _ThemeButtonSystemChecked = false;

                    CommonTheming.ThemeDarkSet();
                    CommonTheming.ThemeDarkSave();
                }
            }
        }

        public bool ThemeButtonLightChecked
        {
            get
            {
                return _ThemeButtonLightChecked;
            }

            set
            {
                if (_ThemeButtonLightChecked != value)
                {
                    _ThemeButtonLightChecked = true;
                    _ThemeButtonDarkChecked = false;
                    _ThemeButtonSystemChecked = false;

                    CommonTheming.ThemeLightSet();
                    CommonTheming.ThemeLightSave();
                }
            }
        }

        public bool ThemeButtonSystemChecked
        {
            get
            {
                return _ThemeButtonSystemChecked;
            }

            set
            {
                if (_ThemeButtonSystemChecked != value)
                {
                    _ThemeButtonLightChecked = false;
                    _ThemeButtonDarkChecked = false;
                    _ThemeButtonSystemChecked = true;

                    CommonTheming.ThemeSystemSet();
                    CommonTheming.ThemeSystemSave();
                }
            }
        }

        //public RadioItems DaRadioItems
        //{
        //    get
        //    {
        //        return _daRadioItems;
        //    }

        //    //set
        //    //{
        //    //    SetProperty(ref _daRadioItems, value);
        //    //}
        //}
        //public DelegateCommand<RadioItemToggledEventArgs> RadioButtonToggledCommand { get; private set; }

        //public static void HandleRadioToggle()
        //{
        //    switch (argRadioItem.SelectedItem.Text)
        //    {
        //        case "Dark Theme":
        //            {
        //                CommonTheming.ThemeDarkSet();
        //                CommonTheming.ThemeDarkSave();
        //                break;
        //            }

        // case "Light Theme": { CommonTheming.ThemeLightSet(); CommonTheming.ThemeLightSave();
        // break; }

        //        default:
        //            {
        //                CommonTheming.ThemeSystemSet();
        //                CommonTheming.ThemeSystemSave();
        //                break;
        //            }
        //    }
        //}

        ///// <summary>
        ///// Raises the <see cref="NavigatedTo"/> event.
        ///// </summary>
        ///// <param name="e">
        ///// The <see cref="NavigatedToEventArgs"/> instance containing the event data.
        ///// </param>
        ///// <param name="viewModelState">
        ///// State of the view ViewModel.
        ///// </param>
        ///// <returns>
        ///// A <see cref="Task"/> representing the asynchronous operation.
        ///// </returns>
        //public override async Task<bool> PopulateViewModelAsync()
        //{
        //    return true;
        //}

        //public void TestPage()
        //{
        //    BaseNavigationService.NavigateAsync(nameof(TestPage));

        //    //BaseNavigationService.NavigateAsync(nameof(AShellPage));
        //}
    }
}