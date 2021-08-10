using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppointmentsApi.Services
{
    public class CalendarHttpClient : ICalendarApi
    {
        private readonly HttpClient _httpClient;

        public CalendarHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<DateTime> GetNextDateAsync()
        {
            var response = await _httpClient.GetAsync("/availabledates");
            if(response.IsSuccessStatusCode)
            {
                // Check is it json? - check the response headers
                var responseJson = await response.Content.ReadAsStringAsync();
                var responseObj = JsonSerializer.Deserialize<GetDateResponse>(responseJson);

                return responseObj.date;
            }
            else
            {
                // do something real here.  Throw a nonhttp exception like "SorryNotAbileToFindAnAppointmentDateException"
                return DateTime.Now.AddDays(30);
            }
        }

        public record GetDateResponse(DateTime date);
    }
}
