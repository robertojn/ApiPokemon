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
using PokemonApi.Infrastructure.Mappers;
using PokemonApi.Infrastructure.PokeApi;
using PokemonApi.Infrastructure.Repositories;
using Refit;




namespace PokemonApi.Infrastructure.Repositories
{
    public class PokeApiRepository : IPokemonRepository

    {
        private readonly IPokeApi _pokeApi;

        public PokeApiRepository(IPokeApi pokeApi)
        {
            _pokeApi = pokeApi;
        }

        public async Task<PokemonDto> GetPokemonByNameAsync(string name)
        {
            try
            {
                //chama a pokeApi real
                var response = await _pokeApi.GetPokemonAsync(name.ToLower());

                return PokeApiMapper.ToPokemonDto(response);

            }
            catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new NotFoundException($"Pokemon '{name}' não encontrado.");
            }
            catch (Exception ex)
            {
                throw new ExternalServiceException("Erro ao acessar a PokéAPI", ex);
            }
        }
    }
}
