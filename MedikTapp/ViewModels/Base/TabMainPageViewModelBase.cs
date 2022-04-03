using MedikTapp.Enums;
using MedikTapp.Services.NavigationService;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace MedikTapp.ViewModels.Base
{
    public abstract class TabMainPageViewModelBase : ViewModelBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly List<int> _tabsIndex;

        public TabMainPageViewModelBase(NavigationService navigationService,
            IServiceProvider serviceProvider) : base(navigationService)
        {
            _serviceProvider = serviceProvider;

            Tabs = new();
            _tabsIndex = new();
            TabSelectionChangedCmd = new Command<TabSelectionChangedEventArgs>(args => SetActiveTab(args.OldPosition, args.NewPosition));
        }

        public int ActiveTabIndex { get; set; }
        public ICommand TabSelectionChangedCmd { get; }
        public ObservableRangeCollection<TabItemPageViewModelBase> Tabs { get; private set; }

        public void AddTab<TPage>(int index = -1) where TPage : TabItemPageViewModelBase
        {
            TPage page = _serviceProvider.GetRequiredService<TPage>();
            if (page is null) throw new NullReferenceException("No ViewModel found for tab page");
            if (Tabs.Count == 0) page.IsCurrentTab = true;
            Tabs.Add(page);
            _tabsIndex.Add(index == -1 ? Tabs.Count - 1 : index);
        }

        private void InvokeTabNavigateEvent(int tabPositionIndex, NavigationParameters parameters,
            NavigationType navigationType = NavigationType.To)
        {
            int index = _tabsIndex.IndexOf(tabPositionIndex);
            if (index == -1) return;
            TabItemPageViewModelBase tabVm = Tabs[index];
            if (navigationType.Equals(NavigationType.To))
            {
                ActiveTabIndex = index;
                tabVm.IsCurrentTab = true;
                tabVm.OnNavigatedTo(parameters);
            }
            else
                tabVm.OnNavigatedFrom(parameters);
        }

        /// <summary>
        /// Change the active tab and invoke OnNavigatedTo/From events.
        /// </summary>
        /// <param name="previousIndex"></param>
        /// <param name="nextIndex"></param>
        protected void SetActiveTab(int previousIndex, int nextIndex)
        {
            for (int i = 0; i < Tabs.Count; i++)
                Tabs[i].IsCurrentTab = false;
            NavigationParameters parameters = new();
            InvokeTabNavigateEvent(previousIndex, parameters, NavigationType.From);
            InvokeTabNavigateEvent(nextIndex, parameters);
        }

        /// <summary>
        /// Change the active tab but DO NOT invoke OnNavigatedTo/From events.
        /// </summary>
        /// <param name="tabPositionIndex"></param>
        protected void SetActiveTab(int tabPositionIndex)
        {
            for (int i = 0; i < Tabs.Count; i++)
                Tabs[i].IsCurrentTab = false;
            int index = _tabsIndex.IndexOf(tabPositionIndex);
            if (index == -1) return;
            ActiveTabIndex = index;
            Tabs[index].IsCurrentTab = true;
        }
    }
}