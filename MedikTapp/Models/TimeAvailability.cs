using MedikTapp.Constants;
using System;
using Xamarin.Forms;

namespace MedikTapp.Models
{
    public class TimeAvailability
    {
        public bool IsAvailable { get; set; }
        public Color TimeColor => IsAvailable ? (Color)Application.Current.Resources[Colors.DarkPurple] : Color.Gray;
        public DateTime Time { get; set; }
    }
}