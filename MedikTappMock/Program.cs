using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace MedikTappMock
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Writing mock data.");
            //CreateMockBookings();
            //CreateMockSchedules();
            CreateMockRandomServices();
            Console.WriteLine("Done writing mock data.");
            Console.Read();
        }

        private static void CreateMockBookings()
        {
            var filePath = @"C:\Users\Echo Lumaque\Desktop\MockBookings.json";
            var listOfMockData = new List<Bookings>
            {
                new Bookings
                {
                    EarliestAvailableDate = new DateTime(2022, 4, 20, 4, 20, 0),
                    ProductName = "Urinalysis",
                    ProductDescription = "A urinalysis is a test of your urine. It's used to detect and manage a wide range of disorders, such as urinary tract infections, kidney disease and diabetes. A urinalysis involves checking the appearance, concentration and content of urine.",
                    ProductPrice = 5000.00
                },
                new Bookings
                {
                    EarliestAvailableDate = new DateTime(2022, 4, 21, 4, 20, 0),
                    ProductName = "Drug Test",
                    ProductDescription = "A drug test looks for the presence of one or more illegal or prescription drugs in your urine, blood, saliva, hair, or sweat. Urine testing is the most common type of drug screening.",
                    ProductPrice = 5000.00
                },
                new Bookings
                {
                    EarliestAvailableDate = new DateTime(2022, 4, 22, 4, 20, 0),
                    ProductName = "COVID 19 RT-PCR Test",
                    ProductDescription = "Also called a molecular test, this COVID-19 test detects genetic material of the virus using a lab technique called reverse transcription polymerase chain reaction (RT-PCR)",
                    ProductPrice = 5000.00
                },
                 new Bookings
                 {
                     EarliestAvailableDate = new DateTime(2022, 4, 25, 4, 20, 0),
                     ProductName = "Dr. Chriz Frazier",
                     ProductDescription = "Pediatrician",
                     ProductImagePath = "MedikTapp.Resources.SVGs.doctor1.png",
                     ProductPrice = 500.00
                 },
                 new Bookings
                 {
                     EarliestAvailableDate = new DateTime(2022, 4, 26, 4, 20, 0),
                     ProductName = "Dr. Charlie Black",
                     ProductDescription = "Cardiologist",
                     ProductImagePath = "MedikTapp.Resources.SVGs.doctor2.png",
                     ProductPrice = 500.00
                 },
                 new Bookings
                 {
                     EarliestAvailableDate = new DateTime(2022, 4, 27, 4, 20, 0),
                     ProductName = "Dr. Viola Dunn",
                     ProductDescription = "Therapist",
                     ProductImagePath = "MedikTapp.Resources.SVGs.doctor3.png",
                     ProductPrice = 500.00
                 },
            };
            var jsonString = JsonConvert.SerializeObject(listOfMockData, Formatting.Indented);
            File.WriteAllText(filePath, jsonString);
        }

        private static void CreateMockSchedules()
        {
            var filePath = @"C:\Users\Echo Lumaque\Desktop\MockSchedules.json";
            var listOfMockData = new List<Schedules>
            {
                new Schedules
                {
                    Schedule = new DateTime(2022, 4, 20, 4, 20, 0),
                    ProductName = "Urinalysis",
                    BookingStatus = BookingStatus.Cancelled,
                    ProductDescription = "A urinalysis is a test of your urine. It's used to detect and manage a wide range of disorders, such as urinary tract infections, kidney disease and diabetes. A urinalysis involves checking the appearance, concentration and content of urine.",
                    ProductPrice = 5000.00
                },
                new Schedules
                {
                    Schedule = new DateTime(2022, 4, 21, 4, 20, 0),
                    BookingStatus = BookingStatus.Pending,
                    ProductName = "Drug Test",
                    ProductDescription = "A drug test looks for the presence of one or more illegal or prescription drugs in your urine, blood, saliva, hair, or sweat. Urine testing is the most common type of drug screening.",
                    ProductPrice = 5000.00
                },
                new Schedules
                {
                    Schedule = new DateTime(2022, 4, 22, 4, 20, 0),
                    BookingStatus = BookingStatus.Confirmed,
                    ProductName = "COVID 19 RT-PCR Test",
                    ProductDescription = "Also called a molecular test, this COVID-19 test detects genetic material of the virus using a lab technique called reverse transcription polymerase chain reaction (RT-PCR)",
                    ProductPrice = 5000.00
                },
                 new Schedules
                 {
                     Schedule = new DateTime(2022, 4, 25, 4, 20, 0),
                     BookingStatus = BookingStatus.Cancelled,
                     ProductName = "Dr. Chriz Frazier",
                     ProductDescription = "Pediatrician",
                     ProductImagePath = "MedikTapp.Resources.SVGs.doctor1.png",
                     ProductPrice = 500.00
                 },
                 new Schedules
                 {
                     Schedule = new DateTime(2022, 4, 26, 4, 20, 0),
                     BookingStatus = BookingStatus.Pending,
                     ProductName = "Dr. Charlie Black",
                     ProductDescription = "Cardiologist",
                     ProductImagePath = "MedikTapp.Resources.SVGs.doctor2.png",
                     ProductPrice = 500.00
                 },
                 new Schedules
                 {
                     Schedule = new DateTime(2022, 4, 27, 4, 20, 0),
                     BookingStatus = BookingStatus.Completed,
                     ProductName = "Dr. Viola Dunn",
                     ProductDescription = "Therapist",
                     ProductImagePath = "MedikTapp.Resources.SVGs.doctor3.png",
                     ProductPrice = 500.00
                 },
            };
            var jsonString = JsonConvert.SerializeObject(listOfMockData, Formatting.Indented);
            File.WriteAllText(filePath, jsonString);
        }

        private static void CreateMockRandomServices()
        {
            var filePath = @"C:\Users\Echo Lumaque\Desktop\MockServices.json";
            var listOfMockData = new List<Services>
            {
                new Services
                {
                    ServiceDescription = "An electrocardiogram (ECG) is one of the simplest and fastest tests used to evaluate the heart. Electrodes (small, plastic patches that stick to the skin) are placed at certain spots on the chest, arms, and legs. The electrodes are connected to an ECG machine by lead wires.",
                    ServiceImagePath = "Mediktapp.Resources.SVGs.doctor1.png",
                    ServiceName = "Laboratory (ECG and Creatinine included)",
                    ServicePrice = 750,
                    EarliestAvailableDate = new DateTime(2022, 4, 20, 16, 20, 0)
                },
                new Services
                {
                    ServiceDescription = "HbA1c is your average blood glucose (sugar) levels for the last two to three months. If you have diabetes, an ideal HbA1c level is 48mmol/mol (6.5%) or below. If you're at risk of developing type 2 diabetes, your target HbA1c level should be below 42mmol/mol (6%)",
                    ServiceImagePath = "Mediktapp.Resources.SVGs.doctor2.png",
                    ServiceName = "HbA1c",
                    ServicePrice = 900,
                    EarliestAvailableDate = new DateTime(2022, 4, 21, 16, 20, 0)
                },
                new Services
                {
                    ServiceDescription = "A urinalysis is a test of your urine. It's used to detect and manage a wide range of disorders, such as urinary tract infections, kidney disease and diabetes. A urinalysis involves checking the appearance, concentration and content of urine.",
                    ServiceImagePath = "Mediktapp.Resources.SVGs.doctor3.png",
                    ServiceName = "Urinalysis",
                    ServicePrice = 110,
                    EarliestAvailableDate = new DateTime(2022, 4, 22, 16, 20, 0)
                },
                new Services
                {
                    ServiceDescription = "A 2-D (or two-dimensional) echocardiogram is capable of displaying a cross-sectional “slice” of the beating heart, including the chambers, valves and the major blood vessels that exit from the left and right ventricle. A Doppler echocardiogram measures the speed and direction of the blood flow within the heart.",
                    ServiceImagePath = "Mediktapp.Resources.SVGs.doctor4.png",
                    ServiceName = "2D Echo w/ Doppler",
                    ServicePrice = 2200,
                    EarliestAvailableDate = new DateTime(2022, 4, 23, 16, 20, 0)
                },
                new Services
                {
                    ServiceDescription = "A chest X-ray is an imaging test that uses X-rays to look at the structures and organs in your chest. It can help your healthcare provider see how well your lungs and heart are working. Certain heart problems can cause changes in your lungs. Certain diseases can cause changes in the structure of the heart or lungs.",
                    ServiceImagePath = "Mediktapp.Resources.SVGs.doctor1.png",
                    ServiceName = "Chest X-Ray",
                    ServicePrice = 250,
                    EarliestAvailableDate = new DateTime(2022, 4, 24, 16, 20, 0)
                },
                new Services
                {
                    ServiceDescription = "The polymerase chain reaction (PCR) test for COVID-19 is a molecular test that analyzes your upper respiratory specimen, looking for genetic material (ribonucleic acid or RNA) of SARS-CoV-2, the virus that causes COVID-19.",
                    ServiceImagePath = "Mediktapp.Resources.SVGs.doctor2.png",
                    ServiceName = "COVID-19 RT-PCR Swab Test",
                    ServicePrice = 4000,
                    EarliestAvailableDate = new DateTime(2022, 4, 25, 16, 20, 0)
                },
                new Services
                {
                    ServiceDescription = "Antigen tests are immunoassays that detect the presence of a specific viral antigen, which indicates current viral infection. Antigen tests are currently authorized to be performed on nasopharyngeal, nasal swab, or saliva specimens placed directly into the assay's extraction buffer or reagent.",
                    ServiceImagePath = "Mediktapp.Resources.SVGs.doctor3.png",
                    ServiceName = "COVID-19 Rapid AntiGEN Test",
                    ServicePrice = 1300,
                    EarliestAvailableDate = new DateTime(2022, 4, 26, 16, 20, 0)
                },
                new Services
                {
                    ServiceDescription = "Antibody testing determines if you had COVID-19 (coronavirus) infection in the past. It differs from testing to diagnose if you currently have COVID-19.",
                    ServiceImagePath = "Mediktapp.Resources.SVGs.doctor4.png",
                    ServiceName = "COVID-19 Rapid Antibody Test",
                    ServicePrice = 800,
                    EarliestAvailableDate = new DateTime(2022, 4, 27, 16, 20, 0)
                },
            };
            var jsonString = JsonConvert.SerializeObject(listOfMockData, Formatting.Indented);
            File.WriteAllText(filePath, jsonString);
        }
    }

    public class Bookings
    {
        public DateTime EarliestAvailableDate { get; set; }
        public string ProductImagePath { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public double ProductPrice { get; set; }
    }

    public class Schedules
    {
        public DateTime Schedule { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public string ProductImagePath { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public double ProductPrice { get; set; }
    }

    public class Services
    {
        public string ServiceImagePath { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public double ServicePrice { get; set; }
        public DateTime EarliestAvailableDate { get; set; }
    }

    public enum BookingStatus
    {
        Confirmed,
        Pending,
        Completed,
        Cancelled
    }
}
