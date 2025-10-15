using StarWars_API.DataAccess;
using StarWars_API.UserInteraction;
using StarWars_API.Utilities;

namespace StarWars_API.App;

public class StarWarsPlanetStatsApp
{

    private readonly IPlanetsReader _planetsReader;
    private readonly IPlanetsStatisticsAnalyzer _planetsStatisticsAnalyzer;
    private readonly IPlanetsStatsUserInteractor _planetStatsUserInteractor;

    public StarWarsPlanetStatsApp(IPlanetsReader planetsReader, IPlanetsStatisticsAnalyzer planetsStatisticsAnalyzer, PlanetsStatsUserInteractor planetStatsUserInteractor)
    {

        _planetsReader = planetsReader;
        _planetsStatisticsAnalyzer = planetsStatisticsAnalyzer;
        _planetStatsUserInteractor = planetStatsUserInteractor;
    }

    public async Task Run()
    {
        var planets = await _planetsReader.Read();

        TablePrinter.Print(planets);

        _planetsStatisticsAnalyzer.Analyze(planets);

    }




}
