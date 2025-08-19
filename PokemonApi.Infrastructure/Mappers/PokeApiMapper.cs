using PokemonApi.Domain.DTOs;
using PokemonApi.Infrastructure.Repositories;


namespace PokemonApi.Infrastructure.Mappers
{
    public static class PokeApiMapper
    {
        public static PokemonDto ToPokemonDto(PokeApiResponse response)
        {
            return new PokemonDto
            {
                Name = response.Name,
                Height = response.Height,
                Weight = response.Weight,
                Types = response.Types.Select(t => t.Type.Name).ToList()
            };
        }
    }
}
