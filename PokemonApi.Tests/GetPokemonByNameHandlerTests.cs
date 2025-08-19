using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using PokemonApi.Application.Queries;
using PokemonApi.Domain.DTOs;
using PokemonApi.Domain.Exceptions;
using PokemonApi.Domain.Repositories;
using Xunit;

public class GetPokemonByNameHandlerTests
{
    [Fact]
    public async Task DeveRetornarDto_QuandoRepositorioRetornaResultado()
    {
        var repoMock = new Mock<IPokemonRepository>();
        var dto = new PokemonDto { Name = "pikachu", /* ... */ };

        repoMock.Setup(r => r.GetPokemonByNameAsync("pikachu", It.IsAny<CancellationToken>()))
                .ReturnsAsync(dto);

        var handler = new GetPokemonByNameHandler(repoMock.Object);
        var query = new GetPokemonByNameQuery("pikachu");

        var result = await handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        result.Name.Should().Be("pikachu");
        repoMock.Verify(r => r.GetPokemonByNameAsync("pikachu", It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DevePropagarNotFoundException_QuandoRepositorioLancaNotFound()
    {
        var repoMock = new Mock<IPokemonRepository>();
        repoMock.Setup(r => r.GetPokemonByNameAsync("missingno", It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException("não encontrado"));

        var handler = new GetPokemonByNameHandler(repoMock.Object);
        var query = new GetPokemonByNameQuery("missingno");

        var act = () => handler.Handle(query, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();
    }
}
