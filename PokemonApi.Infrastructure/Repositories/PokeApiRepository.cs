using PokemonApi.Domain.DTOs;
using PokemonApi.Domain.Exceptions;
using PokemonApi.Domain.Repositories;
using PokemonApi.Infrastructure.Mappers;
using PokemonApi.Infrastructure.PokeApi;
using Refit;
using System.Net;

namespace PokemonApi.Infrastructure.Repositories;

public class PokeApiRepository : IPokemonRepository

{
    private readonly IPokeApi _pokeApi;

    public PokeApiRepository(IPokeApi pokeApi)
    {
        _pokeApi = pokeApi;
    }

    public async Task<PokemonDto> GetPokemonByNameAsync(string name, CancellationToken ct = default)
    {
        try
        {
            //chama a pokeApi real
            var response = await _pokeApi.GetPokemonAsync(name.ToLower(), ct);
            return response.ToPokemonDto();

        }
        catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            throw new NotFoundException($"Pokemon '{name}' não encontrado.");
        }
        catch (Exception ex)
        {
            throw new ExternalServiceException("Erro ao acessar a PokéAPI", ex);
        }
    }
}
