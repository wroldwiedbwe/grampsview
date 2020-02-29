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

        public CardListLineCollection PersonNameCards { get; } = new CardListLineCollection();

        public bool Visible { get; set; } = true;

        private void Frame_BindingContextChanged(object sender, System.EventArgs e)
        {
            PersonNameModel tt = this.BindingContext as PersonNameModel;

            if (!(tt is null))
            {
                // TODO Show All Surnames

                PersonNameCards.Add(new CardListLine("GType:", tt.GType));
                PersonNameCards.Add(new CardListLine("Full Name:", tt.FullName));
                PersonNameCards.Add(new CardListLine("GTitle:", tt.GTitle));
                PersonNameCards.Add(new CardListLine("GFirstName:", tt.GFirstName));
                PersonNameCards.Add(new CardListLine("GSurName:", tt.GSurName.GetPrimarySurname));

                PersonNameCards.Add(new CardListLine("GAlt:", tt.GAlt.GetDefaultText));
                PersonNameCards.Add(new CardListLine("GCall:", tt.GCall));
                PersonNameCards.Add(new CardListLine("GDate:", tt.GDate.GetShortDateAsString));
                PersonNameCards.Add(new CardListLine("GDisplay:", tt.GDisplay));
                PersonNameCards.Add(new CardListLine("GFamilyNick:", tt.GFamilyNick));

                PersonNameCards.Add(new CardListLine("GGroup:", tt.GGroup));
                PersonNameCards.Add(new CardListLine("GNick:", tt.GNick));
                PersonNameCards.Add(new CardListLine("GPriv:", tt.GPriv));
                PersonNameCards.Add(new CardListLine("GSort:", tt.GSort));

                //// Get extra name details
                //if (PersonObject.GPersonNamesCollection.GetPrimaryName.GSurName.Count > 0)
                //{
                //    foreach (SurnameModel item in PersonObject.GPersonNamesCollection.GetPrimaryName.GSurName)
                //    {
                //        CardListLineCollection extraNameDetails = new CardListLineCollection
                //        {
                //            new CardListLine("Surname:", item.GText),
                //            new CardListLine("Surname Connector:", item.GConnector),
                //            new CardListLine("Surname Derivation:", item.GDerivation),
                //            new CardListLine("Surname Prefix:", item.GPrefix),
                //            new CardListLine("Surname Primary:", item.GPrim)
                //        };

                //        t.Cards.Add(extraNameDetails);
                //    }
                //}
                this.BindingContext = PersonNameCards;
            }
        }
    }
}