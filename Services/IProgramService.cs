using CapitalPlacement.DTOs;

namespace CapitalPlacement.Services
{
    public interface IProgramService
    {
         Task<ProgramDTO> CreateProgramAsync(ProgramDTO programDto);
         Task<List<ProgramDTO>> GetProgramsAsync();
         Task<ProgramDTO> UpdateProgramAsync(ProgramDTO programDto);
    }

}
