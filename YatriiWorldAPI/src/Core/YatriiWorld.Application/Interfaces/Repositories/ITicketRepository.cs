using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Application.DTOs.Tickets;
using YatriiWorld.Domain.Entities;

namespace YatriiWorld.Application.Interfaces.Repositories
{
    public interface ITicketRepository : IRepository<Ticket> 
    {
        Task<List<TicketDetailsDto>> GetUserTicketsWithDetailsAsync(long userId);
    }
}
