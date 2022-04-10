using MedikTapp.ViewModels.Base;
using Xamarin.Forms;

namespace MedikTapp.Views.MainPage
{
    public class TabContentTemplateSelector : DataTemplateSelector
    {
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is TabItemPageViewModelBase viewModel)
                return viewModel.Template;

            throw new System.ArgumentNullException("TabTemplate should have value");
        }
    }
}