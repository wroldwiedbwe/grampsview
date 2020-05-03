namespace GrampsView.UserControls
{
    using System;
    using Xamarin.Forms;

    public class FormsRadioButton : ContentView
    {
        public static readonly BindableProperty RadioItemsProperty

             = BindableProperty.Create(returnType: typeof(RadioItems), declaringType: typeof(FormsRadioButton), propertyChanged: OnItemsSourceChanged, propertyName: nameof(RadioItems));

        private ListView list;

        public FormsRadioButton()
        {
            BuildList();
            Content = list;
            list.BindingContextChanged += OnListBindingContextChanged;
        }

        public event EventHandler<RadioItemToggledEventArgs> ItemToggled;

        public RadioItems RadioItems
        {
            get => (RadioItems)GetValue(RadioItemsProperty);
            //set => SetValue(RadioItemsProperty, value);
        }

        private static void OnItemsSourceChanged(BindableObject obj, object oldValue, object newValue)
        {
            FormsRadioButton tt = obj as FormsRadioButton;

            tt.list.BindingContextChanged -= tt.OnListBindingContextChanged;

            if (!(tt.RadioItems is null))
            {
                tt.RadioItems.ItemToggled += (radioItem, _) => tt.OnItemToggled(radioItem as RadioItem);
            }

            tt.list.ItemsSource = tt.RadioItems;
            //tt.list.SetBinding(ListView.ItemsSourceProperty, new Binding(nameof(RadioItems)));
        }

        private void BuildList()
        {
            list = new ListView
            {
                SelectionMode = ListViewSelectionMode.None,
                SeparatorColor = Color.Transparent,
                ItemTemplate = new DataTemplate(typeof(SwitchCell)),
                RowHeight = 50
            };

            list.ItemTemplate.SetBinding(SwitchCell.TextProperty, nameof(RadioItem.Text));
            list.ItemTemplate.SetBinding(SwitchCell.OnProperty, nameof(RadioItem.Toggled));
            list.ItemTemplate.SetBinding(SwitchCell.IsEnabledProperty, nameof(RadioItem.Enabled));
        }

        private void OnItemToggled(RadioItem selectedRadioItem)
        {
            ItemToggled?.Invoke(this, new RadioItemToggledEventArgs(selectedRadioItem));
        }

        private void OnListBindingContextChanged(object sender, EventArgs e)
        {
            list.BindingContextChanged -= OnListBindingContextChanged;
            var radioItems = ((ListView)sender).ItemsSource as RadioItems;

            if (!(radioItems is null))
            {
                radioItems.ItemToggled += (radioItem, _) => OnItemToggled(radioItem as RadioItem);
            }
        }
    }
}