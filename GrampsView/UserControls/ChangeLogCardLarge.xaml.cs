// <copyright file="PersonCardLarge.xaml.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

namespace GrampsView.UserControls
{
    using GrampsView.Data.Repository;

    using System;
    using System.IO;
    using System.Reflection;

    using Xamarin.Forms;

    public partial class ChangeLogCardLarge : Frame
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderCardLarge"/> class.
        /// </summary>
        public ChangeLogCardLarge()
        {
            InitializeComponent();

            try
            {
                // Load Resource
                var assemblyExec = Assembly.GetExecutingAssembly();
                var resourceName = "GrampsView.changelog.md";

                using (Stream stream = assemblyExec.GetManifestResourceStream(resourceName))
                {
                    if (!(stream is null))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            this.mdview.Markdown = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                DataStore.CN.NotifyException("File not Found Exception trying to open GrampsView.changelog.md", ex);
            }
        }
    };
}