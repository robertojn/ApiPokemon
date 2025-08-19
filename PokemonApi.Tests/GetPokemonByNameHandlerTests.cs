using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using PokemonApi.Application.Queries;
using PokemonApi.Domain.DTOs;
using PokemonApi.Domain.Exceptions;
using PokemonApi.Domain.Repositories;
using Xunit;

namespace PokemonApi.Tests.Application.Pokemons.Queries.GetByName
{
    public class GetPokemonByNameHandlerTests
    {
        [Fact]
        public async Task Should_return_dto_when_repository_returns_result()
        {
            // arrange
            var repo = new Mock<IPokemonRepository>();
            var dto = new PokemonDto
            {
                Name = "pikachu",
                Height = 4,
                Weight = 60,
                Types = new[] { "electric" }
            };

            repo.Setup(r => r.GetPokemonByNameAsync("pikachu", It.IsAny<CancellationToken>()))
               .ReturnsAsync(dto);

            var handler = new GetPokemonByNameHandler(repo.Object);
            var query = new GetPokemonByNameQuery("pikachu");
            var ct = CancellationToken.None;

            // act
            var result = await handler.Handle(query, ct);

            // assert
            result.Should().NotBeNull();
            result.Name.Should().Be("pikachu");
            result.Types.Should().Contain("electric");

            repo.Verify(r => r.GetPokemonByNameAsync("pikachu", It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Should_throw_NotFound_when_repository_throws_NotFound()
        {
            var repo = new Mock<IPokemonRepository>();
            repo.Setup(r => r.GetPokemonByNameAsync("missingno", It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException("not found"));

            var handler = new GetPokemonByNameHandler(repo.Object);
            var query = new GetPokemonByNameQuery("missingno");

            var act = () => handler.Handle(query, CancellationToken.None);

            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task Should_pass_cancellationToken_to_repository()
        {
            var repo = new Mock<IPokemonRepository>();
            var dto = new PokemonDto { Name = "pikachu" };

            var token = new CancellationTokenSource().Token;

            repo.Setup(r => r.GetPokemonByNameAsync(
                    It.IsAny<string>(),
                    It.Is<CancellationToken>(ct => ct == token)))
               .ReturnsAsync(dto);

            var handler = new GetPokemonByNameHandler(repo.Object);
            var query = new GetPokemonByNameQuery("pikachu");

            var result = await handler.Handle(query, token);

            result.Should().NotBeNull();
            repo.VerifyAll();
        }
    }
}
