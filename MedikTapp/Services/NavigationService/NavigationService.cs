using MedikTapp.Helpers.CustomViews;
using Microsoft.Extensions.DependencyInjection;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using NavUtils = MedikTapp.Services.NavigationService.NavigationUtilities;

namespace MedikTapp.Services.NavigationService
{
    public partial class NavigationService
    {
        private readonly IPopupNavigation _popupNavigation;
        private readonly IServiceProvider _serviceProvider;

        public NavigationService(IServiceProvider serviceProvider)
        {
            _popupNavigation = PopupNavigation.Instance;
            _serviceProvider = serviceProvider;
        }

        public Page GetCurrentPage() => ((InvisibleNavigationPage)Application.Current.MainPage)?.CurrentPage;

        public Task GoTo<TPage>(NavigationParameters parameters = null) where TPage : Page
        {
            NavUtils.IsSystemBackButtonPressed = false;
            var toPage = ActivatorUtilities.CreateInstance<TPage>(_serviceProvider);
            return toPage is PopupPage popup
                ? NavUtils.DoNavigateAsync(GetCurrentPage(), toPage, parameters,
                    () => _popupNavigation.PushAsync(popup, true))
                : NavUtils.DoNavigateAsync(GetCurrentPage(), toPage, parameters,
                    () => Application.Current.MainPage.Navigation.PushAsync(toPage, true));

        }

        public Task PopPage(NavigationParameters parameters = null)
        {
            NavUtils.IsSystemBackButtonPressed = false;
            var navStack = Application.Current.MainPage.Navigation.NavigationStack;
            var toPage = navStack.Count > 1 ? navStack[^2] : null;
            return NavUtils.DoNavigateAsync(GetCurrentPage(), toPage, parameters,
                () => Application.Current.MainPage.Navigation.PopAsync(true));
        }

        public Task PopPopup(Page toPage = null, NavigationParameters parameters = null)
        {
            NavUtils.IsSystemBackButtonPressed = false;
            var fromPage = _popupNavigation.PopupStack.LastOrDefault();
            return NavUtils.DoNavigateAsync(fromPage, GetCurrentPage(), parameters, () => _popupNavigation.PopAsync(true));
        }

        public Task PopToRoot(NavigationParameters parameters = null)
        {
            NavUtils.IsSystemBackButtonPressed = false;
            return NavUtils.DoNavigateAsync(GetCurrentPage(), Application.Current.MainPage, parameters,
                () => Application.Current.MainPage.Navigation.PopToRootAsync(true));
        }

        public void SetRootPage<TView>(NavigationParameters parameters = null) where TView : Page
        {
            NavUtils.IsSystemBackButtonPressed = false;
            Page toPage = ActivatorUtilities.CreateInstance<TView>(_serviceProvider);
            if (toPage is PopupPage) return;
            InvisibleNavigationPage navPage = new(toPage);
            navPage.Behaviors.Add(new NavigationPageBehavior());
            NavUtils.DoNavigate(GetCurrentPage(), toPage, parameters, () => Application.Current.MainPage = navPage);
        }
    }
}