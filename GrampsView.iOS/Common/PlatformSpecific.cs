namespace GrampsView.iOS.Common
{
    using GrampsView.Common.CustomClasses;
    using ObjCRuntime;
    using Prism.Events;

    internal class PlatformSpecific : IPlatformSpecific
    {
        public PlatformSpecific(IEventAggregator iocEventAggregator)
        {
        }

        public void ActivityTimeLineAdd() => throw new System.NotImplementedException();

        public bool IsRunningInEmulator()
        {
            // Check if running on a Simulator
            if (Runtime.Arch == Arch.SIMULATOR) return true;

            return false;
        }
    }