using ChuckNorisJokes.DataAccess;

using var userInteractor = new ConsoleUserInteractor(new JokesApiDataReader());

try
{
    string category = await userInteractor.ReadSingleWord("Enter category of joke: ");
    int numberOfPages = userInteractor.ReadInteger("How many pages do you want to read? ");
    int jokesPerPage = userInteractor.ReadInteger("How many jokes per page? ");
    
    
    userInteractor.ShowMessage("Fetchig data...");
    string data = await FetchDataFromAllPagesAsync(
        numberOfPages, jokesPerPage,category);
    userInteractor.ShowMessage("Data is ready.");
    Console.WriteLine(data);

    Console.ReadKey();


}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}

async Task<string> FetchDataFromAllPagesAsync(
    int numberOfPages, 
    int jokesPerPage,
    string category)
{
    using var jokesApiDataReader = new JokesApiDataReader();

    var result = jokesApiDataReader.ReadAsync(numberOfPages, jokesPerPage, category);
    
    await PrintLoadingSymbol(result);

    Console.WriteLine();



    return result.Result;
}

static async Task PrintLoadingSymbol(Task<string> result2)
{
    var loadingChars = new[] { '|', '/', '-', '\\' };
    var charIndex = 0;
    do
    {
        Console.Write($"\rLoading {loadingChars[charIndex++ % loadingChars.Length]}");
        await Task.Delay(100);
    } while (result2.Status != TaskStatus.RanToCompletion);
}