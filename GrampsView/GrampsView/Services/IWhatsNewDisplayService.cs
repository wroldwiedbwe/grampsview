// <copyright file="IWhatsNewDisplayService.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Prism.Navigation;

namespace GrampsView.Services
{
    public interface IWhatsNewDisplayService
    {
        bool ShowIfAppropriate(INavigationService iocNavigationService);
    }
}