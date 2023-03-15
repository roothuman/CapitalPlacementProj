using CapitalPlacement.DTOs;
using Microsoft.Azure.Cosmos;
using System.Net;

namespace CapitalPlacement.Services
{
    public class WorkFlowService : IWorkFlowService
    {

        private readonly ICosmosDbService _cosmosDbService;
        private readonly Container _container;

        public WorkFlowService(ICosmosDbService cosmosDbService, Container container)
        {
            _cosmosDbService = cosmosDbService;
            _container = container;
        }

        public async Task<List<WorkFlowDTO>> GetWorkFlowAsync()
        {
            var workFlow = await _cosmosDbService.GetItemsAsync<WorkFlowDTO>(w => true);
            return workFlow.ToList();
        }

        public async Task<WorkFlowDTO> UpdateWorkFlowAsync(WorkFlowDTO workFlowDTO)
        {
            try
            {
                await _cosmosDbService.UpdateItemAsync(workFlowDTO.id, workFlowDTO);
                return workFlowDTO;
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new Exception($"Failed to retrieve Work Flow with Id {workFlowDTO.id} and PartitionKey {workFlowDTO.id}");
            }
        }
    }
}
