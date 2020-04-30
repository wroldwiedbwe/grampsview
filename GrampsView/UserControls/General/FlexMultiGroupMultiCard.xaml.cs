namespace GrampsView.UserControls
{
    using GrampsView.Common;

    using Xamarin.Forms;
    using Xamarin.Forms.Internals;

    public partial class FlexMultiGroupMultiCard : FlexBase
    {
        public static readonly BindableProperty FsctSourceProperty
                   = BindableProperty.Create(returnType: typeof(CardGroup), declaringType: typeof(FlexMultiGroupMultiCard), propertyChanged: OnItemsSourceChanged, propertyName: nameof(FsctSource), defaultValue: new CardGroup());

        public FlexMultiGroupMultiCard()
        {
            InitializeComponent();
        }

        public bool FlexMultiCardVisible
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
            get { return (CardGroup)GetValue(FsctSourceProperty); }
            set { SetValue(FsctSourceProperty, value); }
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

            this.multiflexer.Children.Clear();

            foreach (CardGroup item in DisplayList)
            {
                this.multiflexer.Children.Add(CreateChildView(item));
            }
        }

        private View CreateChildView(object item)
        {
            Application.Current.Resources.TryGetValue("CardGroupTemplate", out var cardGroupTemplate);

            DataTemplate t = cardGroupTemplate as DataTemplate;

            View view = (View)t.CreateContent(item, null);

            view.BindingContext = item;

            return view;
        }
    }
}