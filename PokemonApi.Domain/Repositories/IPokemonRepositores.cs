using PokemonApi.Domain.DTOs;

namespace PokemonApi.Domain.Repositories
{
    public interface IPokemonRepository
    {
        Task<PokemonDto> GetPokemonByNameAsync(string name, CancellationToken ct = default);
    }
}
