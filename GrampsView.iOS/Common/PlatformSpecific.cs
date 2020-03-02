namespace GrampsView.iOS.Common
{
    using GrampsView.Common.CustomClasses;

    using Prism.Events;

    internal class PlatformSpecific : IPlatformSpecific
    {
        public PlatformSpecific(IEventAggregator iocEventAggregator)
        {
        }

        public void ActivityTimeLineAdd() => throw new System.NotImplementedException();
    }
}