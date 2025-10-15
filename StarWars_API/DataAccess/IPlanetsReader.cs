using StarWars_API.Model;

namespace StarWars_API.DataAccess;

public interface IPlanetsReader
{
    Task<IEnumerable<Planet>> Read();
}
