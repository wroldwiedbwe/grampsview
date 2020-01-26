﻿namespace GrampsView.UserControls
{
    using GrampsView.Data.Repository;

    using Xamarin.Forms;

    public partial class BottomMenu : Frame
    {
        public BottomMenu()
        {
            InitializeComponent();
        }

        public static string StatusText
        {
            get
            {
                return DataStore.CN.MajorStatusMessage;
            }
        }
    }
}