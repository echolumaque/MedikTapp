﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedikTappFunctionApp.Models
{
    [Table("Services")]
    public class ServiceModel
    {
        [Key]
        public int ServiceId { get; set; }
        public string ServiceImage { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public double ServicePrice { get; set; }
        public bool IsPromo { get; set; }
    }
}