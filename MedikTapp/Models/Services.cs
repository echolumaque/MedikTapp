using MedikTapp.Enums;
using MedikTapp.Tables;
using SQLite;
using System;

namespace MedikTapp.Models
{
    public class Services : DatabaseTable
    {
        [PrimaryKey, Indexed, NotNull, AutoIncrement]
        public int LocalServiceId { get; set; }

        public int ServiceId { get; set; }
        public string ServiceImage { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public double ServicePrice { get; set; }
        public DateTime AvailableTime { get; set; }
        public bool IsPromo { get; set; }
        public BookingStatus BookingStatus { get; set; }
    }
}