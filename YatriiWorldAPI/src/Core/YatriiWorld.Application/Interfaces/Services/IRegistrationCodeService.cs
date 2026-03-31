using YatriiWorld.Application.DTOs.RegistrationCodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Interfaces.Services
{
    public interface IRegistrationCodeService
    {
        Task<RegistrationCodeListDto> CreateAsync(PostRegistrationCodeDto dto);
        Task<IEnumerable<RegistrationCodeListDto>> GetAllAsync(bool? isUsed = null, int page = 0, int take = 0);
        Task<int> GetTotalCountAsync(bool? isUsed = null);
        Task<RegistrationCodeListDto> GetByIdAsync(long id);
        Task<bool> DeleteAsync(long id);
    }
}
