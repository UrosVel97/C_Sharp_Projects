using StarWars_API.Model;
using StarWars_API.Utilities;

namespace StarWars_API.UserInteraction;

public class PlanetsStatsUserInteractor : IPlanetsStatsUserInteractor
{

    private readonly IUserIteractor _userIteractor;

    public PlanetsStatsUserInteractor(IUserIteractor userIteractor)
    {
        _userIteractor = userIteractor;
    }

    public string? ChooseStatisticsToBeShown(IEnumerable<string> propertiesThatCanBeChosen)
    {
        _userIteractor.ShowMessage(Environment.NewLine);
        _userIteractor.ShowMessage("The statistics of which property would you " +
            "like to see?");
        _userIteractor.ShowMessage(string.Join(Environment.NewLine,
            propertiesThatCanBeChosen));

        return _userIteractor.ReadFromUser();
    }

    public void Show(IEnumerable<Planet> planets)
    {
        TablePrinter.Print(planets);
    }

    public void ShowMessage(string message)
    {
        throw new NotImplementedException();
    }
}
