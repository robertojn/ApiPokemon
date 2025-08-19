using Microsoft.AspNetCore.Mvc;
using Moq;
using PokemonApi.Domain.DTOs;
using PokemonApi.WebApi.Controllers;

namespace PokemonApi.Tests
{
    public class PokemonControllerUnitTests
    {
        [Fact]
        public async Task GetByName_DeveRetornarBadRequest_QuandoNomeEhVazio()
        {
            // Arrange
            var serviceMock = new Mock<IPokemonService>();
            var controller = new PokemonController(serviceMock.Object);

            // Act
            var result = await controller.GetByName(" "); // nome vazio

            // Assert – verifica se retorna BadRequest
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetByName_DeveRetornarOk_QuandoPokemonExiste()
        {
            // Arrange
            var esperado = new PokemonDto { Name = "pikachu" };
            var serviceMock = new Mock<IPokemonService>();
            serviceMock.Setup(s => s.GetPokemonByNameAsync("pikachu"))
                       .ReturnsAsync(esperado);

            var controller = new PokemonController(serviceMock.Object);

            // Act
            var result = await controller.GetByName("pikachu");

            // Assert – verifica se é OkObjectResult e se o DTO é o esperado
            var okResult = Assert.IsType<OkObjectResult>(result);
            var dto = Assert.IsType<PokemonDto>(okResult.Value);
            Assert.Equal("pikachu", dto.Name);
        }
    }
}
