using MedikTapp.Enums;
using System;
using Xamarin.Forms;

namespace MedikTapp.Models
{
    public class Bookings
    {
        public string DoctorName { get; set; }
        public string DoctorSpecialty { get; set; }
        public DateTime BookingDate { get; set; }
        public bool IsConfirmed { get; set; }
        public ImageSource DoctorImage { get; set; }
        public BookingStatus BookingStatus { get; set; }
    }
}