using GeoPagos.Domain.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace GeoPagos.API.Controllers
{
    [Route("api/jugadores")]
    [ApiController]
    public class JugadoresController : Controller
    {
        private readonly IJugadorService _jugadorService;
        
        public JugadoresController(IJugadorService jugadorService)
        {
            _jugadorService = jugadorService;
        }

        [HttpGet]
        [Route("{genero}")]
        public async Task<IActionResult> GetJugadoresByGeneroAsync(string genero)
        {
            if (String.IsNullOrEmpty(genero))
                return BadRequest("'Genero' no puede ser nulo o estar vacío.");

            var jugadores = await _jugadorService.GetJugadoresByGeneroAsync(genero);

            return Ok(jugadores);
        }
    }
}
