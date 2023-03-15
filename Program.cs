using CapitalPlacement.Extensions;
namespace CapitalPlacement
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var progamServiceExtension = new ProgamServiceExtension();
            await progamServiceExtension.AddProgramAsync();

            //await progamServiceExtension.UpdateProgramAsync();

            //await progamServiceExtension.GetProgramsAsync();


        }
    }
}