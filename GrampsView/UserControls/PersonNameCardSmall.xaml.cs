namespace GrampsView.UserControls
{
    using GrampsView.Data.Model;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonNameCardSmall : Frame
    {
        public PersonNameCardSmall()
        {
            InitializeComponent();
        }

        public CardListLineCollection PersonNameCards { get; set; } = new CardListLineCollection();

        public bool Visible { get; set; } = true;

        private void Frame_BindingContextChanged(object sender, System.EventArgs e)
        {
            PersonNameModel tt = this.BindingContext as PersonNameModel;

            if (!(tt is null))
            {
                // TODO Show CitationRef and NoteRef and All Surnames
                CardListLineCollection newCards = new CardListLineCollection
                {
                    new CardListLine("Full Name:", tt.FullName),
                    new CardListLine("GAlt:", tt.GAlt.GetDefaultText),
                    new CardListLine("GCall:", tt.GCall),
                    new CardListLine("GDate:", tt.GDate.GetShortDateAsString),
                    new CardListLine("GDisplay:", tt.GDisplay),
                    new CardListLine("GFamilyNick:", tt.GFamilyNick),
                    new CardListLine("GFirstName:", tt.GFirstName),
                    new CardListLine("GGroup:", tt.GGroup),
                    new CardListLine("GNick:", tt.GNick),
                    new CardListLine("GPriv:", tt.GPriv),
                    new CardListLine("GSort:", tt.GSort),
                    new CardListLine("GSurName:", tt.GSurName.GetPrimarySurname),
                    new CardListLine("GTitle:", tt.GTitle),
                    new CardListLine("GType:", tt.GType)
                };

                PersonNameCards = newCards;

                this.BindingContext = PersonNameCards;
            }
        }
    }
}