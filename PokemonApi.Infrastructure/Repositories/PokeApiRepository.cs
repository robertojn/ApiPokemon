using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using PokemonApi.Domain.DTOs;
using PokemonApi.Domain.Exceptions;
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
            try
            {
                //chama a pokeApi real
                var response = await _httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon/{name.ToLower()}");

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new NotFoundException($"Pokemon '{name}' não encontrado.");
                }

                response.EnsureSuccessStatusCode();

                var pokeApiResponse = await response.Content.ReadFromJsonAsync<PokeApiResponse>();

                //mapeia o json
                return new PokemonDto
                {
                    Name = pokeApiResponse.Name,
                    Height = pokeApiResponse.Height,
                    Weight = pokeApiResponse.Weight,
                    Types = pokeApiResponse.Types.Select(t => t.Type.Name).ToList()
                };
            }
            catch (HttpRequestException ex)
            {
                throw new ExternalServiceException("Erro ao acessar a PokéAPI.", ex);
            }




        }

    }
}
