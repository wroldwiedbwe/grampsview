using Android.App;
using Android.Content;

using Microsoft.Identity.Client;

using Plugin.CurrentActivity;

using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.OneDrive;

[assembly: Dependency(typeof(OneDriveService))]

namespace Xamarin.OneDrive
{
    public partial class Connector
    {
        public static void Init(Context activity)
        {
            Init(activity, string.Empty);
        }

        public static void Init(Context activity, string redirectUrl)
        {
            var dependency = new OneDriveService();
            dependency._activity = activity;
            dependency._redirectUrl = redirectUrl;
        }

        public static void SetAuthenticationContinuationEventArgs(int requestCode, Result resultCode, Intent data)
        {
            // TODO add back when going to Pie
            // AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(requestCode,
            // resultCode, data);
        }
    }

    internal class OneDriveService : ICommonOneDrive
    {
        internal Context _activity;
        internal string _redirectUrl;

        public async Task<AuthenticationResult> GetAuthResult(IPublicClientApplication client, Configs configs)
        {
            try
            {
                return await client.AcquireTokenInteractive(configs.Scopes).WithParentActivityOrWindow(CrossCurrentActivity.Current.Activity).ExecuteAsync().ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Initialize(Configs configs)
        {
            if (_activity != null)
            {
                configs.UiParent = _activity;
            }
            else
            {
                var mainActivity = CrossCurrentActivity.Current.Activity; // Xamarin.Forms.Forms.Context as Forms.Platform.Android.FormsAppCompatActivity;
                configs.UiParent = mainActivity;
            }

            if (!string.IsNullOrEmpty(_redirectUrl))
            {
                configs.RedirectUri = _redirectUrl;
            }
            else
            {
                configs.RedirectUri = $"msal{configs.ClientID}://auth";
            }
        }
    }
}