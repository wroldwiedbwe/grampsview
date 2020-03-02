using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using GrampsView.Common;
using GrampsView.Data.DataView;

namespace GrampsView.Data.ExternalStorageNS
{
    //public class CustomCacheKeyFactory : ICacheKeyFactory

    //{
    //    public string GetKey(ImageSource imageSource, object bindingContext)

    // { if (!(imageSource is CustomStreamImageSource keySource))

    // return null;

    //        return keySource.Key;
    //    }
    //}

    public partial class GrampsStorePostLoad : CommonBindableBase, IStorePostLoad
    {
        public async Task LoadImageSources()
        {
            foreach (var item in DV.MediaDV.MediaData)
            {
                if ((!item.IsMediaStorageFileValid) && (item.IsMediaFile))
                {
                    break;
                }

                //Stream stream = await item.MediaStorageFile.OpenStreamForReadAsync();

                //if (stream.CanRead)
                //{
                item.MediaStorageFileImageStream = new CustomStreamImageSource()
                {
                    Key = item.Id,
                    Stream = (Func<CancellationToken, Task<Stream>>)(token => item.MediaStorageFile.OpenStreamForReadAsync()),

                    //Stream = ImageSource.FromStream(() => stream),
                };

                //}
                //else
                //{
                //    DataStore.CN.NotifyError("LoadImageSources: Can not read MediaStorageFile (" + item.MediaStorageFilePath + ")");
                //}
            }
        }
    }
}