using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;

namespace MedikTapp.Views
{
    public class onboardingModels 
    {

        public string AppTitle { get; set; }
        public string Description { get; set; }
        public string MedikTappDescription { get; set; }
        public IAsyncCommand SignupCommand { get; set; }
        public IAsyncCommand LoginCommand { get; set; }


     }
            
               
    
}
