namespace StarWars_API.UserInteraction;

public interface IUserIteractor
{
    void ShowMessage(string message);
    string? ReadFromUser();
}
