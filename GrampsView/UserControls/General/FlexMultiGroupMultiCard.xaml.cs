namespace GrampsView.UserControls
{
    using GrampsView.Common;

    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;

    using Xamarin.Forms;
    using Xamarin.Forms.Internals;

    public partial class FlexMultiCardType : FlexBase
    {
        public static readonly BindableProperty FMultiSourceProperty
                   = BindableProperty.Create(returnType: typeof(CardGroup), declaringType: typeof(FlexMultiCardType), propertyChanged: OnItemsSourceChanged, propertyName: nameof(FsctSource));

        public FlexMultiCardType()
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
            get { return (CardGroup)GetValue(FMultiSourceProperty); }
            set { SetValue(FMultiSourceProperty, value); }
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