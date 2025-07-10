using PokemonApi.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonApi.Domain.Repositories
{
    public interface IPokemonRepository
    {
        Task<PokemonDto> GetPokemonByNameAsync(string name);
    }
}
