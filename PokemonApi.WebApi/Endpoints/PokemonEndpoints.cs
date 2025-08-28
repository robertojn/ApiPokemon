using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using PokemonApi.Application.Queries;
using PokemonApi.Domain.DTOs;

namespace PokemonApi.WebApi.Endpoints;

public static class PokemonEndpoints
{
    public static RouteGroupBuilder MapPokemonEndpoints(this IEndpointRouteBuilder endpoints)
    {
        // agrupando todas as rotas de Pokémon sob /api/pokemon
        var group = endpoints.MapGroup("/api/pokemon")
                             .WithTags("Pokémon");

        group.MapGet("{name}", GetPokemonByName);
            
        return group;
    }

    private static async Task<Results<Ok<PokemonDto>, BadRequest<string>>> GetPokemonByName(
        string name,
        ISender sender,
        CancellationToken cancellationToken)
    {
        
        if (string.IsNullOrWhiteSpace(name))
        {
            return TypedResults.BadRequest("O parâmetro 'name' é obrigatório.");
        }

        var dto = await sender.Send(new GetPokemonByNameQuery(name.Trim()), cancellationToken);

        return TypedResults.Ok(dto);
    }
}
