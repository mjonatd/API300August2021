using System;
using System.Threading.Tasks;

namespace AppointmentsApi.Services
{
    public interface ILookupDoctorAppointmentTimes
    {
        Task<DateTime> GetNextAvailableAppoinmentFor(string doctor);
    }
}