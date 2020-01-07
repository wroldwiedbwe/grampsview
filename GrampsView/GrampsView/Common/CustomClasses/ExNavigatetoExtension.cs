namespace GrampsView.Common
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using GrampsView.Data.Repository;

    using Prism.Navigation;
    using Prism.Navigation.Xaml;

    public class ExNavigateToExtension : NavigateToExtension
    {
        protected override async Task HandleNavigation(INavigationParameters parameters, INavigationService navigationService)
        {
            Debug.WriteLine($"Navigating to: {Name}");

            DataStore.NV.Nav(parameters, Name);

            //await base.HandleNavigation(parameters, navigationService);
        }
    }
}