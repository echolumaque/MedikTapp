using SQLite;
using System;

namespace MedikTapp.Tables
{
    public class Bookings : DatabaseTable
    {
        [PrimaryKey, AutoIncrement, Indexed, NotNull]
        public int BookingId { get; set; }

        public DateTime EarliestAvailableDate { get; set; }
        public string ProductImagePath { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public double ProductPrice { get; set; }
    }
}