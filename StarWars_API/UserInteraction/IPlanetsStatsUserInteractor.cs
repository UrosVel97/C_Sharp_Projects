using StarWars_API.Model;

namespace StarWars_API.UserInteraction;

public interface IPlanetsStatsUserInteractor
{
    void Show(IEnumerable<Planet> planets);

    string? ChooseStatisticsToBeShown(IEnumerable<string> propertiesThatCanBeChosen);
    void ShowMessage(string message);
}
