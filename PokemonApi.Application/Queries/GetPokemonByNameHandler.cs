using MediatR;
using PokemonApi.Domain.DTOs;
using PokemonApi.Domain.Exceptions;
using PokemonApi.Domain.Repositories;

namespace PokemonApi.Application.Queries;
public sealed class GetPokemonByNameHandler : IRequestHandler<GetPokemonByNameQuery, PokemonDto>
{
    private readonly IPokemonRepository _repository;

    public GetPokemonByNameHandler(IPokemonRepository repository)
    {
        _repository = repository;
    }
 
    public async Task<PokemonDto> Handle(GetPokemonByNameQuery request, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.name))
            throw new ArgumentException("Nome inválido.", nameof(request.name));

        var dto = await _repository.GetPokemonByNameAsync(request.name.Trim().ToLower(), ct);

        if (dto is null)
            throw new NotFoundException($"Pokemon '{request.name}' não encontrado.");

        return dto;
    }
}