using MediatR;
using Microsoft.AspNetCore.Mvc;
using PokemonApi.Application.Queries;

namespace PokemonApi.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly ISender _sender;

        public PokemonController(ISender sander)
        {
            _sender = sander;

        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName([FromRoute] string? name, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest(new { error = "O parametro 'name' é obrigatorio e não pode estar vazio" });
            }

            var Query = new GetPokemonByNameQuery(name);
            var result = await _sender.Send(Query, ct);
            return Ok(result);
        }

    }
}
