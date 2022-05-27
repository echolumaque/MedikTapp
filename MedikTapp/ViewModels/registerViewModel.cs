using MedikTapp.Services.NavigationService;
using MedikTapp.ViewModels.Base;
using MedikTapp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;

namespace MedikTapp.ViewModels
{
    public class registerViewModel : ViewModelBase
    {
        public registerViewModel(NavigationService navigationService) : base(navigationService)
        {
            _Registers = new ObservableCollection<registerModels>()
            {
                new registerModels
                {
                    Description="Your information is safe with us",
                    RegistraterNow="Register Now!",
                    RegisterCommand = new AsyncCommand (() => NavigationService.GoTo<loginViewPage>(),allowsMultipleExecutions:false)
                }

                
            };
            

        }

        private ObservableCollection<registerModels> _registers;
        public ObservableCollection<registerModels> _Registers
        {
            get { return _registers; }
            set { _registers = value; } 
        }
    }
}
