using YatriiWorld.Application.DTOs.Tickets;
using System.Threading.Tasks;
using YatriiWorld.MVC.ViewModels.Booking;

namespace YatriiWorld.MVC.Services.Interfaces
{
    // Sadece bir tane interface tanımı olmalı
    public interface ITicketClientService
    {
        Task<bool> CreateBookingAsync(TicketCreateVM model);
    }
}