// <copyright file="CommonLogPrism.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Common
{
    using System;
    using System.Diagnostics;

    using Prism.Logging;

    /// <summary>
    /// Logger Facade for Prism.
    /// </summary>
    /// <seealso cref="Prism.Logging.ILoggerFacade"/>
    public class CommonLogPrism : ILoggerFacade
    {
        /// <summary>
        /// Writes a log message.
        /// </summary>
        /// <param name="message">
        /// The message to write.
        /// </param>
        /// <param name="category">
        /// The message category.
        /// </param>
        /// <param name="priority">
        /// Not used by Log4Net; pass Priority.None.
        /// </param>
        public void Log(string message, Category category, Priority priority)
        {
            string now = DateTime.Now.Ticks.ToString(System.Globalization.CultureInfo.CurrentCulture);

            switch (category)
            {
                case Category.Debug:
                    Debug.WriteLine(now + "Debug:" + message);
                    break;

                case Category.Warn:
                    Debug.WriteLine(now + "Warn:" + message);
                    break;

                case Category.Exception:
                    Debug.WriteLine(now + "Exception:" + message);
                    break;

                case Category.Info:
                    Debug.WriteLine(now + "Info:" + message);
                    break;

                default:
                    Debug.WriteLine(now + "Unknown category:" + category + ":" + message);
                    break;
            }
        }
    }
}