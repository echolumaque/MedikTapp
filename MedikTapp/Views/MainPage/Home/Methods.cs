using MedikTapp.Services.NavigationService;
using System;
using Xamarin.Forms;

namespace MedikTapp.Views.MainPage.Home
{
    public partial class HomeTabViewModel
    {
        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            InitCollections();
        }

        private void InitCollections()
        {
            Tuple<string, string>[] promosTuple = new Tuple<string, string>[3]
            {
                new("Temperature", "🤒"),
                new("Snuffle", "🤧"),
                new("Headache", "🤕")
            };
            PromosCollection = new()
            {
                new()
                {
                    Category = "Temperature",
                    Emoji = "🤒"
                },
                new()
                {
                    Category = "Snuffle",
                    Emoji = "🤧"
                },
                new()
                {
                    Category = "Headache",
                    Emoji = "🤕"
                },
                new()
                {
                    Category = "Headache",
                    Emoji = "🤕"
                },
            };

            ServicesCollection = new()
            {
                new()
                {
                    ServiceDescription = "An HIV test shows whether you are infected with HIV (human immunodeficiency virus). HIV is a virus that attacks and destroys cells in the immune system. These cells protect your body against disease-causing germs, such as bacteria and viruses. If you lose too many immune cells, your body will have trouble fighting off infections and other diseases.",
                    ServiceImage = ImageSource.FromResource("MedikTapp.Resources.SVGs.doctor.png", Application.Current.GetType().Assembly),
                    ServiceName = "HIV Test",
                    ServicePrice = 5000.00
                },
                new()
                {
                    ServiceDescription = "A drug test looks for the presence of one or more illegal or prescription drugs in your urine, blood, saliva, hair, or sweat. Urine testing is the most common type of drug screening.",
                    ServiceImage = ImageSource.FromResource("MedikTapp.Resources.SVGs.doctor.png", Application.Current.GetType().Assembly),
                    ServiceName = "Drug Test",
                    ServicePrice = 5000.00
                },
                new()
                {
                    ServiceDescription = "A urinalysis is a test of your urine. It's used to detect and manage a wide range of disorders, such as urinary tract infections, kidney disease and diabetes. A urinalysis involves checking the appearance, concentration and content of urine.",
                    ServiceImage = ImageSource.FromResource("MedikTapp.Resources.SVGs.doctor.png", Application.Current.GetType().Assembly),
                    ServiceName = "Urinalysis",
                    ServicePrice = 5000.00
                },
                new()
                {
                    ServiceDescription = "Also called a molecular test, this COVID-19 test detects genetic material of the virus using a lab technique called reverse transcription polymerase chain reaction (RT-PCR)",
                    ServiceImage = ImageSource.FromResource("MedikTapp.Resources.SVGs.doctor.png", Application.Current.GetType().Assembly),
                    ServiceName = "COVID 19 RT-PCR Test",
                    ServicePrice = 5000.00
                },
            };
        }
    }
}
