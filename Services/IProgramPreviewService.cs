using CapitalPlacement.DTOs;

namespace CapitalPlacement.Services
{
    public interface IProgramPreviewService
    {
        Task<List<ProgramDTO>> GetProgramsAsync();
    }
}
