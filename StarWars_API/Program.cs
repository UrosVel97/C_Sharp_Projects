using StarWars_API.ApiDataAccess;
using StarWars_API.App;
using StarWars_API.DataAccess;
using StarWars_API.UserInteraction;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace StarWars_API;

internal class Program
{

    static async Task Main(string[] args)
    {

        try
        {
            var consoleUserInteractor = new ConsoleUserInteractor();

            var planetStatsUserInteractor = new PlanetsStatsUserInteractor(consoleUserInteractor);

            await new StarWarsPlanetStatsApp(
                new PlanetsFromApiReader(
                new ApiDataReader(),
                new MockStarWarsApiDataReader()),
                new PlanetsStatisticsAnalyzer(
                    planetStatsUserInteractor),planetStatsUserInteractor).Run();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An unexpected error occurred. Exception message: " + ex.Message);

        }
    }
}
