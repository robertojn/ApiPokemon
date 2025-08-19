using PokemonApi.Infrastructure.Repositories;
using Refit;


namespace PokemonApi.Infrastructure.PokeApi;

public interface IPokeApi
{
    [Get("/pokemon/{name}")]
    Task<PokeApiResponse> GetPokemonAsync(string name, CancellationToken ct = default);
}
