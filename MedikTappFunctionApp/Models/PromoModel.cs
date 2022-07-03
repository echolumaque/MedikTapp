using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedikTappFunctionApp.Models
{
    [Table("Promos")]
    public class PromoModel
    {
        [Key]
        public int PromoId { get; set; }
        public string PromoImage { get; set; }
        public string PromoName { get; set; }
        public string PromoDescription { get; set; }
        public double PromoPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
