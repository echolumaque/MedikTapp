using MedikTapp.Enums;
using MedikTapp.Tables;
using SQLite;
using System;

namespace MedikTapp.Models
{
    public class Services : DatabaseTable
    {
        [PrimaryKey, AutoIncrement, Indexed, NotNull]
        public int ServiceId { get; set; }

        public string ServiceImagePath { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public double ServicePrice { get; set; }
        public DateTime AvailableTime { get; set; }
        public bool IsPromo { get; set; }
        public BookingStatus BookingStatus { get; set; }
    }
}