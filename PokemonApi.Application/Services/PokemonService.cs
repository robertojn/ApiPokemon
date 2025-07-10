using PokemonApi.Domain.DTOs;
using PokemonApi.Domain.Repositories;
using PokemonApi.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonApi.Application.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly IPokemonRepository _pokemonRepository;

        public PokemonService(IPokemonRepository pokemonRepository)
        { 
            _pokemonRepository = pokemonRepository;
        }

        public async Task<PokemonDto> GetPokemonByNameAsync(string name)
        {
            //logica do useCase
            var pokemon = await _pokemonRepository.GetPokemonByNameAsync(name);

            //Possivel validacao no futuro.
            return pokemon;
        }
    }


}
