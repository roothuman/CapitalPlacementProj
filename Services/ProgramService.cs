using CapitalPlacement.DTOs;
using Microsoft.Azure.Cosmos;
using System.Net;

namespace CapitalPlacement.Services
{
    public class ProgramService : IProgramService
    {
        private readonly ICosmosDbService _cosmosDbService;
        private readonly Container _container;

        public ProgramService(ICosmosDbService cosmosDbService, Container container)
        {
            _cosmosDbService = cosmosDbService;
            _container = container;
        }

        public async Task<ProgramDTO> CreateProgramAsync(ProgramDTO programDto)
        {
            var program = new ProgramDTO
            {
                id = Guid.NewGuid().ToString(),
                ProgramTitle = programDto.ProgramTitle,
                Summary = programDto.Summary,
                ProgramDescription = programDto.ProgramDescription,
                KeySkills = programDto.KeySkills,
                ProgramBenefits = programDto.ProgramBenefits,
                ApplicationCriteria = programDto.ApplicationCriteria,
                ProgramInfo = new ProgramInfoDto
                {
                    ProgramType = programDto.ProgramInfo.ProgramType,
                    ProgramOpenDate = programDto.ProgramInfo.ProgramOpenDate,
                    ApplicationOpenDate = programDto.ProgramInfo.ApplicationOpenDate,
                    ApplicationCloseDate = programDto.ProgramInfo.ApplicationCloseDate,
                    DurationInWeeks = programDto.ProgramInfo.DurationInWeeks,
                    ProgramLocation = programDto.ProgramInfo.ProgramLocation,
                    MinQualification = programDto.ProgramInfo.MinQualification,
                    MaxNumberOfApplications = programDto.ProgramInfo.MaxNumberOfApplications
                }
            };

            // Store the program in Cosmos DB
            var createdProgram = await _cosmosDbService.AddItemAsync(program);
            return createdProgram;
        }

        public async Task<List<ProgramDTO>> GetProgramsAsync()
        {
            var programs = await _cosmosDbService.GetItemsAsync<ProgramDTO>(p => true);
            return programs.ToList();
        }

        public async Task<ProgramDTO> UpdateProgramAsync(ProgramDTO programDto)
        {
            try
            {
                await _cosmosDbService.UpdateItemAsync(programDto.id, programDto);
                return programDto;
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new Exception($"Failed to retrieve Program with Id {programDto.id} and PartitionKey {programDto.id}");
            }
        }


    }
}
