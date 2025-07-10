using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using PokemonApi.Domain.DTOs;
using PokemonApi.Domain.Repositories;
using PokemonApi.Infrastructure.Repositories;




namespace PokemonApi.Infrastructure.Repositories
{
    public class PokeApiRepository : IPokemonRepository

    {
        private readonly HttpClient _httpClient;

        public PokeApiRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PokemonDto> GetPokemonByNameAsync(string name)
        {
            //chama a pokeApi real
            var response = await _httpClient.GetFromJsonAsync<PokeApiResponse>(
                $"https://pokeapi.co/api/v2/pokemon/{name.ToLower()}");

            if (response == null)
            {
                throw new Exception("Pokemon não encontrado na PokéAPI");
            }

            //mapeia o json
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
