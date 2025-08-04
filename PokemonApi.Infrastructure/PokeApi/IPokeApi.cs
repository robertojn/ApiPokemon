using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonApi.Infrastructure.Repositories;
using Refit;


namespace PokemonApi.Infrastructure.PokeApi;

public interface IPokeApi
{
    [Get("/pokemon/{name}")]
    Task<PokeApiResponse> GetPokemonAsync(string name);
}
