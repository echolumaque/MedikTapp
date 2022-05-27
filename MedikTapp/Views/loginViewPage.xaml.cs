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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class loginViewPage : BasePage<loginViewModel>
    {
        public loginViewPage(loginViewModel vm) : base(GetViewModel(vm))
        {
            InitializeComponent();
        }

    }
}