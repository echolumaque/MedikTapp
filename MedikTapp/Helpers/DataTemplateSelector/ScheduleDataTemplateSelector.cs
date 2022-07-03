using MedikTapp.Enums;
using Xamarin.Forms;

namespace MedikTapp.Helpers.DataTemplateSelector
{
    public class ScheduleDataTemplateSelector : Xamarin.Forms.DataTemplateSelector
    {
        public DataTemplate CancelledTemplate { get; set; }
        public DataTemplate NotCancelledTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((Models.ScheduleModel)item).BookingStatus == BookingStatus.Cancelled.ToString()
                ? CancelledTemplate
                : NotCancelledTemplate;
        }
    }
}