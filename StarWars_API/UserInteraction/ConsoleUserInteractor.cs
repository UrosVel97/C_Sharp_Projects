namespace StarWars_API.UserInteraction;

public class ConsoleUserInteractor : IUserIteractor
{
    public string? ReadFromUser()
    {
        return Console.ReadLine();
    }

    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }
}
