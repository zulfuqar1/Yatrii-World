using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Application.DTOs.Tickets;
using YatriiWorld.Application.Interfaces.Repositories;
using YatriiWorld.Domain.Entities;
using YatriiWorld.Persistance.Data;
using YatriiWorld.Persistance.Implementations.Repositories.Generic;

namespace YatriiWorld.Persistance.Implementations.Repositories
{
    internal class TicketRepository : Repository<Ticket>, ITicketRepository
    {

        private readonly AppDbContext _context;

        public TicketRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<List<TicketDetailsDto>> GetUserTicketsWithDetailsAsync(long userId)
        {
            return await _context.Tickets
                .Include(x => x.Tour)
                .Where(x => x.AppUserId == userId && !x.IsDeleted)
                .Select(x => new TicketDetailsDto
                {
                    Id = x.Id,
                    UserFullName = x.CustomerFullName,
                    UserEmail = x.CustomerEmail,
                    AppUserId = x.AppUserId,
                    AdultCount = x.TravellerCount,
                    TotalPrice = x.TotalPrice,
                    CheckInDate = x.CheckinDate,
                    CheckOutDate = x.CheckOutDate,
                    TourName = x.Tour.Title,
                    IsDeleted = x.IsDeleted
                }).ToListAsync();
        }
    }
}
