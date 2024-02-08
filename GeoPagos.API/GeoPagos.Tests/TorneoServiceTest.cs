using GeoPagos.API.Controllers;
using GeoPagos.Domain.Contracts.Repository;
using GeoPagos.Domain.Contracts.Services;
using GeoPagos.Domain.Dtos;
using GeoPagos.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GeoPagos.Tests
{
    public class TorneoServiceTests
    {
        private readonly Mock<ITorneoRepository> _mockRepository;
        private readonly ITorneoService _service;

        public TorneoServiceTests()
        {
            _mockRepository = new Mock<ITorneoRepository>();
            _service = new TorneoService(_mockRepository.Object);
        }
        [Fact]
        public async Task GetGanadorTorneoAsync_ShouldReturnGanador_WhenGeneroIsProvided()
        {
            // Arrange
            var expectedGanador = new JugadorDTO
            {
                Nombre = "Juan",
                Fuerza = 11,
                Genero = "Masculino",
                Habilidad = 30,
                TiempoReaccion = 1,
                Velocidad = 20
            };
            _mockRepository.Setup(repo => repo.GetGanadorTorneoAsync("masculino"))
                          .ReturnsAsync(expectedGanador);

            // Act
            var result = await _service.GetGanadorTorneoAsync("masculino");

            // Assert
            Assert.Equal(expectedGanador, result);
        }

        [Fact]
        public async Task GetGanadorTorneoAsync_ShouldThrowException_WhenGeneroIsNull()
        {
            // Arrange

            // Act & Assert
            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await _service.GetGanadorTorneoAsync(null);
            });
        }
    }

    public class JugadorServiceTests
    {
        private readonly Mock<IJugadorRepository> _mockRepository;
        private readonly IJugadorService _service;

        public JugadorServiceTests()
        {
            _mockRepository = new Mock<IJugadorRepository>();
            _service = new JugadorService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetJugadoresByGeneroAsync_ShouldReturnJugadores_WhenGeneroIsProvided()
        {
            // Arrange
            var expectedJugadores = new List<JugadorDTO>
            {
                new JugadorDTO { Nombre = "Juan", Fuerza = 11, Genero = "Masculino",
                Habilidad = 30, TiempoReaccion = 1, Velocidad = 20 },
                new JugadorDTO { Nombre = "Pedro", Fuerza = 9, Genero = "Masculino",
                Habilidad = 50, TiempoReaccion = 1, Velocidad = 20 }
            };
            _mockRepository.Setup(repo => repo.GetJugadoresByGeneroAsync("masculino"))
                          .ReturnsAsync(expectedJugadores);

            // Act
            var result = await _service.GetJugadoresByGeneroAsync("masculino");

            // Assert
            Assert.Equal(expectedJugadores, result);
        }

        [Fact]
        public async Task GetJugadoresByGeneroAsync_ShouldReturnBadRequest_WhenGeneroIsEmpty()
        {
            // Act & Assert
            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await _service.GetJugadoresByGeneroAsync(null);
            });
        }

        public class TorneosControllerTests
        {
            private readonly Mock<ITorneoService> _mockService;
            private readonly TorneosController _controller;

            public TorneosControllerTests()
            {
                _mockService = new Mock<ITorneoService>();
                _controller = new TorneosController(_mockService.Object);
            }

            [Fact]
            public async Task GetGanadorTorneoAsync_ShouldReturnOk_WhenGeneroIsProvided()
            {
                // Arrange
                var expectedGanador = new JugadorDTO
                {
                    Nombre = "Juan",
                    Fuerza = 11,
                    Genero = "Masculino",
                    Habilidad = 30,
                    TiempoReaccion = 1,
                    Velocidad = 20
                };
                _mockService.Setup(service => service.GetGanadorTorneoAsync("masculino"))
                           .ReturnsAsync(expectedGanador);

                // Act
                var result = await _controller.GetGanadorTorneoAsync("masculino");

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var ganador = Assert.IsAssignableFrom<JugadorDTO>(okResult.Value);
                Assert.Equal(expectedGanador, ganador);
            }

            [Fact]
            public async Task GetGanadorTorneoAsync_ShouldReturnBadRequest_WhenGeneroIsNull()
            {
                // Act
                var result = await _controller.GetGanadorTorneoAsync(null);

                // Assert
                var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
                Assert.Equal("El campo 'Genero' no puede ser nulo o estar vacío.", badRequestResult.Value);
            }
        }

        public class JugadoresControllerTests
        {
            private readonly Mock<IJugadorService> _mockService;
            private readonly JugadoresController _controller;

            public JugadoresControllerTests()
            {
                _mockService = new Mock<IJugadorService>();
                _controller = new JugadoresController(_mockService.Object);
            }

            [Fact]
            public async Task GetJugadoresByGeneroAsync_ShouldReturnOk_WhenGeneroIsProvided()
            {
                // Arrange
                var expectedJugadores = new List<JugadorDTO>
            {
                new JugadorDTO { Nombre = "Juan", Fuerza = 11, Genero = "Masculino",
                Habilidad = 30, TiempoReaccion = 1, Velocidad = 20 },
                new JugadorDTO { Nombre = "Damian", Fuerza = 20, Genero = "Masculino",
                Habilidad = 50, TiempoReaccion = 1, Velocidad = 20 }
            };
                _mockService.Setup(service => service.GetJugadoresByGeneroAsync("masculino"))
                           .ReturnsAsync(expectedJugadores);

                // Act
                var result = await _controller.GetJugadoresByGeneroAsync("masculino");

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var jugadores = Assert.IsAssignableFrom<IEnumerable<JugadorDTO>>(okResult.Value);
                Assert.Equal(expectedJugadores, jugadores);
            }

            [Fact]
            public async Task GetJugadoresByGeneroAsync_ShouldReturnBadRequest_WhenGeneroIsEmpty()
            {
                // Act
                var result = await _controller.GetJugadoresByGeneroAsync("");

                // Assert
                var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
                Assert.Equal("'Genero' no puede ser nulo o estar vacío.", badRequestResult.Value);
            }
        }
    }
}
