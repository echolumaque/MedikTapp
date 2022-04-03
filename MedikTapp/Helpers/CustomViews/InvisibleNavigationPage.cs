using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using NavigationPage = Xamarin.Forms.NavigationPage;
using Page = Xamarin.Forms.Page;

namespace MedikTapp.Helpers.CustomViews
{
    public class InvisibleNavigationPage : NavigationPage
    {
        public InvisibleNavigationPage(Page page) : base(page)
        {
            BarBackgroundColor = Color.Transparent;
            BarTextColor = Color.Transparent;

            On<iOS>().SetIsNavigationBarTranslucent(true);
            On<iOS>().SetHideNavigationBarSeparator(true);
        }

        public bool IgnoreLayoutChange { get; set; } = false;

        protected override void OnSizeAllocated(double width, double height)
        {
            if (!IgnoreLayoutChange)
                base.OnSizeAllocated(width, height);
        }
    }
}