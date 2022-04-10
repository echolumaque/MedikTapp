using MedikTapp.Enums;
using MedikTapp.Services.NavigationService;
using System;
using Xamarin.Forms;

namespace MedikTapp.Views.MainPage.Schedule
{
    public partial class ScheduleTabViewModel
    {
        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            InitScheduleCollections();
        }

        private void InitScheduleCollections()
        {
            Bookings = new()
            {
                new()
                {
                    BookingDate = new DateTime(2022, 4, 20, 4, 20, 0),
                    BookingStatus = BookingStatus.Confirmed,
                    DoctorImage = ImageSource.FromResource("MedikTapp.Resources.SVGs.doctor.png", Application.Current.GetType().Assembly),
                    DoctorName = "Dr. Chriz Frazier",
                    DoctorSpecialty = "Pediatrician",
                    IsConfirmed = true
                },
                new()
                {
                    BookingDate = new DateTime(2022, 4, 20, 16, 20, 0),
                    BookingStatus = BookingStatus.Pending,
                    DoctorImage = ImageSource.FromResource("MedikTapp.Resources.SVGs.doctor.png", Application.Current.GetType().Assembly),
                    DoctorName = "Dr. Charlie Black",
                    DoctorSpecialty = "Cardiologist",
                    IsConfirmed = true
                },
                new()
                {
                    BookingDate = new DateTime(2022, 4, 20, 16, 20, 0),
                    BookingStatus = BookingStatus.Confirmed,
                    DoctorImage = ImageSource.FromResource("MedikTapp.Resources.SVGs.doctor.png", Application.Current.GetType().Assembly),
                    DoctorName = "Dr. Viola Dunn",
                    DoctorSpecialty = "Therapist",
                    IsConfirmed = true
                },
            };
        }
    }
}
