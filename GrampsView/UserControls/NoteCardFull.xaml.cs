﻿// <copyright file="NoteCardLarge.xaml.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

namespace GrampsView.UserControls
{
    using GrampsView.Data.Model;

    using Xam.Forms.Markdown;

    using Xamarin.Forms;

    /// <summary>
    /// Code behind for Note Card Large.
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.UserControl" />
    public partial class NoteCardFull : Frame
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoteCardFull" /> class.
        /// </summary>
        public NoteCardFull()
        {
            InitializeComponent();

            // Set MarkdownView information that is not easily set in XAML
            MarkdownTheme t = (MarkdownTheme)new DarkMarkdownTheme();
            t.BackgroundColor = Common.CommonRoutines.ResourceColourGet("CardBackGroundNote");

            this.mdview.Theme = t;
        }

        private void PersonCardSmallRoot_BindingContextChanged(object sender, System.EventArgs e)
        {
            NoteCardFull card = (sender as NoteCardFull);

            if (card is null)
            {
                this.IsVisible = false;
                return;
            }

            HLinkNoteModel t = (card.BindingContext as HLinkNoteModel);

            if (t is null)
            {
                this.IsVisible = false;
                return;
            }

            if (t.Valid)
            {
                this.IsVisible = true;
            }
            else
            {
                this.IsVisible = false;
            }
        }
    }
}