using CapitalPlacement.DTOs;

namespace CapitalPlacement.Services
{
    public interface IWorkFlowService
    {
        Task<List<WorkFlowDTO>> GetWorkFlowAsync();
        Task<WorkFlowDTO> UpdateWorkFlowAsync(WorkFlowDTO workFlowDTO);
    }
}
