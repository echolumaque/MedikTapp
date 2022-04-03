﻿using MedikTapp.Services.NavigationService;
using Xamarin.CommunityToolkit.ObjectModel;

namespace MedikTapp.ViewModels.Base
{
    public class ViewModelBase : ObservableObject
    {
        protected NavigationService NavigationService { get; private set; }
        public ViewModelBase(NavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public virtual void OnNavigatedFrom(NavigationParameters parameters) { }
        public virtual void OnNavigatedTo(NavigationParameters parameters) { }
        public virtual void Initialized(NavigationParameters parameters) { }
    }
}