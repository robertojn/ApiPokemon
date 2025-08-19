namespace PokemonApi.Domain.DTOs;

public sealed class PokemonDto
{
    public string Name { get; init; } = string.Empty;
    public IReadOnlyList<string> Types { get; init; } = Array.Empty<string>();
    public int Height { get; init; }
    public int Weight { get; init; }
}

