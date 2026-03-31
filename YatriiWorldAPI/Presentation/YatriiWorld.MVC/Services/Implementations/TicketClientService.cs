using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using YatriiWorld.Application.DTOs.Tickets;
using YatriiWorld.MVC.Services.Interfaces;
using YatriiWorld.MVC.ViewModels.Booking;

namespace YatriiWorld.MVC.Services.Implementations
{
    public class TicketClientService : ITicketClientService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TicketClientService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClientFactory.CreateClient("YatriiApiClient");
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> CreateBookingAsync(TicketCreateVM model)
        {
            var token = _httpContextAccessor.HttpContext?.Request.Cookies["JWTToken"]; 
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.PostAsJsonAsync("Booking/create-booking", model);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"--- API ERROR --- Status: {response.StatusCode}, error: {errorMessage}");
                return false;
            }

            return true;
        }




    }
}