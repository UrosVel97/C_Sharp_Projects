using StarWars_API.Model;

namespace StarWars_API.App;

public interface IPlanetsStatisticsAnalyzer
{
    void Analyze(IEnumerable<Planet> planets);
}
