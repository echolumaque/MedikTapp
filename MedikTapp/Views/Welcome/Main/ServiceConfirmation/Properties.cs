﻿using System;
using Xamarin.CommunityToolkit.ObjectModel;

namespace MedikTapp.Views.Welcome.Main.ServiceConfirmation
{
    public partial class ServiceConfirmationPopupViewModel
    {
        public string ServiceImage { get; set; }
        public string ServiceName { get; set; }
        public double ServicePrice { get; set; }
        public string ServiceDescription { get; set; }
        public IAsyncCommand CancelCmd { get; }
        public IAsyncCommand AddToBookingCmd { get; }
        public DateTime EarliestAvailableDate { get; set; }
        public Tuple<int, int> ProductImageSize { get; set; }
    }
}