//-----------------------------------------------------------------------
//
// Handles GRAMPS URL fields
//
// <copyright file="URLModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Model
{
    using Prism.Commands;
    using System;
    using System.Runtime.Serialization;
    using Xamarin.Essentials;

    /// <summary>
    /// GRAMPS URL element class.
    /// </summary>
    public class URLModel : ModelBase, IURLModel
    {
        //// "url-content"
        //// "priv"
        //// "type"
        //// "href"
        //// "description"

        /// <summary>Initializes a new instance of the <see cref="URLModel"/> class.</summary>
        public URLModel()
        {
            OpenURLCommand = new DelegateCommand(OpenURL, CanOpenURL);

            HomeImageHLink.HomeSymbol = GrampsView.Common.IconFont.Link;
        }

        /// <summary>Gets the default text.</summary>
        /// <value>The default text.</value>
        public string DefaultText
        {
            get
            {
                string returnVal = string.Empty;

                if (!string.IsNullOrEmpty(GType))
                {
                    returnVal = GType + ":";
                }

                return returnVal + GDescription;
            }
        }

        /// <summary>Gets or sets the description.</summary>
        /// <value>The g description.</value>
        [DataMember]
        public string GDescription
        {
            get;
            set;
        }

        /// <summary>Gets or sets the hlink reference.</summary>
        /// <value>The gh reference.</value>
        [DataMember]
        public Uri GHRef
        {
            get;
            set;
        }

        [DataMember]
        public string GType
        {
            get;
            set;
        }

        public DelegateCommand OpenURLCommand { get; private set; }

        private bool CanOpenURL()
        {
            return true;
        }

        /// <summary>
        /// Opens the URL.
        /// </summary>
        private void OpenURL()
        {
            Launcher.OpenAsync(GHRef);
        }
    }
}