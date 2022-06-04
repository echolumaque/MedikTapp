using MedikTapp.Models;
using MedikTapp.Tables;
using System;
using System.Collections.Generic;

namespace MedikTapp.Services.MockService
{
    public partial class MockService
    {
        public IEnumerable<Bookings> MockBookings { get; set; }
        public IEnumerable<Schedules> MockSchedules { get; set; }
        public IEnumerable<Models.Services> MockServices { get; set; }
        public event EventHandler OnMockServiceInitialized
        {
            add => _onMockServiceInitialized.AddEventHandler(value);
            remove => _onMockServiceInitialized.RemoveEventHandler(value);
        }
    }
}