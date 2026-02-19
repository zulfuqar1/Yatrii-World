using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YatriiWorld.Domain.Entities
{
    using System.Collections.Generic;

    namespace YatriiWorld.Domain.Entities
    {
        public class User : Base.BaseNameableEntitiy
        {
            public string Surname { get; set; }
            public string Email { get; set; }
            public string PasswordHash { get; set; }
            public string Role { get; set; }
            public ICollection<Review> Reviews { get; set; }
            public ICollection<Ticket> Tickets { get; set; }
        }
    }
}
