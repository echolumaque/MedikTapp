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
        [Ignore]
        public DateTime StartDate { get; set; }
        [Ignore]
        public DateTime EndDate { get; set; }
    }
}