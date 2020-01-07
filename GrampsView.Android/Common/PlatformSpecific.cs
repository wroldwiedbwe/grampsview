namespace GrampsView.Droid.Common
{
    using Android.OS;
    using GrampsView.Common.CustomClasses;

    using Prism.Events;

    internal class PlatformSpecific : IPlatformSpecific
    {
        public void ActivityTimeLineAdd()
        {
            // TODO add this
        }

        public void Init(IEventAggregator iocEventAggregator)
        {
        }

        public bool IsRunningInEmualtor()
        {
            // Check if running on an Emulator
            if (Build.Fingerprint.Contains("vbox") || Build.Fingerprint.Contains("generic"))
                return true;

            return false;
        }
    }
}