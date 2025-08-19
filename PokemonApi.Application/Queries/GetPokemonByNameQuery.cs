using MediatR;
using PokemonApi.Domain.DTOs;

namespace PokemonApi.Application.Queries
{
    public sealed record GetPokemonByNameQuery(string name) : IRequest<PokemonDto>;
}
