// <copyright file="TextDetailCardModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Data.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class TextDetailCardModel : ModelBase, ITextDetailCardModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextDetailCardModel" /> class.
        /// </summary>
        public TextDetailCardModel()
        {
        }

        /// <summary>
        /// Gets or sets the text details.
        /// </summary>
        /// <value>
        /// The text details.
        /// </value>
        public string TextDetails
        {
            get; set;
        }

        = string.Empty;
    }
}