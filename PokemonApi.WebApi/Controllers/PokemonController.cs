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
        public async Task<IActionResult> GetByName([FromRoute]string? name)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                return BadRequest(new { error = "O parametro name 'name' é obrigatorio e não pode estar vazio" });
            }

            var pokemon = await _pokemonService.GetPokemonByNameAsync(name.Trim()) ;
            return Ok(pokemon);
        }

    }
}
