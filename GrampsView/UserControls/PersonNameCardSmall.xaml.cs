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

                PersonNameCards.Add(new CardListLine("Type:", tt.GType));
                PersonNameCards.Add(new CardListLine("Full Name:", tt.FullName));
                PersonNameCards.Add(new CardListLine("Title:", tt.GTitle));
                PersonNameCards.Add(new CardListLine("FirstName:", tt.GFirstName));
                PersonNameCards.Add(new CardListLine("GSurName:", tt.GSurName.GetPrimarySurname));

                PersonNameCards.Add(new CardListLine("Alt:", tt.GAlt.GetDefaultText));
                PersonNameCards.Add(new CardListLine("Call:", tt.GCall));
                PersonNameCards.Add(new CardListLine("Date:", tt.GDate.GetShortDateAsString));
                PersonNameCards.Add(new CardListLine("Display:", tt.GDisplay));
                PersonNameCards.Add(new CardListLine("Family Nick:", tt.GFamilyNick));

                PersonNameCards.Add(new CardListLine("Group:", tt.GGroup));
                PersonNameCards.Add(new CardListLine("Nick:", tt.GNick));
                PersonNameCards.Add(new CardListLine("Priv:", tt.GPriv));
                PersonNameCards.Add(new CardListLine("Sort:", tt.GSort));

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