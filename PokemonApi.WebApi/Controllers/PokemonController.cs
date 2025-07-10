using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PokemonApi.Domain.Services;

namespace PokemonApi.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;

        public PokemonController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var pokemon = await _pokemonService.GetPokemonByNameAsync(name);
            return Ok(pokemon);
        }

    }
}
