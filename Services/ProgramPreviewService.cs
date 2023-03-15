using CapitalPlacement.DTOs;
using Microsoft.Azure.Cosmos;

namespace CapitalPlacement.Services
{
    public class ProgramPreviewService : IProgramPreviewService
    {
        private readonly ICosmosDbService _cosmosDbService;
        private readonly Container _container;

        public ProgramPreviewService(ICosmosDbService cosmosDbService, Container container)
        {
            _cosmosDbService = cosmosDbService;
            _container = container;
        }

        public async Task<List<ProgramDTO>> GetProgramsAsync()
        {

            var programs = await _cosmosDbService.GetItemsAsync<ProgramDTO>(p => true);
            return programs.ToList();
        }
    }
}
