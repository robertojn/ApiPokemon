using Moq;
using System.Net;
using System.Threading.Tasks;
using PokemonApi.Infrastructure.PokeApi; 
using PokemonApi.Infrastructure.Repositories;
using PokemonApi.Domain.Exceptions;
using Refit;

namespace PokemonApi.Tests
{
    public class PokeApiRepositoryTests
    {
        [Fact]
        public async Task GetPokemonByNameAsync_DeveRetornarPokemon_QuandoSucesso()
        {
            var respostaApi = new PokeApiResponse
            {
                Name = "pikachu",
                Height = 4,
                Weight = 60,
                Types = new()
                {
                    new PokeApiResponse.TypeSlot
                    {
                        Type = new PokeApiResponse.TypeInfo { Name = "electric" }
                    }
                }
            };

            var apiMock = new Mock<IPokeApi>();
            apiMock.Setup(x => x.GetPokemonAsync(It.Is<string>(s => s == "pikachu")))
                   .ReturnsAsync(respostaApi);

            var repo = new PokeApiRepository(apiMock.Object);

            var resultado = await repo.GetPokemonByNameAsync("pikachu");

            Assert.Equal("pikachu", resultado.Name);
            Assert.Single(resultado.Types);
        }
        [Fact]
        public async Task GetPokemonByNameAsync_DeveLancarNotFoundException_QuandoNaoEncontrado()
        {
            var apiEx = await ApiException.Create(
                new HttpRequestMessage(HttpMethod.Get, "https://pokeapi.co/api/v2/pokemon/Desconhecido"), 
                HttpMethod.Get,                                                                         
                new HttpResponseMessage(HttpStatusCode.NotFound),                                        
                new RefitSettings()                                                                       
            );

            var apiMock = new Mock<IPokeApi>();
            apiMock.Setup(x => x.GetPokemonAsync(It.IsAny<string>()))
                   .ThrowsAsync(apiEx);

            var repo = new PokeApiRepository(apiMock.Object);

            await Assert.ThrowsAsync<NotFoundException>(
                () => repo.GetPokemonByNameAsync("Desconhecido"));
        }
    }
}
