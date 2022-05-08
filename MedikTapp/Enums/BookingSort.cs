using System;

namespace MedikTapp.Enums
{
    public enum BookingSort
    {
        Ascending,
        Descending
    }

    public static partial class Extensions
    {
        public static string ToShortDescription(this BookingSort bookingSort) => bookingSort switch
        {
            BookingSort.Ascending => "Sort by earliest date",
            BookingSort.Descending => "Sort by latest date",
            _ => throw new NotImplementedException(),
        };
    }
}