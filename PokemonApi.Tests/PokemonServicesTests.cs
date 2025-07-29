using Moq;
using PokemonApi.Application.Services;
using PokemonApi.Domain.DTOs;
using PokemonApi.Domain.Repositories;
using PokemonApi.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonApi.Tests
{
    public class PokemonServicesTests
    {
        [Fact]
        public async Task GetPokemonByNameAsync_RetornaPokemonDto()
        {   
            //arrange: cria um mock do repositorio 
            var repoMock = new Mock<IPokemonRepository>();
            var esperado = new PokemonDto { Name = "pikachu" };
            repoMock.Setup(r => r.GetPokemonByNameAsync("pikachu"))
                .ReturnsAsync(esperado);

            IPokemonService service = new PokemonService(repoMock.Object);

            //act
            var result = await service.GetPokemonByNameAsync("pikachu");

            //assert
            Assert.Equal(esperado, result);
            repoMock.Verify(r => r.GetPokemonByNameAsync("pikachu"), Times.Once());
        }
    }
}
