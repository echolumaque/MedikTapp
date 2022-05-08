using MedikTapp.Enums;
using MedikTapp.Tables;
using SQLite;
using System;

namespace MedikTapp.Models
{
    public class Schedules : DatabaseTable
    {
        [PrimaryKey, AutoIncrement, NotNull]
        public int ScheduleId { get; set; } //todo: to be removed when in production

        public DateTime Schedule { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public string ProductImagePath { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public double ProductPrice { get; set; }
    }
}