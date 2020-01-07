namespace GrampsView.UWP
{
    using GrampsView.Common.CustomClasses;
    using GrampsView.Events;
    using GrampsView.UWP.Common;

    using Prism.Events;

    internal class PlatformSpecific : IPlatformSpecific
    {
        public PlatformSpecific(IEventAggregator iocEventAggregator)
        {
            iocEventAggregator.GetEvent<DataLoadCompleteEvent>().Subscribe(UpdateTile, ThreadOption.UIThread);
        }

        public void ActivityTimeLineAdd()
        {
            // TODO
        }

        private void UpdateTile(object obj)

        {
            CommonTileUpdate.UpdateTile();
        }
    }
}