using ChuckNorisJokes.DataAccess;
using ChuckNorisJokes.Models;
using ChuckNorisJokes.UserInteraction;

internal class ConsoleUserInteractor : IUserInteractor
{
    private readonly IJokesApiDataReader _dataReader;

    public ConsoleUserInteractor(IJokesApiDataReader dataReader)
    {
        _dataReader = dataReader;
    }

    public void Dispose()
    {
        Console.WriteLine("Disposing ConsoleUserInteractor resources...");

        _dataReader.Dispose();    
    }

    public int ReadInteger(string prompt)
    {
        Console.WriteLine(prompt);
        return Console.ReadLine() is string input && 
            int.TryParse(input, out int result) ? result : 0;
    }

    public async Task<string> ReadSingleWord(string prompt)
    {
        string ? result;
        var categories = await _dataReader.ReadAllCategories();

        do
        {
            Console.WriteLine();
            Console.WriteLine(prompt);
            for (int i=0; i<categories.Count;i++)
            {
                Console.WriteLine($"{i + 1}. {categories[i]}");
            }

            result = Console.ReadLine();
            
            
        } while (!IsValidCategory(result, categories));

        return result!;

    }

    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }

    private bool IsValidCategory(string? result, List<string> categories)
    {
        return categories.Contains(result!);
    }
}