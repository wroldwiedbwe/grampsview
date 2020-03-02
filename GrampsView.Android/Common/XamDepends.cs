using Android.Graphics;
using GrampsView.Common;
using GrampsView.Droid;
using System.IO;
using System;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

[assembly: Dependency(typeof(ImageResource))]

namespace GrampsView.Droid
{
    public class ImageResource : IImageResource
    {
        public Size GetSize(string fileName)
        {
            return Task.Run(async () =>

            {
                try
                {
                    var options = new BitmapFactory.Options
                    {
                        InJustDecodeBounds = true
                    };

                    fileName = fileName.Replace(".png", "").Replace(".jpg", "");
                    var resField = typeof(GrampsView.Droid.Resource.Drawable).GetField(fileName);
                    var resID = (int)resField.GetValue(null);

                    BitmapFactory.DecodeResource(Forms.Context.Resources, resID, options);

                    Size ttt = new Size((double)options.OutWidth, (double)options.OutHeight);

                    return ttt;
                }

                // Handle errors with catch blocks
                catch (FileNotFoundException)
                {
                    //DataStore.CN.MajorStatusMessage( "UWP Size GetSize(string fileName)");

                    // TODO For example, handle a file not found error
                    return new Size(0, 0);
                }

                return new Size(0, 0);
            }).Result;
        }
    }
}