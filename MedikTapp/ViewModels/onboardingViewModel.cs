using MedikTapp.Services.NavigationService;
using MedikTapp.ViewModels.Base;
using MedikTapp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace MedikTapp.ViewModels
{
    public class onboardingViewModel : ViewModelBase
    {  

        public onboardingViewModel(NavigationService navigationService ) : base(navigationService)
        {
            _Content = new ObservableCollection<onboardingModels>
            {
                new onboardingModels
                {
                    AppTitle  = "MedikTapp",
                    Description ="Book an Appointment" + 
                     "\n" + "with us" ,
                    MedikTappDescription = " offers an easy appointment" +"\n"+
                     "to our best Physicians available"+"\n"+
                    "and ensuring the best quality to you"
                    
                }
            };
        }

        private ObservableCollection<onboardingModels> _content;
        public ObservableCollection<onboardingModels> _Content
        {
            get { return _content; }
            set { _content = value; }
        }
    }
    
}

