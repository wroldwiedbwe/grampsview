using GrampsView.Common;
using GrampsView.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace GrampsView.Views
{
    public class TestPage : ContentPage
    {
        public TestPage()
        {
            Button button = new Button
            {
                Text = "Click to Rotate Text!",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Center
            };
            button.Clicked += async (sender, args) => throw new ArgumentNullException("Test"); // DataStore.CN.NotifyError("Test Error");

            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Welcome to Xamarin.Forms!" },
                      button
                }
            };
        }
    }
}