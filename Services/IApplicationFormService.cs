using CapitalPlacement.DTOs;

namespace CapitalPlacement.Services
{
    public interface IApplicationFormService
    {
        Task<List<ApplicationFormDTO>> GetApplicationFormAsync();
        Task<ApplicationFormDTO> UpdateApplicationFormAsync(ApplicationFormDTO applicationFormDTO);
    }
}
