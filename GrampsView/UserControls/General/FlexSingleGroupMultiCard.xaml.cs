namespace GrampsView.UserControls
{
    using GrampsView.Common;

    using Xamarin.Forms;
    using Xamarin.Forms.Internals;

    public partial class FlexSingleGroupMultiCard : FlexBase
    {
        public static readonly BindableProperty FMultiSourceProperty = BindableProperty.Create(returnType: typeof(CardGroup), declaringType: typeof(FlexSingleGroupMultiCard), propertyChanged: OnItemsSourceChanged, propertyName: nameof(FsctSource), defaultValue: new CardGroup());

        public static readonly BindableProperty FsctTemplateProperty = BindableProperty.Create(nameof(FsctTemplate), typeof(DataTemplateSelector), typeof(FlexSingleGroupMultiCard), propertyChanged: OnItemTemplateChanged);

        public FlexSingleGroupMultiCard()
        {
            InitializeComponent();
        }

        public bool FlexSingleCardVisible
        {
            get
            {
                if (!(FsctSource is null) && (FsctSource.Cards.Count > 0))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public override CardGroup FsctSource
        {
            get { return (CardGroup)GetValue(FMultiSourceProperty); }
            set { SetValue(FMultiSourceProperty, value); }
        }

        public new DataTemplateSelector FsctTemplate
        {
            get { return (DataTemplateSelector)GetValue(FsctTemplateProperty); }
            set { SetValue(FsctTemplateProperty, value); }
        }

        public override void BuildLayout()
        {
            if (string.IsNullOrWhiteSpace(FsctSource.Title))
            {
                this.flextitle.IsVisible = false;
            }
            else
            {
                this.flextitle.Text = FsctSource.Title;
                this.flextitle.IsVisible = true;
            }

            this.flexer.Children.Clear();

            AddToLayout(0);
        }

        private View CreateChildView(object item)
        {
            //var dts = FsctTemplate as DataTemplateSelector;
            //var itemTemplate = dts.SelectTemplate(item, null);
            //itemTemplate.SetValue(BindableObject.BindingContextProperty, item);
            //return (View)itemTemplate.CreateContent();

            Application.Current.Resources.TryGetValue("CardGroupTemplate", out var cardGroupTemplate);

            DataTemplate t = cardGroupTemplate as DataTemplate;

            View view = (View)t.CreateContent(item, null);

            view.BindingContext = item;

            return view;
        }
    }
}