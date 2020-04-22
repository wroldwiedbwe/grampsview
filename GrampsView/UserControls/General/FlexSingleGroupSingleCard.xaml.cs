namespace GrampsView.UserControls
{
    using GrampsView.Common;

    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;

    using Xamarin.Forms;
    using Xamarin.Forms.Internals;

    public partial class FlexSingleGroupSingleCard : FlexBase
    {
        public static readonly BindableProperty FsctSourceProperty
                 = BindableProperty.Create(returnType: typeof(CardGroup), declaringType: typeof(FlexSingleGroupSingleCard), propertyChanged: OnItemsSourceChanged, propertyName: nameof(FsctSource));

        public static readonly BindableProperty FsctTemplateProperty
            = BindableProperty.Create(nameof(FsctTemplate), typeof(DataTemplate), typeof(FlexSingleGroupSingleCard), propertyChanged: OnItemTemplateChanged);

        public FlexSingleGroupSingleCard()
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
            get { return (CardGroup)GetValue(FsctSourceProperty); }
            set { SetValue(FsctSourceProperty, value); }
        }

        public override DataTemplate FsctTemplate
        {
            get { return (DataTemplate)GetValue(FsctTemplateProperty); }
            set { SetValue(FsctTemplateProperty, value); }
        }

        public override void AddToLayout(int argStartIndex)
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

            for (int i = argStartIndex + 1; i < DisplayList.Count; i++)
            {
                View view;

                if (FsctTemplate is DataTemplateSelector)
                {
                    view = (View)FsctTemplate.CreateContent(DisplayList[i], null);
                }
                else
                {
                    view = (View)FsctTemplate.CreateContent();
                }

                view.BindingContext = DisplayList[i];
                this.flexer.Children.Add(view);
            }
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

            //foreach (var item in DisplayList)
            //{
            //    View view;

            // if (FsctTemplate is DataTemplateSelector) { view =
            // (View)FsctTemplate.CreateContent(item, null); } else { view =
            // (View)FsctTemplate.CreateContent(); }

            //    view.BindingContext = item;
            //    this.flexer.Children.Add(view);
            //}
        }
    }
}