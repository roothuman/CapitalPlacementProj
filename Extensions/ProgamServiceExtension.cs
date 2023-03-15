using CapitalPlacement.DTOs;
using CapitalPlacement.Services;
using Microsoft.Azure.Cosmos;
using System.Net;

namespace CapitalPlacement.Extensions
{ // This class was used to try out certain functionalities. didn't want the the main method in the program class to be too busy
    public class ProgamServiceExtension
    {
        private static string _connectionString =
                         "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        private static Container _container = null;
        ICosmosDbService cosmosDbService = new CosmosDbService("Tabs", "program", _connectionString);
        #region Adding program Details
        public async Task AddProgramAsync()
        {

            // Create an instance of ICosmosDbService
            ICosmosDbService cosmosDbService = new CosmosDbService("Tabs", "program", _connectionString);

            // Inject the ProgramService class into the Program class
            ProgramService programService = new ProgramService(cosmosDbService, _container);

            Console.WriteLine("Adding Program...");

            try
            {
                // Create a new ProgramDTO instance
                var programDto = new ProgramDTO
                {
                    id = Guid.NewGuid().ToString(),
                    ProgramTitle = "for unit test?",
                    Summary = "A brief summary of my program",
                    ProgramDescription = "A detailed description of my Test program",
                    KeySkills = new List<string>
                {
                    "UI",
                    "UX",
                    "Content Writing",
                    "SEO",
                    "Graphics Design"
                },
                    ProgramBenefits = "The benefits of my Test program",
                    ApplicationCriteria = "The criteria for applying to my test program",
                    ProgramInfo = new ProgramInfoDto
                    {
                        ProgramType = ProgramType.Internship,
                        ProgramOpenDate = new DateTime(2023, 5, 1),
                        ApplicationOpenDate = new DateTime(2023, 1, 1),
                        ApplicationCloseDate = new DateTime(2023, 4, 30),
                        DurationInWeeks = 12,
                        ProgramLocation = "Makurdi",
                        MinQualification = MinQualification.Masters,
                        MaxNumberOfApplications = 100
                    }
                };

                // Call the CreateProgramAsync() method to create the program
                await programService.CreateProgramAsync(programDto);

                Console.WriteLine("Program Added successfully!");
            }
            catch (CosmosException ex)
            {
                Console.WriteLine($"Error creating document: {ex.StatusCode} - {ex.Message}");
            }

            Console.ReadKey();
        }
        #endregion

        #region Get Programs details
        public async Task<List<ProgramDTO>> GetProgramsAsync()
        {
            Console.WriteLine("fectching program details");
            try
            {
                // Create an instance of the ProgramService class
                ICosmosDbService cosmosDbService = new CosmosDbService("Tabs", "program", _connectionString);
                ProgramService programService = new ProgramService(cosmosDbService, _container);

                // Call the GetProgramsAsync method and print the results to the console
                List<ProgramDTO> programs = await programService.GetProgramsAsync();
                foreach (ProgramDTO program in programs)
                {
                    Console.WriteLine(program.id.ToString());
                    Console.WriteLine(program.ProgramTitle.ToString());
                    Console.WriteLine(program.ProgramDescription.ToString());


                }

                return programs;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }
        #endregion

        #region update program
        public async Task<ProgramDTO> UpdateProgramAsync()
        {
            Console.WriteLine("Updating program");

            try
            {
                // Create an instance of the ProgramService class
                ICosmosDbService cosmosDbService = new CosmosDbService("Tabs", "program", _connectionString);

                // Create an instance of the ProgramServiceExtension class
                ProgramService programService = new ProgramService(cosmosDbService, _container);

                // Create a ProgramDTO object with the data you want to update
                ProgramDTO programDto = new ProgramDTO
                {
                    id = "28aecd32-467c-498c-99d3-d028f45aab71",
                    Summary = "I love that it works"
                    // set other properties
                };

                // Call the UpdateProgramAsync method on the ProgramServiceExtension instance
                ProgramDTO updatedProgramDto = await programService.UpdateProgramAsync(programDto);

                // Do something with the updatedProgramDto object
                Console.WriteLine("Update suceesfully");
                return null;
            }

            catch(Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex.Message}");
                // handle the exception as needed
                return null;
            }
            #endregion
        }
    }
}
