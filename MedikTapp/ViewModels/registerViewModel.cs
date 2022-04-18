using MedikTapp.Services.NavigationService;
using MedikTapp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

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
                    AppTitle="MedikTapp",
                    RegistraterNow="Register Now!"
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
