// <copyright file="PersonCardLarge.xaml.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

namespace GrampsView.UserControls
{
    using GrampsView.Data.Model;

    using Xamarin.Forms;

    public partial class HeaderCardLarge : Frame
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderCardLarge" /> class.
        /// </summary>
        public HeaderCardLarge()
        {
            InitializeComponent();
        }

        public CardListLineCollection HeaderCard { get; set; } = new CardListLineCollection();

        private void HeaderCardLargeRoot_BindingContextChanged(object sender, System.EventArgs e)
        {
            if (this.BindingContext is HeaderModel HeaderData)
            {
                HeaderCard = new CardListLineCollection
                    {
                        new CardListLine("Created using version:", HeaderData.GCreatedVersion),
                        new CardListLine("Created on:", HeaderData.GCreatedDate),
                        new CardListLine("Researcher Name:", HeaderData.GResearcherName),
                        new CardListLine("Researcher State:", HeaderData.GResearcherState),
                        new CardListLine("Researcher Country:", HeaderData.GResearcherCountry),
                        new CardListLine("Researcher Email:", HeaderData.GResearcherEmail),
                        new CardListLine("MediaPath:", HeaderData.GMediaPath),
                    }
                ;
            }

            HeaderCard.Title = "Header Details";

            this.LLCL.UCCardListLineCol = HeaderCard;
        }
    };
}