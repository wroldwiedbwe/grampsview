namespace GrampsView.UserControls
{
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;

    public class RadioItems : ObservableCollection<RadioItem>
    {
        private bool busy;

        private int highestSelectedIndex;

        public RadioItems()
        {
            CollectionChanged += RadioItemsCollectionChanged;
        }

        public event EventHandler ItemToggled;

        private void OnItemToggled(RadioItem selectedItem)
        {
            ItemToggled?.Invoke(selectedItem, null);
        }

        private void OnRadioItemToggled(object sender, PropertyChangedEventArgs e)
        {
            if (busy) return;
            busy = true;

            if (sender is RadioItem toggledRadioItem)
            {
                highestSelectedIndex = IndexOf(toggledRadioItem) > highestSelectedIndex ? IndexOf(toggledRadioItem) : highestSelectedIndex;

                // I know which the highest selected index is, nothing can be togged after that so
                // don't bother to loop any more than required.
                for (int i = 0; i < highestSelectedIndex + 1; i++)
                {
                    if (toggledRadioItem != this[i])
                    {
                        this[i].PropertyChanged -= OnRadioItemToggled;
                        this[i].Toggled = false;
                        this[i].Enabled = true;
                        this[i].PropertyChanged += OnRadioItemToggled;
                    }

                    // Stop changes to selected toggle
                    if (toggledRadioItem == this[i])
                    {
                        this[i].Enabled = false;
                    }
                }

                highestSelectedIndex = IndexOf(toggledRadioItem) < highestSelectedIndex ? IndexOf(toggledRadioItem) : highestSelectedIndex;
                OnItemToggled(toggledRadioItem);
            }

            busy = false;
        }

        private void RadioItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (RadioItem radioItem in e.NewItems)
                {
                    radioItem.PropertyChanged += OnRadioItemToggled;
                }
            }

            if (e.OldItems != null)
            {
                foreach (RadioItem radioItem in e.OldItems)
                {
                    radioItem.PropertyChanged -= OnRadioItemToggled;
                }
            }
        }
    }
}