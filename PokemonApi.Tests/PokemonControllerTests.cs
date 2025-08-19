using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PokemonApi.Application.Queries;
using PokemonApi.Domain.DTOs;
using PokemonApi.WebApi.Controllers;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

public class PokemonControllerTests
{
    [Fact]
    public async Task GetByName_DeveRetornarBadRequest_QuandoNomeEhVazio()
    {
        var senderMock = new Mock<ISender>();
        var controller = new PokemonController(senderMock.Object);

        var result = await controller.GetByName(" ", CancellationToken.None);

        Assert.IsType<BadRequestObjectResult>(result);
        senderMock.Verify(s => s.Send(It.IsAny<GetPokemonByNameQuery>(), It.IsAny<CancellationToken>()), Times.Never());
    }

    [Fact]
    public async Task GetByName_DeveRetornarOk_QuandoPokemonExiste()
    {
        var senderMock = new Mock<ISender>();
        var esperado = new PokemonDto { Name = "pikachu" };

        senderMock.Setup(s => s.Send(It.Is<GetPokemonByNameQuery>(q => q.name == "pikachu"),
                                     It.IsAny<CancellationToken>()))
                  .ReturnsAsync(esperado);

        var controller = new PokemonController(senderMock.Object);

        var result = await controller.GetByName("pikachu", CancellationToken.None);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var dto = Assert.IsType<PokemonDto>(okResult.Value);
        Assert.Equal("pikachu", dto.Name);

        senderMock.Verify(s => s.Send(It.IsAny<GetPokemonByNameQuery>(), It.IsAny<CancellationToken>()), Times.Once());
    }
}
