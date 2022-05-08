using System;

namespace MedikTapp.Models
{
    public class Services
    {
        public string ServiceImagePath { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public double ServicePrice { get; set; }
        public DateTime EarliestAvailableDate { get; set; }
        public bool IsPromo { get; set; }
    }
}