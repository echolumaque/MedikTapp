using MedikTapp.ViewModels.Base;
using Xamarin.Forms;

namespace MedikTapp.Services.NavigationService
{
    public class NavigationPageBehavior : Behavior<NavigationPage>
    {
        public NavigationPage Page { get; private set; }

        protected override void OnAttachedTo(NavigationPage bindable)
        {
            bindable.Popped += OnPopped;
            base.OnAttachedTo(bindable);
            Page = bindable;
        }

        private void OnPopped(object sender, NavigationEventArgs e)
        {
            if (!NavigationUtilities.IsSystemBackButtonPressed) return;
            if (Page.CurrentPage.BindingContext is ViewModelBase)
                NavigationUtilities.InvokeNavigatedEvents(e.Page, Page.CurrentPage, null);
        }

        protected override void OnDetachingFrom(NavigationPage bindable)
        {
            bindable.Popped -= OnPopped;
            base.OnDetachingFrom(bindable);
            Page = null;
        }
    }
}