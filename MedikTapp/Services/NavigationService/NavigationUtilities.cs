using MedikTapp.ViewModels.Base;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MedikTapp.Services.NavigationService
{
    public static class NavigationUtilities
    {
        public static bool IsSystemBackButtonPressed = true;

        public static void DoNavigate(Page fromPage, Page toPage, NavigationParameters parameters, Action action)
        {
            NavigationParameters navParameters = parameters ?? new();
            InvokeOnNavigatedFrom(fromPage, navParameters);
            InvokeSetBindings(toPage, navParameters);
            action();
            InvokeOnNavigatedTo(toPage, navParameters);
            IsSystemBackButtonPressed = true;
        }

        public static async Task DoNavigateAsync(Page fromPage, Page toPage, NavigationParameters parameters, Func<Task> action)
        {
            NavigationParameters navParameters = parameters ?? new();
            InvokeOnNavigatedFrom(fromPage, navParameters);
            InvokeSetBindings(toPage, parameters);
            await action();
            InvokeOnNavigatedTo(toPage, navParameters);
            IsSystemBackButtonPressed = true;
        }

        public static void InvokeNavigatedEvents(Page fromPage, Page toPage, NavigationParameters parameters)
        {
            NavigationParameters navParameters = parameters ?? new();
            InvokeOnNavigatedFrom(fromPage, navParameters);
            InvokeOnNavigatedTo(toPage, navParameters);
            IsSystemBackButtonPressed = true;
        }

        private static void InvokeOnNavigatedFrom(Page page, NavigationParameters parameters)
        {
            if (page != null)
            {
                if (page.BindingContext is TabMainPageViewModelBase tabVm)
                    if (tabVm.Tabs?.FirstOrDefault(x => x.IsCurrentTab) is TabItemPageViewModelBase tab && tab != null)
                        tab.OnNavigatedFrom(parameters);
                if (page.BindingContext is ViewModelBase vm) vm.OnNavigatedFrom(parameters);
            }

        }

        private static void InvokeOnNavigatedTo(Page page, NavigationParameters parameters)
        {
            if (page != null)
            {
                if (page.BindingContext is TabMainPageViewModelBase tabVm)
                    if (tabVm.Tabs?.FirstOrDefault(x => x.IsCurrentTab) is TabItemPageViewModelBase tab && tab != null)
                        tab.OnNavigatedTo(parameters);
                if (page.BindingContext is ViewModelBase vm) vm.OnNavigatedTo(parameters);
            }
        }

        private static void InvokeSetBindings(Page page, NavigationParameters parameters)
        {
            if (page != null)
            {
                if (page.BindingContext is TabMainPageViewModelBase tabVm)
                    if (tabVm.Tabs?.FirstOrDefault(x => x.IsCurrentTab) is TabItemPageViewModelBase tab && tab != null)
                        tab.Initialized(parameters);
                if (page.BindingContext is ViewModelBase vm) vm.Initialized(parameters);
            }
        }
    }
}