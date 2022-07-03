using System;

namespace MedikTapp.Models
{
    public class ScheduleModel
    {
        public int ServiceId { get; set; }
        public string ServiceImage { get; set; }
        public string ServiceName { get; set; }//
        public string ServiceDescription { get; set; }
        public double ServicePrice { get; set; }
        public string BookingStatus { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}