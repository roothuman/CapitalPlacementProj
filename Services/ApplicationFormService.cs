using CapitalPlacement.DTOs;
using System;
using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace CapitalPlacement.Services
{
    public class ApplicationFormService : IApplicationFormService
    {
        private readonly ICosmosDbService _cosmosDbService;
        private readonly Container _container;

        public ApplicationFormService(ICosmosDbService cosmosDbService, Container container)
        {
            _cosmosDbService = cosmosDbService;
            _container = container;
        }
        public async Task<List<ApplicationFormDTO>> GetApplicationFormAsync()
        {
            var appForm = await _cosmosDbService.GetItemsAsync<ApplicationFormDTO>(a => true);
            return appForm.ToList();
        }   

        public async Task<ApplicationFormDTO> UpdateApplicationFormAsync(ApplicationFormDTO applicationFormDTO)
        {
            try
            {
                await _cosmosDbService.UpdateItemAsync(applicationFormDTO.id, applicationFormDTO);
                return applicationFormDTO;
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new Exception($"Failed to retrieve Application Form with Id {applicationFormDTO.id} and PartitionKey {applicationFormDTO.id}");
            }
        }
    }
}
