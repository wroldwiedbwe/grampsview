namespace GrampsView.UserControls
{
    using System;

    public class RadioItemToggledEventArgs : EventArgs
    {
        public RadioItemToggledEventArgs(RadioItem selectedItem)
        {
            SelectedItem = selectedItem;
        }

        public RadioItem SelectedItem { get; }
    }
}