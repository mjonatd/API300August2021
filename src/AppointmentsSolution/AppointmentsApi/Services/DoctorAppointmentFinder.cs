using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentsApi.Services
{
    public class DoctorAppointmentFinder : ILookupDoctorAppointmentTimes
    {

        public Task<DateTime> GetNextAvailableAppoinmentFor(string doctor)
        {
            var random = new Random();
            return Task.FromResult(DateTime.Now.AddDays(random.Next(3, 5)));
        }
    }
}
