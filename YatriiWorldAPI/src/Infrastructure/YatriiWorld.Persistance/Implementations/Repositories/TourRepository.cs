using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Application.Interfaces.Repositories;
using YatriiWorld.Domain.Entities;
using YatriiWorld.Persistance.Data;
using YatriiWorld.Persistance.Implementations.Repositories.Generic;

namespace YatriiWorld.Persistance.Implementations.Repositories
{
    internal class TourRepository : Repository<Tour>, ITourRepository
    {
        public TourRepository(AppDbContext context) : base(context) { }
    }
}
