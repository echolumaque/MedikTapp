using MedikTapp.ViewModels;
using MedikTapp.Views.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MedikTapp.Views
{

    public partial class registerViewPage : BasePage<registerViewModel>
    {
        public registerViewPage(registerViewModel vm) : base(GetViewModel(vm))
        {
            InitializeComponent();
        }
    }
}