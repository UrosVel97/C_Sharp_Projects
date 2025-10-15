using StarWars_API.ApiDataAccess;
using StarWars_API.DTOs;
using StarWars_API.Model;
using System.Text.Json;

namespace StarWars_API.DataAccess;

public class PlanetsFromApiReader : IPlanetsReader
{
    private readonly IApiDataReader _apiDataReader;
    private readonly IApiDataReader _secondaryApiDataReader;

    public PlanetsFromApiReader(IApiDataReader apiDataReader, IApiDataReader secondaryApiDataReader)
    {
        _apiDataReader = apiDataReader;
        _secondaryApiDataReader = secondaryApiDataReader;
    }
    public async Task<IEnumerable<Planet>> Read()
    {
        string? json = null;

        try
        {

            json = await _apiDataReader.Read("https://swapi.dev/", "api/planets");

        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("API request was unsuccessful. " +
                "Switching to mock data. " +
                "Exception message: " + ex.Message);

        }

        json ??= await _secondaryApiDataReader.Read("https://swapi.dev/", "api/planets");

        var root = JsonSerializer.Deserialize<Root>(json);

        return ToPlanets(root);
    }

    private static IEnumerable<Planet> ToPlanets(Root? root)
    {
        if (root is null)
        {
            throw new ArgumentNullException(nameof(root));
        }


        return root.results.Select(planetDto => (Planet)planetDto);
    }
}
