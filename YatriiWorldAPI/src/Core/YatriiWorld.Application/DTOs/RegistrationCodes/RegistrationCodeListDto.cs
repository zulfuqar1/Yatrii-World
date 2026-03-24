using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.DTOs.RegistrationCodes
{
    public class RegistrationCodeListDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public bool IsUsed { get; set; }
        public string? UsedByName { get; set; }
        public DateTime? UsedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
