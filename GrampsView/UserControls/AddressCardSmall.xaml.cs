// <copyright file="AddressCardSmall.xaml.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

namespace GrampsView.UserControls
{
    using Xamarin.Forms;

    /// <summary>
    /// Code behind for the Address Card Small User Control.
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// <seealso cref="Windows.UI.Xaml.Controls.UserControl" />
    public partial class AddressCardSmall : Frame
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddressCardSmall" /> class.
        /// </summary>
        public AddressCardSmall()
        {
            InitializeComponent();

            // setup command delegates
            //OpenMapCommand = new DelegateCommand<PointerRoutedEventArgs>(OpenMap);

            //DataContextChanged += (s, e) => Bindings.Update();
        }

        //public DelegateCommand<PointerRoutedEventArgs> OpenMapCommand
        //{
        //    get;
        //    private set;
        //}

        ///// <summary>
        ///// Gets.
        ///// </summary>
        ///// <value>
        ///// The ViewModel.
        ///// </value>
        //public AddressModel ViewModel
        //{
        //    get
        //    {
        //        if ((DataContext != null) && (DataContext.GetType() == typeof(AddressModel)))
        //        {
        //            return (AddressModel)DataContext;
        //        }
        //        else
        //        {
        //            return new AddressModel();
        //        }
        //    }
        //}

        //private async void OpenMap(PointerRoutedEventArgs parameter)
        //{
        //    // Set the option to stay on the screen but at the minimum size var options = new Windows.System.LauncherOptions();

        // //// storeapp //// options.DesiredRemainingView = Windows.UI.ViewManagement.ViewSizePreference.UseMinimum;

        // // Center on New York City

        // //var uriAddress = new Uri(@"bingmaps:?q=" + ViewModel.Formatted);

        // //var uriAddress = new Uri(@"bingmaps:?q=1600%20Pennsylvania%20Ave,%20Washington,%20DC");

        // // Launch the Windows Maps app var launcherOptions = new Windows.System.LauncherOptions {
        // TargetApplicationPackageFamilyName = "Microsoft.WindowsMaps_8wekyb3d8bbwe", };

        // //var success = await Launcher.LaunchUriAsync(uriAddress, launcherOptions);

        //    //if (success == false)
        //    //{
        //    //    await DataStore.CN.MajorStatusAdd("Open Map failure").ConfigureAwait(false);
        //    //}
        //}
    }
}