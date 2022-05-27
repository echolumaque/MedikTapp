using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;

namespace MedikTapp.ViewModels
{
    public class registerModels
    {
        public string AppTitle { get; set; }
        public string RegistraterNow { get; set; }
        public string Description { get; set; }
        public IAsyncCommand RegisterCommand { get; set; }
       

    }
}
