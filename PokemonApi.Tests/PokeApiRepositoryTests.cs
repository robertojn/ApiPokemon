using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using PokemonApi.Infrastructure.Repositories;
using PokemonApi.Domain.Exceptions;

namespace PokemonApi.Tests
{
    public class PokeApiRepositoryTests
    {
        //Criando Httpclient com um handler mockado
        private HttpClient CriarHttpClient(HttpResponseMessage reponse)
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(reponse);

            return new HttpClient(handlerMock.Object);
        }

        [Fact]
        public async Task GetPokemonByNameAsync_DeveRetornarPokemon_QuandoSucesso()
        {
            var json = @"{""name"":""pikachu"",""height"":4,""weight"":60,""types"":[{""type"":{""name"":""electric""}}]}";
            var reponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json)

        };
            var client = CriarHttpClient(reponse);
            var repo = new PokeApiRepository(client);
            
            var resultado = await repo.GetPokemonByNameAsync("pikachu");

            Assert.Equal("pikachu", resultado.Name);
            Assert.Single(resultado.Types);
        }

        [Fact]
        public async Task GetPokemonByNameAsync_DeveLancarNotFoundException_QuandoNaoEncontrado()
        {
            var response = new HttpResponseMessage(HttpStatusCode.NotFound);
            var client = CriarHttpClient(response);
            var repo = new PokeApiRepository(client);

            await Assert.ThrowsAsync<NotFoundException>(
                () => repo.GetPokemonByNameAsync("Desconhecido"));
        }

    }
}
