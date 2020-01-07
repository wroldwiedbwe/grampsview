namespace GrampsView.UserControls
{
    using System.Collections;

    using Xamarin.Forms;

    /// <summary>See <a href="https://evgenyzborovsky.com/2018/06/06/repeater-or-bindable-stacklayout/">
    /// https://evgenyzborovsky.com/2018/06/06/repeater-or-bindable-stacklayout/ </a></summary>
    public class BindableStackLayout : StackLayout
    {
        public static readonly BindableProperty ItemDataTemplateProperty =
            BindableProperty.Create(nameof(ItemDataTemplate), typeof(DataTemplate), typeof(BindableStackLayout));

        public static readonly BindableProperty ItemsSourceProperty =
                BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(BindableStackLayout),
                                        propertyChanged: (bindable, oldValue, newValue) => ((BindableStackLayout)bindable).PopulateItems());

        public DataTemplate ItemDataTemplate
        {
            get { return (DataTemplate)GetValue(ItemDataTemplateProperty); }
            set { SetValue(ItemDataTemplateProperty, value); }
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        private void PopulateItems()
        {
            if (ItemsSource == null) return;
            foreach (var item in ItemsSource)
            {
                if (ItemDataTemplate.CreateContent() is View itemTemplate)
                {
                    itemTemplate.BindingContext = item;
                    Children.Add(itemTemplate);

                    //// Set min
                    //itemTemplate.MinimumHeightRequest = 40;
                }
            }
        }
    }
}